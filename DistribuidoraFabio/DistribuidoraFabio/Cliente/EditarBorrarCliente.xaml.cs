﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Cliente
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarBorrarCliente : ContentPage
	{
        private int IdCliente = 0;
        public EditarBorrarCliente(int id_cliente, string nombre, string ubicacion_latitud, string ubicacion_longitud, int telefono, int nit)
		{
			InitializeComponent();
            IdCliente = id_cliente;
			idClienteEntry.Text = id_cliente.ToString();
			nombreClienteEntry.Text = nombre;
			ubicacionLatitudEntry.Text = ubicacion_latitud;
			ubicacionLongitudEntry.Text = ubicacion_longitud;
			telefonoClienteEntry.Text = telefono.ToString();
			nitClienteEntry.Text = nit.ToString();
		}
        private async void BtnEditarCliente_Clicked(object sender, EventArgs e)
        {
            Models.Cliente cliente = new Models.Cliente()
            {
                id_cliente = Convert.ToInt32(idClienteEntry.Text),
                nombre = nombreClienteEntry.Text,
                ubicacion_latitud = ubicacionLatitudEntry.Text,
                ubicacion_longitud = ubicacionLongitudEntry.Text,
                telefono = Convert.ToInt32(telefonoClienteEntry.Text),
                nit = Convert.ToInt32(nitClienteEntry.Text)
            };

            var json = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/clientes/editarCliente.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("EDITADO", "Se edito correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }
        }
        private async void BtnBorrarCliente_Clicked(object sender, EventArgs e)
        {
            Models.Cliente cliente = new Models.Cliente()
            {
                id_cliente = Convert.ToInt32(idClienteEntry.Text),
                nombre = nombreClienteEntry.Text,
                ubicacion_latitud = ubicacionLatitudEntry.Text,
                ubicacion_longitud = ubicacionLongitudEntry.Text,
                telefono = Convert.ToInt32(telefonoClienteEntry.Text),
                nit = Convert.ToInt32(nitClienteEntry.Text)
            };

            var json = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/clientes/borrarCliente.php", content);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("ELIMINAR", "Se elimino correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", result.StatusCode.ToString(), "OK");
                await Navigation.PopAsync();
            }
        }
        private async void BtnVerUbicacion_Clicked(object sender, EventArgs e)
        {
            var location = new Location(Convert.ToDouble(ubicacionLatitudEntry.Text), Convert.ToDouble(ubicacionLongitudEntry.Text));
            var options = new MapLaunchOptions { Name = nombreClienteEntry.Text };
            await Map.OpenAsync(location, options);
        }
        private async void BtnObtenerUbicacion_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    ubicacionLatitudEntry.Text = location.Latitude.ToString();
                    ubicacionLongitudEntry.Text = location.Longitude.ToString();
                    ubconfirmacionEntry.Text = "Ubicacion Guardada";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistorialCliente(IdCliente));
        }
    }
}