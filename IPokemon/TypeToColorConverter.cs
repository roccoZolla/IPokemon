using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace IPokemon
{
    public class TypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string type = value as string;

            // Assegna il colore in base al tipo
            switch (type)
            {
                case "Normal":
                    return new SolidColorBrush(Colors.Gray);

                case "Steel":
                    return new SolidColorBrush(Colors.LightCyan);
                
                case "Water":
                    return new SolidColorBrush(Colors.Blue);
                
                case "Dark":
                    return new SolidColorBrush(Colors.Black);
                
                case "Bug":
                    return new SolidColorBrush(Colors.ForestGreen);
                
                case "Dragon":
                    return new SolidColorBrush(Colors.Purple);
                
                case "Electric":
                    return new SolidColorBrush(Colors.Yellow);
                
                case "Grass":
                    return new SolidColorBrush(Colors.Green);
                
                case "Fairy":
                    return new SolidColorBrush(Colors.Pink);
                
                case "Fire":
                    return new SolidColorBrush(Colors.Red);
                
                case "Ice":
                    return new SolidColorBrush(Colors.LightSkyBlue);
                
                case "Fighting":
                    return new SolidColorBrush(Colors.Orange);
                
                case "Psychic":
                    return new SolidColorBrush(Colors.Fuchsia);
                
                case "Rock":
                    return new SolidColorBrush(Colors.SlateGray);
                
                case "Ghost":
                    return new SolidColorBrush(Colors.DarkMagenta);
                
                case "Ground":
                    return new SolidColorBrush(Colors.Brown);
                
                case "Poison":
                    return new SolidColorBrush(Colors.Violet);
                
                case "Flying":
                    return new SolidColorBrush(Colors.SkyBlue);

                case "Acero":
                    return new SolidColorBrush(Colors.LightCyan);
                
                case "Agua":
                    return new SolidColorBrush(Colors.Blue);
                
                case "Siniestro":
                    return new SolidColorBrush(Colors.Black);
                
                case "Bicho":
                    return new SolidColorBrush(Colors.ForestGreen);
                
                case "Electrico":
                    return new SolidColorBrush(Colors.Yellow);
                
                case "Planta":
                    return new SolidColorBrush(Colors.Green);
                
                case "Hada":
                    return new SolidColorBrush(Colors.Pink);
                
                case "Fuego":
                    return new SolidColorBrush(Colors.Red);
                
                case "Hielo":
                    return new SolidColorBrush(Colors.LightSkyBlue);
                
                case "Lucha":
                    return new SolidColorBrush(Colors.Orange);
                
                case "Psiquico":
                    return new SolidColorBrush(Colors.Fuchsia);
                
                case "Roca":
                    return new SolidColorBrush(Colors.SlateGray);
                
                case "Fantasma":
                    return new SolidColorBrush(Colors.DarkMagenta);
                
                case "Tierra":
                    return new SolidColorBrush(Colors.Brown);
                
                case "Veneno":
                    return new SolidColorBrush(Colors.Violet);
                
                case "Volador":
                    return new SolidColorBrush(Colors.SkyBlue);
                
                default:
                    return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

}

