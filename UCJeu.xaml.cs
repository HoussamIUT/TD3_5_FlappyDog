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
using System.Drawing;


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
        private static readonly double ForceSaut = -8;
        private static BitmapImage AilesHaut;
        private static BitmapImage AilesBas;
        private int timerAnimationAiles = 0;
        private double vitesseOs = 3;
        private List<Image> lesOs = new List<Image>();
        private SoundPlayer sonSaut;
        private SoundPlayer sonCollision;
        private int score = 0;
        private bool osHaut1Depasse = false;
        private bool osHaut2Depasse = false;
        private bool osHaut3Depasse = false;
        private bool osHaut4Depasse = false;
        private bool osHaut5Depasse = false;



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
            AilesHaut = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{MainWindow.Perso}Corrigé.png"));
            string nomPersoBas = MainWindow.Perso.Replace("Haut", "");
            AilesBas = new BitmapImage(new Uri($"pack://application:,,,/img/Chien{nomPersoBas}BasCorrigé.png"));
            
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
            double limiteSol = (imgFond1.ActualHeight - 80) - imgChien.ActualHeight;
            if (nouvellePosY < 0)
            {
                Canvas.SetTop(imgChien, 0);
                vitesseChien = 0;
            }
            else if (nouvellePosY > limiteSol)
            {
                minuterie.Stop();
                AfficherGameOver();
            }
            else
            {
                Canvas.SetTop(imgChien, nouvellePosY);
            }
            Canvas.SetTop(imgChien, nouvellePosY);

            foreach (Image os in lesOs)
            {
                Deplace(os, (int)vitesseOs);

                if (DetecterCollision(imgChien, os))
                {
                    sonCollision.Stop();
                    sonCollision.Play();

                    minuterie.Stop();
                    AfficherGameOver();
                    return;
                }
            }
            Canvas.SetLeft(imgFond1, Canvas.GetLeft(imgFond1) - 2);
            if (Canvas.GetLeft(imgFond1) + imgFond1.ActualWidth <= 0)
                Canvas.SetLeft(imgFond1, canvasJeu.ActualWidth);

            Canvas.SetLeft(imgFond2, Canvas.GetLeft(imgFond2) - 2);
            if (Canvas.GetLeft(imgFond2) + imgFond2.ActualWidth <= 0)
                Canvas.SetLeft(imgFond2, canvasJeu.ActualWidth);

            if (Canvas.GetLeft(imgChien) > Canvas.GetLeft(osHaut1) + osHaut1.ActualWidth && !osHaut1Depasse)
            {
                score++;
                lblScore.Content = score.ToString();
                osHaut1Depasse = true;
            }

            if (Canvas.GetLeft(imgChien) > Canvas.GetLeft(osHaut2) + osHaut2.ActualWidth && !osHaut2Depasse)
            {
                score++;
                lblScore.Content = score.ToString();
                osHaut2Depasse = true;
            }

            if (Canvas.GetLeft(imgChien) > Canvas.GetLeft(osHaut3) + osHaut3.ActualWidth && !osHaut3Depasse)
            {
                score++;
                lblScore.Content = score.ToString();
                osHaut3Depasse = true;
            }

            if (Canvas.GetLeft(imgChien) > Canvas.GetLeft(osHaut4) + osHaut4.ActualWidth && !osHaut4Depasse)
            {
                score++;
                lblScore.Content = score.ToString();
                osHaut4Depasse = true;
            }

            if (Canvas.GetLeft(imgChien) > Canvas.GetLeft(osHaut5) + osHaut5.ActualWidth && !osHaut5Depasse)
            {
                score++;
                lblScore.Content = score.ToString();
                osHaut5Depasse = true;
            }

            if (Canvas.GetLeft(osHaut1) >= canvasJeu.ActualWidth - 10)
                osHaut1Depasse = false;

            if (Canvas.GetLeft(osHaut2) >= canvasJeu.ActualWidth - 10)
                osHaut2Depasse = false;

            if (Canvas.GetLeft(osHaut3) >= canvasJeu.ActualWidth - 10)
                osHaut3Depasse = false;

            if (Canvas.GetLeft(osHaut4) >= canvasJeu.ActualWidth - 10)
                osHaut4Depasse = false;

            if (Canvas.GetLeft(osHaut5) >= canvasJeu.ActualWidth - 10)
                osHaut5Depasse = false;

        }

        private bool DetecterCollision(Image img1, Image img2)
        {
            System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
                (int)Canvas.GetLeft(img1),
                (int)Canvas.GetTop(img1),
                (int)img1.ActualWidth,
                (int)img1.ActualHeight
            );

            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(
                (int)Canvas.GetLeft(img2),
                (int)Canvas.GetTop(img2),
                (int)img2.ActualWidth,
                (int)img2.ActualHeight
            );

            return rect1.IntersectsWith(rect2);
        }


        private void AfficherGameOver()
        {

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            UCGameOver ucGameOver = new UCGameOver(score);
            mainWindow.ZoneJeu.Content = ucGameOver;
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
                Canvas.SetLeft(image, canvasJeu.ActualWidth + 450);
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

                Uri uriSonCollision = new Uri("pack://application:,,,/sons/collision.wav");
                var streamInfoCollision = Application.GetResourceStream(uriSonCollision);

                if (streamInfoCollision != null)
                {
                sonCollision = new SoundPlayer(streamInfoCollision.Stream);
                sonCollision.Load();
                }

        }


    }
}