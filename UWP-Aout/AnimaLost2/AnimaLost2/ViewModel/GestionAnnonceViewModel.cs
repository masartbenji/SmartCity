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
        private string userID;
        private string emailUser;
        private string phone;
        private int nbAnnouncement;
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

        public GestionAnnonceViewModel(INavigationService lg)
        {
            InitializeAsync();
            navPage = lg;
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
        public string UserID
        {
            get
            {
                userID = SelectedUser.User.UserName;
                return userID;
            }
            set
            {
                userID = value;
                RaisePropertyChanged("UserID");
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
        private string search;

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
        public void Home()
        {
            navPage.NavigateTo("UserManagement");
        }
        public async Task<ObservableCollection<AnnouncementVisu>> GetAnnouncementsUser()
        {
            ObservableCollection<AnnouncementVisu> announcements = new ObservableCollection<AnnouncementVisu>();
            try
            {
                SingleConnection.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await SingleConnection.Client.GetAsync(SingleConnection.Client.BaseAddress + "Account/Announcement/" + SelectedUser.User.UserName);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAnnouncement = response.Content.ReadAsStringAsync().Result;
                    Announcements = AnnouncementVisu.Deserialize(jsonAnnouncement);
                    
                }
            }
            catch (HttpRequestException)
            {

            }
            NbAnnonceUser = Announcements.Count;
            return Announcements;
        }
        public async Task Recherche()
        {
            
            bool trouvé = false;
            var AnnouncementsTemp = await GetAnnouncementsUser();
            AnnouncementVisu anouncementTemp = new AnnouncementVisu();
            foreach(AnnouncementVisu announc in AnnouncementsTemp)
            {
                if (trouvé) break;
                if(announc.idAnnoun == Int32.Parse(Search))
                {
                    trouvé = true;
                    anouncementTemp = announc;
                }
            }
            Announcements.Clear();
            Announcements.Add(anouncementTemp);
        }
    }
}
