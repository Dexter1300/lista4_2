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
        public static List<Student> m_oPersonList = new List<Student>();

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
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Student>),
                        new XmlRootAttribute("ArrayOfPerson"));
                    m_oPersonList = (List<Student>)deserializer.Deserialize(reader);
                }
            }
            catch
            {
                MessageBox.Show("Brak pliku do załadowania!", "Uwaga", MessageBoxButton.OK);
            }

            if (m_oPersonList.Count == 0)
            {
                m_oPersonList.Add(new Student(1, "Jan", "Kowalski", 25, 9909090, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
                m_oPersonList.Add(new Student(2, "Adam", "Nowak", 24, 899898030, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
                m_oPersonList.Add(new Student(3, "Agnieszka", "Kowalczyk", 20, 0032329309, "C:\\xampp\\htdocs\\pro\\img\\arrow-left.png"));
            }


            lstPersons.ItemsSource = m_oPersonList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            window2.Show();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3(id.Text);
            win3.Show();
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            m_oPersonList = null;
            lstPersons.ItemsSource = m_oPersonList;
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            var serializer = new XmlSerializer(m_oPersonList.GetType());
            using (var writer = XmlWriter.Create("PersonList.xml"))
            {
                serializer.Serialize(writer, m_oPersonList);
            }
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            lstPersons.ItemsSource = m_oPersonList;
        }

        public class Student
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

            public Student(int nStudentId, string sFirstName, string sLastName, int nAge, long lPesel, string Obraz)
            {
                StudentId = nStudentId;
                FirstName = sFirstName;
                LastName = sLastName;
                Age = nAge;
                Pesel = lPesel;
                obraz = Obraz;
            }

            public Student()
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
