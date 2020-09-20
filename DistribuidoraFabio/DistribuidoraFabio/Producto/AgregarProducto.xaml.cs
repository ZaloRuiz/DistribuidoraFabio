﻿using DistribuidoraFabio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio.Producto
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarProducto : ContentPage
	{
        List<Tipo_producto> TP_prods = new List<Tipo_producto>();
        List<Sub_producto> SP_prods = new List<Sub_producto>();
        public AgregarProducto()
		{
			InitializeComponent();
		}
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/tipoproductos/listaTipoproducto.php");
            var tipoproductos = JsonConvert.DeserializeObject<List<Models.Tipo_producto>>(response);

            foreach(var item in tipoproductos)
			{
                TP_prods.Add(item);
			}
            tpPicker.ItemsSource = TP_prods;

            var response1 = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/subproductos/listaSubproducto.php");
            var subproducto = JsonConvert.DeserializeObject<List<Models.Sub_producto>>(response1);

            foreach(var item in subproducto)
			{
                SP_prods.Add(item);
			}
            spPicker.ItemsSource = SP_prods;
        }
        private string pickTP;
        private int pickedID_TP;
        private async void TpPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickTP = picker.Items[selectedIndex];
            }
            try
			{
                foreach(var item in TP_prods)
				{
                    if(pickTP == item.nombre_tipo_producto)
					{
                        pickedID_TP = item.id_tipoproducto;
					}
				}
			}
            catch(Exception err)
			{
                await DisplayAlert("ERROR", err.ToString(), "OK");
			}
        }
        private string pickSP;
        private int pickedID_SP;
        private async void SpPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickSP = picker.Items[selectedIndex];
            }
            try
			{
                foreach(var item in SP_prods)
				{
                    if(pickSP == item.nombre_sub_producto)
					{
                        pickedID_SP = item.id_subproducto;
					}
				}
			}
            catch(Exception err)
			{
                await DisplayAlert("ERROR", err.ToString(), "OK");
			}
        }
        private async void BtnGuardarPr_Clicked(object sender, EventArgs e)
        {
            try
            {
                Models.Producto producto = new Models.Producto()
                {
                    nombre = nombrePEntry.Text,
                    id_tipo_producto = pickedID_TP,
                    id_sub_producto = pickedID_SP,
                    stock = Convert.ToDecimal(stockProductoEntry.Text),
                    stock_valorado = Convert.ToDecimal(stockValoradoProductoEntry.Text),
                    promedio = Convert.ToDecimal(promedioProductoEntry.Text),
                    precio_venta = Convert.ToDecimal(precioventaEntry.Text),
                    producto_alerta = Convert.ToDecimal(alertaProductoEntry.Text)
                };

                var json = JsonConvert.SerializeObject(producto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                var result = await client.PostAsync("https://dmrbolivia.com/api_distribuidora/productos/agregarProducto.php", content);

                if (result.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Guardado", "Se agrego correctamente", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
                    await Navigation.PopAsync();
                }
            }
            catch (Exception error)
            {
                await DisplayAlert("ERROR", error.ToString(), "OK");
            }
        }
    }
}