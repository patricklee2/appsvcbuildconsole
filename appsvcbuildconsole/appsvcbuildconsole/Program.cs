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
                //"oryxprod/node-4.4:20190401.5",
                //"oryxprod/node-4.5:20190401.5",
                //"oryxprod/node-4.8:20190401.5",
                //"oryxprod/node-6.2:20190401.5",
                //"oryxprod/node-6.6:20190401.5",
                //"oryxprod/node-6.9:20190401.5",
                //"node-6.10:20190401.5",
                //"node-6.11:20190401.5",
                //"node-8.0:20190401.5",
                //"node-8.1:20190401.5",
                //"node-8.2:20190401.5",
                //"node-8.8:20190401.5",
                //"node-8.9:20190401.5"`
                //"node-8.11:20190401.5",
                //"oryxprod/node-8.12:20190401.5",
                //"oryxprod/node-9.4:20190401.5",
                //"oryxprod/node-10.1:20190401.5",
                //"oryxprod/node-10.10:20190427.2",
                //"oryxprod/node-10.12:20190401.5",
                //"oryxprod/node-10.14:20190427.2"
                "oryxprod/php-7.3:20190501.3",
                "oryxprod/php-7.2:20190501.3",
                "oryxprod/php-7.0:20190501.3",
                "oryxprod/php-5.6:20190501.3"
                /*"oryxprod/php-5.6:20190422.4 ",
                "oryxprod/php-7.0:20190422.4 ",
                "oryxprod/php-7.2:20190422.4 ",
                "oryxprod/php-7.3:20190422.4 "*/
                /*"ruby:2.6.2",
                "ruby:2.5.5",
                "ruby:2.4.5",
                "ruby:2.3.8"*/
                /*"oryxprod/dotnetcore-1.0:20190301.2",
                "oryxprod/dotnetcore-1.1:20190301.2",
                "oryxprod/dotnetcore-2.1:20190301.2",
                "oryxprod/dotnetcore-2.2:20190422.4"*/
                /*"oryxprod/python-2.7:20190223.1",
                "oryxprod/python-3.6:20190223.1,",
                "oryxprod/python-3.7:20190223.1"*/
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
            String stack = "";
            if (tag.ToLower().Contains("python"))
            {
                stack = "Python";
            } else if (tag.ToLower().Contains("node"))
            {
                stack = "Node";
            }
            else if (tag.ToLower().Contains("ruby"))
            {
                stack = "Ruby";
            } else if (tag.ToLower().Contains("php"))
            {
                stack = "Php";
            } else if (tag.ToLower().Contains("dotnetcore"))
            {
                stack = "Dotnetcore";
            }
            String secretKey = "";
            //String url = String.Format("https://appsvcbuildfunc.azurewebsites.net/api/Http{0}Pipeline?code={1}", stack, secretKey);
            String url = String.Format("http://localhost:7071/api/Http{0}Pipeline", stack);

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

