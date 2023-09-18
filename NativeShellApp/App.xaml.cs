using NativeShell.Pages;

namespace SocialMailApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NativeShellMainPage() {
			
		};
	}
}
