using DistribuidoraFabio.Models;
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

namespace DistribuidoraFabio.Venta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgregarVenta : ContentPage
	{
		List<string> entries = new List<string>();
		public AgregarVenta()
		{
			InitializeComponent();
            tipoVentaEntry.ItemsSource = new List<string> { "Contado", "Credito" };
            GetDataCliente();
			GetDataVendedor();
		}
        private async void CampoVenta()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/ventas/listaVenta.php");
            var ventas = JsonConvert.DeserializeObject<List<Ventas>>(response);
        }
        private async void GetDataCliente()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/clientes/listaCliente.php");
            var clientes = JsonConvert.DeserializeObject<List<Models.Cliente>>(response).ToList();
            clientePicker.ItemsSource = clientes;
        }
        private async void GetDataVendedor()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/vendedores/listaVendedores.php");
                var vendedores = JsonConvert.DeserializeObject<List<Vendedores>>(response).ToList();
                vendedorPicker.ItemsSource = vendedores;
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.ToString(), "OK");
            }
        }

        private int contcampos = 0;
        private string pickTipoProdChanged;
        private string pickProdNomChanged;
        private decimal MontoTotal = 0;
        private decimal Precio = 0;
        private decimal Cantidad = 0;
        private decimal Descuento = 0;
        private decimal Subtotal = 0;
        int contador = 0;
        int IdStkPrimer = 0;
        int IdStkSegundo = 0;
        int IdStkF1 = 0;
        int IdStkFV1 = 0;
        int IdStkFB1 = 0;
        int IdStkF2 = 0;
        int IdStkF3 = 0;
        int IdPickTP = 0;
        int IdPickPR = 0;
        int IdEntSubtotal = 0;
        int IdEntPrecio = 0;
        int IdEntDesc = 0;
        int IdEntCant = 0;
        int contPosicion = 0;
        string[] tiProArr = new string[20];
        string[] prodArr = new string[20];
        string[] precioArr = new string[20];
        string[] cantArr = new string[20];
        string[] descArr = new string[20];
        string[] subtArr = new string[20];
        string[] subProdArr = new string[20];
        string[] idProdArr = new string[20];
        string[] nombreProdArr = new string[20];
        string[] promedioArr = new string[20];
        string[] stockArr = new string[20];
        string[] stockValoradoArr = new string[20];
        string[] precioVentArr = new string[20];

        string[] arrayColor = { "#CED8F6", "#ACE0A2", "#CED8F6", "#ACE0A2", "#CED8F6", "#ACE0A2", "#CED8F6", "#ACE0A2", "#CED8F6", "#ACE0A2" };

        private decimal PrecioPrueba = 0;

        public async void NuevoProducto()
        {
            try
            {
                
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK", "No se pudo quitar");
            }
        }

        private string vendedorPick;
        private void VendedorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;
                if (selectedIndex != -1)
                {
                    vendedorPick = picker.Items[selectedIndex];
                }
            }
            catch (Exception err)
            {
                DisplayAlert("ERROR", err.ToString(), "OK");
            }
        }

        private string clientePick;
        private void ClientePicker_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectIndex = picker.SelectedIndex;
            if (selectIndex != -1)
            {
                clientePick = picker.Items[selectIndex];
            }
        }
        private string tipoVentaPick;
        private void TipoVentaEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                tipoVentaPick = picker.Items[selectedIndex];
                if (selectedIndex == 1)
                {
                    saldo_VentaEntry.IsVisible = true;
                }

                if (selectedIndex == 0)
                {
                    saldo_VentaEntry.IsVisible = false;
                }
            }
        }
        private async void resultado()
        {
            int x = 0;
            for (int r = 0; r < contcampos; r++)
            {
                x = x + 1;
                MontoTotal = MontoTotal + Convert.ToDecimal(subtArr[x]);
            }
            totalVentaEntry.Text = MontoTotal.ToString();
        }

        private async void BtnVentaGuardar_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("El moonto total es : ", totalVentaEntry.Text, "OK");
            //try
            //{
            //    int y = 0;
            //    for (int r = 0; r < contcampos; r++)
            //    {
            //        y = y + 1;

            //        DetalleVenta detalleVenta = new DetalleVenta()
            //        {
            //            cantidad = Convert.ToInt32(cantArr[y]),
            //            id_producto = tiProArr[y] + " " + prodArr[y],
            //            precio_producto = Convert.ToDecimal(precioArr[y]),
            //            descuento = Convert.ToDecimal(descArr[y]),
            //            factura = Convert.ToInt32(numero_facturaVentaEntry.Text)
            //        };

            //        var json1 = JsonConvert.SerializeObject(detalleVenta);
            //        var content1 = new StringContent(json1, Encoding.UTF8, "application/json");
            //        HttpClient client1 = new HttpClient();
            //        var result1 = await client1.PostAsync("http://dmrbolivia.online/api/ventas/agregarDetalleVenta.php", content1);



            //        Inventario inventario = new Inventario()
            //        {
            //            nombre_p = tiProArr[y] + " " + prodArr[y],
            //            fecha_inv = fechaVentaEntry.Date,
            //            numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
            //            detalle = "Venta",
            //            precio_compra = 0,
            //            unidades = Convert.ToInt32(cantArr[y]),
            //            entrada_fisica = 0,
            //            salida_fisica = Convert.ToInt32(cantArr[y]),
            //            saldo_fisica = Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y]),
            //            entrada_valorado = 0,
            //            salida_valorado = Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y]),
            //            saldo_valorado = Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y])),
            //            promedio = (Convert.ToDecimal(stockValoradoArr[y]) / (Convert.ToDecimal(stockArr[y])))
            //            //promedio = (Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y]))) / (Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y])),
            //        };

            //        var json2 = JsonConvert.SerializeObject(inventario);
            //        var content2 = new StringContent(json2, Encoding.UTF8, "application/json");
            //        HttpClient client2 = new HttpClient();
            //        var result2 = await client2.PostAsync("http://dmrbolivia.online/api/inventarios/agregarInventario.php", content2);

            //        Producto producto = new Producto()
            //        {
            //            id_producto = Convert.ToInt32(idProdArr[y]),
            //            stock = Convert.ToDecimal(stockArr[y]) - Convert.ToInt32(cantArr[y]),
            //            stock_valorado = Convert.ToDecimal(stockValoradoArr[y]) - (Convert.ToInt32(cantArr[y]) * Convert.ToDecimal(promedioArr[y])),

            //            promedio = (Convert.ToDecimal(stockValoradoArr[y]) / (Convert.ToDecimal(stockArr[y])))
            //        };
            //        var json3 = JsonConvert.SerializeObject(producto);
            //        var content3 = new StringContent(json3, Encoding.UTF8, "application/json");
            //        HttpClient client3 = new HttpClient();
            //        var result3 = await client3.PostAsync("http://dmrbolivia.online/api/productos/editarProducto.php", content3);
            //        //DisplayAlert("Valores", "cantidad= " + cantArr[y] + " | promedio= " + promedioArr[y] + " | stock= " + stockArr[y], "OK");

            //    }
            //    Ventas ventas = new Ventas()
            //    {
            //        fecha = fechaVentaEntry.Date,
            //        numero_factura = Convert.ToInt32(numero_facturaVentaEntry.Text),
            //        id_cliente = clientePick,
            //        id_vendedor = vendedorPick,
            //        tipo_venta = tipoVentaPick,
            //        saldo = Convert.ToDecimal(saldo_VentaEntry.Text),
            //        total = Convert.ToDecimal(totalVentaEntry.Text)
            //    };

            //    var json = JsonConvert.SerializeObject(ventas);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    HttpClient client = new HttpClient();
            //    var result = await client.PostAsync("http://dmrbolivia.online/api/ventas/agregarVenta.php", content);
            //    if (result.StatusCode == HttpStatusCode.OK)
            //    {
            //        await DisplayAlert("OK", "Se agrego correctamente", "OK");
            //        await Navigation.PopAsync();
            //    }
            //    else
            //    {
            //        await DisplayAlert("Error", result.StatusCode.ToString(), "OK");
            //        await Navigation.PopAsync();
            //    }
            //}
            //catch (Exception error)
            //{
            //    await DisplayAlert("ERROR", error.ToString(), "OK");
            ////}
        }
    }
}