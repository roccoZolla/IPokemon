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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class ActionPage : Page
    {
        public PokemonData pokemon1 { get; set; }
        public PokemonData pokemon2 { get; set; }

        private int maxHP1;
        private int maxHP2;

        public ActionPage()
        {
            this.InitializeComponent();
        }

        private void AttackPokemon(PokemonData attacker, PokemonData defender, PokemonMoves move)
        {
            // Controlla se il Pokémon attaccante ha abbastanza punti potenza per utilizzare la mossa
            if (move.PP <= 0)
            {
                // La mossa non può essere utilizzata, poiché i punti potenza sono esauriti
                // Esegui le azioni necessarie, come l'aggiornamento dell'interfaccia utente per segnalare il problema
                return;
            }

            // Effettua un attacco
            // int damage = CalculateDamage(attacker, defender, move);
            int damage = CalculateDamage(move);
            move.PP--;
            

            // Sottrai il danno ai punti ferita del Pokémon difensore
            defender.HP -= damage;
            
            // Riduci i punti potenza della mossa utilizzata
            move.PP--;
            
            // Aggiorna la barra della vita del Pokémon difensore
            if (defender == pokemon1)
            { 
                HPText1.Text = defender.HP.ToString();
                // PPtext1.Text = move.PP.ToString();
                UpdateHealthBarAnimation(healthBar1, defender.HP, maxHP1);
            }  
            else if (defender == pokemon2)
            {
                HPText2.Text = defender.HP.ToString();
                // PPtext2.Text = move.PP.ToString();
                UpdateHealthBarAnimation(healthBar2, defender.HP, maxHP2);
            }
                

            // Controlla se il Pokémon difensore è stato sconfitto
            if (defender.HP <= 0)
            {
                // Il Pokémon difensore è stato sconfitto
                // Esegui le azioni necessarie, come la rimozione del Pokémon dalla lista o l'aggiornamento dell'interfaccia utente
            }
        }

        private int CalculateDamage(PokemonMoves move)
        {
            //Calcola il danno in base ai valori dei Pokémon attaccante e difensore
            // Implementa qui la tua logica per il calcolo del danno
            int damage = move.Power / 5;

            // Restituisci il valore del danno calcolato
            return damage;
        }

        private void MoveButton1_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni il pulsante (button) che ha generato l'evento
            Button clickedButton = sender as Button;

            // Ottieni la mossa associata al pulsante tramite il DataContext
            PokemonMoves selectedMove = clickedButton.DataContext as PokemonMoves;

            // Chiamata alla funzione di attacco passando la mossa
            AttackPokemon(pokemon1, pokemon2, selectedMove);
             
        }        
        private void MoveButton2_Click(object sender, RoutedEventArgs e)
        {
            // Ottieni il pulsante (button) che ha generato l'evento
            Button clickedButton = sender as Button;

            // Ottieni la mossa associata al pulsante tramite il DataContext
            PokemonMoves selectedMove = clickedButton.DataContext as PokemonMoves;

            // Chiamata alla funzione di attacco passando la mossa
            AttackPokemon(pokemon2, pokemon1, selectedMove);
             
        }        

        // per la CPU
        //private void PokemonMoves GetRandomMove()
        //{
        //    // Implementa la logica per ottenere una mossa casuale dal set di mosse

        //    //// Ad esempio, puoi generare un numero casuale per selezionare una mossa casuale dall'elenco
        //    //Random random = new Random();
        //    //int index = random.Next(moves.Count);
        //    //Move randomMove = moves[index];

        //    //// Restituisci la mossa casuale selezionata
        //    //return randomMove;
        //}


        private void UpdateHealthBarAnimation(ProgressBar healthBar, int currentHP, int maxHP)
        {
            double animationDuration = 2.0; // Durata dell'animazione in secondi
            double newValue = (currentHP / (double)maxHP) * 100.0; // Calcola il nuovo valore proporzionale

            // Crea un Storyboard per l'animazione
            Storyboard storyboard = new Storyboard();

            // Crea una DoubleAnimation per modificare gradualmente il valore della barra della salute
            DoubleAnimation animation = new DoubleAnimation
            {
                To = newValue,
                Duration = TimeSpan.FromSeconds(animationDuration)
            };

            // Aggiungi l'animazione al Storyboard
            Storyboard.SetTarget(animation, healthBar);
            Storyboard.SetTargetProperty(animation, "Value");
            storyboard.Children.Add(animation);

            // Avvia il Storyboard
            storyboard.Begin();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if(e.Parameter is PokemonDataBundle bundle)
            {
                pokemon1 = bundle.Pokemon1;
                pokemon2 = bundle.Pokemon2;

                pokemon1Image.Source = new BitmapImage(new Uri(pokemon1.ImagePath));
                pokemon2Image.Source = new BitmapImage(new Uri(pokemon2.ImagePath));

                HPText1.Text = pokemon1.HP.ToString();
                HPText2.Text = pokemon2.HP.ToString();

                maxHP1 = pokemon1.HP;
                maxHP2 = pokemon2.HP;
            }
        }

        //
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Imposta il contesto dati appropriato
            DataContext = this;
        }
    }
}
