# OpenAIFor.NET6.0
Scripts to use OpenAI Api in .NET 6.0 c#, this is the version that works with GODOT game engine.

for this to work you need packages newtonsoft.json and system.net.http

Example usage:
```c#
OpenAIConnector openAIConnector = new OpenAIConnector.OpenAIConnector("YOUR API KEY");
CompletionResponse response = await openAIConnector.GetCompletionResponse("is this a test?", OpenAIConnector.OpenAIConnector.CompletionModel.Davinci, 64, 0.5f);
string responsetext = response.choices[0].text;
```

```c#
OpenAIConnector openAIConnector = new OpenAIConnector.OpenAIConnector("YOUR API KEY");
ImageGenerationResponse response = await openAIConnector.GetImageGenerationResponse("is this a test?", 1);
string responseimageurl = response.data[0].url;
```
