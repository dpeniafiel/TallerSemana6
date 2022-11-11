using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TallerSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VentanaActualizacion : ContentPage
    {
        private const string URL = "http://192.168.1.53/clientes/post.php";
        private readonly HttpClient client = new HttpClient();

        public VentanaActualizacion(Datos registroSeleccionado)
        {
            InitializeComponent();
            GetData(registroSeleccionado.codigo);
        }

        private async void GetData(int codigo)
        {
            var url = $"{URL}?codigo={codigo}";
            var content = await client.GetStringAsync(url);
            Datos post = JsonConvert.DeserializeObject<Datos>(content);
            txtCodigo.Text = post.codigo.ToString();
            txtNombre.Text = post.nombre;
            txtApellido.Text = post.apellido.ToString();
            txtEdad.Text = post.edad.ToString();
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var url = $"{URL}?codigo={txtCodigo.Text}&nombre={txtNombre.Text}&apellido={txtApellido.Text}&edad={txtEdad.Text}";
                var res = await client.PutAsync(url,null);
                Console.WriteLine(res);
                
                //DisplayAlert("Correcto", "Dato actualizado con éxito", "aceptar");
                 await DisplayAlert("Correcto","Dato actualizado con éxito", "aceptar");
            }
            catch (Exception ex)
            {
                 await DisplayAlert("Alerta", ex.Message, "aceptar");
            }
        }

        private async void btnRegresar_Clicked(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            await Navigation.PushAsync(new MainPage());
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var content = await client.DeleteAsync($"{URL}?codigo={txtCodigo.Text}");
                //Console.WriteLine(content.ToString());
                //DisplayAlert("Correcto", "Dato actualizado con éxito", "aceptar");
                await DisplayAlert("Correcto", "Dato eliminado con éxito", "aceptar");
                txtCodigo.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "aceptar");
            }
        }
    }
}