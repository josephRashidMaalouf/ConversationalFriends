using ConversationalFriends.Common.Enums;
using OpenAI.Chat;

namespace ConversationalFriends.Common.Models;

public class ConversationalFriend
{
    private readonly ChatClient _chatClient;
    private readonly List<SystemChatMessage> _systemChatMessages = new();
    private readonly List<ChatMessage> _chatHistory = new();
    public OpenAiVoice Voice { get; set; }
    public string Name { get; set; }

    public ConversationalFriend(ChatClient chatClient, OpenAiVoice voice = OpenAiVoice.alloy, string name = "Alloy")
    {
        Voice = voice;
        Name = name;
        _chatClient = chatClient;
        _systemChatMessages.Add(new SystemChatMessage($"Your name is {Name}"));
        _chatHistory.AddRange(_systemChatMessages);
    }

    public async Task<UserChatMessage> SendUserChatMessageAsync()
    {
        var completion = await _chatClient.CompleteChatAsync(_chatHistory);
        var generatedResponse = new UserChatMessage(completion.Value.Content[0].Text);
        _chatHistory.Add(generatedResponse);
        return generatedResponse;
    }
    public async Task<UserChatMessage> SendUserChatMessageAsync(UserChatMessage message)
    {
        _chatHistory.Add(message);
        var completion = await _chatClient.CompleteChatAsync(_chatHistory);
        var generatedResponse = new UserChatMessage(completion.Value.Content[0].Text);
        _chatHistory.Add(generatedResponse);
        return generatedResponse;
    }
    public async Task<UserChatMessage> SendUserChatMessageAsync(List<UserChatMessage> messages)
    {
        _chatHistory.Clear();
        _chatHistory.AddRange(_systemChatMessages);
        _chatHistory.AddRange(messages);
        var completion = await _chatClient.CompleteChatAsync(_chatHistory);
        var generatedResponse = new UserChatMessage(completion.Value.Content[0].Text);
        _chatHistory.Add(generatedResponse);
        return generatedResponse;
    }
    public void SetSystemMessage(string systemMessage)
    {
        var msg = new SystemChatMessage(systemMessage);
        _systemChatMessages.Add(msg);
        _chatHistory.Add(msg);
    }


}