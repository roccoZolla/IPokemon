using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent(); 
            // this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = grid.ActualWidth; // Ottiene la larghezza effettiva della griglia
            double gridHeight = grid.ActualHeight; // Ottiene l'altezza effettiva della griglia

            double buttonWidth = gridWidth * 0.5; // Imposta la larghezza del pulsante al 50% della larghezza della griglia
            double buttonHeight = gridHeight * 0.3; // Imposta l'altezza del pulsante al 30% dell'altezza della griglia

            myButton.Width = buttonWidth; // Imposta la larghezza del pulsante
            myButton.Height = buttonHeight; // Imposta l'altezza del pulsante
        }


        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            // Azioni da eseguire quando il pulsante viene cliccato
            MainFrame.Navigate(typeof(Pokedex));
        }

        private void btnSecondo_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
