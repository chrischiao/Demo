using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOLQuerier.Models
{
    public class LeagueEntryDTO
    {
        public string QueueType { get; set; }

        public string SummonerName { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public string Rank { get; set; }

        public string Tier { get; set; }

        public int LeaguePoints { get; set; }
    }
}
