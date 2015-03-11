using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Diagnostics;
using Media.Plugin;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency (typeof (MediaPickerSample02.iOS.TestViewModel))]

namespace MediaPickerSample02.iOS
{
	public class TestViewModel : ITestViewModel, INotifyPropertyChanged
	{
		public TestViewModel ()
		{
		}

		private string _textMessage = "My Test";
		public string TextMessage
		{
			get
			{
				return _textMessage;
			}
			set
			{
				if (value != _textMessage) 
				{
					_textMessage = value;
					OnPropertyChanged ("TextMessage");
				}
			}
		}

		private Command _cmdToVerifyIsWorking;
		public Command CmdToVerifyIsWorking 
		{
			get
			{
				return _cmdToVerifyIsWorking ?? (_cmdToVerifyIsWorking = new Command(
					(m) => VerifyIsWoking(),
					(o) => true));
			}
		}

		private void VerifyIsWoking()
		{
			this.TextMessage = "YES! It seems to be working!!";
			return;
		}

		/*************************************************************************************/

		private Command _cmdToUseMediaPicker;
		public Command CmdToUseMediaPicker 
		{
			get
			{
				return _cmdToUseMediaPicker ?? (_cmdToUseMediaPicker = new Command(
					async (m) => await UseMediaPicker(),
					(o) => true));
			}
		}

		private async Task UseMediaPicker()
		{
			await SelectPicture ();

			return;
		}

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>Select Picture Task.</returns>
		private async Task SelectPicture()
		{
			try
			{
				if (!CrossMedia.Current.IsPickPhotoSupported)
				{
					Debug.WriteLine("IsPickPhotoSupported false");
					return;
				}

				var mediaFile = await CrossMedia.Current.PickPhotoAsync();

				Debug.WriteLine("SelectPicture.Path." + mediaFile.Path);

			}
			catch (System.Exception ex)
			{
				Debug.WriteLine ("SelectPicture.Exception. " + ex.Message);
			}
		}


		/*************************************************************************************/


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
		}
	}

}

