using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JishoTangoAssistant.Models.Messages;

public class ChangeMeaningMessage(Meaning value) : ValueChangedMessage<Meaning>(value);