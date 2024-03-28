using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class PerformUpdateOutputTextMessage(bool isValid) : ValueChangedMessage<bool>(isValid);