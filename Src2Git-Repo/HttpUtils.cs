using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Src2Git_Repo
{
    class HttpUtils
    {
        /// <summary>
        /// 通过HTTP协议获取网点的Jsion返回
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string Get(string Url, Dictionary<string, string> header)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                foreach (KeyValuePair<string, string> item in header)
                {
                    request.Headers.Set(item.Key, item.Value);
                }

                //获取api的返回
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                //关闭非托管流和资源
                myStreamReader.Close();
                myResponseStream.Close();
                if (response != null)
                    response.Close();
                if (request != null)
                    request.Abort();

                return retString;
            }
            catch (Exception ex)
            {
                //记录日志 获取接口异常：ex
                Output.WriteLine(ex.ToString());
                return "";
            }

        }

        public static string Post(string Url, string Data, string Referer, Dictionary<string, string> header)
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Referer = Referer;
                byte[] bytes = Encoding.UTF8.GetBytes(Data);
                request.ContentType = "application/json; charset=UTF-8";
                request.ContentLength = bytes.Length;
                foreach (KeyValuePair<string, string> item in header)
                {
                    request.Headers.Set(item.Key, item.Value);
                }

                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
                return retString;
            }
            catch (Exception ex)
            {
                //记录日志 获取接口异常：ex
                Output.WriteLine(ex.ToString());
                return "";
            }
        }
    }
}
