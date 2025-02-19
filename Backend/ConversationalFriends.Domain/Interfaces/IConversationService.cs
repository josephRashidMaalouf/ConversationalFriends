using ConversationalFriends.Domain.Models;

namespace ConversationalFriends.Domain.Interfaces;

public interface IConversationService
{
    Task<List<ConversationMessage>> GetConversationAsync(int conversationLength);
}