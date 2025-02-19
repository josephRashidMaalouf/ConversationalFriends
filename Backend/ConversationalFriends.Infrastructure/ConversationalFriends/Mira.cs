using ConversationalFriends.Common.Enums;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.ConversationalFriends;

public class Mira : PodcastSpeaker
{
    public Mira(ChatClient chatClient, OpenAiVoice voice = OpenAiVoice.sage, string name = "Mira") : base(chatClient, voice, name)
    {
        SetSystemMessage("Your personality is confident and dominant. You curse alot and use a horrible language. When someone is rude you call them out, and you are not afraid of conflicts. You are very charismatic, and tell clever jokes when you see fit.");

    }

    public static Mira Create(ChatClient chatClient, string topicDescription)
    {
        var mira = new Mira(chatClient);
        mira.SetSystemMessage(topicDescription);
        return mira;
    }
    public static Mira Create(ChatClient chatClient, string topicDescription, string language)
    {
        var mira = new Mira(chatClient);
        mira.SetSystemMessage(topicDescription);
        mira.SetSystemMessage($"You speak this language: {language}.");
        return mira;
    }
}