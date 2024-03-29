﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IPokemon
{
    public class PokemonData : INotifyPropertyChanged
    {
        public int pokedexID { get; set; }
        public string Name { get; set; }
        private int _hp;
        public int HP
        {
            get { return _hp; }
            set
            {
                if (_hp != value)
                {
                    _hp = value;
                    OnPropertyChanged(nameof(HP));
                }
            }
        }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImagePathType1 { get; set; }
        public string ImagePathType2 { get; set; }
        public List<PokemonMoves> Moves { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
