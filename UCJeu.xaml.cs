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
        private bool saut = false;
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private double gravite = 0.5;
        private double vitesseChien = 0;
        private const double ForceSaut = -10;
        private static BitmapImage AilesHautSansFond;

        public UCJeu()
        {
            InitializeComponent();
            ChargeImagesHauts();
            imgChien.Source = AilesHautSansFond;
            gameTimer.Interval = TimeSpan.FromMilliseconds(4);
            gameTimer.Tick += Jeu;
            gameTimer.Start();
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
                saut = true;
        }
        private void canvasJeu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                saut = false;
        }
        private void Jeu(object? sender, EventArgs e)
        {
            if (saut)
            {
                vitesseChien = ForceSaut;
                saut = false;
            }
            vitesseChien += gravite;
            double nouvellePosY = Canvas.GetTop(imgChien) + vitesseChien;
            Canvas.SetTop(imgChien, nouvellePosY);
        }
    }
}
