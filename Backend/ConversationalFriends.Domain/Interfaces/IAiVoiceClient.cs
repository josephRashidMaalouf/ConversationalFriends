using ConversationalFriends.Domain.Models;

namespace ConversationalFriends.Domain.Interfaces;

public interface IAiVoiceClient
{
    Task<string> GetSpeechAsync(List<ConversationLine> conversationLines);
}