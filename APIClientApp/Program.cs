using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace APIClientApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Encapsualtes the info we need to make the API call
            // Allows us to send authenticated HTTP requests
            var restClient = new RestClient("https://api.postcodes.io/");
            // Set up my request
            var restRequest = new RestRequest();
            //Optional 
            restRequest.Method = Method.Get;
            // Adding my request headers
            restRequest.AddHeader("Content-Type", "application/json");
            string postcode = "EC2Y 5AS";
            restRequest.Resource = $"postcodes/{postcode.ToLower()}";
            RestResponse singlePostcodeResponse = restClient.Execute(restRequest);
            Console.WriteLine("Response content (string)");
            // .Content returns the response body as an unformatted string
            Console.WriteLine(singlePostcodeResponse.Content);
            Console.WriteLine("Response status (int)");
            Console.WriteLine((int)singlePostcodeResponse.StatusCode);
            Console.WriteLine("Response Headers");
            foreach (var header in singlePostcodeResponse.Headers)
            {
                Console.WriteLine(header);
            }

            var headers = singlePostcodeResponse.Headers;
            var responseDateheader = headers.Where(h => h.Name == "Date")
                .Select(h => h.Value).FirstOrDefault();
            Console.WriteLine(responseDateheader);


            var options = new RestClientOptions("https://api.postcodes.io")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var bulkPostcodeRequest = new RestRequest("/postcodes/", Method.Post);
            bulkPostcodeRequest.AddHeader("Content-Type", "application/json");
            var postcodes = new
            {
                Postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" }
            };
            //request.AddStringBody(body, DataFormat.Json);
            bulkPostcodeRequest.AddJsonBody(postcodes);
            RestResponse bulkPostcodeResponse = await client.ExecuteAsync(bulkPostcodeRequest);
            Console.WriteLine(bulkPostcodeResponse.Content);

            var singlePostcodeJsonResponse = JObject.Parse(singlePostcodeResponse.Content);
            Console.WriteLine("\nResponse content as a Jobject");
            Console.WriteLine(singlePostcodeJsonResponse);
            Console.WriteLine("status");
            Console.WriteLine(singlePostcodeJsonResponse["status"]);
            Console.WriteLine("Admins district");
            Console.WriteLine(singlePostcodeJsonResponse["result"]["admin_district"]);

            var bulkPostcodeJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);
            var adminDistrict = bulkPostcodeJsonResponse["result"][1]["result"]["admin_district"];
            Console.WriteLine($"Admin District of 2nd postcode: {adminDistrict}");

            var singlePostcodeObjectResponse = JsonConvert.DeserializeObject<SinglePostcodeResponse>(singlePostcodeResponse.Content);
            Console.WriteLine(singlePostcodeObjectResponse.status);
            Console.WriteLine(singlePostcodeObjectResponse.result.parish);
        }
    }
}