using System;
using System.Windows.Documents;

namespace WpfPrintDemo
{
    public interface IDocumentRenderer
    {
        void Render(FlowDocument doc, Object data);
    }
}