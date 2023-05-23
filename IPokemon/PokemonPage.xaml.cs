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
using Windows.UI.Xaml.Navigation;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class PokemonPage : Page
    {
        public PokemonData pokemon { get; set; }

        public PokemonPage()
        {
            this.InitializeComponent();
            // this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        //private void backButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Frame frame = Window.Current.Content as Frame;
        //    if (frame.CanGoBack)
        //    {
        //        frame.GoBack();
        //    }
        //}


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pokemon = (PokemonData)e.Parameter;
            DataContext = pokemon;
            pokemonNumber.Text = pokemon.Number.ToString("D3");
        }
    }
}
