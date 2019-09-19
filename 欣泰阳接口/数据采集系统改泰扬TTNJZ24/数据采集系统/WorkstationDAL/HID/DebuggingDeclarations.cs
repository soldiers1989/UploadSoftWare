using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WorkstationDAL.HID
{
    public class DebuggingDeclarations
    {
        public const Int16 FORMAT_MESSAGE_FROM_SYSTEM = 0X1000;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 FormatMessage(Int32 dwFlags, ref Int64 lpSource, Int32 dwMessageId, Int32 dwLanguageZId, String lpBuffer, Int32 nSize, Int32 Arguments);        
    }
}
