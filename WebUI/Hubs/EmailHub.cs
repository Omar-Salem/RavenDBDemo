using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebUI.Hubs
{
    [HubName("emailHub")]
    public class EmailHub : Hub
    {
        public void Send(string name, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<EmailHub>();
            context.Clients.All.addEmail(name, message);
        }
    }
}