using ConversationalFriends.Common.Enums;

namespace ConversationalFriends.Domain.Models;

public record ConversationLine(string Name, OpenAiVoice Voice, string Line);