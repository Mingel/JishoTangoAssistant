using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class UpdateMeaningViewModelMessage(IEnumerable<IEnumerable<string>> newMeanings) : ValueChangedMessage<IEnumerable<IEnumerable<string>>>(newMeanings);