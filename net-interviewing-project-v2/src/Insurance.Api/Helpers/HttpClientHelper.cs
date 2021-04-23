using System;
using System.Net.Http;

namespace Insurance.Api.Helpers
{
    public class HttpClientHelper
    {
        public static HttpClient CreateHttpConnection(string baseAddress)
        {
            return new HttpClient { BaseAddress = new Uri(baseAddress) };
        }
    }
}
