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
            navPage.NavigateTo("ModificationUser");//RAjouter le user de la liste comme objet 
        }
        public void ManagementUser()
        {
            navPage.NavigateTo("ManagementUser");// envoyer le user aussi 
        }

        public void Recherche()
        {
            if (Search != null)
            {
                //REcherche et garnir la listview avec 


                // Effacer la liste 
                //ApplicationUser.Clear();
                //ApplicationUser.Add(ELEMENT RECHERHCE);
            }
        }


        public void SuppressionUser() {

           //    SelectUser





        }
        public UserManagementViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            accueil = "Bienvenue " + (string)e.Parameter + " !";
            ApplicationUser = new ObservableCollection<ApplicationUser>();
            InitializeAsync();
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
