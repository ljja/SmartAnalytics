using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SmartAnalytics.Cache;

namespace SmartAnalytics.Report.Hubs
{
    public class FmHub : Hub
    {
        private readonly CacheContext _subscribe = new RedisContext();

        public void Fm()
        {
            Clients.All.Message("hello");
        }

        private void CacheContextOnMessage(string channel, string message)
        {
            var messageArray = message.Split((char)1);
            if (messageArray.Length >= 14 && string.Compare(messageArray[13], "_trackPageview", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                Clients.Client(Context.ConnectionId).Message(new
                {
                    timestamp = messageArray[0],
                    userip = messageArray[1],
                    tracecode = messageArray[2],
                    sessionid = messageArray[3],
                    domain = messageArray[4],
                    width = messageArray[5],
                    height = messageArray[6],
                    dept = messageArray[7],
                    lang = messageArray[8],
                    url = messageArray[9],
                    referrer = messageArray[10],
                    useragent = messageArray[11],
                    account = messageArray[12],
                    traceevent = messageArray[13],
                });
            }
        }

        public override Task OnConnected()
        {
            _subscribe.Subscribe("main:fm");

            _subscribe.Message += CacheContextOnMessage;

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _subscribe.Message -= CacheContextOnMessage;

            return base.OnDisconnected(stopCalled);
        }
    }
}