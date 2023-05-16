using Newtonsoft.Json;


namespace APIClientApp.PostcodesIOService.DataHandling
{
    // We can deserialise response strings to anything that implements the IResponse interface
    public class DTO<T> where T : IResponse, new()
    {
        // Model the data returned by the API call
        public T Response { get; set; }

        public void DeserializeResponse(string postCoderesponse)
        {
            Response = JsonConvert.DeserializeObject<T>(postCoderesponse);
        }
    }
}
