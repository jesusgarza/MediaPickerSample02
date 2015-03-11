using System;

using Xamarin.Forms;

namespace MediaPickerSample02
{
	public class App : Application
	{
		public App ()
		{
			ITestViewModel vm = DependencyService.Get<ITestViewModel> ();

			Page page = new MyTestPage ();

			var page2 = new NavigationPage (page);

			page.BindingContext = vm;

			MainPage = page2;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

