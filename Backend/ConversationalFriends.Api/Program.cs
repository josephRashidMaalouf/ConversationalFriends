using System.Net.Http.Headers;
using ConversationalFriends.Api.Dtos;
using ConversationalFriends.Common.Interfaces;
using ConversationalFriends.Common.Models;
using ConversationalFriends.Common.Services;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


var openAiKey = builder.Configuration["OpenAiKey"] ?? "";

builder.Services.AddHttpClient("OpenAi", options => { options.BaseAddress = new Uri("https://api.openai.com"); })
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiKey);
    });

builder.Services.AddScoped<ChatClient>(p => new ChatClient("gpt-4o", openAiKey));
builder.Services.AddScoped<ConversationRoom>();
builder.Services.AddScoped<PodcastService>();
builder.Services.AddScoped<IAudioService, AudioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/", async (
    HttpContext context,
    [FromServices]ChatClient client, 
    [FromServices]PodcastService podcastService, 
    [FromServices]IAudioService audioService,
    [FromBody]GetPodcastRequest podcastRequest) =>
{
    var mira = Mira.Create(client, podcastRequest.Topic, podcastRequest.Language);
    var pierre = Pierre.Create(client, podcastRequest.Topic, podcastRequest.Language);
    
    pierre.SetSystemMessage("The name of your co host is Mira");
    mira.SetSystemMessage("The name of your co host is Pierre");

    await podcastService.AddPodcastSpeaker(mira);
    await podcastService.AddPodcastSpeaker(pierre);

    var messages = await podcastService.StartPodcast(podcastRequest.Length);

    var conversation = messages.Select(x =>
        new ConversationLine(
            x.ConversationalFriend.Name,
            x.ConversationalFriend.Voice.ToString(), 
            x.Content[0].Text))
        .ToList();

    var mp3Path = await audioService.GetSpeechAsync(conversation);
    
    var uniqueFolder = Path.GetDirectoryName(mp3Path);
    
    var mergedStream = new FileStream(mp3Path, FileMode.Open, FileAccess.Read, FileShare.Read);

    context.Response.OnCompleted(() =>
    {
        try
        {
            if (!string.IsNullOrEmpty(uniqueFolder) && Directory.Exists(uniqueFolder))
            {
                Directory.Delete(uniqueFolder, recursive: true);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Cleanup error: {ex.Message}");
        }
        return Task.CompletedTask;
    });
    
    return Results.File(mergedStream, "audio/mpeg", "merged.mp3");
});

app.Run();