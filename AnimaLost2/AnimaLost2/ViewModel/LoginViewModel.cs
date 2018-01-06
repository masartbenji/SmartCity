using AnimaLost2.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnimaLost2.ViewModel
{
    public class LoginViewModel: ViewModelBase, INotifyPropertyChanged
    {
        private string uLogin;
        private string password;
        private ICommand singIn;
        private INavigationService navPage;

        public string ULogin
        {
            get
            {
                return uLogin;
            }
            set
            {
                uLogin = value;
                RaisePropertyChanged("ULogin");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public ICommand SingIn
        {

            get
            {
                if (singIn == null)
                {
                    singIn = new RelayCommand(async () => await Connection());
                }
                return singIn;
            }
        }
        public async Task Connection()
        {
            var http = new HttpClient();
            var idUser = new IdUser() { UserName = ULogin, Password = Password };
            try
            {
                var stringInput = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Jwt", idUser);
                if (stringInput.IsSuccessStatusCode)
                {
                    var content2 = await stringInput.Content.ReadAsStringAsync();
                    var tokenSplit = content2.Split('{', '}', ':', ',');
                    Token.Id = tokenSplit[2].TrimEnd('\"').TrimStart('\"');
                }
            }
            catch (HttpRequestException e)
            {
                Token.Id = null;
            }
            if (Token.Id == null)
            {
                GoBackHome();
            }
            else
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.Id);
                var response = await http.GetAsync("http://smartcityanimal.azurewebsites.net/api/Account/Admin");
                if (!response.IsSuccessStatusCode) GoBackHome();
                else
                {
                    navPage.NavigateTo("UserManagement", ULogin);
                }

            }
        }
        public LoginViewModel(INavigationService lg)
        {
            navPage = lg;
        }
        public void GoBackHome()
        {
            navPage.NavigateTo("Login");
        }
    }
    class IdUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
