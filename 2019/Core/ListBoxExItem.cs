using System.Drawing;

namespace CppBrowser.Core
{
    class ListBoxExItem
    {
        public ListBoxExItem(string text, bool display, Color color, int index)
        {
            Text = text;
            Display = display;
            Color = color;
            Index = index;
        }

        public override string ToString()
        {
            return Text;
        }

        public string Text { get; set; }
        public bool Display { get; set; }
        public Color Color { get; set; }
        public int Index { get; set; }
    }
}
