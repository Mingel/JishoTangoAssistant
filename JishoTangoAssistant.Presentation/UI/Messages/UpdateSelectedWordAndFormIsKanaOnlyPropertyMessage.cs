using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Presentation.UI.Messages;

public class UpdateSelectedWordAndFormIsKanaOnlyPropertyMessage(bool value)
    : ValueChangedMessage<bool>(value);