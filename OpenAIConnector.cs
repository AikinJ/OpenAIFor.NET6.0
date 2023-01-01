using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIConnector
{
    public class OpenAIConnector
    {
        private string apiKey;

        private static string baseDomain = "https://api.openai.com/v1/";
        private static string daleDomainExtension = "images/generations";
        private static string completionExtension = "completions";

        private static string completionUrl
        {
            get
            {
                return baseDomain + completionExtension;
            }
        }

        private static string imageGenerationUrl
        {
            get
            {
                return baseDomain + daleDomainExtension;
            }
        }

        private static HttpClient authorizedClient(string apikey)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apikey);
            return client;        
        }

        public OpenAIConnector(string apikey)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            apiKey = apikey;
        }

        public async Task<CompletionResponse> GetCompletionResponse(string prompt, CompletionModel modelToUse,int maxTokens,float temperature)
        {
            var payload = new CompletionRequest
            {
                model = CompletionModelStrings[(int)modelToUse],
                prompt = prompt,
                max_tokens = maxTokens,
                temperature = temperature
            };
            string responsejson = await GetResponseJson(payload,completionUrl);
            return JsonConvert.DeserializeObject<CompletionResponse>(responsejson);
        }

        public async Task<ImageGenerationResponse> GetImageGenerationResponse(string prompt, int amountOfImages)
        {
            var payload = new ImageGenerationRequest
            {
                prompt = prompt,
                n = amountOfImages,
                response_format = "url"
            };
            string responsejson = await GetResponseJson(payload, imageGenerationUrl);
            return JsonConvert.DeserializeObject<ImageGenerationResponse>(responsejson);

        }

        private async Task<string> GetResponseJson(object requestPayload,string url)
        {
            HttpClient client = authorizedClient(apiKey);
            var uri = new Uri(url);
            string questionjson = JsonConvert.SerializeObject(requestPayload);
            GD.Print(questionjson);
            var httpcontent = new StringContent(questionjson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(completionUrl, httpcontent);
            string responsestring = await response.Content.ReadAsStringAsync();
            GD.Print(responsestring);
            return responsestring;
        }

        /// <summary>
        /// different models that can be used for text completion tasks
        /// </summary>
        public enum CompletionModel
        {
            /// <summary>
            /// Most capable GPT-3 model. Can do any task the other models can do, often with higher quality, longer output and better instruction-following. Also supports inserting completions within text.
            /// </summary>
            Davinci,
            /// <summary>
            /// Very capable, but faster and lower cost than Davinci.
            /// </summary>
            Curie,
            /// <summary>
            /// Capable of straightforward tasks, very fast, and lower cost.
            /// </summary>
            Babbage,
            /// <summary>
            /// Capable of very simple tasks, usually the fastest model in the GPT-3 series, and lowest cost.
            /// </summary>
            Ada
        }

        private string[] CompletionModelStrings = new string[] { "text-davinci-003", "text-curie-001", "text-babbage-001", "text-ada-001" };

    }

    public class CompletionRequest
    {
        public string model;
        public string prompt;
        public int max_tokens;
        public float temperature;

    }


    public class CompletionResponse
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Choice[] choices { get; set; }
        public Usage usage { get; set; }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    public class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class ImageGenerationRequest
    {
        public string prompt;
        public int n;
        public string response_format;
    }




    public class ImageGenerationResponse
    {
        public int created { get; set; }
        public Datum[] data { get; set; }
    }


}

