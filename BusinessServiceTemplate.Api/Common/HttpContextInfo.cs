using System.Text;

namespace BusinessServiceTemplate.Api.Common
{
    public class HttpContextInfo
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string Scheme { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public Dictionary<string, string> QueryString { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }
        public string Body { get; set; }
    }

    public static class HttpContextInfoHelper
    {
        public static async Task<HttpContextInfo> GetHttpRequestInfoAsync(HttpContext httpContext)
        {
            var httpRequest = httpContext?.Request;

            if (httpRequest == null)
            {
                return null;
            }

            string body = "";

            if (httpRequest.ContentLength.HasValue && httpRequest.ContentLength > 0)
            {
                httpRequest.EnableBuffering();

                using (var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, false, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                }

                // Reset the request body stream position so the next middleware can read it
                httpRequest.Body.Position = 0;
            }

            return new HttpContextInfo()
            {
                Host = httpRequest.Host.ToString(),
                Path = httpRequest.Path,
                Scheme = httpRequest.Scheme,
                Method = httpRequest.Method,
                Protocol = httpRequest.Protocol,
                QueryString = httpRequest.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Headers = httpRequest.Headers
                            .ToDictionary(x => x.Key, y => y.Value.ToString()),
                Cookies = httpRequest.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Body = body
            };
        }
    }
}
