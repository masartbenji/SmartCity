using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace NavApp1.ViewModel
{
    public class ModificationUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;


        public ModificationUserViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
          //todo last time  
        }

    }
}
