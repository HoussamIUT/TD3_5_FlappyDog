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

namespace FlappyDog
{
    /// <summary>
    /// Logique d'interaction pour UCJeu.xaml
    /// </summary>
    public partial class UCJeu : UserControl
    {
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private static BitmapImage AilesHautSansFond;
        public int nb;
        public BitmapImage[] chiens;
        private static BitmapImage ChienAilesHautSansFond;
        private static BitmapImage PereNoelDroit;

        public UCJeu()
        {
            InitializeComponent();
            ChargeImagesHauts();
            imgChien.Source = AilesHautSansFond;
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);     
        }
        private void ChargeImagesHauts()
        {
            AilesHautSansFond = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{MainWindow.Perso}.png"));
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.KeyDown += canvasJeu_KeyDown;
            Application.Current.MainWindow.KeyUp += canvasJeu_KeyUp;
        }
        private void canvasJeu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                Canvas.SetTop(imgChien, Canvas.GetTop(imgChien) - 10);
            #if DEBUG
            Console.WriteLine("Position Top chien :" + Canvas.GetTop(imgChien));
            #endif
        }
        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            Canvas.SetTop(imgChien, Canvas.GetTop(imgChien) + 10);
        }
    }
}
