using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = grid.ActualWidth; // Ottiene la larghezza effettiva della griglia
            double gridHeight = grid.ActualHeight; // Ottiene l'altezza effettiva della griglia

            double buttonWidth = gridWidth * 0.5; // Imposta la larghezza del pulsante al 50% della larghezza della griglia
            double buttonHeight = gridHeight * 0.3; // Imposta l'altezza del pulsante al 30% dell'altezza della griglia

            pokedexBtn.Width = buttonWidth; // Imposta la larghezza del pulsante
            pokedexBtn.Height = buttonHeight; // Imposta l'altezza del pulsante
        }

        // pokedexBtn
        private void pokedexBtn_Click(object sender, RoutedEventArgs e)
        {
            // Azioni da eseguire quando il pulsante viene cliccato
            MainFrame.Navigate(typeof(Pokedex));
        }

        private void PokedexBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            poketextBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void PokedexBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            poketextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

        // infoBtn
        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            // apre la pagina relativa alle istruzioni
            
        }

        private void InfoBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            infoText.Foreground = new SolidColorBrush(Colors.White);
        }

        private void InfoBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            infoText.Foreground = new SolidColorBrush(Colors.Black);
        }

        // fightBtn
        private void fightBtn_Click(object sender, RoutedEventArgs e)
        {
            // apre la pagina relativa al combattimento
            MainFrame.Navigate(typeof(FightPage));
        }

        private void FightBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            fightText.Foreground = new SolidColorBrush(Colors.White);
        }

        private void FightBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            fightText.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tornaIndietro(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().BackRequested += tornaIndietro;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SystemNavigationManager.GetForCurrentView().BackRequested -= tornaIndietro;
        }

        private void fightBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void fightBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        private void infoBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
