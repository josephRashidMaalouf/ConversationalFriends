namespace ConversationalFriends.Common.Managers;

public static class DirectoryManager
{
    public static void SetAudioOutputDirectory(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
        Directory.SetCurrentDirectory(dirName);
    }
}