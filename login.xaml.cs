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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        Node Starting = new Node("9099", null, 0, 0, 0, null);
        public login()
        {
            InitializeComponent();
        }

        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            Regex rgx = new Regex("^[0-9]{4}$");
            MainWindow mainWindow = new MainWindow();
            Starting.data = Backend.SortString(enter.Text);
            if (computer.IsChecked == true && human.IsChecked == false&& rgx.IsMatch(Starting.data))
            {
                Backend.createTree2(Starting.data, Starting);
                Backend.MiniMaxSmol(Starting);
                Backend.aistarts = true;
                var biggestMinimax = Backend.aistarts == true ?
                    Starting.child.Where(x => x != null).Min(x => x.minimax) :
                    Starting.child.Where(x => x != null).Max(x => x.minimax);
                var MaxChild = from ch in Starting.child
                               where ch.minimax == biggestMinimax
                               select ch;
                Starting = MaxChild.First();
                mainWindow.Starting = Starting;
                mainWindow.lbl_main.Content = Starting.data;
                mainWindow.st.Text = Starting.ScoreMax.ToString();
                mainWindow.nd.Text = Starting.ScoreMin.ToString();
                mainWindow.lbl_main_Copy.Content = Starting.parent.data;
                mainWindow.Show();



            }
            else if(computer.IsChecked == false&&human.IsChecked==true && rgx.IsMatch(Starting.data))
            {
                Starting.data = Backend.SortString(enter.Text);
                Backend.createTree2(Starting.data, Starting);
                Backend.MiniMaxSmol(Starting);
                mainWindow.Starting = Starting;
                mainWindow.lbl_main.Content = Starting.data;
                Backend.aistarts = false;
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Check input, somethings off");
            }
        }
    }
}
