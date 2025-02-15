## Json body for request

```
{
  "topic": "",
  "language": "",
  "length": 1
}
```

## HttpClient

```
using System.Net.Http.Headers;
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Post,
    RequestUri = new Uri("http://[::]:5000"),
    Content = new StringContent("{\n  \"topic\": \"\",\n  \"language\": \"\",\n  \"length\": 1\n}")
    {
        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
    }
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}
```

## Fetch

```
fetch('http://[::]:5000/', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    topic: '',
    language: '',
    length: 1
  })
})
```
