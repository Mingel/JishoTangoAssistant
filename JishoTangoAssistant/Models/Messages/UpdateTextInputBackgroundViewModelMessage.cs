using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class UpdateTextInputBackgroundViewModelMessage(VocabularyItem? value) : ValueChangedMessage<VocabularyItem?>(value);