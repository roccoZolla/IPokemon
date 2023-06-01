﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPokemon
{
    public class PokemonMoves : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Power { get; set; }
        private int pp;
        public int PP
        {
            get { return pp; }
            set
            {
                if (pp != value)
                {
                    pp = value;
                    OnPropertyChanged(nameof(PP));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
