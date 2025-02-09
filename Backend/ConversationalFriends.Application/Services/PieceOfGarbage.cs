using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using NAudio.Wave;

namespace ConversationalFriends.Common.Services;

public class PieceOfGarbage
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public PieceOfGarbage(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task CreateAudio(string saveAudioPath, string voice, string textScript)
    {
        var requestBody = new
        {
            model = "tts-1",
            input = textScript,
            voice = voice,
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/audio/speech", content);

        if (response.IsSuccessStatusCode)
        {
            byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(saveAudioPath, audioBytes);

        }
    }



    public static void PlayAudio(List<string> pathToMp3Files)
    {
        foreach (var file in pathToMp3Files)
        {
            using (var audiofile = new AudioFileReader(file))
            {
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audiofile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1);
                    }
                }
            }
        }
    }
}