using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace lista4
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private string sciezka;
        public string idP;
        public Window3(string text)
        {
            string id = text;
            InitializeComponent();
            InitBinding(id);
        }

        private void InitBinding(string id)
        {
            try
            {
                using (var reader = new StreamReader("PersonList.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<MainWindow.Person>),
                        new XmlRootAttribute("ArrayOfPerson"));
                    MainWindow.m_oPersonList = (List<MainWindow.Person>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }
            MainWindow.Person oFoundPerson = MainWindow.m_oPersonList.Find(oElement => oElement.StudentId == Convert.ToInt32(id));
            imie.Text = oFoundPerson.FirstName;
            nazwisko.Text = oFoundPerson.LastName;
            wiek.Text = Convert.ToString(oFoundPerson.Age);
            Pesel.Text = Convert.ToString(oFoundPerson.Pesel);
            idP = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imie.IsEnabled = true;
            nazwisko.IsEnabled = true;
            wiek.IsEnabled = true;
            Pesel.IsEnabled = true;
            obrazek.IsEnabled = true;
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            MainWindow.Person oFoundPerson = MainWindow.m_oPersonList.Find(oElement => oElement.StudentId == Convert.ToInt32(idP));
            oFoundPerson.FirstName = imie.Text;
            oFoundPerson.LastName = nazwisko.Text;
            oFoundPerson.Age = Convert.ToInt32(wiek.Text);
            oFoundPerson.Pesel = Convert.ToInt64(Pesel.Text);
            MessageBox.Show("Edytowano dane"); 
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Filses(*.jpg; *.jpeg; *.gif; .bmp;)|.jpg; *.jpeg; *.gif; *.bmp; *.png;";

            if (openFileDialog.ShowDialog() == true)
            {
                sciezka = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                Picture.Source = new BitmapImage(fileUri);
            }
            MainWindow.Person oFoundPerson = MainWindow.m_oPersonList.Find(oElement => oElement.StudentId == Convert.ToInt32(idP));
            oFoundPerson.obraz = Convert.ToString(Picture.Source);
        }

        private void imie_preview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Z]").IsMatch(e.Text);
        }

        private void Nazwisko_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Z]").IsMatch(e.Text);
        }

        private void Wiek_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Pesel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
