using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuMaster : ContentPage
	{
		public ListView ListView;

		public MenuMaster()
		{
			InitializeComponent();

			BindingContext = new MenuMasterViewModel();
			ListView = MenuItemsListView;
		}

		class MenuMasterViewModel : INotifyPropertyChanged
		{
			public ObservableCollection<MenuMasterMenuItem> MenuItems { get; set; }

			public MenuMasterViewModel()
			{
				MenuItems = new ObservableCollection<MenuMasterMenuItem>(new[]
				{
					new MenuMasterMenuItem { Id = 0, Title = "Inicio", icon="icon_inventario.png" },
					new MenuMasterMenuItem { Id = 1, Title = "Inventario", TargetType = typeof(Inventario.InventarioGeneral), icon="icon_venta.png" },
					new MenuMasterMenuItem { Id = 2, Title = "Ventas", TargetType = typeof(Venta.ListaVenta), icon="icon_compra.png" },
					new MenuMasterMenuItem { Id = 3, Title = "Compras", icon="icon_producto.png" },
					new MenuMasterMenuItem { Id = 4, Title = "Producto", icon="icon_cliente.png" },
					new MenuMasterMenuItem { Id = 5, Title = "Cliente", TargetType = typeof(Cliente.AgregarCliente), icon="icon_cliente.png"},
					new MenuMasterMenuItem { Id = 6, Title = "Proveedor", icon="icon_proveedor.png" },
					new MenuMasterMenuItem { Id = 7, Title = "Vendedor", icon="icon_vendedor.png" },
					new MenuMasterMenuItem { Id = 8, Title = "Finanzas", icon="fina.png" },
					new MenuMasterMenuItem { Id = 9, Title = "Agenda", icon="agendaicono.png" },
					new MenuMasterMenuItem { Id = 10, Title = "Scaner", icon="icon_app.png" },
				});
			}

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
			void OnPropertyChanged([CallerMemberName] string propertyName = "")
			{
				if (PropertyChanged == null)
					return;

				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
			#endregion
		}
	}
}