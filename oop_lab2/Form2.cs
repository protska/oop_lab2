using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace oop_lab2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            { 
            Manufactur manufactur = new Manufactur();
                //if (listBox3.Text == null)
                if(string.IsNullOrWhiteSpace(listBox3.Text) == true)
                    throw new Exception("Выберите производителя");
                else
                    manufactur.Name = listBox3.Text;

                manufactur.Address = comboBox1.Text;

                if (treeView1.SelectedNode == null)
                //if(string.IsNullOrEmpty(treeView1.SelectedNode.Text) == true)
                    throw new Exception("Выберите страну");
                else
                manufactur.Country = treeView1.SelectedNode.Text;

                manufactur.Phone = maskedTextBox1.Text;


                //XmlSerializeWrapper.Serialize(manufactur, "manufactur.xml");

                //string.IsNullOrWhiteSpace(textBox1.Text)

                //if (treeView1.SelectedNode != null && comboBox1.Text != null && listBox3.Text != null && maskedTextBox1.Text != null)
                if (string.IsNullOrWhiteSpace(listBox3.Text) != true && string.IsNullOrWhiteSpace(treeView1.SelectedNode.Text) != true 
                    && string.IsNullOrWhiteSpace(comboBox1.Text) != true && /*string.IsNullOrEmpty(maskedTextBox1.Text) != true*/ maskedTextBox1.MaskFull == true)
                {
                    XmlSerializeWrapper.Serialize(manufactur, "manufactur.xml");
                    MessageBox.Show($"Данные записаны в файл \"manufactur.xml\"");
                }
                else { MessageBox.Show($"Данные не записаны в файл \"manufactur.xml\""); }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }


/*            if (treeView1.SelectedNode != null && comboBox1.Text != null && listBox3.Text != null && maskedTextBox1.Text != null)
            {
                XmlSerializeWrapper.Serialize(manufactur, "manufactur.xml");
                MessageBox.Show($"Данные записаны в файл \"manufactur.xml\"");
            }
            else { MessageBox.Show($"Данные не записаны в файл \"manufactur.xml\""); }*/

        }
    }
}
