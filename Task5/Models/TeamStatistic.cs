using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class TeamStatistic
    {
        public Team team { get; set; }
        public int numOfWins { get; set; }
        public int numOfDraws { get; set; }
        public int numOfLosses { get; set; }
    }
}
