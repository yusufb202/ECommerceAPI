namespace Service
{
    public interface IRabbitMQService
    {
        void PublishMessage(string queueName, string message);
    }

}
