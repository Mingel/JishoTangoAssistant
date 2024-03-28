using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class ResetMeaningsViewModelMessage(bool isValid) : ValueChangedMessage<bool>(isValid);