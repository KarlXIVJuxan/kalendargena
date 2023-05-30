using System;
using System.Collections.Generic;
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

namespace Kalendar
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            InitializeComponent();
            MyData.SelectedDate = DateTime.Now;
            DateTime data = MyData.DisplayDate;
            MainPage newpage = new MainPage(data);
            newpage.Otobr(data);
            PageFrame.Content = newpage;
        }
        private void MyData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime data = MyData.DisplayDate;
            MainPage newpage = new MainPage(data);
            newpage.Otobr(data);
            PageFrame.Content = newpage;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MyData.SelectedDate =  MyData.DisplayDate.AddMonths(1);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyData.SelectedDate = MyData.DisplayDate.AddMonths(-1);
        }    
    }
}
