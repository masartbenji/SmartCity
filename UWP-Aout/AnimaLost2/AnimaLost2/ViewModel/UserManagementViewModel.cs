<<<<<<< HEAD
﻿using AnimaLost2.Static;
using AnimaLost2.Model;
=======
﻿using AnimaLost2.Model;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
        private IDialogService dialogService;

        private ICommand searchBox;
=======

        //private ICommand searchBox;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        private ICommand searchBt;
        private string search;

        //public ICommand SearchBox_QuerySubmitted
        //{
<<<<<<< HEAD
        //    set
        //    {
        //        if (searchBox != null)
        //        {
        //            new RelayCommand(async () => await Recherche());
        //        }
=======
        //    get
        //    {
        //        if(searchBox != null)
        //        {
        //            new RelayCommand(async () => await Recherche());
        //        }
        //        return searchBox;
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        //    }
        //}

        //private SearchBoxQuerySubmittedEventArgs searchBoxQuerySubmitted;

        //public SearchBoxQuerySubmittedEventArgs SearchBox_QuerySubmitted
        //{
        //    set
        //    {
<<<<<<< HEAD
        //        if (searchBoxQuerySubmitted != null)
        //        {
        //            new RelayCommand(async () => await Recherche());
=======
        //        if(searchBoxQuerySubmitted != null)
        //        {
        //           new RelayCommand(async () => await Recherche());
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        //        }
        //    }
        //}

<<<<<<< HEAD
        public void SearchBox_QuerySubmitted()
        {

        }
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
=======

>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
                    gestionAnnonce = new RelayCommand(async() => await ManagementUser());
=======
                    gestionAnnonce = new RelayCommand(() => ManagementUser());
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
                    suppression = new RelayCommand(async () => await SuppUser());
=======
                    suppression = new RelayCommand(async() => await SuppUser());
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                }
                return suppression;
            }
        }
<<<<<<< HEAD
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
=======
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc

        public ICommand SearchBt
        {
            get
            {
                if (searchBt == null)
                {
<<<<<<< HEAD
                    searchBt = new RelayCommand(async () => await Recherche());
=======
                    searchBt = new RelayCommand(async() => await Recherche());
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
=======

>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
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
<<<<<<< HEAD
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
=======
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
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
            InitializeAsync();
        }
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            accueil = "Bienvenue " + (string)e.Parameter + " !";
<<<<<<< HEAD
=======

>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
        }
        public void OnNavigatedTo()
        {
            accueil = "Bienvenue";
        }
<<<<<<< HEAD
=======
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
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc

        private async void InitializeAsync()
        {
            Users = await GetUsersAsync();
        }
        public async Task<ObservableCollection<ApplicationUser>> GetUsersAsync()
        {
            ObservableCollection<ApplicationUser> users = new ObservableCollection<ApplicationUser>();
<<<<<<< HEAD
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account");
=======
            using (HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/");
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                if (response.IsSuccessStatusCode)
                {
                    var responseUser = await response.Content.ReadAsStringAsync();
                    var listUser = responseUser.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string user in listUser)
                    {
                        ApplicationUser userApp = ApplicationUser.Deserialize(user);
<<<<<<< HEAD
                        SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + userApp.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        userApp.RoleName = ApplicationUser.GetRoleUser(roleName);
=======
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                        var roleResponse = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Role/" + userApp.UserName);
                        string roleName = await roleResponse.Content.ReadAsStringAsync();
                        userApp.RoleName = roleName.TrimStart('[').TrimEnd(']').TrimStart('"').TrimEnd('"');
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
                        users.Add(userApp);
                    }
                }
            }
<<<<<<< HEAD
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
                using (HttpClient http = new HttpClient())
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
=======
            return users;
        }
    }


}
>>>>>>> 552da27e22a235f78e9c502f064d704a16429fbc
