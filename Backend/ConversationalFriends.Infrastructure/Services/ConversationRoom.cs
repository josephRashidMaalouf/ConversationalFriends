using ConversationalFriends.Infrastructure.ConversationalFriends;
using ConversationalFriends.Infrastructure.Models;

namespace ConversationalFriends.Infrastructure.Services;

public class ConversationRoom
{
    public List<ConversationalFriend> Talkers { get; set; } = [];

    public ConversationRoom(params ConversationalFriend[] talkers)
    {
        Talkers.AddRange(talkers);
    }
    public ConversationRoom()
    {

    }
    /// <summary>
    /// /The conversational friends engage in a conversation in a random order
    /// </summary>
    /// <param name="conversationLength">For every 1 conversation length, a talk-and-respond will happen</param>
    /// <returns></returns>
    public async Task<List<ConversationalFriendChatMessage>> GetConversationAsync(int conversationLength)
    {
        var conversation = new List<ConversationalFriendChatMessage>();

        var random = new Random();

        var talker = Talkers[random.Next(0, Talkers.Count)];
        var response = await talker.SendUserChatMessageAsync();
        conversation.Add(new ConversationalFriendChatMessage(talker, response));

        for (int i = 0; i < conversationLength; i++)
        {
            var nextSpeakerIndex = random.Next(0, Talkers.Count);
            while (nextSpeakerIndex == Talkers.IndexOf(talker))
            {
                nextSpeakerIndex = random.Next(0, Talkers.Count);
            }

            talker = Talkers[nextSpeakerIndex];
            if (i == conversationLength - 1)
            {
                talker.SetSystemMessage("The next thing you say should wrap up this conversation and end in a goodbye");
            }
            var conversationHistory = conversation.Select(c => c.ToUserChatMessage()).ToList();
            response = await talker.SendUserChatMessageAsync(conversationHistory);
            conversation.Add(new ConversationalFriendChatMessage(talker, response));
        }

        return conversation;
    }
}