using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aaCHAT
{
    public class MyHub: Hub
    {
        public void Sendmessage(string from,string msg , int sessionid )
        {
            Clients.All.Sendmessage(from, msg,sessionid);
        }
    }
}