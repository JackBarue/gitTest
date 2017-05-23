using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StRttmy.Model;

namespace StRttmy.Common
{
    /// <summary>
    /// 超级管理员用户名处理工具类
    /// </summary>
    public class SuperNameUtility
    {
        /// <summary>
        /// 对超级管理员用户名进行加密操作
        /// </summary>
        /// <param name="superName">超级管理员原始用户名</param>
        /// <returns>加密结果</returns>
        public static String encodeSuperName(string superName)
        {
            byte[] encodeBy = Encoding.Default.GetBytes(superName);
            string encodeName = Convert.ToBase64String(encodeBy);
            return encodeName;
        }

        /// <summary>
        /// 对超级管理员用户名进行解密操作
        /// </summary>
        /// <param name="encodeName">被加密过后的用户名</param>
        /// <returns>原始用户名</returns>
        public static string decodeSuperName(string encodeName)
        {
            byte[] decodeBy = Convert.FromBase64String(encodeName);
            string decodeName = Encoding.Default.GetString(decodeBy);
            return decodeName;
        }

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <param name="loginx">登录模型对象</param>
        /// <returns></returns>
        public static bool isSuper(UserLogin loginx)
        {
            bool flag = false;
            flag = (loginx.LoginName == "super") ? true : false;

            return flag;

        }
    }
}
