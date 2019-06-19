using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace WpfMvvmlight.ViewModel
{
    public class WelcomeViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public WelcomeViewModel()
        {
            Messenger.Default.Register<string>(this, "UPDATE", str =>
            {
                Name = str;
            });
        }
    }
}
