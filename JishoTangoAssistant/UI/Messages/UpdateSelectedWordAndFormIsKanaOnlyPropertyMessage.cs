using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.UI.Messages;

public class UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage(bool value)
    : ValueChangedMessage<bool>(value);