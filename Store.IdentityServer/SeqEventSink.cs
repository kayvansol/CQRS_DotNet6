using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Serilog;
using Serilog.Core;

namespace Store.IdentityServer
{
    public class SeqEventSink : IEventSink
    {
        private readonly Logger _log;

        public SeqEventSink()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
        }

        public Task PersistAsync(Event evt)
        {
            if (evt.EventType == EventTypes.Success ||
                evt.EventType == EventTypes.Information)
            {
                _log.Information("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);
            }
            else
            {
                _log.Error("{Name} ({Id}), Details: {@details}",
                    evt.Name,
                    evt.Id,
                    evt);
            }

            return Task.CompletedTask;
        }
    }
}
