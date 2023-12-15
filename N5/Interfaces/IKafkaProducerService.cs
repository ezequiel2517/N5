namespace N5.Interfaces
{
    public interface IKafkaProducerService
    {
        public Task Produce(string topic, string message);
    }
}
