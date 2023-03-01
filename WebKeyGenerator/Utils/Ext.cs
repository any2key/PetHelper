using MailKit.Net.Smtp;
using MimeKit;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace WebKeyGenerator.Utils
{
    public static class Ext
    {


        public static string RandomPassword(int length=8)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
        public static string ToXML<T>(this T instance)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, instance);
                ms.Position = 0;
                TextReader reader = new StreamReader(ms);
                return reader.ReadToEnd();
            }
        }
        public static T FromXml<T>(this string xml) where T : new()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(xml))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                return new T();
            }
        }


        public static string ToMd5(this string data)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(data.ToLower());
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes);
            }
        }

        /// <summary>
        /// Сериализует объект в JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// Десериализует JSON в объект указанного типа 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }


       

    }

    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class UniqueKeyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Marker attribute for unique key
        /// </summary>
        /// <param name="groupId">Optional, used to group multiple entity properties together into a combined Unique Key</param>
        /// <param name="order">Optional, used to order the entity properties that are part of a combined Unique Key</param>
        public UniqueKeyAttribute(string groupId = null, int order = 0)
        {
            GroupId = groupId;
            Order = order;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // we simply return success as no actual data validation is needed because this class implements a "marker attribute" for "create a unique index"
            return ValidationResult.Success;
        }

        public string GroupId { get; set; }
        public int Order { get; set; }
    }




    public static class Email
    {

        public static void Send(IConfiguration config, string msg, string to = "writeto@any2key.ru")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Pet helper", config["Smtp:login"]));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = $"Обращение от администрации Pet Helper";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            message.Body = new TextPart("plain")
            {
                Text = msg
            };

            using (var client = new SmtpClient())
            {
                client.Timeout = 100000;
                client.Connect(config["Smtp:server"], int.Parse(config["Smtp:port"]), bool.Parse(config["Smtp:useSSL"]));
                client.Authenticate(config["Smtp:login"], config["Smtp:password"]);
                var res = client.Send(message);

                client.Disconnect(true);
            }
        }

    }

}
