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

        public Pokedex()
        {
            this.InitializeComponent();

            SelectedPokemon = new PokemonData();

            string pokedexBasePath = "Assets/PokemonDB/";
            string jsonFilePath = Path.Combine(pokedexBasePath, "pokemonList.json"); // Percorso completo del file JSON

            string imageBasePath = "Assets/Pokemon/"; // Percorso di base delle immagini
            string typeBasePath = "Assets/Types/";

            // Inizializza la lista dei Pokémon
            PokemonList = new ObservableCollection<PokemonData>();

            // Leggi il contenuto del file JSON
            string json = File.ReadAllText(jsonFilePath);

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
            if (PagePokedex.CanGoBack)
            {
                PagePokedex.GoBack();
            }
        }


        // animazione di sfondo
        private void StartBackgroundAnimation()
        {

        }

        //fa partire l'animazione quando viene caricata l'immagine
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StartBackgroundAnimation();
        }
    }
}
