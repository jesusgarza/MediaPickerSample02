using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Media;
using Android.Content;
using Android.App;
//using Media.Plugin;

[assembly: Xamarin.Forms.Dependency (typeof (MediaPickerSample02.Droid.TestViewModel))]

namespace MediaPickerSample02.Droid
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
			//await SelectPicture ();
			await SelectPicture2 ();

			return;
		}

//		/// <summary>
//		/// Selects the picture.
//		/// </summary>
//		/// <returns>Select Picture Task.</returns>
//		private async Task SelectPicture()
//		{
//			try
//			{
//				if (!CrossMedia.Current.IsPickPhotoSupported)
//				{
//					Debug.WriteLine("IsPickPhotoSupported false");
//					return;
//				}
//
//				var mediaFile = await CrossMedia.Current.PickPhotoAsync();
//
//				Debug.WriteLine("SelectPicture.Path." + mediaFile.Path);
//
//			}
//			catch (System.Exception ex)
//			{
//				Debug.WriteLine ("SelectPicture.Exception. " + ex.Message);
//			}
//		}

		/// <summary>
		/// Selects the picture.
		/// </summary>
		/// <returns>Select Picture Task.</returns>
		private async Task SelectPicture2()
		{
			try
			{
				var activity = Forms.Context as Activity;

				var picker = new MediaPicker(activity);

				if (!picker.PhotosSupported) {
					Debug.WriteLine("PhotosSupported false");
					return;
				}

				Intent intent = picker.GetPickPhotoUI();

				activity.StartActivityForResult (intent, 2);
			}
			catch (System.Exception ex)
			{
				Debug.WriteLine ("SelectPicture2.Exception. " + ex.Message);
			}
		}

		public async Task OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// User canceled
			if (resultCode == Result.Canceled)
				return;

			Context context = Forms.Context;
			var mediaFile = await data.GetMediaFileExtraAsync(context);
			Debug.WriteLine("SelectPicture2.Path." + mediaFile.Path);

//			data.GetMediaFileExtraAsync (this).ContinueWith (t => {
//				if (requestCode == 2) { // Image request
//					Debug.WriteLine("SelectPicture2.Path." + t.Result.Path);				
//				}
//			}, TaskScheduler.FromCurrentSynchronizationContext());
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

