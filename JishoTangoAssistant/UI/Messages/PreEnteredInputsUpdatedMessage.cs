using System.Collections.Generic;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.UI.Messages;

public class PreEnteredInputsUpdatedMessage(List<string> value) : ValueChangedMessage<List<string>>(value);