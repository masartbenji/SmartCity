using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Service
{
    public sealed class SingleConnection
    {
        private static HttpClient client;
        public static HttpClient Client
        {
            get
            {
                if(client == null)
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:55945/api/");
                }
                return client;
            }
        }

    }
}
