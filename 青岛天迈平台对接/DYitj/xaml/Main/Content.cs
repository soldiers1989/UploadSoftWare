using System;
using System.Windows;

namespace AIO
{
    public partial class MainWindow : Window
    {
        [Serializable]
        public class Content
        {
            public string Address { get; set; }
            public Address_Detail Address_Detail { get; set; }
            public Point Point { get; set; }
        }

    }
}