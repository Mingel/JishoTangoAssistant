using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Presentation.UI.Messages;

public class PreEnteredInputsUpdatedMessage(List<string> value) : ValueChangedMessage<List<string>>(value);