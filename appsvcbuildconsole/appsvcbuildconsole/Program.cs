using System;
using System.Net.Http;
using System.Threading.Tasks;
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
            String[] tags =
            {
                //"node-4.4:20190401.5",
                //"node-4.5:20190401.5",
                //"node-4.8:20190401.5",
                //"node-6.2:20190401.5",
                //"node-6.6:20190401.5",
                //"node-6.9:20190401.5",
                //"node-6.10:20190401.5",
                //"node-6.11:20190401.5",
                //"node-8.0:20190401.5",
                //"node-8.1:20190401.5",
                //"node-8.2:20190401.5",
                //"node-8.8:20190401.5",
                //"node-8.9:20190401.5"`
                //"node-8.11:20190401.5",
                //"node-8.12:20190401.5",
                //"node-9.4:20190401.5",
                //"node-10.1:20190401.5",
                //"node-10.10:20190401.5",
                //"node-10.12:20190401.5",
                "node-10.14:20190401.5"
                /*"7.3.2-apache",
                "7.2.15-apache",
                "7.0.33-apache",
                "5.6.40-apache"*/
            };

            foreach(String t in tags)
            {
                await makeRequestAsync(t);
                System.Threading.Thread.Sleep(1 * 60 * 1000); // sleep 1 mins between builds
            }
        }

        static async Task makeRequestAsync(String tag)
        {
            Console.WriteLine(String.Format("making tag: {0}", tag));

            //String url = "https://appsvcbuildfunc.azurewebsites.net/api/HttpNodePipeline?code=";
            String url = "http://localhost:7071/api/HttpNodePipeline";
            String body = String.Format("{{\"newTags\": [\"{0}\"]}}", tag);
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
