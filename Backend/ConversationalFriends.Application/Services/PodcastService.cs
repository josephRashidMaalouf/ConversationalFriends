using ConversationalFriends.Common.Models;
using OpenAI.Chat;

namespace ConversationalFriends.Common.Services;

public class PodcastService
{
    private readonly ConversationRoom _conversationRoom;
    public PodcastService(ConversationRoom conversationRoom)
    {
        _conversationRoom = conversationRoom;
    }

    /// <summary>
    /// The podcast speakers will engage in conversation
    /// </summary>
    /// <param name="length">Specify how many lines will be spoken by the speakers</param>
    /// <returns>Conversation history</returns>
    public async Task<List<ConversationalFriendChatMessage>> StartPodcast(int length)
    {
        return await _conversationRoom.StartConversationRandomOrder(length);
    }

    public async Task AddPodcastSpeaker(PodcastSpeaker podcastSpeaker)
    {
        _conversationRoom.Talkers.Add(podcastSpeaker);
    }
}