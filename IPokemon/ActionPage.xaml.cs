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
using System.Threading.Tasks;
using Windows.UI.Xaml.Hosting;
using System.Drawing;

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
        public bool IsPlayerTurn { get; set; }
        public bool IsPlayer2Turn { get; set; }
        private string idioma { get; set; }
        private bool isPlayer1ButtonEnabled { get; set; }
        private bool isPlayer2ButtonEnabled { get; set; }
        private int maxHP1 { get; set; }
        private int maxHP2 { get; set; }

        private int gameType;   // indica che tipo di combattimento è: 1 - player vs CPU, 2 - player1 vs player2
        private bool gameOver; // Flag per indicare se il combattimento è terminato

        public ActionPage()
        {
            this.InitializeComponent();

            IsPlayerTurn = true;
            IsPlayer2Turn = false;

            gameOver = false;

            isPlayer1ButtonEnabled = true;
            isPlayer2ButtonEnabled = false;

            winnerImage.Visibility = Visibility.Collapsed;
            winnerBlock.Visibility = Visibility.Collapsed;
        }

        private async void MoveButtonPvsCPU_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlayerTurn || gameOver)
                return;

            Button clickedButton = sender as Button;
            PokemonMoves selectedMove = clickedButton.DataContext as PokemonMoves;


            if (selectedMove != null)
            {
                AttackPokemon(pokemon1, pokemon2, selectedMove);

                if (pokemon2.HP <= 0)
                {
                    await EndBattle(pokemon1);
                    return;
                }

                IsPlayerTurn = false;
                IsPlayer2Turn = true;

                isPlayer1ButtonEnabled = false;
                isPlayer2ButtonEnabled = true;


                UpdateButtonEnabledState();


                await Task.Delay(1000);

                if (!gameOver)
                    await PerformCPUTurn();

                UpdateButtonEnabledState();
            }

        }

        private async void MoveButtonPvP2_Click(object sender, RoutedEventArgs e)
        {
            if (gameOver)
                return;

            Button clickedButton = sender as Button;
            PokemonMoves selectedMove = clickedButton.DataContext as PokemonMoves;

            if (selectedMove != null)
            {
                if (IsPlayerTurn)
                {
                    AttackPokemon(pokemon1, pokemon2, selectedMove);

                    if (pokemon2.HP <= 0)
                    {
                        await EndBattle(pokemon1);
                        return;
                    }

                    IsPlayerTurn = false;
                    IsPlayer2Turn = true;

                    isPlayer1ButtonEnabled = false;
                    isPlayer2ButtonEnabled = true;
                }
                else if (IsPlayer2Turn)
                {
                    AttackPokemon(pokemon2, pokemon1, selectedMove);

                    if (pokemon1.HP <= 0)
                    {
                        await EndBattle(pokemon2);
                        return;
                    }

                    IsPlayerTurn = true;
                    IsPlayer2Turn = false;

                    isPlayer1ButtonEnabled = true;
                    isPlayer2ButtonEnabled = false;
                }
            }

            UpdateButtonEnabledState();
        }

        private async Task PerformCPUTurn()
        {
            if (gameOver)
                return;

            PokemonMoves cpuMove = GetRandomMove();

            await Task.Delay(1000);

            AttackPokemon(pokemon2, pokemon1, cpuMove);

            if (pokemon1.HP <= 0)
                await EndBattle(pokemon2);

            IsPlayerTurn = true;
            IsPlayer2Turn = false;

            isPlayer1ButtonEnabled = true;
            isPlayer2ButtonEnabled = false;
        }

        private async Task EndBattle(PokemonData winner)
        {
            gameOver = true;

            // Esegui le azioni necessarie quando la partita termina, ad esempio mostrare un messaggio di vittoria/sconfitta
            firstGrid.Visibility = Visibility.Collapsed;
            secondGrid.Visibility = Visibility.Collapsed;
            titleBlock.Visibility = Visibility.Collapsed;

            backImage.Opacity = 0.7;

            if (winner == pokemon1)
            {
                winnerBlock.Text = winnerBlock.Text + player1Text.Text;
                winnerBlock.Visibility = Visibility.Visible;

                winnerImage.Source = new BitmapImage(new Uri(winner.ImagePath));
                winnerImage.Visibility = Visibility.Visible;
            }
            else if(winner == pokemon2)
            {
                winnerBlock.Text = winnerBlock.Text + player2Text.Text;   
                winnerBlock.Visibility = Visibility.Visible;

                winnerImage.Source = new BitmapImage(new Uri(winner.ImagePath));
                winnerImage.Visibility = Visibility.Visible;
            }


            await Task.Delay(1700);
            // Vai alla pagina di risultato passando il vincitore come parametro
            Frame.Navigate(typeof(FightPage), idioma);
        }

        private PokemonMoves GetRandomMove()
        {
            Random random = new Random();

            // Ottieni una mossa casuale dal Pokémon della CPU
            int index = random.Next(pokemon2.Moves.Count);
            PokemonMoves randomMove = pokemon2.Moves[index];

            // Restituisci la mossa casuale selezionata
            return randomMove;
        }

        private void AttackPokemon(PokemonData attacker, PokemonData defender, PokemonMoves move)
        {
            if (move.PP <= 0 || gameOver)
                return;

            int damage = CalculateDamage(move);
            defender.HP -= damage;
            move.PP--;

            if (defender.HP < 0) // Impedisce ai punti ferita di scendere sotto lo 0
                defender.HP = 0;

            if (defender == pokemon1)
            {
                maxHP1 = maxHP1 - damage;
                healthBar1.Value = maxHP1;

                healthAnimation1.Begin();


                if (defender.HP >= 50)
                {
                    VisualStateManager.GoToState(healthBar1, "Healthy", true);
                }
                else if (defender.HP >= 20)
                {
                    VisualStateManager.GoToState(healthBar1, "Warning", true);
                }
                else
                {
                    VisualStateManager.GoToState(healthBar1, "Critical", true);
                }
            }
            else if (defender == pokemon2)
            {
                maxHP2 = maxHP2 - damage;
                healthBar2.Value = maxHP2;

                healthAnimation2.Begin();

                if (defender.HP >= 50)
                {
                    VisualStateManager.GoToState(healthBar2, "Healthy", true);
                }
                else if (defender.HP >= 20)
                {
                    VisualStateManager.GoToState(healthBar2, "Warning", true);
                }
                else
                {
                    VisualStateManager.GoToState(healthBar2, "Critical", true);
                }
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
            if(gameType == 1)
            {
                MoveButtonPvsCPU_Click(sender, e);
            } else if(gameType== 2)
            {
                MoveButtonPvP2_Click(sender, e);
            }
            
        }

        private void MoveButton2_Click(object sender, RoutedEventArgs e)
        {
            if (gameType == 1)
            {
                MoveButtonPvsCPU_Click(sender, e);
            }
            else if (gameType == 2)
            {
                MoveButtonPvP2_Click(sender, e);
            }
        }

        private void UpdateButtonEnabledState()
        {
            var buttons1 = FindVisualChildren<Button>(btnPlayer1);
            var buttons2 = FindVisualChildren<Button>(btnPlayer2);

            if (IsPlayerTurn)
            {
                foreach (var button in buttons1)
                {
                    button.IsEnabled = isPlayer1ButtonEnabled;
                    button.Visibility = Visibility.Visible;
                }

                foreach (var button in buttons2)
                {
                    button.IsEnabled = isPlayer2ButtonEnabled;
                    button.Visibility = Visibility.Collapsed;
                }
            } 
            else if(IsPlayer2Turn)
            {
                foreach (var button in buttons2)
                {
                    button.IsEnabled = isPlayer2ButtonEnabled;
                    button.Visibility = Visibility.Visible;
                }

                foreach (var button in buttons1)
                {
                    button.IsEnabled = isPlayer1ButtonEnabled;
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is PokemonDataBundle bundle)
            {
                idioma = bundle.idioma;

                pokemon1 = bundle.Pokemon1;
                pokemon2 = bundle.Pokemon2;

                pokemon1Image.Source = new BitmapImage(new Uri(pokemon1.ImagePath));
                pokemon2Image.Source = new BitmapImage(new Uri(pokemon2.ImagePath));

                maxHP1 = pokemon1.HP;
                maxHP2 = pokemon2.HP;

                healthBar1.Value = pokemon1.HP;
                healthBar2.Value = pokemon2.HP;

                pokePlayer1.Text = pokemon1.Name;
                pokePlayer2.Text = pokemon2.Name;

                gameType = bundle.gameType;
            }

            if (gameType == 1)
            {
                if(idioma == "Español")
                {
                    titleBlock.Text = "Jugador 1 vs CPU";
                    player1Text.Text = "Jugador 1";
                    player2Text.Text = "CPU";
                    winnerBlock.Text = "El ganador es: ";
                }
                else if(idioma == "English") 
                {
                    titleBlock.Text = "Player 1 vs CPU";
                    player1Text.Text = "Player 1";
                    player2Text.Text = "CPU";
                    winnerBlock.Text = "The winner is: ";
                }
            } 
            else if(gameType == 2)
            {
                if (idioma == "Español")
                {
                    titleBlock.Text = "Jugador 1 vs Jugador 2";
                    player1Text.Text = "Jugador 1";
                    player2Text.Text = "Jugador 2";
                    winnerBlock.Text = "El ganador es: ";
                }
                else if (idioma == "English")
                {
                    titleBlock.Text = "Player 1 vs Player 2";
                    player1Text.Text = "Player 1";
                    player2Text.Text = "Player 2";
                    winnerBlock.Text = "The winner is: ";
                }
            }
        }

        //
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Imposta il contesto dati appropriato
            DataContext = this;

            UpdateButtonEnabledState();
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child is T obj)
                    {
                        yield return obj;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
