using ConversationalFriends.Domain.Models;
using ConversationalFriends.Infrastructure.ConversationalFriends;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.Models;

public class ConversationalFriendChatMessage : UserChatMessage
{
    public ConversationalFriend ConversationalFriend { get; set; }

    public ConversationalFriendChatMessage(ConversationalFriend conversationalFriend, UserChatMessage chatMessage) : base(chatMessage.Content)
    {
        ConversationalFriend = conversationalFriend;
    }

    public UserChatMessage ToUserChatMessage()
    {
        return new UserChatMessage(this.Content);
    }
}