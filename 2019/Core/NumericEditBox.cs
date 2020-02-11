using System;
using System.Globalization;

namespace CppBrowser.Core
{
    class NumericEditBox : System.Windows.Forms.TextBox
    {
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(negativeSign))
            {
                // Negative sign is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }
    }
}
