using ConversationalFriends.Domain.Models;

namespace ConversationalFriends.Domain.Interfaces;

public interface IMessageSender<T> where T : class
{
    Task<T> SendUserChatMessageAsync(T message);
    Task<T> SendUserChatMessageAsync(List<T> messages);
}