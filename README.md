# OpenaiForGodot3.5.1
Scripts to use OpenAI Api in Godot game engine for c#, this woul

Example usage:
```c#
OpenAIConnector openAIConnector = new OpenAIConnector.OpenAIConnector("OUR API KEY");
CompletionResponse response = await openAIConnector.GetCompletionResponse("is this a test?", OpenAIConnector.OpenAIConnector.CompletionModel.Davinci, 64, 0.5f);
string responsetext = response.choices[0].text;
```

