﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DistribuidoraFabio.Helpers;
using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Compra
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaCompra : ContentPage
	{
		public ListaCompra()
		{
			InitializeComponent();
		}
		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AgregarCompra());
		}
		protected async override void OnAppearing()
		{
			base.OnAppearing();
			string BusyReason = "Cargando...";
			await PopupNavigation.Instance.PushAsync(new BusyPopup(BusyReason));
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/compras/listaCompra.php");
			var compras = JsonConvert.DeserializeObject<List<Compras>>(response);

			listaCompra.ItemsSource = compras;
			await PopupNavigation.Instance.PopAsync();
		}
		private async void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			var detalles = e.Item as Compras;
			await Navigation.PushAsync(new MostrarCompra(detalles.id_compra, detalles.fecha_compra, detalles.numero_factura, detalles.id_proveedor,
														detalles.saldo, detalles.total));
		}
	}
}