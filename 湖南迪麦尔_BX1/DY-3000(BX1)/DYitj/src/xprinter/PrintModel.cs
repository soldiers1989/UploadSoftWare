using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AIO.src.xprinter
{
    public class PrintModel
    {
        public StringReader Text { get; set; }

        public string FontFamily { get; set; }

        public int FontSize { get; set; }

        public bool IsBold { get; set; }
    }
}
