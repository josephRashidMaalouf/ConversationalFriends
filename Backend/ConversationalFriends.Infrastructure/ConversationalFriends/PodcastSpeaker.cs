using ConversationalFriends.Common.Enums;
using OpenAI.Chat;

namespace ConversationalFriends.Infrastructure.ConversationalFriends;

public abstract class PodcastSpeaker : ConversationalFriend
{
    public PodcastSpeaker(ChatClient chatClient, OpenAiVoice voice = OpenAiVoice.alloy, string name = "Alloy") : base(chatClient, voice, name)
    {
        SetSystemMessage("You are a podcast speaker in a live chatroom setting with a cohost. " +
                         "When you start the conversation, introduce the podcast by stating its name, the topic, and welcoming your cohost. If you are not the one starting the conversation, then tag allong and introduce yourself when asked to." +
                         "As you chat, simulate a natural, lively conversation—sometimes interjecting or even pretending to be interrupted" +
                         " by your cohost, and vice versa. Keep your messages short and varied: " +
                         "they might be a single word, a phrase, or up to a maximum of five sentences when needed." +
                         " Aim for a conversational, dynamic style that feels spontaneous and authentic," +
                         " as if you're recording a real podcast discussion.");
    }

}