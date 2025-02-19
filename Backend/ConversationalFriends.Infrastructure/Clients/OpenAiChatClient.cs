using ConversationalFriends.Domain.Interfaces;
using ConversationalFriends.Domain.Models;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.Clients;

public class OpenAiChatClient : IMessageSender<UserChatMessage>
{
    private readonly ChatClient _chatClient;

    public OpenAiChatClient(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<UserChatMessage> SendUserChatMessageAsync(UserChatMessage message)
    {
        var completion = await _chatClient.CompleteChatAsync(message);
        var generatedResponse = new UserChatMessage(completion.Value.Content[0].Text);
        return generatedResponse;
    }

    public async Task<UserChatMessage> SendUserChatMessageAsync(List<UserChatMessage> messages)
    {
        var completion = await _chatClient.CompleteChatAsync(messages);
        var generatedResponse = new UserChatMessage(completion.Value.Content[0].Text);
        return generatedResponse;
    }
}