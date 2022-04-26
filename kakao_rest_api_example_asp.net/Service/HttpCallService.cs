using kakao_rest_api_example_asp.net.Common;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace kakao_rest_api_example_asp.net.Service
{
    public class HttpCallService : IHttpCallService
    {
        public String Call(String method, String reqURL, String header, string param)
        {
            var request = (HttpWebRequest)WebRequest.Create(reqURL);
            request.Headers["Authorization"] = header;
            HttpWebResponse response;
            var responseString = "";
            try
            {
                if (method.Equals(Const.GET))
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                else
                {
                    var data = Encoding.ASCII.GetBytes(param);
                    request.Method = Const.POST;
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    response = (HttpWebResponse)request.GetResponse();
                }
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    int code = (int)((HttpWebResponse)e.Response).StatusCode;
                    using (var reader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        responseString = "["+code+"]" + reader.ReadToEnd();
                    }
                }
            }

            return responseString;
        }

        public String CallwithToken(String method, String reqURL, String access_Token)
        {
            return CallwithToken(method, reqURL, access_Token, null);
        }
        public String CallwithToken(String method, String reqURL, String access_Token, string param)
        {
            String header = "Bearer " + access_Token;
            return Call(method, reqURL, header, param);
        }
    }
}
