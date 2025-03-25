using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.UI.Messages;

public class UpdateAllNonCollectionPropertiesMessage(bool isProcessingInput)
    : ValueChangedMessage<bool>(isProcessingInput);