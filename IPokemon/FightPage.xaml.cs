using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Markup;
using Windows.UI;
using System.Threading.Tasks;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>

    public sealed partial class FightPage : Page
    {
        public ObservableCollection<PokemonData> PokemonList { get; set; }
        public PokemonData SelectedPokemon { get; set; }
        public PokemonDataBundle pokemonBundle { get; set; }
        private string idioma { get; set; }

        // variabili d'uso
        private int gameType;
        private static int i = 0;

        public FightPage()
        {
            this.InitializeComponent();

            player1Text.Visibility = Visibility.Collapsed; 
            player2Text.Visibility = Visibility.Collapsed;
            player1PokemonImage.Visibility = Visibility.Collapsed;
            player2PokemonImage.Visibility = Visibility.Collapsed;
            TitleTextBox.Visibility = Visibility.Collapsed;

            pokemonBundle = new PokemonDataBundle();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedPokemon = new PokemonData();

            string pokedexBasePath = "Assets/PokemonDB/";
            string typeBasePath = "";
            string jsonFilePathEng;
            string jsonFilePathEsp;
            string json = "";

            if (idioma == "Español")
            {
                typeBasePath = "Assets/TypesEsp/";
                jsonFilePathEsp = Path.Combine(pokedexBasePath, "pokemonListEsp.json"); // Percorso completo del file JSON in spagnolo

                // Leggi il contenuto del file JSON
                json = File.ReadAllText(jsonFilePathEsp);

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Regresar";

                // imposta la lingua dei bottoni per selezionare la modalita di combattimento
                pVsCpuText.Text = "Jugador 1 vs CPU";
                pVsP2Text.Text = "Jugador 1 vs Jugador 2";
                TitleTextBox.Text = "¡Elige tu Pokémon!";
            }
            else if (idioma == "English")
            {
                typeBasePath = "Assets/TypesEng/";
                jsonFilePathEng = Path.Combine(pokedexBasePath, "pokemonListEng.json"); // percorso al file Json in inglese

                // Leggi il contenuto del file JSON
                json = File.ReadAllText(jsonFilePathEng);

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Back";

                // imposta la lingua dei bottoni per selezionare la modalita di combattimento
                pVsCpuText.Text = "Player 1 vs CPU";
                pVsP2Text.Text = "Player 1 vs Player 2";
                TitleTextBox.Text = "Choose your pokemon!";
            }

            string imageBasePath = "ms-appx:///Assets/Pokemon/"; // Percorso di base delle immagini

            // Inizializza la lista dei Pokémon
            PokemonList = new ObservableCollection<PokemonData>();

            // Deserializza il JSON in una lista di oggetti PokémonData
            List<PokemonData> pokemonDataList = JsonConvert.DeserializeObject<List<PokemonData>>(json);

            // Aggiungi i Pokémon al Pokédex utilizzando i dati deserializzati
            foreach (PokemonData pokemonData in pokemonDataList)
            {
                PokemonList.Add(new PokemonData
                {
                    pokedexID = pokemonData.pokedexID,
                    Name = pokemonData.Name,
                    HP = pokemonData.HP,
                    ImagePathType1 = Path.Combine(typeBasePath, pokemonData.ImagePathType1),
                    ImagePathType2 = Path.Combine(typeBasePath, pokemonData.ImagePathType2),
                    Description = pokemonData.Description,
                    ImagePath = Path.Combine(imageBasePath, pokemonData.ImagePath),
                    Moves = pokemonData.Moves
                }
                );
            }

            // Collega l'ObservableCollection<Pokémon> alla ListBox
            pokemonListView.ItemsSource = PokemonList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            idioma = e.Parameter as string; // Recupera il parametro "idioma"
            pokemonBundle.idioma = idioma;
        }


        private void Player1VsCpu_Click(object sender, RoutedEventArgs e)
        {
            pokemonListView.Visibility = Visibility.Visible;
            P1vsP2.Visibility = Visibility.Collapsed;
            PvsCpu.Visibility = Visibility.Collapsed;

            ////
            player1Text.Visibility = Visibility.Visible;
            player1PokemonImage.Visibility = Visibility.Visible;

            player2Text.Text = "CPU";
            player2Text.Visibility = Visibility.Visible;
            player2PokemonImage.Visibility = Visibility.Visible;

            TitleTextBox.Visibility = Visibility.Visible;

            // imposta il tipo di gioco 
            gameType = 1;
            pokemonBundle.gameType = gameType;
        }

        private void Player1VsPlayer2Button_Click(object sender, RoutedEventArgs e)
        {
            pokemonListView.Visibility = Visibility.Visible;
            PvsCpu.Visibility = Visibility.Collapsed;
            P1vsP2.Visibility = Visibility.Collapsed;

            ////
            player1Text.Visibility = Visibility.Visible;
            player1PokemonImage.Visibility = Visibility.Visible;

            player2Text.Text = "Player 2";
            player2Text.Visibility = Visibility.Visible;
            player2PokemonImage.Visibility = Visibility.Visible;

            TitleTextBox.Visibility = Visibility.Visible;

            // imposta il tipo di gioco 
            gameType = 2;
            pokemonBundle.gameType = gameType;
        }

        private async void pokemon_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni il Pokémon selezionato
            var button = sender as Button;
            var selectedPokemon = button.DataContext as PokemonData;


            if(gameType == 1)   // player vs cpu
            {
                // imposta l'oogetto pokemon da passare come parametro
                pokemonBundle.Pokemon1 = selectedPokemon;
                player1PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));
                pkmnHPText1.Text = "HP: " + selectedPokemon.HP.ToString();

                // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                pokemonBundle.Pokemon2 = SelectRandomPokemonForCPU();
                player2Text.Text = "CPU";
                pkmnHPText2.Text = "HP: " + selectedPokemon.HP.ToString();

                // disabilito la selezione per il player
                pokemonListView.IsEnabled = false;

                await Task.Delay(1500);

                // passaggio al frame action
                GoToPage(pokemonBundle);
                
            } else if(gameType == 2){            // player1 vs player2
                if(i == 0)
                {
                    pokemonBundle.Pokemon1 = selectedPokemon;

                    // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                    player1PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));
                    pkmnHPText1.Text = "HP: " + selectedPokemon.HP.ToString();

                    i++;
                } else if(i == 1) {
                    pokemonBundle.Pokemon2 = selectedPokemon;

                    // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                    player2PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));
                    pkmnHPText2.Text = "HP: " + selectedPokemon.HP.ToString();

                    pokemonListView.IsEnabled = false;

                    i = 0;

                    await Task.Delay(1500);

                    // passaggio a frame action
                    GoToPage(pokemonBundle);
                }
            }
        }

        private PokemonData SelectRandomPokemonForCPU()
        {
            // Genera un indice casuale per selezionare un Pokémon dalla lista
            Random random = new Random();
            int randomIndex = random.Next(PokemonList.Count);

            // Ottieni il Pokémon selezionato casualmente
            PokemonData randomPokemon = PokemonList[randomIndex];

            // Aggiorna l'immagine del Pokémon della CPU con l'immagine del Pokémon selezionato
            player2PokemonImage.Source = new BitmapImage(new Uri(randomPokemon.ImagePath));

            return randomPokemon;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), idioma);
        }

        private void GoToPage(PokemonDataBundle bundle)
        {
            pokemonListView.IsEnabled = false;
            pokemonListView.Visibility = Visibility.Collapsed;

            player1Text.Visibility = Visibility.Collapsed;
            player2Text.Visibility = Visibility.Collapsed;

            player1PokemonImage.Visibility = Visibility.Collapsed;
            player2PokemonImage.Visibility = Visibility.Collapsed;

            pkmnHPText1.Visibility = Visibility.Collapsed;
            pkmnHPText2.Visibility = Visibility.Collapsed;

            // Naviga alla pagina 
            fightFrame.Navigate(typeof(ActionPage), bundle);
        }

        // Cambia il colore del testo del bottone quando il mouse si trova sopra
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var button = (Button)sender;
            var textBlock = FindChild<TextBlock>(button);
            textBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var button = (Button)sender;
            var textBlock = FindChild<TextBlock>(button);
            textBlock.ClearValue(TextBlock.ForegroundProperty);
        }

        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;
                var foundChild = FindChild<T>(child);
                if (foundChild != null)
                    return foundChild;
            }
            return null;
        }

        private void BackButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backtextBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void BackButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backtextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void PvsCpu_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pVsCpuText.Foreground = new SolidColorBrush(Colors.White);
            PvsCpu.Background = new SolidColorBrush(color: Colors.DarkBlue);
        }

        private void PvsCpu_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pVsCpuText.Foreground = new SolidColorBrush(Colors.Black);
            PvsCpu.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void P1vsP2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pVsP2Text.Foreground = new SolidColorBrush(Colors.White);
            P1vsP2.Background = new SolidColorBrush(Colors.DarkBlue);
        }

        private void P1vsP2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pVsP2Text.Foreground = new SolidColorBrush(Colors.Black);
            P1vsP2.Background = new SolidColorBrush(Colors.LightGray);
        }
    }
}

