namespace APITestApp.SinglePostcodeServiceTests
{
    [Category("Happy")]
    public class WhenTheSinglePostcodeServiceIsCalled_WithValidPostcode
    {
        private SinglePostcodeService _singlePostcodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetupAsync()
        {
            _singlePostcodeService = new SinglePostcodeService();
            await _singlePostcodeService.MakeRequestAsync("EC2Y 5AS");
        }

        [Test]
        public void StatisIs200_InJsonResponseBody()
        {
            Assert.That(_singlePostcodeService.JsonResponse["status"].ToString(), Is.EqualTo("200"));
        }

        [Test]
        public void StatusIs200_InResponseHeader()
        {
            Assert.That((int)_singlePostcodeService.GetStatusCode(), Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _singlePostcodeService.JsonResponse["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ContentType_IsJson()
        {
            Assert.That(_singlePostcodeService.GetResponseContentType(), Is.EqualTo("application/json"));
        }

        [Test]
        public void ConnectionIsKeepAlive()
        {
            var result = _singlePostcodeService.GetHeaderValue("Connection");
            Assert.That(result, Is.EqualTo("keep-alive"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            Assert.That(_singlePostcodeService.ResponseObject.status, Is.EqualTo(200));
        }

        [Test]
        public void StatusInResponseHeader_SameAsStatusInResponseBody()
        {
            Assert.That((int)_singlePostcodeService.SinglePostcodeDTO.Response.status, Is.EqualTo(_singlePostcodeService.ResponseObject.status));
        }

        [Test]
        public void AdminDistrict_IsCityOfLondon()
        {
            Assert.That(_singlePostcodeService.ResponseObject.result.admin_district, Is.EqualTo("City of London"));
        }
    }
}
