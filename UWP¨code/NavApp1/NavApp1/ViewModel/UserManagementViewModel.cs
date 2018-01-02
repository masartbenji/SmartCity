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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace NavApp1.ViewModel
{
    public class UserManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ICommand ajoutUser;
        private ICommand modifUser;
        private ICommand gestionAnnonce;
        private INavigationService navPage;
        private string accueil;

        public string Accueil
        {
            get
            {
                return accueil;
            }
            set
            {
                accueil = value;
                RaisePropertyChanged("Accueil");
            }
        }
        public ICommand ModifUser
        {
            get
            {
                if (modifUser == null)
                {
                    modifUser = new RelayCommand(() => ModificationUser());
                }
                return modifUser;
            }
        }
        public ICommand GestionAnnonce
        {
            get
            {
                if (gestionAnnonce == null)
                {
                    gestionAnnonce = new RelayCommand(() => ManagementUser());
                }
                return gestionAnnonce;
            }
        }
        public ICommand AjoutUser {
            get
            {
                if (ajoutUser == null)
                {
                    ajoutUser = new RelayCommand( () => AddUser());
                }
                return ajoutUser;
            }
        }
        public void AddUser() {
            navPage.NavigateTo("NewUser");
        }
        public void ModificationUser()
        {
            navPage.NavigateTo("ModificationUser");//RAjouter le user de la liste comme objet 
        }
        public void ManagementUser()
        {
            navPage.NavigateTo("ManagementUser");// envoyer le user aussi 
        }
        public UserManagementViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void OnNavigatedTo(NavigationEventArgs e) {
            accueil = "Bienvenue "+(string)e.Parameter+" !";
        }
    }
}
     