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

using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI;
using Newtonsoft.Json;


// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class Pokedex : Page
    {
        public ObservableCollection<PokemonData> PokemonList { get; set; }
        public PokemonData SelectedPokemon { get; set; }

        private string idioma { get; set; }

        public Pokedex()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedPokemon = button.DataContext as PokemonData;

            PokemonData pokemonDetails = new PokemonData
            {
                pokedexID = selectedPokemon.pokedexID,
                Name = selectedPokemon.Name,
                ImagePathType1 = selectedPokemon.ImagePathType1,
                ImagePathType2 = selectedPokemon.ImagePathType2,
                Description = selectedPokemon.Description,
                ImagePath = selectedPokemon.ImagePath,
            };

            PagePokedex.Navigate(typeof(PokemonPage), pokemonDetails);
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), idioma);
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
            }
            else if (idioma == "English")
            {
                typeBasePath = "Assets/TypesEng/";
                jsonFilePathEng = Path.Combine(pokedexBasePath, "pokemonListEng.json"); // percorso al file Json in inglese

                // Leggi il contenuto del file JSON
                json = File.ReadAllText(jsonFilePathEng);

                // imposta la lingua del bottone per tornare indietro
                backtextBlock.Text = "Back";
            }

            string imageBasePath = "Assets/Pokemon/"; // Percorso di base delle immagini

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
                    ImagePathType1 = Path.Combine(typeBasePath, pokemonData.ImagePathType1),
                    ImagePathType2 = Path.Combine(typeBasePath, pokemonData.ImagePathType2),
                    Description = pokemonData.Description,
                    ImagePath = Path.Combine(imageBasePath, pokemonData.ImagePath),
                }
                );
            }

            // Collega l'ObservableCollection<Pokémon> alla ListBox
            pokemonListBox.ItemsSource = PokemonList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            idioma = e.Parameter as string; // Recupera il parametro "idioma"
        }

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;

            var textBlock = FindChild<TextBlock>(button);
            textBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Button button = (Button)sender;

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
