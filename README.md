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

to use the image from the url in your GODOT game you will have to do something like this:

```c#
private async void GetSprite(string prompt)
{
OpenAIConnector openAIConnector = new OpenAIConnector.OpenAIConnector("YOUR API KEY");
ImageGenerationResponse response = await openAIConnector.GetImageGenerationResponse("is this a test?", 1);
string responseimageurl = response.data[0].url;
HTTPRequest newrequest = new HTTPRequest();
AddChild(newrequest);
newrequest.Connect("request_completed", this, "ImageDownloaded");
newrequest.Request(responseimageurl);
}

private void ImageDownloaded(int result, int responsecode, string[] header, byte[] body)
{
var image = new Image();
image.LoadPngFromBuffer(body);
ImageTexture itex = new ImageTexture();
itex.CreateFromImage(image);
//this itex is the generated image as a image in engine
}
```
