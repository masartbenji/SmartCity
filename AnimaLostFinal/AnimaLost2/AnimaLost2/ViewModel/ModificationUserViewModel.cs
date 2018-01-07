using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace AnimaLost2.ViewModel
{
    public class ModificationUserViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService navPage;
        private ICommand cancel;
        private ICommand modif;
        private ICommand supp;
        private string login;
        private string password;
        private string email;
        private int tel;

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged("password");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        public int Tel
        {
            get { return tel; }
            set
            {
                tel = value;
                RaisePropertyChanged("Tel");
            }
        }

        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(() => GoHomeBack());
                }
                return cancel;
            }
        }
        public ICommand Modif
        {
            get
            {
                if (modif == null)
                {
                    modif = new RelayCommand(async () => await ModifUser());
                }
                return modif;
            }
        }

        public ModificationUserViewModel(INavigationService lg)
        {
            navPage = lg;
        }

        private async Task ModifUser()
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/" + Login);
                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync();
                    var user = ApplicationUser.Deserialize(userJson);
                    if (user == null) navPage.NavigateTo("ModificationUser");
                    else
                    {
                        if (Password == "") Password = user.Password;
                        if (Email == "") Email = user.Email;
                        if (Tel == 0) Tel = user.Phone;
                        var roleResponse = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Role" + user.UserName);
                        user.RoleName = await roleResponse.Content.ReadAsStringAsync();
                        ApplicationUser userFinal = new ApplicationUser()
                        {
                            UserName = user.UserName,
                            Password = Password,
                            Phone = Tel,
                            Email = Email,
                            RoleName = user.RoleName
                        };
                        var responsePut = await http.PutAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Account", userFinal);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            GoHomeBack();
                        }
                        else
                        {
                            navPage.NavigateTo("ModificationUser");
                        }
                    }
                }
            }
        }

        public void GoHomeBack()
        {
            navPage.NavigateTo("UserManagement");
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationUser user = (ApplicationUser)e.Parameter;
            Login = user.UserName;
            Password = user.Password;
            Email = user.Email;
            Tel = user.Phone;
        }
    }
}
