using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;

namespace _0004_wpf
{
    class Effects
    {
        public void TextBoxGotFocus(TextBox tb, string defaultValue)
        {
            if (tb.Text.Equals(defaultValue, StringComparison.OrdinalIgnoreCase) && tb.Foreground == Brushes.Gray)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        public void TextBoxLostFocus(TextBox tb, string defaultValue)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = defaultValue;
                tb.Foreground = Brushes.Gray;
            }
        }

        public void RichTextBoxGotFocus(RichTextBox rtb, string defaultValue)
        {
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            if (tr.Text.Equals(defaultValue + "\r\n", StringComparison.OrdinalIgnoreCase) && rtb.Foreground == Brushes.Gray)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                paragraph.Inlines.Add(string.Empty);
                document.Blocks.Add(paragraph);

                rtb.Document = document;
                rtb.Foreground = Brushes.Black;
            }
        }

        public void RichTextBoxLostFocus(RichTextBox rtb, string defaultValue)
        {
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

            if (string.IsNullOrWhiteSpace(tr.Text))
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                paragraph.Inlines.Add(defaultValue);
                document.Blocks.Add(paragraph);

                rtb.Document = document;
                rtb.Foreground = Brushes.Gray;
            }
        }
    }
}
