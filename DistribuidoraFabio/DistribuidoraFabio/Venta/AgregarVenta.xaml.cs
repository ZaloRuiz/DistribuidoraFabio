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
		List<ProductoNombre> prods = new List<ProductoNombre>();
        private int idProductoSelected = 0;
		public AgregarVenta()
		{
			InitializeComponent();
            tipoVentaEntry.ItemsSource = new List<string> { "Contado", "Credito" };
            GetDataCliente();
			GetDataVendedor();
            GetTipoProducto();
            GetProductos();

        }
		protected override void OnAppearing()
		{
            listProductos.ItemsSource = App._detalleVData;
			base.OnAppearing();
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
        private async void GetTipoProducto()
		{
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/tipoproductos/listaTipoproducto.php");
                var tp_productos = JsonConvert.DeserializeObject<List<Tipo_producto>>(response).ToList();
                picker_TP.ItemsSource = tp_productos;
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.ToString(), "OK");
            }
        }
        public async void GetProductos()
		{
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync("https://dmrbolivia.com/api_distribuidora/productos/listaProductoNombres.php");
                var prodsList = JsonConvert.DeserializeObject<List<ProductoNombre>>(response).ToList();
                foreach(var item in prodsList)
                {
                    prods.Add(item);
				}
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.ToString(), "OK");
            }
        }

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
            //int x = 0;
            //for (int r = 0; r < contcampos; r++)
            //{
            //    x = x + 1;
            //    MontoTotal = MontoTotal + Convert.ToDecimal(subtArr[x]);
            //}
            //totalVentaEntry.Text = MontoTotal.ToString();
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
        string pickedTP;
		private async void picker_TP_SelectedIndexChanged(object sender, EventArgs e)
		{
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if(selectedIndex != -1)
			{
                pickedTP = picker.Items[selectedIndex];
			}
            try
			{
                foreach (var item in prods)
				{
                    if(item.nombre_tipo_producto == pickedTP)
					{
                        picker_Producto.Items.Clear();
                        picker_Producto.Items.Add(item.display_text_nombre);
					}
				}
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.ToString(), "OK");
            }
            
		}
        string pickedProducto;
        private async void picker_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var picker = (Picker)sender;
                int selectedIndex = picker.SelectedIndex;

                if (selectedIndex != -1)
                {
                    pickedProducto = picker.Items[selectedIndex];
                    try
                    {
                        foreach(var item in prods)
						{
                            if (pickedProducto == item.display_text_nombre)
							{
                                txtPrecio.Text = item.precio_venta.ToString("0.##");
                                idProductoSelected = item.id_producto;
							}
						}
					}
                    catch (Exception err)
                    {
                        await DisplayAlert("ERROR", err.ToString(), "OK");
                    }
                }
            }
            catch (Exception err)
            {
                await DisplayAlert("ERROR", err.ToString(), "OK");
            }
        }
        decimal precioSelected = 0;
        int cantidaSelected = 0;
        decimal descuentoSelected = 0;
        decimal precioFinalSelected = 0;
        decimal subTotalSelected = 0;
		private async void txtDescuento_Completed(object sender, EventArgs e)
		{
            try
			{
                precioSelected = Convert.ToDecimal(txtPrecio.Text);
                cantidaSelected = Convert.ToInt32(txtCantidad.Text);
                descuentoSelected = Convert.ToDecimal(txtDescuento.Text);
                precioFinalSelected = precioSelected - descuentoSelected;
                subTotalSelected = precioFinalSelected * cantidaSelected;
                txtSubTotal.Text = subTotalSelected.ToString();
            }
            catch(Exception err)
			{
                await DisplayAlert("ERROR", err.ToString(), "OK");
			}
		}
		private async void agregarAlista_Clicked(object sender, EventArgs e)
		{
            try
			{
                App._detalleVData.Add(new DetalleVenta_previo
                {
                    cantidad = cantidaSelected,
                    id_producto = idProductoSelected,
                    nombre_producto = pickedTP + " " + pickedProducto,
                    precio_producto = precioSelected,
                    descuento = descuentoSelected,
                    sub_total = subTotalSelected,
                    factura = Convert.ToInt32(numero_facturaVentaEntry.Text)
                });
			}
            catch(Exception err)
			{
                await DisplayAlert("Erro", err.ToString(), "OK");
            }
		}
	}
    }
