using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DistribuidoraFabio
{
	public partial class App : Application
	{
		public static NavigationPage NavigationPage { get; private set; }
		public App()
		{
			InitializeComponent();
			MainPage = new Menu();
			//MainPage = new NavigationPage(new Menu());
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
