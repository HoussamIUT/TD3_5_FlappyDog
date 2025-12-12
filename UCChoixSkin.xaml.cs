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

namespace FlappyDog
{
    /// <summary>
    /// Logique d'interaction pour UCChoixSkin.xaml
    /// </summary>
    public partial class UCChoixSkin : UserControl
    {
        public UCChoixSkin()
        {
            InitializeComponent();
        }
        private void rbSkin1_Click(object sender, RoutedEventArgs e)
        {
            butDemarrer.IsEnabled = true;
            MainWindow.Perso = "1AilesHautSansFond";
        }
        private void rbSkin2_Click(object sender, RoutedEventArgs e)
        {
            butDemarrer.IsEnabled = true;
            MainWindow.Perso = "2AilesHautSansFond";
        }
        private void rbSkin3_Click(object sender, RoutedEventArgs e)
        {
            butDemarrer.IsEnabled = true;
            MainWindow.Perso = "3AilesHautSansFond";
        }
    }
}
