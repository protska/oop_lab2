using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace oop_lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            File.Delete("manufactur.xml");
            File.Delete("product.xml");
            dateTimePicker1.MaxDate = DateTime.Today;

            buttonSave.Enabled = false;
/*            if(File.Exists("manufactur.xml") == true)
                buttonSave.Enabled = true;*/
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label10.Text = String.Format("Текущее значение: {0}", trackBar1.Value);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name;
            string id;
            decimal size;
            bool weight_1 = false;
            bool weight_2 = false;
            string type;
            string price;
            string date;
            int quantity;
            //string manufactur;
            try
            {
                name = textBox1.Text;
                id = textBox2.Text;
                if (numericUpDown1.Value == 0)
                    throw new Exception("Размер должен быть > 0");
                else
                    size = numericUpDown1.Value;
                if (radioButton1.Checked)
                    weight_1 = true;
                if (radioButton2.Checked)
                    weight_2 = true;
                type = listBox1.Text;
                price = textBox5.Text;
                date = dateTimePicker1.Text;
                if (trackBar1.Value == 0)
                    throw new Exception("Количество должен быть > 0");
                else
                    quantity = trackBar1.Value;

                Product product = new Product(name, id, size, weight_1, weight_2, type, price, date, quantity);

                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace((numericUpDown1.Value).ToString()) ||
                    string.IsNullOrWhiteSpace((radioButton1.Checked).ToString()) || string.IsNullOrWhiteSpace((radioButton2.Checked).ToString())
                    || string.IsNullOrWhiteSpace(listBox1.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
                    string.IsNullOrWhiteSpace((trackBar1.Value).ToString()) || trackBar1.Value == 0 || numericUpDown1.Value == 0)
                { MessageBox.Show($"Данные не записаны в файл \"product.xml\""); }
                else
                {
                    XmlSerializeWrapper.Serialize(product, "product.xml");
                    MessageBox.Show($"Данные записаны в файл \"product.xml\"");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }

        }

        private void buttonOutputInfo_Click(object sender, EventArgs e)
        {
            try
            {
                var deserializeM = XmlSerializeWrapper.Deserialize<Manufactur>("manufactur.xml");
                StringBuilder outputLine = new StringBuilder();
                if (string.IsNullOrWhiteSpace(textBox1.Text) != true && string.IsNullOrWhiteSpace(textBox2.Text) != true && string.IsNullOrWhiteSpace((numericUpDown1.Value).ToString()) != true 
                    && string.IsNullOrWhiteSpace((radioButton1.Checked).ToString()) != true && string.IsNullOrWhiteSpace((radioButton2.Checked).ToString()) != true
                    && string.IsNullOrWhiteSpace(listBox1.Text) != true && string.IsNullOrWhiteSpace(textBox5.Text) != true && string.IsNullOrWhiteSpace(dateTimePicker1.Text) != true
                    && string.IsNullOrWhiteSpace((trackBar1.Value).ToString()) != true && trackBar1.Value != 0 && numericUpDown1.Value != 0 && File.Exists("product.xml") == true)
                {
                    outputLine.AppendLine(label1.Text + ": " + textBox1.Text);
                    outputLine.AppendLine(label2.Text + ": " + textBox2.Text);
                    outputLine.AppendLine(label3.Text + ": " + numericUpDown1.Value);
                    bool isChecked = radioButton1.Checked;
                    if (isChecked)
                        outputLine.AppendLine(label4.Text + ": " + radioButton1.Text);
                    else
                        outputLine.AppendLine(label4.Text + ": " + radioButton2.Text);
                    outputLine.AppendLine(label5.Text + ": " + listBox1.Text);
                    outputLine.AppendLine(label8.Text + ": " + textBox5.Text);
                    outputLine.AppendLine(label6.Text + ": " + dateTimePicker1.Text);
                    outputLine.AppendLine(label7.Text + ": " + trackBar1.Value);
                    //outputLine.AppendLine(label9.Text + ": " + listBox2.Text);
                    outputLine.AppendLine($"Организация: {deserializeM.Name}");

                    textBox4.Text = outputLine.ToString();
                }
                else MessageBox.Show("Пусто!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 manufactur = new Form2();
            manufactur.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //значение символа, который будет нажат
            string Symbol = e.KeyChar.ToString();
            if (!Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]").Success && e.KeyChar != 8 && e.KeyChar != 32)
            {
                //событие обработано
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();
            if (!Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]").Success && !Regex.Match(Symbol, @"[0-9]").Success && e.KeyChar != 8 && e.KeyChar != 32)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            string Symbol = e.KeyChar.ToString();
            if (!Regex.Match(Symbol, @"[0-9]").Success && e.KeyChar != 8  && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == ',') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var deserializeM = XmlSerializeWrapper.Deserialize<Manufactur>("manufactur.xml");
                StringBuilder manufacturInfo = new StringBuilder();
                manufacturInfo.AppendLine($"Организация: {deserializeM.Name}");
                manufacturInfo.AppendLine($"Адрес: {deserializeM.Address}");
                manufacturInfo.AppendLine($"Страна: {deserializeM.Country}");
                manufacturInfo.AppendLine($"Телефон: {deserializeM.Phone}");
                textBox6.Text = manufacturInfo.ToString();
                buttonSave.Enabled = true;
            }
            //catch(Exception ex)
            catch
            {
                //MessageBox.Show($"{ex.Message}");
                MessageBox.Show($"Пусто!");
            }
            //buttonSave.Enabled = true;
        }

    }
}