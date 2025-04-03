using CommunityToolkit.Mvvm.Messaging.Messages;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.UI.Messages;

public class EditVocabularyItemMessage(VocabularyItem value) : ValueChangedMessage<VocabularyItem>(value);