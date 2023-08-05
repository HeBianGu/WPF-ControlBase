using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace HeBianGu.Common.Http
{
    public interface IHttpHelper
    {
        JContainer GET(string Url, string postDataStr, ref CookieContainer cookie);
        JContainer Post(string Url, string jsonParas, out string error);
        string Post(string Url, string postDataStr, ref CookieContainer cookie);
        bool PostData(string url, IDictionary<string, string> paras, out JContainer jsonResult, out string errorInfor, bool isJson = false);
    }
}