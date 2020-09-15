﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DistribuidoraFabio
{

	public class MenuMasterMenuItem
	{
		public MenuMasterMenuItem()
		{
			TargetType = typeof(MenuMasterMenuItem);
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public ImageSource icon { get; set; }
		public Type TargetType { get; set; }
	}
}