using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class ProcessInputMessage(string value) : ValueChangedMessage<string>(value);