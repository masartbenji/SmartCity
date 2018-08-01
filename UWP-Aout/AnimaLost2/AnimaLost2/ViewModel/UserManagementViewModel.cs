using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private ICommand suppression;
        private INavigationService navPage;
        private string accueil;
        private ICommand menuBare;
        private bool openPane;

        //private ICommand searchBox;
        private ICommand searchBt;
        private string search;

        //public ICommand SearchBox_QuerySubmitted
        //{
        //    get
        //    {
        //        if(searchBox != null)
        //        {
        //            new RelayCommand(async () => await Recherche());
        //        }
        //        return searchBox;
        //    }
        //}

        //private SearchBoxQuerySubmittedEventArgs searchBoxQuerySubmitted;

        //public SearchBoxQuerySubmittedEventArgs SearchBox_QuerySubmitted
        //{
        //    set
        //    {
        //        if(searchBoxQuerySubmitted != null)
        //        {
        //           new RelayCommand(async () => await Recherche());
        //        }
        //    }
        //}

        public ICommand Buttton_hamburger
        {
            get
            {
                if (menuBare == null)
                {
                    menuBare = new RelayCommand(() => menuHamburger());
                }
                return menuBare;
            }
        }
        public bool IsPaneOpen
        {
            get
            {
                return openPane;
            }
            set
            {
                openPane = value;
                RaisePropertyChanged("IsPaneOpen");
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
                    suppression = new RelayCommand(async() => await SuppUser());
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
                    searchBt = new RelayCommand(async() => await Recherche());
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

        public void menuHamburger()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        public void AddUser()
        {
            navPage.NavigateTo("NewUser");
        }
        public void ModificationUser()
        {
            if (SelectUser != null)
            {
                navPage.NavigateTo("ModificationUser", SelectUser);
            }
        }
        public void ManagementUser()
        {
            navPage.NavigateTo("GestionAnnonce");// envoyer le user aussi 
        }

        public async Task Recherche()
        {
            if (Search != null)
            {
                Users.Clear();
                using(HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/" + Search);
                    if (response.IsSuccessStatusCode)
                    {
                        string userJson = await response.Content.ReadAsStringAsync();
                        ApplicationUser user = ApplicationUser.Deserialize(userJson);
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Role/" + user.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        user.RoleName = roleName.TrimStart('[').TrimEnd(']').TrimStart('"').TrimEnd('"');
                        Users.Add(user);
                    }
                    navPage.NavigateTo("UserManagement");
                }
            }
        }
        private async Task SuppUser()
        {
            if(SelectUser != null)
            {
                using (HttpClient http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await http.DeleteAsync("http://smartcityanimal.azurewebsites.net/api/Account/" + SelectUser.UserName);
                    if (response.IsSuccessStatusCode)
                    {
                        navPage.NavigateTo("UserManagement");
                    }
                    else navPage.NavigateTo("ModificationUser");

                }

            }  
        }
        public UserManagementViewModel(INavigationService lg)
        {
            navPage = lg;
            InitializeAsync();
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            accueil = "Bienvenue " + (string)e.Parameter + " !";

        }
        public void OnNavigatedTo()
        {
            accueil = "Bienvenue";
        }
        private ObservableCollection<ApplicationUser> user;
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
        public ObservableCollection<ApplicationUser> Users
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                RaisePropertyChanged("Users");
            }
        }

        private async void InitializeAsync()
        {
            Users = await GetUsersAsync();
        }
        public async Task<ObservableCollection<ApplicationUser>> GetUsersAsync()
        {
            ObservableCollection<ApplicationUser> users = new ObservableCollection<ApplicationUser>();
            using (HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/");
                if (response.IsSuccessStatusCode)
                {
                    var responseUser = await response.Content.ReadAsStringAsync();
                    var listUser = responseUser.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string user in listUser)
                    {
                        ApplicationUser userApp = ApplicationUser.Deserialize(user);
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Role/" + userApp.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        userApp.RoleName = roleName.TrimStart('[').TrimEnd(']').TrimStart('"').TrimEnd('"');
                        users.Add(userApp);
                    }
                }
            }
            return users;
        }
    }


}
