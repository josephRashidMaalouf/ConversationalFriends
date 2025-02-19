using ConversationalFriends.Common.Enums;

namespace ConversationalFriends.Domain.Models;

public class ConversationalFriend
{
    public required OpenAiVoice Voice { get; set; }
    public required string Name { get; set; }
}