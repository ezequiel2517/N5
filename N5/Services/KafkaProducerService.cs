using Confluent.Kafka;
using N5.Interfaces;
using Newtonsoft.Json;

namespace N5.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducerService(IConfiguration configuration)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:Server"]
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task Produce(string topic, string message)
        {
            var mensaje = new
            {
                Id = Guid.NewGuid().ToString(),
                Mensaje = message
            };

            var mensajeJson = JsonConvert.SerializeObject(mensaje);

            await _producer.ProduceAsync(topic, new Message<string, string> { Value = mensajeJson });
        }
    }
}
