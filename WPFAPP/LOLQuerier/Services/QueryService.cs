using System.Linq;
using LOLQuerier.API;
using LOLQuerier.Models;

namespace LOLQuerier.Services
{
    public class QueryService
    {
        public static SummonerDTO GetSummoner(string region, string summonerName)
        {
            Summoner_V4 summoner_V4 = new Summoner_V4(region);
            SummonerDTO summoner = summoner_V4.GetSummonerByName(summonerName);

            return summoner;
        }

        public static PositionDTO GetPosition(SummonerDTO summoner, string region)
        {
            PositionDTO position = null;

            League_V4 league = new League_V4(region);
            var list = league.GetPositions(summoner.Id);
            if (list != null)
                position = list.Where(p => p.QueueType.Equals("RANKED_SOLO_5x5")).FirstOrDefault();

            return position ?? new PositionDTO();
        }
    }
}
