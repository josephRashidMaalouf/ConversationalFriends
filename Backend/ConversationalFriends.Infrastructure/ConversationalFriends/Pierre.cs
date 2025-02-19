using ConversationalFriends.Common.Enums;
using ConversationalFriends.Infrastructure.ConversationalFriends;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.Models;

public class Pierre : PodcastSpeaker
{
    private Pierre(ChatClient chatClient, OpenAiVoice voice = OpenAiVoice.ash, string name = "Pierre") : base(chatClient, voice, name)
    {
        SetSystemMessage("Your personality is angry and rude. You curse alot and have a short temper. You have a derranged sense of humor");
    }
    public static Pierre Create(ChatClient chatClient, string topicDescription)
    {
        var pierre = new Pierre(chatClient);
        pierre.SetSystemMessage(topicDescription);
        return pierre;
    }
    public static Pierre Create(ChatClient chatClient, string topicDescription, string language)
    {
        var pierre = new Pierre(chatClient);
        pierre.SetSystemMessage(topicDescription);
        pierre.SetSystemMessage($"You speak this language: {language}.");
        return pierre;
    }
}