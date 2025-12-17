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
    public partial class UCGameOver : UserControl
    {
        public UCGameOver()
        {
            InitializeComponent();
        }

        private void butRejouer_Click(object sender, RoutedEventArgs e)
        {
            // Relancer le jeu
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            UCJeu ucJeu = new UCJeu();
            mainWindow.ZoneJeu.Content = ucJeu;
        }

        private void butMenu_Click(object sender, RoutedEventArgs e)
        {
            // Retourner au menu principal
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            UCAccueil ucAccueil = new UCAccueil();
            mainWindow.ZoneJeu.Content = ucAccueil;
            //ucAccueil.butJouer.Click += mainWindow.AfficherChoixPerso;
            //ucAccueil.butRegles.Click += mainWindow.AfficherRegles;
        }
    }
}
