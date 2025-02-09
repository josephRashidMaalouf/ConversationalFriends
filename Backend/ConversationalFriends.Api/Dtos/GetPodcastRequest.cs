namespace ConversationalFriends.Api.Dtos;

public record GetPodcastRequest(string Topic, string Language, int Length);