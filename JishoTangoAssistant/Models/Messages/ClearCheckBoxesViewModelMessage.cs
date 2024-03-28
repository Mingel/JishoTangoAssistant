using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class ClearCheckBoxesViewModelMessage(bool isValid) : ValueChangedMessage<bool>(isValid);