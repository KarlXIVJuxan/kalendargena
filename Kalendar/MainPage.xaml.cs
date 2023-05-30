using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalendar
{
    public partial class MainPage : Page
    {
        List<UserView> users = new List<UserView>();
        public DateTime DATA;
        public MainPage(DateTime dATA)
        {
            InitializeComponent();
            DATA = dATA;
            Otobr(DATA);
        }
        public void MYLIST_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).PageFrame.Content = new ViborPage(MYLIST.SelectedIndex+1,DATA);
        }
        public void Otobr(DateTime data)
        {
            users.Clear();
            for (int i = 0; i < DateTime.DaysInMonth(data.Year, data.Month); i++)
            {
                users.Add(new UserView());
                users[i].Label1.Content = i+1;
            }
            MYLIST.ItemsSource = null;
            MYLIST.ItemsSource = users;
            NoName();
        }
        private void NoName()
        {
            List<NewClass> input = MyConv.MyDeserialize<List<NewClass>>("Календарь.json");
            List<NewClass> input2 = new List<NewClass>();
            DateTime temp = new DateTime(Convert.ToInt32(DATA.Year), Convert.ToInt32(DATA.Month),1);
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].data.Month == temp.Month && input[i].data.Year == temp.Year)
                {
                    input2 = input.Where(x => x.data.Month == temp.Month && x.data.Year == temp.Year).ToList();
                }
            }
            for (int i = 0; i < input2.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (input2[i].superlist[j].vibran == true)
                    {
                        Uri newabs = new Uri(input2[i].superlist[j].Put, UriKind.Absolute);
                        users[input2[i].data.Day-1].PrikolKar.Source = new BitmapImage(newabs);
                        break;
                    }
                }
            }
        }
    }
}
