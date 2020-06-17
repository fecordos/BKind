using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BKind.Hubs
{
    public class ChatHub : Hub
    {
        //trimite mesaj spre toti utilizatorii -> mesaj de grup
        public async Task GroupMessage(object message)
        {
           await Clients.All.SendAsync("ReceiveMessage", message);
        }

    }
}
