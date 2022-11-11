using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TallerSemana6
{
    public partial class MainPage : ContentPage
    {
        private const string URL = "http://192.168.1.53/clientes/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Datos> _post;
        public MainPage()
        {
            InitializeComponent();
            GetData();
        }

        private async void GetData()
        {
            var content = await client.GetStringAsync(URL);
            List<Datos> post = JsonConvert.DeserializeObject<List<Datos>>(content);
            _post= new ObservableCollection<Datos>(post);
            MyListView.ItemsSource = _post;
        }

        private void btnIrIngreso_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VentanaIngreso());

            //DependencyService.Get<Mensaje>().ShortAlert("mensaje de prueba");
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new VentanaActualizacion((Datos)e.SelectedItem));
        }
    }
}
