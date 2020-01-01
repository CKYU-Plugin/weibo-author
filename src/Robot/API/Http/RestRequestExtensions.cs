using RestSharp;
using SinaimgPublisher.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Robot.API.Http
{
    public static class RestRequestExtensions
    {
        public static void SetCookies(this IRestRequest restRequest, string cookies)
        {
            foreach (var s in cookies.Split(';').Select(x => x.Trim()))
            {
                var nameValue = s.Split('=');
                restRequest.AddCookie(nameValue[0], nameValue[1]);
            }
        }

        public static void SetReferer(this IRestRequest restRequest, string referer)
        {
            restRequest.AddHeader("Referer", referer);
        }

        public static void SetAccept(this IRestRequest restRequest,
            string accept = "application/json, text/javascript, */*; q=0.01")
        {
            restRequest.AddHeader("Accept", accept);
        }

        public static void SetUserAgent(this IRestRequest restRequest,
            string userAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36")
        {
            restRequest.AddHeader("User-Agent", userAgent);
        }
    }
}
