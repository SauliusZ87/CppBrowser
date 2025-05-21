using System;
using System.Drawing;
using System.Windows.Forms;

namespace CppBrowser.Core
{
    public class ToolStripSpringTextBox : ToolStripTextBox
    {
        public delegate void KeyNotification(object sender);
        public delegate void ArrowKeyNotification(object sender, Keys keyData);
        public delegate void AltNumNotification(object sender, Keys keyData);

        public event AltNumNotification AltNumberPressed;
        public event KeyNotification EscapeKeyPressed;
        public event KeyNotification EnterKeyPressed;
        public event ArrowKeyNotification ArrowKeyPressed;

        protected override Boolean ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch ((Keys.KeyCode & keyData))
            {
                case Keys.Escape:
                    if (null != EscapeKeyPressed)
                        EscapeKeyPressed(this);
                    return true;
                case Keys.Enter:
                    if (null != EnterKeyPressed)
                        EnterKeyPressed(this);
                    return true;
                case Keys.Up:
                case Keys.Down:
                case Keys.End:
                case Keys.Home:
                    if (null != ArrowKeyPressed)
                        ArrowKeyPressed(this, keyData);
                    return true;
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    if (Keys.Alt == (keyData & Keys.Modifiers))
                    {
                        if (null != AltNumberPressed)
                            AltNumberPressed(this, (keyData & Keys.KeyCode));
                        return true;
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            // Use the default size if the text box is on the overflow menu
            // or is on a vertical ToolStrip.
            if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
            {
                return DefaultSize;
            }

            // Declare a variable to store the total available width as 
            // it is calculated, starting with the display width of the 
            // owning ToolStrip.
            Int32 width = Owner.DisplayRectangle.Width;

            // Subtract the width of the overflow button if it is displayed. 
            if (Owner.OverflowButton.Visible)
            {
                width = width - Owner.OverflowButton.Width -
                    Owner.OverflowButton.Margin.Horizontal;
            }

            // Declare a variable to maintain a count of ToolStripSpringTextBox 
            // items currently displayed in the owning ToolStrip. 
            Int32 springBoxCount = 0;

            foreach (ToolStripItem item in Owner.Items)
            {
                // Ignore items on the overflow menu.
                if (item.IsOnOverflow) continue;

                if (item is ToolStripSpringTextBox)
                {
                    // For ToolStripSpringTextBox items, increment the count and 
                    // subtract the margin width from the total available width.
                    springBoxCount++;
                    width -= item.Margin.Horizontal;
                }
                else
                {
                    // For all other items, subtract the full width from the total
                    // available width.
                    width = width - item.Width - item.Margin.Horizontal;
                }
            }

            // If there are multiple ToolStripSpringTextBox items in the owning
            // ToolStrip, divide the total available width between them. 
            if (springBoxCount > 1) width /= springBoxCount;

            // If the available width is less than the default width, use the
            // default width, forcing one or more items onto the overflow menu.
            if (width < DefaultSize.Width) width = DefaultSize.Width;

            // Retrieve the preferred size from the base class, but change the
            // width to the calculated width. 
            Size size = base.GetPreferredSize(constrainingSize);
            size.Width = width;
            return size;
        }
    }
}
