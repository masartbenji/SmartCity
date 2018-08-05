using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Connection
{
    class SingleConnection
    {
  

        //public static async Task connection(IdUser idUser)
        //{
            
        //    var http = new HttpClient();
        //    IDialogService dialogService;

        //    try
        //    {
        //        var stringInput = await http.PostAsJsonAsync("http://smartcityanimal.azurewebsites.net/api/Jwt", idUser);

        //    }catch(HttpRequestException e)
        //    {
        //      await dialogService.ShowMessageBox("Impossible de se connecter au serveur","Error");
        //    }
        //}
        //public static async Task deco()
        //{

        //}


    }

        
    class IdUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
