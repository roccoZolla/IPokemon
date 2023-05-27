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

        private int gameType;

        //private DispatcherTimer timer1;
        //private DispatcherTimer timer2;
        //private double targetValue1 = 0.0;
        //private double targetValue2 = 0.0;

        public ActionPage()
        {
            this.InitializeComponent();
            // InitializeTimers();
        }

        //private void InitializeTimers()
        //{
        //    timer1 = new DispatcherTimer();
        //    timer1.Interval = TimeSpan.FromMilliseconds(10);
        //    timer1.Tick += Timer1_Tick;

        //    timer2 = new DispatcherTimer();
        //    timer2.Interval = TimeSpan.FromMilliseconds(10);
        //    timer2.Tick += Timer2_Tick;
        //}

        //private void Timer1_Tick(object sender, EventArgs e)
        //{
        //    double decrement = 1.0;
        //    if (healthBar1.Value > targetValue1)
        //    {
        //        healthBar1.Value -= decrement;
        //    }
        //    else
        //    {
        //        timer1.Stop();
        //    }
        //}

        //private void Timer2_Tick(object sender, EventArgs e)
        //{
        //    double decrement = 1.0;
        //    if (healthBar2.Value > targetValue2)
        //    {
        //        healthBar2.Value -= decrement;
        //    }
        //    else
        //    {
        //        timer2.Stop();
        //    }
        //}

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
            int damage = CalculateDamage(move);


            // Sottrai il danno ai punti ferita del Pokémon difensore
            defender.HP -= damage;

            // Riduci i punti potenza della mossa utilizzata
            move.PP--;

            // Aggiorna la barra della vita del Pokémon difensore
            if (defender == pokemon1)
            {
                HPText1.Text = defender.HP.ToString();
                //UpdateHealthBarAnimation(healthBar1, defender.HP, maxHP1);
            }
            else if (defender == pokemon2)
            {
                HPText2.Text = defender.HP.ToString();
                // UpdateHealthBarAnimation(healthBar2, defender.HP, maxHP2);

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

        //private PokemonMoves GetRandomMove()
        //{
        //    Random random = new Random();
        //    //int index = random.Next(moves.Count);
        //    //PokemonMoves randomMove = moves[index];

        //    // Restituisci la mossa casuale selezionata
        //    return randomMove;
        //}


        //private void UpdateHealthBarAnimation(ProgressBar healthBar, int currentHP, int maxHP)
        //{
        //    // Utilizza Storyboard per animare la ProgressBar 1
        //    DoubleAnimation animation1 = new DoubleAnimation
        //    {
        //        From = healthBar1.Value,
        //        To = targetValue1,
        //        Duration = TimeSpan.FromSeconds(2)
        //    };

        //    Storyboard.SetTarget(animation1, healthBar1);
        //    Storyboard.SetTargetProperty(animation1, new PropertyPath(ProgressBar.ValueProperty));
        //    Storyboard storyboard1 = new Storyboard();
        //    storyboard1.Children.Add(animation1);

        //    storyboard1.Begin();

        //    // Utilizza Storyboard per animare la ProgressBar 2
        //    DoubleAnimation animation2 = new DoubleAnimation
        //    {
        //        From = healthBar2.Value,
        //        To = targetValue2,
        //        Duration = TimeSpan.FromSeconds(2)
        //    };

        //    Storyboard.SetTarget(animation2, healthBar2);
        //    Storyboard.SetTargetProperty(animation2, new PropertyPath(ProgressBar.ValueProperty));
        //    Storyboard storyboard2 = new Storyboard();
        //    storyboard2.Children.Add(animation2);

        //    storyboard2.Begin();

        //    // Utilizza DispatcherTimer per animare le ProgressBar
        //    timer1.Start();
        //    timer2.Start();
        //}


    protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is PokemonDataBundle bundle)
            {
                pokemon1 = bundle.Pokemon1;
                pokemon2 = bundle.Pokemon2;

                pokemon1Image.Source = new BitmapImage(new Uri(pokemon1.ImagePath));
                pokemon2Image.Source = new BitmapImage(new Uri(pokemon2.ImagePath));

                HPText1.Text = pokemon1.HP.ToString();
                HPText2.Text = pokemon2.HP.ToString();

                maxHP1 = pokemon1.HP;
                maxHP2 = pokemon2.HP;

                gameType = bundle.gameType;
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
