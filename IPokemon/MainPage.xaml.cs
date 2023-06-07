using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace IPokemon
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string idioma;
        private bool isInitialized;

        public MainPage()
        {
            this.InitializeComponent();
            isInitialized = false;
            idioma = "Español";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!isInitialized)
            {
                if (idioma == "English")
                {
                    poketextBlock.Text = "Access the Pokédex";
                    fightText.Text = "Fight!";
                    infoText.Text = "Help";

                    menuDropDown.SelectedIndex = 1;
                }
                else if (idioma == "Español")
                {
                    poketextBlock.Text = "Accede al pokedex";
                    fightText.Text = "¡Luchar!";
                    infoText.Text = "Ayuda";

                    menuDropDown.SelectedIndex = 0;
                }

                isInitialized = true;
            }
        }

        // pokedexBtn
        private void pokedexBtn_Click(object sender, RoutedEventArgs e)
        {
            // Azioni da eseguire quando il pulsante viene cliccato
            MainFrame.Navigate(typeof(Pokedex), idioma);
        }

        private void PokedexBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            poketextBlock.Foreground = new SolidColorBrush(Colors.White);
        }

        private void PokedexBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            poketextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }

        // infoBtn
        private void infoBtn_Click(object sender, RoutedEventArgs e)
        {
            // apre la pagina relativa alle istruzioni
            MainFrame.Navigate(typeof(ManualPage), idioma);
            
        }

        private void InfoBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            infoText.Foreground = new SolidColorBrush(Colors.White);
        }

        private void InfoBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            infoText.Foreground = new SolidColorBrush(Colors.Black);
        }

        // fightBtn
        private void fightBtn_Click(object sender, RoutedEventArgs e)
        {
            // apre la pagina relativa al combattimento
            MainFrame.Navigate(typeof(FightPage), idioma);
        }

        private void FightBtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            fightText.Foreground = new SolidColorBrush(Colors.White);
        }

        private void FightBtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            fightText.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void tornaIndietro(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        private void MenuDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitialized && menuDropDown.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)menuDropDown.SelectedItem;
                StackPanel selectedStackPanel = (StackPanel)selectedItem.Content;
                TextBlock selectedTextBlock = null;

                if (selectedStackPanel != null && selectedStackPanel.Children.Count > 1)
                {
                    if (selectedStackPanel.Children[1] is TextBlock)
                    {
                        selectedTextBlock = (TextBlock)selectedStackPanel.Children[1];
                        idioma = selectedTextBlock.Text;
                    }
                }

                if (idioma == "English")
                {
                    poketextBlock.Text = "Access the Pokédex";
                    fightText.Text = "Fight!";
                    infoText.Text = "Help";
                }
                else if (idioma == "Español")
                {
                    poketextBlock.Text = "Accede al pokedex";
                    fightText.Text = "¡Luchar!";
                    infoText.Text = "Ayuda";
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().BackRequested += tornaIndietro;   

            if((string)e.Parameter == "Español")
            {
                idioma = (string)e.Parameter;
                isInitialized = false;
            } else if((string)e.Parameter == "English")
            {
                idioma = (string)e.Parameter;
                isInitialized = false;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SystemNavigationManager.GetForCurrentView().BackRequested -= tornaIndietro;
        }
    }
}
