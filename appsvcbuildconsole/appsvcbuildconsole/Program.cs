using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace appsvcbuildconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            buildAsync();
            while (true)    //sleep until user exits
            {
                System.Threading.Thread.Sleep(5000);
            }
        }
        static async void buildAsync()
        {
            String text = File.ReadAllText("../../../requests.json");
            List<BuildRequest> buildRequests = JsonConvert.DeserializeObject<List<BuildRequest>>(text);

            foreach (BuildRequest br in buildRequests)
            {
                await makeRequestAsync(br);
                System.Threading.Thread.Sleep(1 * 60 * 1000); // sleep 1 mins between builds
            }
        }

        static async Task makeRequestAsync(BuildRequest br)
        {
            Console.WriteLine(String.Format("making tag: {0} {1}", br.Stack, br.Version));
            String stack = String.Format("{0}{1}", br.Stack.ToUpper()[0], br.Stack.ToLower().Substring(1));
            String secretKey = "";
            //String url = String.Format("https://appsvcbuildfunc.azurewebsites.net/api/Http{0}Pipeline?code={1}", stack, secretKey);
            String url = String.Format("http://localhost:7071/api/Http{0}Pipeline", stack);

            String body = JsonConvert.SerializeObject(new List<BuildRequest>{ br });
            var client = new RestClient(url);
            client.Timeout = 1000 * 60 * 60; //1h
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.StatusCode.ToString());
            
            String result = response.Content.ToString();
            Console.WriteLine(result);
        }
    }
}

