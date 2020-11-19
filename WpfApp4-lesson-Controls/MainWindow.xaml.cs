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
using System.Windows.Threading;

namespace WpfApp4_lesson_Controls
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    static class a
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
        }
       
        Random rnd = new Random();
        int count = 1;
        int countsSec = 10;
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Start_Click(object sender, RoutedEventArgs e)
        {

            countsSec = (int)slider.Value;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }
        
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            lblTime.Content = Convert.ToString(countsSec--);
            CommandManager.InvalidateRequerySuggested();
            int c = Convert.ToInt32(lblTime.Content);
            if (c == 0)
            {
                dispatcherTimer.Stop();
                MessageBox.Show("Time end");
            }
            else if (count == 7)
            {
                MessageBox.Show("Winner");
            }
            
        }

        List<int> sh = new List<int> {1,2,3,4,5,6};

        private void Start_Loaded(object sender, RoutedEventArgs e)
        { 
            int i = 0;
            sh.Shuffle();
            foreach (Button item in buttons.Children.OfType<Button>())
            {
                item.Content = sh[i++];
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse((sender as Button).Content.ToString()) == count)
            {
                count++;
                bar.Value=count;
                (sender as Button).Background = Brushes.Green;
            }
            else
            {
                (sender as Button).Background = Brushes.Red;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.Title = slider.Value.ToString();
        }
    }
}
