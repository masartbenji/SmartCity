using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using AnimaLost.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost.ViewModel
{
    public class UserManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ICommand ajoutUser;
        private ICommand modifUser;
        private ICommand gestionAnnonce;
        private ICommand recherche;
        private ICommand suppression;
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
        public ICommand AjoutUser
        {
            get
            {
                if (ajoutUser == null)
                {
                    ajoutUser = new RelayCommand(() => AddUser());
                }
                return ajoutUser;
            }
        }
        public ICommand Suppression
        {
            get
            {
                if(suppression == null)
                {
                    suppression = new RelayCommand(() => SuppressionUser());
                }
                return suppression;
            }
        }
        public ICommand Recherche
        {
            get
            {
                if(recherche == null)
                {
                    recherche = new RelayCommand(() => RechercheUser());
                }
                return recherche;
            }
        }
        public void AddUser()
        {
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
        public void RechercheUser() { }
        public void SuppressionUser() { }
        public UserManagementViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            accueil = "Bienvenue " + (string)e.Parameter + " !";
        }
    }
}
