using DistribuidoraFabio.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace DistribuidoraFabio.ViewModels
{
	public class KardexVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}
		private ObservableCollection<Producto> _listaDeProducto;
		public ObservableCollection<Producto> ListaDeProducto
		{
			get { return _listaDeProducto; }
			set
			{
				if (_listaDeProducto != value)
				{
					_listaDeProducto = value;
					OnPropertyChanged("ListaDeProducto");
				}
			}
		}
		
		public KardexVM(int id_producto)
		{

		}
	}
}
