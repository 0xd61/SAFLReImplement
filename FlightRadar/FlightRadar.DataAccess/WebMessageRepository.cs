using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightRadar.Model;
using System.Net;
using System.IO;
using System.Threading;

namespace FlightRadar.DataAccess
{
    public class WebMessageRepository : IMessageRepository
    {
        public string ServerURL { get; private set; } = string.Empty;
        WebRequest request = null;
        WebResponse response = null;
        Stream dataStream = null;
        StreamReader reader = null;

        public WebMessageRepository(string url)
        {
            ServerURL = url;
        }


        public override void StartMessageLoop()
        {
            try
            {
                request = WebRequest.Create(ServerURL);
                ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
                response = request.GetResponse();

                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                dataStream = response.GetResponseStream();
                reader = null;
            }
            catch (Exception)
            {
                Console.WriteLine("Es konnte keine Verbindung hergestellt werden...");
                return;
            }

            char[] buffer = new char[100];
            int bytesRead = 0;

            Console.WriteLine("Verbunden...");
            Connected = true;

            try
            {
                while (StopMessageloop == false)
                {
                    reader = new StreamReader(dataStream);
                    bytesRead = reader.Read(buffer, 0, 100);
                    string message = new string(buffer);
                    if (message.Contains("ADS-B"))
                        base.NotifyListener(message);
                }

            }
            catch (Exception)
            {

            }

            reader?.Close();
            response.Close();

            Console.WriteLine("Verbindung geschlossen...");
        }


    }
}
