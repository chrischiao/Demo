using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using LOLQuerier.Services;

namespace LOLQuerier.ViewModels
{
    public class QueryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        string region;
        public string Region
        {
            get { return region; }
            set { region = value; NotifyPropertyChanged("Region"); }
        }

        string summonerName;
        public string SummonerName
        {
            get { return summonerName; }
            set { summonerName = value; NotifyPropertyChanged("SummonerName"); }
        }

        public ProfileViewModel Query()
        {
            var summoner = QueryService.GetSummoner(Region, SummonerName);
            if (summoner != null)
            {
                var position = QueryService.GetPosition(summoner, Region);
                return new ProfileViewModel(summoner.Name, summoner.ProfileIconId, summoner.SummonerLevel, position.Tier, position.Rank,
                position.Wins, position.Losses);
            }
            return null;
        }
    }
}
