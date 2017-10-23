using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestMillisecondInterviewTask
{
    public class MillisecondTaskTest
    {
        private const string _baseuri = "http://millisecondinterviewtaskapp.azurewebsites.net/api/Deployment";

        [Fact]
        public async Task GetAllDeployments_ShouldReturnAllDeployments()
        {
            
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_baseuri + "/allData"),
                Method = HttpMethod.Get
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string testJsonResponseStr = @"
                {
                    ""deployments"": [
                        {
                            ""name"": ""ta104400ww_dev"",
                                ""description"": ""ND1 TA-1044_00WW (dev-keys)"",
                                ""categories"": [
                                  ""full""
                                ]
                        }
                    ]
                }";
            
            using (var response = client.SendAsync(request).Result)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var respFirstDeploymentJObj = JArray.Parse(responseBody)[0];
                var testRespJObj = JObject.Parse(testJsonResponseStr);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(testRespJObj, respFirstDeploymentJObj);
            }
        
        }
    }
}
