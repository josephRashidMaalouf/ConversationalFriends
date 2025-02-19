using ConversationalFriends.Domain.Interfaces;
using ConversationalFriends.Domain.Models;
using ConversationalFriends.Infrastructure.Models;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.Services;

public class ConversationService : IConversationService
{

    private readonly List<ConversationalFriend> _conversationalFriends;
    private readonly IMessageSender<ConversationalFriendChatMessage> _openAiChatClient;

    public ConversationService(IMessageSender<ConversationalFriendChatMessage> openAiChatClient, params ConversationalFriend[] conversationalFriends)
    {
        _conversationalFriends = conversationalFriends.ToList();
        _openAiChatClient = openAiChatClient;
    }
    
    public async Task<List<ConversationMessage>> GetConversationAsync(int conversationLength)
    {
        var conversation = new List<ConversationalFriendChatMessage>();

        var random = new Random();

        var talker = _conversationalFriends[random.Next(0, _conversationalFriends.Count)];
        var response = await _openAiChatClient.SendUserChatMessageAsync(conversation);
        
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
            conversation.Add(new UserChatMessage(talker, response));
        }

        return conversation;
    }
}