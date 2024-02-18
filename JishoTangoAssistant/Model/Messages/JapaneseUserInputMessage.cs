namespace JishoTangoAssistant.Model.Messages;

public class JapaneseUserInputMessage(JapaneseUserInputMessageType japaneseUserInputMessageType) : Message(MessageType.JapaneseUserInputMessage)
{
    public JapaneseUserInputMessageType JapaneseUserInputMessageType { get; } = japaneseUserInputMessageType;
}