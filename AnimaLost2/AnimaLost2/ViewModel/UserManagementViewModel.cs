using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
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

namespace AnimaLost2.ViewModel
{
    public class UserManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private ICommand ajoutUser;
        private ICommand modifUser;
        private ICommand gestionAnnonce;
        private ICommand searchBt;
        private ICommand suppression;
        private INavigationService navPage;

        private string accueil;
        private string search;


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
                if (suppression == null)
                {
                    suppression = new RelayCommand(() => SuppressionUser());
                }
                return suppression;
            }
        }
        public ICommand SearchBt
        {
            get
            {
                if (searchBt == null)
                {
                    searchBt = new RelayCommand(() => Recherche());
                }
                return searchBt;
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
            InitializeAsync();// a voir si il faut pas clear du coup la liste dans l initialize
        }

        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
                RaisePropertyChanged("Search");
            }
        }

        public void AddUser()
        {
            navPage.NavigateTo("NewUser");
        }
        public void ModificationUser()
        {
            if(SelectUser != null)
            {
                navPage.NavigateTo("ModificationUser", SelectUser);
            }
        }
        public void ManagementUser()
        {
            if (SelectUser != null)
            {
                navPage.NavigateTo("GestionAnnonce", SelectUser);
            }
        }

        public void Recherche()
        {
            if (Search != null)
            {
                
                ApplicationUser.Clear();
                try
                {

                    //ApplicationUser.Add(ELEMENT RECHERHCE);
                }
                catch
                {

                }
                
                
                
            }
        }


        public void SuppressionUser() {

        }
        public UserManagementViewModel(INavigationService lg)
        {
            navPage = lg;
            ApplicationUser = new ObservableCollection<ApplicationUser>();
            InitializeAsync();
        }
        // 2eme constructeur si on arrive d un autre page on perd le user pas grave a voir 

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            accueil = "Bienvenue " + (string)e.Parameter + " !";
            
        }
        public void OnNavigatedTo()
        {
            accueil = "Bienvenue";
        }

        private ObservableCollection<ApplicationUser> applicationUser;
        private ApplicationUser selectUser;

        public ApplicationUser SelectUser
        {
            get
            {
                return selectUser;
            }
            set
            {
                selectUser = value;
                if (selectUser != null)
                {
                    RaisePropertyChanged("SelectUser");
                }
            }
        }
        public ObservableCollection<ApplicationUser> ApplicationUser
        {
            get
            {
                return applicationUser;
            }
            set
            {
                if (applicationUser != null)
                {
                    return;
                }
                applicationUser = value;
                RaisePropertyChanged("ApplicationUser");
            }
        }

        private void InitializeAsync()
        {
            // initialiser la liste 
            //CA MARCHE PAS PQ ?????µ$


      
            ApplicationUser.Add(new ApplicationUser
            {
                UserName = "ruben",
                Password = " retjb",
                Email = "ezfo",
                Phone = 123456,
                RoleName = "Admin"
            });
            ApplicationUser.Add(new ApplicationUser
            {
                UserName = "ghjg",
                Password = " tyjtyj",
                Email = "tyjtyjtyj",
                Phone = 123489756,
                RoleName = "User"
            });

        }
    }


}
