using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class ManualPage : Page
    {
        private string idioma { get; set; }
        public ManualPage()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string imagePath = "";

            if (idioma == "Español")
            {
                imagePath = "ms-appx:///Assets/HelpPage/ayuda.png";

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Regresar";
                pokedexBlock.Text = "Accede al pokedex";
                fightBlock.Text = "¡Luchar!";

                // imposta l'immagine 
                titleImage.Source = new BitmapImage(new Uri(imagePath));
            }
            else if (idioma == "English")
            {
                imagePath = "ms-appx:///Assets/HelpPage/help.png";

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Back";
                pokedexBlock.Text = "Access the Pokédex";
                fightBlock.Text = "Fight!";

                // imposta l'immagine
                titleImage.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            idioma = e.Parameter as string; // Recupera il parametro "idioma"
        }
    }
}
