using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            string pokedexPath = "";
            string fightPath = "";

            if (idioma == "Español")
            {
                imagePath = "ms-appx:///Assets/HelpPage/ayuda.png";
                pokedexPath = "ms-appx:///Assets/HelpPage/accede_pokedex.png";
                fightPath = "ms-appx:///Assets/HelpPage/luchar.png";

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Regresar";

                // imposta l'immagine 
                titleImage.Source = new BitmapImage(new Uri(imagePath));
                pokedexImage.Source = new BitmapImage(new Uri(pokedexPath));
                fightImage.Source = new BitmapImage(new Uri(fightPath));

                // imposta il testo relativo al pokedex
                pokeDesc.Text = "En la sección de Pokédex podrás ver todos los Pokémon capturados, " +
                    "al hacer clic en el Pokémon que te interese podrás tener una visión general detallada del Pokémon.";

                fightDesc.Text = "En la sección 'Lucha' puedes seleccionar dos modos de combate diferentes: 'jugador vs CPU'," +
                    " una vez que hayas seleccionado el pokemon tendrás que enfrentarte al pokemon elegido por la CPU y enfrentarlo; 'jugador 1 vs jugador 2'," +
                    " una vez que selecciones tu pokemon y tu oponente haya elegido su pokemon, te enfrentarás y prevalecerá el mejor. " +
                    "Pero, ¿cómo funciona el combate? Cada pokemon tiene dos movimientos disponibles y cada movimiento tiene puntos de poder que," +
                    " una vez que se agota el movimiento, ya no se pueden usar; El primer pokemon que se quede sin vida(HP) pierde la pelea";
            }
            else if (idioma == "English")
            {
                imagePath = "ms-appx:///Assets/HelpPage/help.png";
                pokedexPath = "ms-appx:///Assets/HelpPage/acces_pokedex.png";
                fightPath = "ms-appx:///Assets/HelpPage/fight.png";

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Back";

                // imposta l'immagine
                titleImage.Source = new BitmapImage(new Uri(imagePath));
                pokedexImage.Source = new BitmapImage(new Uri(pokedexPath));
                fightImage.Source = new BitmapImage(new Uri(fightPath));

                // 
                pokeDesc.Text = "In the Pokedex section you will be able to see all captured pokemon," +
                    " by clicking on the pokemon you are interested in you will be able to have an in-depth overview of the pokemon.";

                fightDesc.Text = "In the 'Fight' section you can select two different combat modes: 'player vs CPU', once you have selected the pokemon" +
                    " you will have to face the pokemon chosen by the CPU and face it; 'player 1 vs player 2'," +
                    " once you select your pokemon and your opponent has chosen his pokemon, you will clash and the best will prevail." +
                    "But how does the combat work? Each pokemon has two moves available and each move has power points which," +
                    " once the move is exhausted, can no longer be used; The first pokemon to run out of life(HP) loses the fight";
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), idioma);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            idioma = e.Parameter as string; // Recupera il parametro "idioma"
        }

        private void backButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backtextBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void backButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backtextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
