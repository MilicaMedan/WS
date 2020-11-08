using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Task5.Models
{
    public class Match
    {
        public int MatchId { get; set; }
        public int HostTeamId { get; set; }
        [ForeignKey("HostTeamId")]
        public virtual Team HostTeam { get; set; }

        public int GuestTeamId { get; set; }
        [ForeignKey("GuestTeamId")]
        public virtual Team GuestTeam { get; set; }

        public DateTime Time { get; set; }
        public string Place { get; set; }
        public string Result { get; set; }

        public int StatussId { get; set; }
        [ForeignKey("StatussId")]
        public virtual Statuss statuss { get; set; }

    }
}
