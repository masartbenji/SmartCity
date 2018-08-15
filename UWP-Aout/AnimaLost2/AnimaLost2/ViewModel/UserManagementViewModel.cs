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
        private ICommand menuBare;
        private bool openPane;
        private IDialogService dialogService;
        private ICommand recherche;
        private string researchLabel;
        private ICommand refreshList;

        public string ResearchLabel
        {
            get
            {
                return researchLabel;
            }
            set
            {
                researchLabel = value;
                RaisePropertyChanged("ResearchLabel");
            }
        }
        public ICommand rechercheBox
        {
            get
            {
                if(recherche == null)
                {
                    recherche = new RelayCommand(async ()=> await Recherche());
                }
                return recherche;
            }
        }
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


        public void menuHamburger()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        public void AddUser()
        {
            navPage.NavigateTo("NewUser");
        }
        public async Task ModificationUser()
        {
            if (SelectUser != null)
            {

                navPage.NavigateTo("ModificationUser");
            }
            else
            {
                await dialogService.ShowMessageBox("Veuillez d'abord selectionner un utilisateur", "Erreur");
            }
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
            }
        }
        public UserManagementViewModel(INavigationService lg, IDialogService service)
        {
            navPage = lg;
            dialogService = service;
            InitializeAsync();
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
                else await dialogService.ShowMessageBox("La requete a rencontre une erreur, veuillez réessayer", "Erreur");
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur connection");
                navPage.NavigateTo("Login");
            }
            return users;
        }
        public async Task Recherche()
        {
            if (researchLabel != null)
            {
                try
                {
                    Users.Clear();
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    // faut il un verif de token saission ?? 

                    var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/" + researchLabel);
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            string userJson = await response.Content.ReadAsStringAsync();
                            ApplicationUser user = ApplicationUser.Deserialize(userJson);
                            SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                            // pas de sens de le verifire si on le fait plus haut non ?

                            if (Token.Id == null)
                            {
                                navPage.NavigateTo("Login");
                                await dialogService.ShowMessageBox("Acces non autorisé aux utilisateurs", "Session expire");
                            }
                            else
                            {
                                var roleResponse = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Role/" + user.UserName);
                                string roleName = await roleResponse.Content.ReadAsStringAsync();
                                user.RoleName = ApplicationUser.GetRoleUser(roleName);
                                Users.Add(user);
                            }
                        }
                        catch
                        {
                            await dialogService.ShowMessageBox("Impossible de retrouve la liste des users, veuillez réessayer", "Error");
                        }
                    }else await dialogService.ShowMessageBox("Une erreur est intervenu veuillez réessayer", "Error");

                }
                catch
                {
                    await dialogService.ShowMessageBox("Impossible de se connecter au serveur", "Error");
                    navPage.NavigateTo("Login");
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
                        if (Token.Id == null)
                        {
                            navPage.NavigateTo("Login");
                            await dialogService.ShowMessageBox("Acces non autorisé aux utilisateurs", "Session expire");
                        }
                        else
                        {
                            var response = await SingleConnection.Client.DeleteAsync(SingleConnection.Client.BaseAddress + "Account/" + SelectedUser.User.UserName);
                            if (response.IsSuccessStatusCode)
                            {
                                Users.Remove(SelectedUser.User);
                                await dialogService.ShowMessageBox("La suppression de l'utilisateur s'est bien déroulée", "Suppression");
                            }
                            else await dialogService.ShowMessageBox("L'utilisateur que vous essayé de supprimé n'existe pas", "Utilisateur inconnu");
                            await GetUsersAsync();
                        }         
                 }
                else await dialogService.ShowMessageBox("Vous n'avez pas selectionné d'utilisateur à supprimer", "Erreur");
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
                navPage.NavigateTo("Login");
            }

        }


    }
}