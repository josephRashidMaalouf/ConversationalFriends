using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ConversationalFriends.Common.Interfaces;
using ConversationalFriends.Common.Models;
using NAudio.Wave;

namespace ConversationalFriends.Common.Services;

public class AudioService : IAudioService
{
    private readonly HttpClient _httpClient;

    public AudioService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OpenAi");
    }

    private async Task<string?> GetAndDownLoadSpeechAsync(ConversationLine conversationLine, string saveToFolderPath)
    {
        var requestBody = new
        {
            model = "tts-1",
            input = conversationLine.Line,
            voice = conversationLine.Voice,
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/speech", content);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var tempPath = Path.Combine(saveToFolderPath, Path.GetRandomFileName() + ".mp3");

        byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
        await File.WriteAllBytesAsync(tempPath, audioBytes);
        return tempPath;
    }

    public async Task<string> GetSpeechAsync(List<ConversationLine> conversationLines)
    {
        
        var uniqueFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(uniqueFolder);
        
        var files = new List<string>();
        foreach (var conversationLine in conversationLines)
        {
            var file = await GetAndDownLoadSpeechAsync(conversationLine, uniqueFolder);
            if (file is null)
            {
                continue;
            }
            files.Add(file);
        }
        var outputFileName = Path.Combine(uniqueFolder, Path.GetRandomFileName() + ".mp3");
        MergeMp3Files(outputFileName, files);
        return outputFileName;
    }

    private void MergeMp3Files(string outputFile, List<string> mp3Files)
    {
        if (File.Exists(outputFile))
        {
            File.Delete(outputFile);
        }


        using (var writer = new FileStream(outputFile, FileMode.Create))
        {
            foreach (var file in mp3Files)
            {
                using (var reader = new Mp3FileReader(file))
                {
                    Mp3Frame frame;
                    while ((frame = reader.ReadNextFrame()) != null)
                    {
                        writer.Write(frame.RawData, 0, frame.RawData.Length);
                    }
                }
            }
        }
    }
}