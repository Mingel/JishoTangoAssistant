using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.UI.Messages;

public class ChangePreEnteredInputViewVisibilityMessage(bool value) : ValueChangedMessage<bool>(value);