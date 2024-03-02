using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class UpdateTextInputBackgroundMessage(VocabularyItem? value) : ValueChangedMessage<VocabularyItem?>(value);