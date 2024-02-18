using JishoTangoAssistant.Model.Messages;

public class ProcessInputMessage : JapaneseUserInputMessage
{
    public ProcessInputMessage(string input) : base(JapaneseUserInputMessageType.ProcessInput)
    {
        Input = input;
    }

    public string Input { get; private set; }
}