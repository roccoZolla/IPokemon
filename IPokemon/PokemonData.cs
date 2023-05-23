using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace IPokemon
{
    public class PokemonData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImagePathType1 { get; set; }
        public string ImagePathType2 { get; set; }
        public int Number { get; set; }
    }
}
