using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Perso { get; set; }
        public static int SautPereNoel { get; set; } = 2;

        public MainWindow()
        {
            InitializeComponent();
            AfficheAccueil();
        }
        private void AfficheAccueil()
        {
            UCAccueil uc = new UCAccueil();
            ZoneJeu.Content = uc;
            uc.butJouer.Click += AfficherChoixPerso;
            uc.butRegles.Click += AfficherRegles;
        }
        public void AfficherChoixPerso(object sender, RoutedEventArgs e)
        {
            UCChoixSkin uc = new UCChoixSkin();
            ZoneJeu.Content = uc;
            uc.butDemarrer.Click += AfficherJeu;
            uc.butRetour.Click += AfficherAccueilRegle;
        }

        private void AfficherJeu(object sender, RoutedEventArgs e)
        {
            UCJeu uc = new UCJeu();
            ZoneJeu.Content = uc;
        }

        public void AfficherRegles(object sender, RoutedEventArgs e)
        {
            UCReglesJeu uc = new UCReglesJeu();
            ZoneJeu.Content = uc;
            uc.butRetour.Click += AfficherAccueilRegle;
        }
        private void AfficherAccueilRegle(object sender, RoutedEventArgs e)
        {
            AfficheAccueil();
        }  
    }
}