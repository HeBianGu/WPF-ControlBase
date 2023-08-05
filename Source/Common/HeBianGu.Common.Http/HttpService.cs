using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Common.Http
{
    /// <summary>
    /// 通过post获取数据帮助类
    /// </summary>
    public class HttpHelper : IHttpHelper
    {
        private CookieContainer cookieContainer;

        private string Token = "";

        private Dictionary<string, Cookie> cacheCookies = new Dictionary<string, Cookie>();

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <param name="jsonResult"></param>
        /// <returns></returns>
        public bool Login(string url, IDictionary<string, string> paras, out JContainer jsonResult, out string error)
        {
            bool re = false;
            error = "";
            string result = "";
            cookieContainer = new CookieContainer();
            jsonResult = null;
            HttpWebRequest request = null;
            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
                request.Accept = "*/*";
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version10;
                request.CookieContainer = cookieContainer;
                request.Timeout = 30000;


                if (paras != null && paras.Count > 0)
                {
                    StringBuilder buffer = new StringBuilder();
                    foreach (string key in paras.Keys)
                    {
                        if (buffer.Length == 0) buffer.AppendFormat("{0}={1}", key, paras[key]);

                        else buffer.AppendFormat("&{0}={1}", key, paras[key]);

                    }
                    byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                using (HttpWebResponse wr = request.GetResponse() as HttpWebResponse)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(wr.GetResponseStream()))
                    {
                        foreach (Cookie ck in wr.Cookies) cookieContainer.Add(ck);
                        result = sr.ReadToEnd();
                        re = true;
                        sr.Close();
                    }
                    wr.Close();
                }

                try
                {
                    jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as JContainer;
                }
                catch (Exception)
                {
                    re = false;
                    //LogLib.CCLog.WriteLog(string.Format("url:{0}返回无效json字符串:{1}", url, result));
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (request != null) request.Abort();
            }

            return re;
        }

        /// <summary>
        /// 通过post获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <param name="jsonresult"></param>
        /// <param name="errorInfor">如果发生错误，则返回错误信息</param>
        /// <returns></returns>
        public bool PostData(string url, IDictionary<string, string> paras, out JContainer jsonResult, out string errorInfor, bool isJson = false)
        {
            bool re = false;
            jsonResult = null;
            HttpWebRequest request = null;
            string result = "";
            errorInfor = "";
            try
            {
                request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
                request.Accept = "*/*";
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version10;
                request.CookieContainer = cookieContainer;
                request.Timeout = 30000;
                request.Headers.Add("Token", Token);

                if (paras != null && paras.Count > 0)
                {
                    if (isJson == true)
                    {
                        string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(paras);
                        byte[] data = Encoding.UTF8.GetBytes(jsonStr.ToString());
                        using (System.IO.Stream stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                    else
                    {
                        StringBuilder buffer = new StringBuilder();
                        foreach (string key in paras.Keys)
                        {
                            if (buffer.Length == 0)
                            {
                                buffer.AppendFormat("{0}={1}", key, paras[key]);
                            }
                            else
                            {
                                buffer.AppendFormat("&{0}={1}", key, paras[key]);
                            }
                        }
                        byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                        using (System.IO.Stream stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }

                using (HttpWebResponse wr = request.GetResponse() as HttpWebResponse)
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(wr.GetResponseStream()))
                    {
                        foreach (Cookie c in wr.Cookies)
                        {
                            if (c.Expired) cacheCookies.Remove(c.Name);
                            else cacheCookies[c.Name] = c;
                        }
                        result = sr.ReadToEnd();
                        re = true;
                        sr.Close();
                    }
                    wr.Close();
                }

                try
                {
                    jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(result) as Newtonsoft.Json.Linq.JContainer;
                }
                catch (Exception)
                {
                    re = false;
                    errorInfor = string.Format("url:{0}返回无效json字符串:{1}", url, result);
                }
            }
            catch (Exception ex)
            {
                errorInfor = ex.Message.ToString();
            }
            finally
            {
                if (request != null) request.Abort();
            }
            return re;
        }

        /// <summary>
        /// 通过post上传
        /// </summary>
        public JContainer Post(string Url, string jsonParas, out string error)
        {
            error = "";
            try
            {
                string strURL = Url;
                HttpWebRequest request;
                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                request.CookieContainer = cookieContainer;
                request.Timeout = 30000;
                request.Headers.Add("Token", Token);
                string paraUrlCoded = jsonParas;
                byte[] payload;
                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
                request.ContentLength = payload.Length;
                Stream writer = request.GetRequestStream();
                writer.Write(payload, 0, payload.Length);
                writer.Close();
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate + "\r\n";
                }
                return Newtonsoft.Json.JsonConvert.DeserializeObject(strValue) as JContainer;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 设置Token
        /// </summary>
        public void SetToken(string token)
        {
            this.Token = token;
        }

        #region - Post方式 -

        /// <summary>
        /// 通过POST方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">Post数据</param>
        /// <param name="cookie">Cookie容器</param>
        /// <returns></returns>
        public string Post(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        #endregion

        #region - Get方式 -

        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        public JContainer GET(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return Newtonsoft.Json.JsonConvert.DeserializeObject(retString) as JContainer;
        }

        #endregion


        #region - 下载 -

        /// <summary> 下载文件 </summary>        
        /// <param name="URL">下载文件地址</param>  
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public static void Download(string URL, string filename, Action<string, string> percentAction = null, int refreshTime = 1000)
        {
            float percent = 0;
            int total = 0;
            int current = 0;
            HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
            Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
            //Myrq.Headers.Add("Token", Token);
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();

            long totalBytes = myrp.ContentLength;
            total = (int)totalBytes;
            Stream st = myrp.GetResponseStream();
            Stream so = new FileStream(filename, FileMode.Create);

            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];

            int osize = st.Read(by, 0, (int)by.Length);


            // Todo ：定时刷新进度 
            if (percentAction != null)
            {
                Action action = () =>
                {
                    while (true)
                    {
                        Thread.Sleep(refreshTime);

                        // Todo ：返回进度 
                        percentAction(current.ToString(), total.ToString());

                        if (current == total) break;
                    }
                };

                Task task = new Task(action);
                task.Start();
            }


            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                current = (int)totalDownloadedByte;

                osize = st.Read(by, 0, (int)by.Length);

                percent = (float)totalDownloadedByte / (float)totalBytes * 100;
            }
            so.Close();
            st.Close();
        }

        #endregion
    }
}
