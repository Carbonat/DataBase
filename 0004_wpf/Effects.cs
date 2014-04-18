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
                tb.Foreground = Brushes.Gray;
                tb.Text = defaultValue;
            }
        }
        public void TextBoxTextChanged(TextBox tb, Label lb, int maxLength)
        {
            if (lb != null)
            {
                if (tb.Foreground == Brushes.Gray)
                {
                    lb.Content = maxLength;
                }
                else
                {
                    string str = tb.Text;
                    lb.Content = maxLength - str.Length;
                }
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
        public void DatePickerSelectedDateChanged(DatePicker dp)
        {
            if (!CheckSelectedDate(dp))
                dp.SelectedDate = DateTime.Today;
        }
       
        public void DatePickerSelectedDateChanged(DatePicker dp, DateTime dateStart)
        {
            if (!CheckSelectedDate(dp, dateStart))
                dp.SelectedDate = DateTime.Today;
            dp.DisplayDateStart = dateStart;
        }
        private bool CheckSelectedDate(DatePicker dp)
        {
            DateTime? _date = dp.SelectedDate;
            if (_date == null || _date > DateTime.Today)
                return false;
            return true;
        }
        private bool CheckSelectedDate(DatePicker dp, DateTime dateStart)
        {
            DateTime? _date = dp.SelectedDate;
            if (_date == null || _date > DateTime.Today || _date < dateStart)
                return false;
            return true;
        }
    }
}
