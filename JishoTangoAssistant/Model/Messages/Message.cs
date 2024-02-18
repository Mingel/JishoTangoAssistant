namespace JishoTangoAssistant.Model.Messages;

public class Message(MessageType type)
{
    public MessageType Type { get; } = type;
}
