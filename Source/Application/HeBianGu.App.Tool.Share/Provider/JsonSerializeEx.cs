using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HeBianGu.App.Tool
{
    public static class JsonSerializeEx
    {
        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string SerializeJson<T>(this object target)
        {
            T result = (T)target;

            DataContractJsonSerializer json = new DataContractJsonSerializer(result.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, result);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 返序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T SerializeDeJson<T>(this string target)
        {
            if (string.IsNullOrEmpty(target)) return default(T);

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(target)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }

    }


}
