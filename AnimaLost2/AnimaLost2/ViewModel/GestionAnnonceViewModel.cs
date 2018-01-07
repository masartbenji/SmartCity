using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class GestionAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand goBackHome;
        private string userID;
        private string emailUser;



        public GestionAnnonceViewModel(INavigationService lg)
        {
            navPage = lg;

        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        public ICommand GoBackHome
        {
            get
            {
                if (goBackHome == null)
                {
                    goBackHome = new RelayCommand(() => Home());
                }
                return goBackHome;
            }

        }
        public string UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
                RaisePropertyChanged("UserID");
            }
        }
        public string EmailUser
        {
            get
            {
                return emailUser;
            }
            set
            {
                emailUser = value;
                RaisePropertyChanged("EmailUser");
            }
        }
        private ICommand refreshList;
        public ICommand RefreshList
        {
            get
            {
                if (refreshList == null)
                {
                    refreshList = new RelayCommand(() => Refresh());
                }
                return refreshList;
            }
        }
        public void Refresh()
        {
            // a voir si il faut pas clear du coup la liste dans l initialize
        }
        public void Home()
        {
            navPage.NavigateTo("UserManagement");
        }







    }
}
