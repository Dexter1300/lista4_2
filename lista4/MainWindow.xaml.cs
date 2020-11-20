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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace lista4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Person> m_oPersonList = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
            InitBinding();
        }

        private void InitBinding()
        {
            try
            {
                using (var reader = new StreamReader("PersonList.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Person>),
                        new XmlRootAttribute("ArrayOfPerson"));
                    m_oPersonList = (List<Person>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
               MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }

            if (m_oPersonList.Count == 0)
            {
                m_oPersonList.Add(new Person(1, "Jan", "Kowalski", 25, 9909090, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
                m_oPersonList.Add(new Person(2, "Adam", "Nowak", 24, 899898030, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
                m_oPersonList.Add(new Person(3, "Agnieszka", "Kowalczyk", 20, 0032329309, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
            }


            lstPersons.ItemsSource = m_oPersonList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 win2 = new Window2();
            win2.Show();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3(id.Text);
            win3.Show();
        }

        //Refresh
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            lstPersons.ItemsSource = null;
            lstPersons.ItemsSource = m_oPersonList;
        }

        //Save
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlSerializer(m_oPersonList.GetType());
            using (var writer = XmlWriter.Create("PersonList.xml"))
            {
                serializer.Serialize(writer, m_oPersonList);
            }
        }

        //Load
        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            InitBinding();
        }

        public class Person
        {
            [XmlAttribute("id")]
            public int StudentId { get; set; }

            [XmlAttribute("imie")]
            public string FirstName { get; set; }

            [XmlAttribute("nazwisko")]
            public string LastName { get; set; }

            [XmlAttribute("wiek")]
            public int Age { get; set; }

            [XmlAttribute("pesel")]
            public long Pesel { get; set; }

            [XmlElement("Obraz")]
            public string obraz { get; set; }

            public Person(int nStudentId, string sFirstName, string sLastName, int nAge, long lPesel, string Obraz)
            {
                StudentId = nStudentId;
                FirstName = sFirstName;
                LastName = sLastName;
                Age = nAge;
                Pesel = lPesel;
                obraz = Obraz;
            }

            public Person()
            {
                StudentId = 0;
                FirstName = "Janusz";
                LastName = "Januszewski";
                Age = 120;
                Pesel = 999909090;
                obraz = "image";
            }
        }
    }
}
