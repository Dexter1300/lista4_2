using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lista4
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private string sciezka = "";

        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            MainWindow.m_oPersonList.Add(new MainWindow.Person() { StudentId = MainWindow.m_oPersonList.Count + 1, FirstName = imie.Text, LastName = nazwisko.Text, Age = Convert.ToInt16(wiek.Text), Pesel = Convert.ToInt64(Pesel.Text), obraz = sciezka });
           
            MessageBox.Show("Dodano nową osobę");
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
        }


    }
}
