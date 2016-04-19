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
        private WebRequest request = null;
        private WebResponse response = null;
        private Stream dataStream = null;
        private StreamReader reader = null;

        public WebMessageRepository(string url)
        {
            ServerURL = url;
        }

        public override void StartMessageLoop()
        {
            try
            {
                Connect();
                Connected = true;

                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);

                Console.WriteLine("Verbunden...");

                MessageLoop();

            }
            catch (Exception)
            {
                Console.WriteLine("Es konnte keine Verbindung hergestellt werden...");
                return;
            }
            finally
            {
                CloseConnection();

            }
        }

        private void Connect()
        {
            request = WebRequest.Create(ServerURL);


            ((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            response = request.GetResponse();
        }

        private void CloseConnection()
        {
            reader?.Close();
            response.Close();

            Console.WriteLine("Verbindung geschlossen...");
        }

        private void MessageLoop()
        {
            char[] buffer = new char[100];

            while (StopMessageloop == false)
            {
                string message = ReadMessageStream(buffer);

                RaiseEventMessageReceived(message);
            }
        }

        private string ReadMessageStream(char[] buffer)
        {
            reader.Read(buffer, 0, 100);
            return new string(buffer);
        }

        private bool IsValidMessage(string message)
        {
            return message.Contains("ADS-B") && message != string.Empty;
        }

        private void RaiseEventMessageReceived(string message)
        {
            if (IsValidMessage(message))
                base.NotifyListener(message);
        }

    }
}
