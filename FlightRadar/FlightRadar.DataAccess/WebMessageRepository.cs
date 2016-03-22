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

        public WebMessageRepository(string url)
        {
            ServerURL = url;
        }


        public override void StartMessageLoop()
        {
            request = WebRequest.Create(ServerURL);
            ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            WebResponse response = request.GetResponse();

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = null;

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

                    base.NotifyListener(new string(buffer));
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
