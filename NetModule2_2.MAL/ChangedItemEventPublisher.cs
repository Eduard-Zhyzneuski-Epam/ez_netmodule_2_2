using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetModule2_2.EAL
{
    internal class ChangedItemEventPublisher : IChangedItemEventPublisher, IDisposable
    {
        private IModel? session = null;
        private bool disposed = false;

        public void Publish(ChangedItem changedItem)
        {
            session ??= OpenSession();
            session.ExchangeDeclare("dead_letters", "direct", true, false);
            var args = new Dictionary<string, object> { { "x-dead-letters-exchange", "dead_letters" } };
            session.QueueDeclare("item_changed", true, false, false, args);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(changedItem));
            session.BasicPublish("", "item_changed", null, body);
        }

        private IModel OpenSession()
        {
            var connectionFactory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            var channel = connectionFactory.CreateConnection();
            return channel.CreateModel();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ChangedItemEventPublisher() => Dispose(false);

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    session?.Dispose();
                }
                disposed = true;
            }
        }
    }
}
