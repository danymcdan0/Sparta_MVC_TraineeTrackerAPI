using APIClientApp.PostcodesIOService.DataHandling;
using APIClientApp.PostcodesIOService.HTTPManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace APIClientApp
{
    public class SinglePostcodeService
    {
        #region Properties
        public CallManager CallManager { get; set; }
        public JObject JsonResponse {get;set;}
        public SinglePostcodeResponse ResponseObject { get; set; }
        public string PostcodeResponse { get; set; }

        public DTO<SinglePostcodeResponse> SinglePostcodeDTO { get; set; }
        #endregion

        public SinglePostcodeService()
        {
            CallManager = new CallManager();
            SinglePostcodeDTO = new DTO<SinglePostcodeResponse>();
        }


        /// <summary>
        /// defines and makes the API request and stores the response
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public async Task MakeRequestAsync(string postcode)
        {
            PostcodeResponse = await CallManager.MakeRequestAsync(postcode);
            JsonResponse = JObject.Parse(PostcodeResponse);
            SinglePostcodeDTO.DeserializeResponse(PostcodeResponse);
        }

        public int GetStatusCode()
        {
            return (int)CallManager.RestResponse.StatusCode;
        }

        public string? GetHeaderValue(string name)
        {
            return CallManager.RestResponse.Headers.Where(x => x.Name == name).Select(x => x.Value.ToString()).FirstOrDefault();
        }

        public string GetResponseContentType()
        {
            return CallManager.RestResponse.ContentType;
        }
    }
}
