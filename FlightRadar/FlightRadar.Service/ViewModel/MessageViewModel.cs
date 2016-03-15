using FlightRadar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightRadar.DataAccess;

namespace FlightRadar.Service.ViewModel
{
    public class MessageViewModel
    {
        /// <summary>
        /// Hält die liste von Nachrichten
        /// </summary>
        public List<ADSBMessageBase> MessageList { get; set; } = new List<ADSBMessageBase>();

        private IMessageRepository repo = null;

        public MessageViewModel(IMessageRepository repository)
        {
            repo = repository;
        }

        /// <summary>
        /// Holt alle Daten
        /// </summary>
        public void Update()
        {
            if (repo == null)
                return;

            MessageList.Add(repo.GetMessage());
        }
    }
}
