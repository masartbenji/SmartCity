﻿using AnimaLost2.Model;
using AnimaLost2.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class GestionAnnonceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand goBackHome;
        private ICommand deconnexion;
        private IDialogService dialogService;
        private ApplicationUser user;
        private string userName;
        private string emailUser;
        private string phone;
        private int nbAnnouncement;
        private string userRole;
        public string UserRole
        {
            get
            {
                userRole = user.RoleName;
                return userName;
            }
            set
            {
                userRole = value;
                RaisePropertyChanged("UserRole");
            }
        }
        private AnnouncementVisu selectedAnnonce;
        public AnnouncementVisu SelectedAnnonce
        {
            get
            {
                return selectedAnnonce;
            }
            set
            {
                selectedAnnonce = value;
                RaisePropertyChanged("SelectedAnnonce");
            }
        }
        private ObservableCollection<AnnouncementVisu> announcements;
        public ObservableCollection<AnnouncementVisu> Announcements
        {
            get
            {
                return announcements;
            }
            set
            {
                announcements = value;
                RaisePropertyChanged("Announcements");
            }
        }
        private ICommand suppression;
        public ICommand Suppression
        {
            get
            {
                if (suppression == null)
                {
                    suppression = new RelayCommand(async() => await SuppressionAnnouncement());
                }
                return suppression;
            }

        }


        public GestionAnnonceViewModel(INavigationService lg,IDialogService dialogService)
        {
            InitializeAsync();
            navPage = lg;
            this.user = SelectedUser.User;
            this.dialogService = dialogService;
        }
        private async void InitializeAsync()
        {
            Announcements = await GetAnnouncementsUser();
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
        public ICommand Deconnexion
        {
            get
            {
                if(deconnexion == null)
                {
                    deconnexion = new RelayCommand(() => Disconnect());
                }
                return deconnexion;
            }
        }
        public string UserName
        {
            get
            {
                userName = SelectedUser.User.UserName;
                return userName;
            }
            set
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }
        public string EmailUser
        {
            get
            {
                emailUser = SelectedUser.User.Email;
                return emailUser;
            }
            set
            {
                emailUser = value;
                RaisePropertyChanged("EmailUser");
            }
        }
        public string PhoneUser
        {
            get
            {
                phone = "0" + SelectedUser.User.Phone.ToString() ;
                return phone;
            }
            set
            {
                phone = value;
                RaisePropertyChanged("PhoneUser");
            }
        }
        public int NbAnnonceUser
        {
            get
            {
                nbAnnouncement = Announcements.Count;
                return nbAnnouncement;
            }
            set
            {
                nbAnnouncement = value;
                RaisePropertyChanged("NbAnnonceUser");
            }
        }
        private ICommand refreshList;
        private ICommand searchBt;
        private string researchLabel;
        public ICommand RefreshList
        {
            get
            {
                if (refreshList == null)
                {
                    refreshList = new RelayCommand(async () => Announcements = await GetAnnouncementsUser());
                }
                return refreshList;
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
        public void Home()
        {
            navPage.NavigateTo("UserManagement");
        }
        public void Disconnect()
        {
            navPage.NavigateTo("Login");
            Token.Id = null;
        }
        public async Task<ObservableCollection<AnnouncementVisu>> GetAnnouncementsUser()
        {
            ApplicationUser user = SelectedUser.User;
            ObservableCollection<AnnouncementVisu> announcements = new ObservableCollection<AnnouncementVisu>();
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                if (Token.Id == null)
                {
                    navPage.NavigateTo("Login");
                    await dialogService.ShowMessageBox("Acces non autorisé aux utilisateurs", "Session expire");
                }else
                {
                    var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Announcement/" + user.UserName);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAnnouncement = response.Content.ReadAsStringAsync().Result;
                        announcements = AnnouncementVisu.Deserialize(jsonAnnouncement);
                    }
                    else
                    {
                        await dialogService.ShowMessageBox("Impossible de retrouve la liste des annonces, veuillez réessayer", "Error");
                    }
                }
                return announcements;

            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur connection");
                navPage.NavigateTo("Login");
            }
            NbAnnonceUser = Announcements.Count;
            return announcements;
        }
        public async Task Recherche()
        {
            bool trouvé = false;
            ObservableCollection<AnnouncementVisu> announcementsTemp = await GetAnnouncementsUser();
            Announcements.Clear();
            foreach (AnnouncementVisu announc in announcementsTemp)
            {
                if (trouvé) break;
                if(announc.idAnnoun == Int32.Parse(ResearchLabel))
                {
                    trouvé = true;
                    Announcements.Add(announc);
                }
            }
        }
        public async Task SuppressionAnnouncement()
        {
            try
            {
                if (SelectedAnnonce != null)
                {
                    SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                    var response = await SingleConnection.Client.DeleteAsync(SingleConnection.Client.BaseAddress + "Announcement/" + SelectedAnnonce.idAnnoun);
                    if (response.IsSuccessStatusCode)
                    {
                        await dialogService.ShowMessageBox("La suppression de l'annonce s'est bien déroulée", "Suppression");
                        Announcements.Remove(SelectedAnnonce);
                    }
                    else await dialogService.ShowMessageBox("L'annonce que vous essayé de supprimé n'existe pas", "Non autorisé");
                }
                else await dialogService.ShowMessageBox("Vous n'avez pas selectionné d'annonce à supprimer", "Erreur");
            }
            catch (HttpRequestException)
            {
                await dialogService.ShowMessageBox("La connection au serveur a été perdue", "Erreur");
            }
        }
        private bool openPane;
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
        public void menuHamburger()
        {
            IsPaneOpen = !IsPaneOpen;
        }
        private ICommand menuBare;
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
    }
}
