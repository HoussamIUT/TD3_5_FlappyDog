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
    /// Logique d'interaction pour UCJeu.xaml
    /// </summary>
    public partial class UCJeu : UserControl
    {
        private static BitmapImage AilesHautSansFond;
        public UCJeu()
        {
            InitializeComponent();
            ChargeImagesHauts();
            imgChien.Source = AilesHautSansFond;
        }
        private void ChargeImagesHauts()
        {
            AilesHautSansFond = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{MainWindow.Perso}.png"));
        }
    }
}
