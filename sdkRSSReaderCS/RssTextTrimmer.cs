/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.Net;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace sdkRSSReaderCS
{
    public class RssTextTrimmer : IValueConverter
    {
     
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            int maxLength = 200;
            int strLength = 0;
            string fixedString = "";

  
            fixedString = Regex.Replace(value.ToString(), "<[^>]+>", string.Empty);

     
            fixedString = fixedString.Replace("\r", "").Replace("\n", "");

   
            fixedString = HttpUtility.HtmlDecode(fixedString);

            strLength = fixedString.ToString().Length;

            if (strLength == 0)
            {
                return null;
            }

            else if (strLength >= maxLength)
            {
                fixedString = fixedString.Substring(0, maxLength);

                fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "));
            }

            fixedString += "...";

            return fixedString;
        }

  
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
