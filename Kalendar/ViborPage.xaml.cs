using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class ViborPage : Page
    {
        public int Selected;
        public DateTime data;
        List<NewPolzControl> forMy = new List<NewPolzControl>();
        Uri absolute = new Uri("C:\\",UriKind.Absolute);
        Uri absolute1 = new Uri("C:\\", UriKind.Absolute);
        Uri absolute2 = new Uri("C:\\", UriKind.Absolute);
        Uri absolute3 = new Uri("C:\\", UriKind.Absolute);
        Uri absolute4 = new Uri("C:\\", UriKind.Absolute);
        List<Uri> images = new List<Uri>();
        List<string> names = new List<string>();
        public ViborPage(int newSelected,DateTime newdata)
        {
            InitializeComponent();
            images.Add(absolute);
            images.Add(absolute1);
            images.Add(absolute2);
            images.Add(absolute3);
            images.Add(absolute4);
            names.Add("Первая");
            names.Add("Вторая");
            names.Add("Третья");
            names.Add("Четвертая");
            names.Add("Пятая");
            Selected = newSelected;
            data = newdata;
            for (int i = 0; i < 5; i++)
            {
                forMy.Add(new NewPolzControl());
                forMy[i].Kartinka.Source = new BitmapImage(images[i]);
                forMy[i].Kartinka.Width = 150;
                forMy[i].Kartinka.Height = 150;
                if (i == 0)
                {
                    forMy[i].Kartinka.Source = new BitmapImage(absolute);
                    forMy[i].Kartinka.Width = 150;
                    forMy[i].Kartinka.Height = 150;
                } 
                if (i == 1)
                {
                    forMy[i].Kartinka.Source = new BitmapImage(absolute1);
                    forMy[i].Kartinka.Width = 150;
                    forMy[i].Kartinka.Height = 150;
                } 
                if (i == 2)
                {
                    forMy[i].Kartinka.Source = new BitmapImage(absolute2);
                    forMy[i].Kartinka.Width = 150;
                    forMy[i].Kartinka.Height = 150;
                }
                if (i == 3)
                {
                    forMy[i].Kartinka.Source = new BitmapImage(absolute3);
                    forMy[i].Kartinka.Width = 150;
                    forMy[i].Kartinka.Height = 150;
                }
                if (i == 4)
                {
                    forMy[i].Kartinka.Source = new BitmapImage(absolute4);
                    forMy[i].Kartinka.Width = 150;
                    forMy[i].Kartinka.Height = 150;
                }
            }
            MyList.ItemsSource = forMy;
            SuperOtobr();        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewClass super = new NewClass();
            DateTime temp = new DateTime(Convert.ToInt32(data.Year), Convert.ToInt32(data.Month), Selected);
            super.data = temp;
            List<Punkt> lok = new List<Punkt>();
            for (int i = 0; i < 5; i++)
            {
                Punkt privet = new Punkt();
                privet.Put = images[i].AbsolutePath;
                privet.Name = names[i];
                privet.vibran = Convert.ToBoolean(forMy[i].prButton.IsChecked);
                lok.Add(privet);                
            }
            super.superlist = lok;       
            Zapis(super);
        }
        private void Zapis(NewClass temp)
        {
            List<NewClass> input =  MyConv.MyDeserialize<List<NewClass>>("Календарь.json");
            DateTime tempd = new DateTime(Convert.ToInt32(data.Year), Convert.ToInt32(data.Month), Selected); 
            int shet = 0;
            if (input.Count != 0)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i].data == tempd)
                    {
                        shet = 1;
                    }
                }
                if(shet == 1)
                {
                    int newshet = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if(forMy[i].prButton.IsChecked == false)
                        {
                            newshet++;
                        }
                    }
                    if(newshet == 5)
                    {
                        foreach (var item in input)
                        {
                            if (item.data == tempd)
                            {
                                
                                input.Remove(item);
                                MyConv.Myserialize(input, "Календарь.json");
                                break;
                            } 
                        }
                    }
                    else
                    {
                        foreach (var item in input)
                        {
                            if(item.data != tempd)
                            {

                            }
                            else
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    item.superlist[i].vibran = Convert.ToBoolean(forMy[i].prButton.IsChecked);                     
                                }
                                MyConv.Myserialize(input, "Календарь.json");
                            }
                        }
                       
                    }
                }
                else
                {
                    input.Add(temp);
                    MyConv.Myserialize(input, "Календарь.json");
                }
               



                foreach (var item in input)
                {
                    shet = 0;
                    if (item.data == tempd)
                    {
                        MessageBox.Show(tempd.ToString());
                        for (int i = 0; i < 5; i++)
                        {
                            if (forMy[i].prButton.IsChecked == false)
                            {
                                shet++;
                            }
                        }
                        if(shet == 5)
                        {
                            MessageBox.Show("Я удалил");
                            input.Remove(item);
                            MyConv.Myserialize(input, "Календарь.json");
                        }
                        else
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                item.superlist[i].vibran = Convert.ToBoolean(forMy[i].prButton.IsChecked);                      
                            }
                            MyConv.Myserialize(input, "Календарь.json");                           
                        }
                        break;
                    }                 
                }               
            }
            else
            {
                input.Add(temp);
                MyConv.Myserialize(input, "Календарь.json");
            }                        
            (Application.Current.MainWindow as MainWindow).PageFrame.Content = new MainPage(data);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).PageFrame.Content = new MainPage(data);
        }
        private void SuperOtobr()
        {
            List<NewClass> input = MyConv.MyDeserialize<List<NewClass>>("Календарь.json");
            List<NewClass> input2 = new List<NewClass>();
            DateTime temp = new DateTime(Convert.ToInt32(data.Year), Convert.ToInt32(data.Month), Selected);
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].data == temp)
                {
                    input2 = input.Where(x => x.data == temp).ToList();
                }
            }
            if(input2.Count != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (input2[0].superlist[i].vibran == true)
                    {
                        forMy[i].prButton.IsChecked = true;
                    }
                    else
                    {
                        forMy[i].prButton.IsChecked = false;
                    }
                }
            }                 
        }
    }
}
