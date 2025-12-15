using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
        private static DispatcherTimer minuterie;
        private double gravite = 0.5;
        private double vitesseChien = 0;
        private static readonly double ForceSaut = -10;
        private static BitmapImage AilesHaut;
        private static BitmapImage AilesBas;
        private int timerAnimationAiles = 0;
        private double vitesseOs = 3;
        private List<Image> lesOs = new List<Image>();
        private SoundPlayer sonSaut;

        public UCJeu()
        {
            InitializeComponent();
            ChargeImages(); 
            InitializeSons();
            InitializeTimer();
            InitializeOs();

            imgChien.Source = AilesHaut;
        }

        private void InitializeTimer()
        {
            minuterie = new DispatcherTimer();
            minuterie.Interval = TimeSpan.FromMilliseconds(4);
            minuterie.Tick += Jeu;
            minuterie.Start();
        }

        private void ChargeImages()
        {
            AilesHaut = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{MainWindow.Perso}.png"));
            string nomPersoBas = MainWindow.Perso.Replace("Haut", "");
            AilesBas = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{nomPersoBas}Bas.png"));
            
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

                if (sonSaut != null)
                {
                    sonSaut.Stop();
                    sonSaut.Play();
                }

                imgChien.Source = AilesBas;
                timerAnimationAiles = 15;
            }

            if (timerAnimationAiles > 0)
            {
                timerAnimationAiles--;
            }
            else
            {
                imgChien.Source = AilesHaut;
            }

            vitesseChien += gravite;
            double nouvellePosY = Canvas.GetTop(imgChien) + vitesseChien;
            Canvas.SetTop(imgChien, nouvellePosY);

            foreach (Image os in lesOs)
            {
                Deplace(os, (int)vitesseOs);
            }
        }
        
        private void InitializeOs()
        {
            lesOs.Add(osHaut1);
            lesOs.Add(osBas1);
            lesOs.Add(osHaut2);
            lesOs.Add(osBas2);
            lesOs.Add(osHaut3);
            lesOs.Add(osBas3);
            lesOs.Add(osHaut4);
            lesOs.Add(osBas4);
            lesOs.Add(osHaut5);
            lesOs.Add(osBas5);
        }
        private void Deplace(Image image, int pas)
        {
            Canvas.SetLeft(image, Canvas.GetLeft(image) - pas);

            if (Canvas.GetLeft(image) + image.ActualWidth <= 0)
            {
                Canvas.SetLeft(image, canvasJeu.ActualWidth + 250);
            }
        }





        private void InitializeSons()
        {
                Uri uriSon = new Uri("pack://application:,,,/sons/flap.wav");
                var streamInfo = Application.GetResourceStream(uriSon);

                if (streamInfo != null)
                {
                    sonSaut = new SoundPlayer(streamInfo.Stream);
                    sonSaut.Load();
                }


        }
    }
}