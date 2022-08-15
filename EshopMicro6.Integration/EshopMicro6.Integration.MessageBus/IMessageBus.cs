namespace EshopMicro6.Integration.MessageBus;

public interface IMessageBus
{
    Task PublishMessage(BaseMessage baseMessage, string TopicName);
}
