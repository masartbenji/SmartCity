using AnimaLost2.Service;
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
        private IDialogService dialogService;

        private ICommand searchBox;
        private ICommand searchBt;
        private string search;

        public ICommand SearchBox_QuerySubmitted
        {
            get { return searchBox; }
            set
            {
                if (searchBox != null)
                {
                    new RelayCommand(async () => await Recherche());
                }
            }
        }

        //private SearchBoxQuerySubmittedEventArgs searchBoxQuerySubmitted;

        //public SearchBoxQuerySubmittedEventArgs SearchBox_QuerySubmitted
        //{
        //    set
        //    {
        //        if (searchBoxQuerySubmitted != null)
        //        {
        //            new RelayCommand(async () => await Recherche());
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
                    gestionAnnonce = new RelayCommand(async() => await ManagementUser());
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
                    suppression = new RelayCommand(async () => await SuppUser());
                }
                return suppression;
            }
        }
        private ICommand refreshList;
        public ICommand RefreshList
        {
            get
            {
                if (refreshList == null)
                {
                    refreshList = new RelayCommand(async () => Users = await GetUsersAsync());
                }
                return refreshList;
            }
        }
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
                SelectedUser.User = selectUser;
            }
        }
        private ObservableCollection<ApplicationUser> user;
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

        public ICommand SearchBt
        {
            get
            {
                if (searchBt == null)
                {
                    searchBt = new RelayCommand(async () => await Recherche());
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
            navPage.NavigateTo("ModificationUser");
        }
        public async Task ManagementUser()
        {
            if(SelectUser != null)
            {
                navPage.NavigateTo("GestionAnnonce");
            }
            else
            {
                await dialogService.ShowMessageBox("Veuillez d'abord selectionner un utilisateur", "Erreur");
                navPage.NavigateTo("UserManagement");
            }
        }


        public UserManagementViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
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

        private async void InitializeAsync()
        {
            Users = await GetUsersAsync();
        }
        public async Task<ObservableCollection<ApplicationUser>> GetUsersAsync()
        {
            ObservableCollection<ApplicationUser> users = new ObservableCollection<ApplicationUser>();
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account");
                if (response.IsSuccessStatusCode)
                {
                    var responseUser = await response.Content.ReadAsStringAsync();
                    var listUser = responseUser.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string user in listUser)
                    {
                        ApplicationUser userApp = ApplicationUser.Deserialize(user);
                        SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + userApp.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        userApp.RoleName = ApplicationUser.GetRoleUser(roleName);
                        users.Add(userApp);
                    }
                }
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
            }
            return users;
        }
        public async Task Recherche()
        {
            if (Search != null)
            {
                Users.Clear();
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/" + Search);
                    if (response.IsSuccessStatusCode)
                    {
                        string userJson = await response.Content.ReadAsStringAsync();
                        ApplicationUser user = ApplicationUser.Deserialize(userJson);
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + user.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        user.RoleName = ApplicationUser.GetRoleUser(roleName);
                    Users.Add(user);
                    }
                    navPage.NavigateTo("UserManagement");
                }
        }
        public async Task SuppUser()
        {
            try
            {
                if (SelectUser != null)
                {
                        SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var response = await SingleConnection.Client.DeleteAsync(SingleConnection.Client.BaseAddress + "Account/" + SelectedUser.User.UserName);
                        if (response.IsSuccessStatusCode)
                        {
                            await dialogService.ShowMessageBox("La suppression de l'utilisateur s'est bien déroulée", "Suppression");
                            Users.Remove(SelectedUser.User);
                        }
                        else await dialogService.ShowMessageBox("L'utilisateur que vous essayé de supprimé n'existe pas", "Non autorisé");

                        navPage.NavigateTo("UserManagement");
                }
                else await dialogService.ShowMessageBox("Vous n'avez pas selectionné d'utilisateur à supprimer", "Erreur");
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
            }

        }


    }
}