using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Presentation.UI.Messages;

public class ChangePreEnteredInputViewVisibilityMessage(bool value) : ValueChangedMessage<bool>(value);