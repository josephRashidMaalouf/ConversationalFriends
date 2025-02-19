namespace ConversationalFriends.Domain.Models;

public class ConversationMessage
{
    public required ConversationalFriend Author { get; set; }
    public required string Content { get; set; }
}