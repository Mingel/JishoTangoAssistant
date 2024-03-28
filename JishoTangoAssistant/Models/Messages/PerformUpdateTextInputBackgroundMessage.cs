using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class PerformUpdateTextInputBackgroundMessage(bool isValid) : ValueChangedMessage<bool>(true); // TODO use another way 