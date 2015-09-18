using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Exceler
{
    /// <summary>
    /// 常用Helper
    /// </summary>
    public partial class Common
    {
        /// <summary>
        ///  替换特殊字符
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string ReplaceSpecialChars(string input)
        {
            // space 	-> 	_x0020_
            // %		-> 	_x0025_
            // #		->	_x0023_
            // &		->	_x0026_
            // /		->	_x002F_

            input = input.Replace(" ", "_x0020_")
                .Replace("%", "_x0025_")
                .Replace("#", "_x0023_")
                .Replace("&", "_x0026_")
                .Replace("/", "_x002F_");

            return input;
        }
    }
}
