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


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Node Starting;
        public MainWindow()
        {
            InitializeComponent();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();

            string path = inp_box.Text;//cilvēka izdarītais gājiens (1,2 vai 3)
            try
            {
                Starting = Starting.child[Convert.ToInt16(path, 10)-1];//pārejam uz nākamo mezglu


                if (Starting.data == "")//tiek pārbaudīts vai nākamais mezgls nav galējais
                {
                    win1.Score1.Text = Starting.ScoreMax.ToString();
                    win1.Score2.Text = Starting.ScoreMin.ToString();
                    if (Starting.ScoreMax == Starting.ScoreMin)
                    {
                        win1.congratulations.Text = "tie";
                        win1.Show();
                    }
                    else
                    {
                        if (Backend.aistarts)
                        {
                            String winner = Starting.ScoreMax > Starting.ScoreMin ? 
                                "Unfortunately artificial intelligence is superb this time :(" :
                                "Congratz human, you are the last hope for humanity since you still are able to beat highly sophisticated AI @!@!!";
                            win1.congratulations.Text = winner;
                            win1.Show();
                        }
                        else
                        {
                            String winner = Starting.ScoreMax < Starting.ScoreMin ?
                                "Unfortunately artificial intelligence is superb this time :(" :
                                "Congratz human, you are the last hope for humanity since you still are able to beat highly sophisticated AI @!@!!";
                            win1.congratulations.Text = winner;
                            win1.Show();
                        }
                    }


                }
                else
                {//dators izdara gājienu
                    var biggestMinimax = Backend.aistarts == true ? Starting.child.Where(x => x != null).Min(x => x.minimax) : 
                        Starting.child.Where(x => x != null).Max(x => x.minimax);
                    var MaxChild = from ch in Starting.child
                                   where ch.minimax == biggestMinimax
                                   select ch;
                    Starting = MaxChild.First();
                    if (Starting.data == "")//jāpārbauda vēlreiz, jo iespējams dators izdarot savu gājienu ir sasniedzis finišu
                    {
                        win1.Score1.Text = Starting.ScoreMax.ToString();
                        win1.Score2.Text = Starting.ScoreMin.ToString();
                        if (Starting.ScoreMax == Starting.ScoreMin)
                        {
                            win1.congratulations.Text = "tie";
                            win1.Show();
                        }
                        else
                        {
                            if (Backend.aistarts)
                            {
                                String winner = Starting.ScoreMax > Starting.ScoreMin ?
                                    "Unfortunately artificial intelligence is superb this time :(" :
                                    "Congratz human, you are the last hope for humanity since you still are able to beat highly sophisticated AI @!@!!";
                                win1.congratulations.Text = winner;
                                win1.Show();
                            }
                            else
                            {
                                String winner = Starting.ScoreMax < Starting.ScoreMin ?
                                    "Unfortunately artificial intelligence is superb this time :(" :
                                    "Congratz human, you are the last hope for humanity since you still are able to beat highly sophisticated AI @!@!!";
                                win1.congratulations.Text = winner;
                                win1.Show();
                            }
                        }
                    }
                    else
                    {
                        lbl_main.Content = Starting.data;//maina vērtības nākamajam gājienam
                        lbl_main_Copy.Content = Starting.parent.data;
                        nd.Text = Starting.ScoreMin.ToString();
                        st.Text = Starting.ScoreMax.ToString();
                    }


                }

            }
            catch (Exception ex) { MessageBox.Show("no such option"); }
        }

    }
}
