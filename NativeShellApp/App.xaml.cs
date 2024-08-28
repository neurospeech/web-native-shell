using NativeShell.Pages;

namespace SocialMailApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var mp = new NativeShellMainPage() {
			Url = "https://test.800casting.com/ProfileEditor/Agency"
		};
		mp.WebView.UserAgent = "800Casting-Hybrid-Mobile-App/1.0 (Android;)";


        MainPage = mp;
	}
}
