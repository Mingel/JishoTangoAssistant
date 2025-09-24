using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Presentation.UI.Messages;

public class UpdateAllNonCollectionPropertiesMessage(bool isProcessingInput)
    : ValueChangedMessage<bool>(isProcessingInput);