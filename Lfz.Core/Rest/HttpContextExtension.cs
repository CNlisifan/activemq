using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using Lfz.Logging;
using Lfz.Utitlies;

namespace Lfz.Rest
{
    /// <summary>
    /// HttpContext ��չ��Ϣ 
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// ����context.Request������ת��UTF8�ַ���
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string ReadAsString(this HttpContext context)
        {
            string s = string.Empty;
            using (var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
            {
                s = reader.ReadToEnd();
                LoggerFactory.GetLog().Log(LogLevel.Information, "Recevice��" + s);
            }
            return s;
        }


        /// <summary>
        ///  ��ȡ HTTP ��ѯ�ַ�����������
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static NameValueCollection QueryString(this HttpContext context)
        {
            return context.Request.QueryString;
        }

        /// <summary>
        /// ���ݼ�ֵ��HTTP��ѯ�ַ������������л�ȡIntֵ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetIntFromQueryString(this HttpContext context, string key)
        {
            return TypeParse.StrToInt(context.Request.QueryString[key]);
        }

        /// <summary>
        /// ���ݼ�ֵ��HTTP��ѯ�ַ������������л�ȡ�ַ���ֵ
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueFromQueryString(this HttpContext context, string key)
        {
            return context.Request.QueryString[key];
        }
    }
}