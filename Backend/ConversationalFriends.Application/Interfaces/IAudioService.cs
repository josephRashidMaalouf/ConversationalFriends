using ConversationalFriends.Common.Models;

namespace ConversationalFriends.Common.Interfaces;

public interface IAudioService
{
    Task<string> GetSpeechAsync(List<ConversationLine> conversationLines);
}