using System;
using System.Net.Http;
using System.Threading.Tasks;

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
                //"node-4.4:20190104.1",
                //"node-4.5:20190104.1",
                "node-4.8:20190104.1",
                //"node-6.2:20190104.1",
                "node-6.6:20190104.1"
                //"node-6.9:20190104.1",
                //"node-6.10:20190104.1",
                //"node-6.11:20190104.1",
                //"node-8.0:20190104.1",
                //"node-8.1:20190104.1",
                //"node-8.2:20190104.1",
                //"node-8.8:20190104.1",
                //"node-8.9:20190104.1",
                //"node-8.11:20190104.1",
                //"node-8.12:20190104.1",
                //"node-9.4:20190104.1",
                //"node-10.1:20190104.1",
                //"node-10.10:20190104.1",
                //"node-10.12:20190104.1",
                //"node-10.14:20190104.1"
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
            HttpClient client = new HttpClient();
            String url = "https://appsvcbuildfunc.azurewebsites.net/api/HttpNodePipeline?code=";
            //String url = "http://localhost:7071/api/HttpNodePipeline";
            String body = String.Format("{{\"newTags\": [\"{0}\"]}}", tag);

            client.Timeout = new TimeSpan(3, 0, 0);

            HttpResponseMessage response = await client.PostAsync(url, new StringContent(body));

            Console.WriteLine(response.StatusCode);

            String result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
    }
}
