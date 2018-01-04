using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavApp1.ViewModel
{
    public class GestiuonAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;

        public GestiuonAnnonceViewModel(INavigationService lg)
        {
            navPage = lg;
        }

    }
}
