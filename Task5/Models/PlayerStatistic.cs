using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class PlayerStatistic
    {
        public Player player { get; set; }
        public int numOfMatches { get; set; }
        public int numOfGoals { get; set; }
    }
}
