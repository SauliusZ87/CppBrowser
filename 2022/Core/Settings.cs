using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CppBrowser.Core
{
    public partial class Settings : Form
    {
        private State m_state;

        public Settings(State state)
        {
            InitializeComponent();

            m_state = state;

            InitializeDataGrid();
            InitializeBackColors();

            checkCaseSensitiveFind.Checked = m_state.CaseSensitiveFind;
            checkCurrentItem.Checked = m_state.SelectCurrentItem;
            checkFindOnlyName.Checked = m_state.FindOnlyName;
            fontBox.Font = m_state.Font;
            fontBox.Text = m_state.Font.Name;

            FillCombo();
        }

        private void FillCombo()
        {
            comboPosition.Items.Add("Mouse");
            comboPosition.Items.Add("Center screen");
            comboPosition.Items.Add("User defined");

            comboPosition.SelectedIndex = Convert.ToInt32(m_state.WindowLocation);

            editX.Text = m_state.WindowPos.X.ToString();
            editY.Text = m_state.WindowPos.Y.ToString();
        }

        private void InitializeBackColors()
        {
            backColor1.BackColor = m_state.BackColor1;
            backColor2.BackColor = m_state.BackColor2;
            selectionColor.BackColor = m_state.SelectionColor;
            findColor.BackColor = m_state.FindColor;
        }

        private void InitializeDataGrid()
        {
            dataGridView1.RowCount = m_state.ItemsInfo.Count;

            int nCounter = 0;
            foreach(KeyValuePair<Element.IdentifiersIDs, KeyValuePair<bool, Color>> pair in m_state.ItemsInfo)
            {
                dataGridView1.Rows[nCounter].Cells[0].Value = pair.Value.Key;
                dataGridView1.Rows[nCounter].Cells[1].Value = Element.GetIdentifierString(pair.Key);
                dataGridView1.Rows[nCounter].Cells[2].Style.BackColor = pair.Value.Value;
                ++nCounter;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (2 == e.ColumnIndex)
            {
                ColorDialog dialog =  new ColorDialog();
                dialog.Color = dataGridView1.Rows[e.RowIndex].Cells[2].Style.BackColor;
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    dataGridView1.Rows[e.RowIndex].Cells[2].Style.BackColor = dialog.Color;
                }
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.OK == this.DialogResult)
            {
                m_state.BackColor1 = backColor1.BackColor;
                m_state.BackColor2 = backColor2.BackColor;
                m_state.SelectionColor = selectionColor.BackColor;
                m_state.FindColor = findColor.BackColor;
                m_state.Font = fontBox.Font;

                m_state.CaseSensitiveFind = checkCaseSensitiveFind.Checked;
                m_state.SelectCurrentItem = checkCurrentItem.Checked;
                m_state.FindOnlyName = checkFindOnlyName.Checked;

                m_state.WindowLocation = (ListBoxEx.ListBoxLocation)comboPosition.SelectedIndex;
                m_state.WindowPos = new Point(Convert.ToInt32(editX.Text), Convert.ToInt32(editY.Text));

                if (m_state.ItemsInfo.Count == dataGridView1.RowCount)
                {
                    Dictionary<string, Element.IdentifiersIDs> dict = Element.GetDictionaryOfCodeItems();

                    m_state.ItemsInfo.Clear();

                    for (int i = 0; i < dataGridView1.RowCount; ++i)
                    {
                        string name = (string)dataGridView1.Rows[i].Cells[1].Value;
                        if(0 < name.Length)
                        {
                            Element.IdentifiersIDs id = dict[name];
                            bool bEnabled = (bool)dataGridView1.Rows[i].Cells[0].Value;
                            Color color = dataGridView1.Rows[i].Cells[2].Style.BackColor;
                            m_state.ItemsInfo.Add(id, new KeyValuePair<bool, Color>(bEnabled, color));
                        }
                    }
                }
            }
        }

        private void resetColors_Click(object sender, EventArgs e)
        {
            backColor1.BackColor = State.defaultBackColor1;
            backColor2.BackColor = State.defaultBackColor2;
            selectionColor.BackColor = State.defaultSelectionColor;
            findColor.BackColor = State.defaultFindColor;
            fontBox.Font = State.defaultFont;
        }

        private int RGBtoBGR(int RGB)
        {
            return ((RGB & 0x000000ff) << 16 | (RGB & 0x0000FF00) | (RGB & 0x00FF0000) >> 16);
        }

        private Color ShowColorDialog(Color color)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.CustomColors = new int[] { RGBtoBGR(backColor1.BackColor.ToArgb()), RGBtoBGR(backColor2.BackColor.ToArgb()), RGBtoBGR(selectionColor.BackColor.ToArgb()), RGBtoBGR(findColor.BackColor.ToArgb()) };
            dialog.Color = color;
            if (DialogResult.OK == dialog.ShowDialog())
                color = dialog.Color;

            return color;
        }

        private void backColor1_Click(object sender, EventArgs e)
        {
            backColor1.BackColor = ShowColorDialog(backColor1.BackColor);
        }

        private void backColor2_Click(object sender, EventArgs e)
        {
            backColor2.BackColor = ShowColorDialog(backColor2.BackColor);
        }

        private void selectedColor_Click(object sender, EventArgs e)
        {
            selectionColor.BackColor = ShowColorDialog(selectionColor.BackColor);
        }

        private void findColor_Click(object sender, EventArgs e)
        {
            findColor.BackColor = ShowColorDialog(findColor.BackColor);
        }

        private void backColor1_Enter(object sender, EventArgs e)
        {
            backColor1.Enabled = false;
            backColor1.Enabled = true;
        }

        private void backColor2_Enter(object sender, EventArgs e)
        {
            backColor2.Enabled = false;
            backColor2.Enabled = true;
        }

        private void selectedColor_Enter(object sender, EventArgs e)
        {
            backColor2.Enabled = false;
            backColor2.Enabled = true;
        }

        private void findColor_Enter(object sender, EventArgs e)
        {
            findColor.Enabled = false;
            findColor.Enabled = true;
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog dialog = new FontDialog();
            dialog.Font = m_state.Font;
            if (DialogResult.OK == dialog.ShowDialog())
            {
                fontBox.Font = dialog.Font;
                fontBox.Text = fontBox.Font.Name;
            }
        }

        private void comboPosition_SelectedValueChanged(object sender, EventArgs e)
        {
            bool bEnabled = Convert.ToInt32(ListBoxEx.ListBoxLocation.Manual) == comboPosition.SelectedIndex;

            labelX.Visible = bEnabled;
            labelX.Enabled = bEnabled;

            labelY.Visible = bEnabled;
            labelY.Enabled = bEnabled;

            editY.Visible = bEnabled;
            editY.Enabled = bEnabled;

            editX.Visible = bEnabled;
            editX.Enabled = bEnabled;
        }
    }
}
