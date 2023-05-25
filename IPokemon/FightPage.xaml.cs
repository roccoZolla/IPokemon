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
        public PokemonData pokemonPlayer1 { get; set; }
        public PokemonData pokemonPlayer2 { get; set; }

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

            SelectedPokemon = new PokemonData();

            string pokedexBasePath = "Assets/PokemonDB/";
            string jsonFilePath = Path.Combine(pokedexBasePath, "pokemonList.json"); // Percorso completo del file JSON

            string imageBasePath = "ms-appx:///Assets/Pokemon/"; // Percorso di base delle immagini
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
                    Moves = pokemonData.Moves
                }
                );
            }

            // Collega l'ObservableCollection<Pokémon> alla ListBox
            pokemonListView.ItemsSource = PokemonList;
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

            // imposta il tipo di gioco 
            gameType = 1;
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

            // imposta il tipo di gioco 
            gameType = 2;
        }

        private void pokemon_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni il Pokémon selezionato
            var button = sender as Button;
            var selectedPokemon = button.DataContext as PokemonData;


            if(gameType == 1)   // player vs cpu
            {
                // imposta l'oogetto pokemon da passare come parametro
                pokemonPlayer1 = selectedPokemon;

                // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                player1PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));
                pokemonPlayer2 = SelectRandomPokemonForCPU();

                // disabilito la selezione per il player
                pokemonListView.IsEnabled = false;
            } else if(gameType == 2){            // player1 vs player2
                if(i == 0)
                {
                    pokemonPlayer1 = selectedPokemon;

                    // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                    player1PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));

                    i++;
                } else if(i == 1) {
                    pokemonPlayer2 = selectedPokemon;

                    // Aggiorna l'immagine del giocatore 1 con l'immagine del Pokémon selezionato
                    player2PokemonImage.Source = new BitmapImage(new Uri(selectedPokemon.ImagePath));

                    pokemonListView.IsEnabled = false;

                    i = 0;
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

    }
}

