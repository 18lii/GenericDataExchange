using System;
using System.Text;

namespace Database.Helper
{
    public class ExceptionMessage
    {
        /// <summary>
        ///  用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        ///  直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExMessage { get; private set; }

        /// <summary>
        ///  异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string ErrorDetails { get; private set; }
        public ExceptionMessage(Exception e, string userMessage = null, bool isHideStackTrace = false)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? e.Message : userMessage;

            var sb = new StringBuilder();
            ExMessage = string.Empty;
            int count = 0;
            string appString = "";
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                ExMessage = e.Message;
                sb.AppendLine(appString + "异常消息：" + e.Message);
                sb.AppendLine(appString + "异常类型：" + e.GetType().FullName);
                sb.AppendLine(appString + "异常方法：" + (e.TargetSite?.Name));
                sb.AppendLine(appString + "异常源：" + e.Source);
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(appString + "异常堆栈：" + e.StackTrace);
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(appString + "内部异常：");
                    count++;
                }
                e = e.InnerException;
            }
            ErrorDetails = sb.ToString();
            sb.Clear();
        }
    }
}
