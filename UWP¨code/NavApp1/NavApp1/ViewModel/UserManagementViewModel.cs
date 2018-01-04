using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace NavApp1.ViewModel
{
    public class UserManagementViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ICommand ajoutUser;
        private ICommand utilisateures;
        private ICommand gestionAnnonce;
        private ICommand modifUser;
        private ICommand utilisateur;
        private INavigationService navPage;
        private string accueil;

        private string test;

        public string Test
        {
            get
            {
                return test;
            }
            set
            {
                test = value;
                RaisePropertyChanged("Test");
            }
        }

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
        public ICommand Utilisateurs
        {
            get
            {
                if (utilisateur == null)
                {
                    utilisateur = new RelayCommand(() => AddUtilisateurs());
                }
                return utilisateur;
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

        public void Recherche(SearchBox sender, SearchBoxQuerySubmittedEventArgs textRecherche)
        {
            // todo  1 recherche par rapport a "textRecherhce.QuarryText";
            string s = textRecherche.QueryText;
            test = s;
        }
        
        public void Suppression(ListViewItem userSelect)
        {




        }
        public void AddUtilisateurs()
        {
            //ApplicatioUser user;


            //todo init liste de user 
        }


    }
}
     