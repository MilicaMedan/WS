using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class Team
    {
        public int TeamId {get; set;}
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public string Description { get; set; }
        public int NumeberOfPlayers { get; set; }

    }
}
