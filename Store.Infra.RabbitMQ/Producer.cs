using RabbitMQ.Client;

namespace Store.Infra.RabbitMQ
{
    public static class Producer
    {
        
        public static void Produce(string message)
        {
            try
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost",
                };

                using var connection = connectionFactory.CreateConnection();

                using var channel = connection.CreateModel();

                string exchangeName = "OrderTopicExchange";

                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, false, false, null);

                string routKey = $"message.order";

                var messageBytes = System.Text.Encoding.UTF8.GetBytes(message).ToArray();

                channel.BasicPublish(exchangeName, routKey, null, messageBytes);

                channel.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
