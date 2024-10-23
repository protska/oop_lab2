using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace oop_lab2
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    //атрибут позволяет классу или структуре быть сериализуемыми
    [Serializable]
    [XmlRoot(Namespace = "oop_lab2")]
    [XmlType("product")]
    public class Product
    {
        public Product(string name, string id, decimal size, bool weight_1, bool weight_2, string type,
            string price, string date, int quantity)
        {
            Name = name;
            Id = id;
            Size = size;
            Weight_1 = weight_1;
            Weight_2 = weight_2;
            Type = type;
            Price = price;
            Date = date;
            Quantity = quantity;
            //Manufactur = manufactur;
        }

        public Product() { }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }


        [XmlElement(ElementName = "size")]
        public decimal Size { get; set; }

        [XmlElement(ElementName = "weight_1")]
        public bool Weight_1 { get; set; }

        [XmlElement(ElementName = "weight_2")]
        public bool Weight_2 { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "price")]
        public string Price { get; set; }

        [XmlElement(ElementName = "date")]
        public string Date { get; set; }

        [XmlElement(ElementName = "quantity")]
        public int Quantity { get; set; }

/*        [XmlElement(ElementName = "manufacturer")]
        public string Manufactur { get; set; }*/
    }
    //
    [Serializable]
    public class Manufactur
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "address")]
        public string Address { get; set; }
        [XmlElement(ElementName = "country")]
        public string Country { get; set; }
        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }
    }
    //


    public static class XmlSerializeWrapper
    {
        public static void Serialize<T>(T obj, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }
        public static T Deserialize<T>(string filename)
        {
            T obj;
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = (T)formatter.Deserialize(fs);
            }
            return obj;
        }
    }
}
