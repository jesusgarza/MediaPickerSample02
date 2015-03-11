using System;
using Xamarin.Forms;

namespace MediaPickerSample02
{
	public interface ITestViewModel
	{
		string TextMessage
		{
			get;
			set;
		}

		Command CmdToVerifyIsWorking
		{
			get;
		}

		Command CmdToUseMediaPicker
		{
			get;
		}
	}
}

