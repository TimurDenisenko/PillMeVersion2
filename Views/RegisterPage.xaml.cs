using PillMe.Models;

namespace PillMe.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
    private async void Cancel(object sender, EventArgs e) =>
        await Navigation.PopAsync();
    private async void SaveUser(object sender, EventArgs e)
    {
        try
        {
            Tuple<string, byte[]> a = PasswordSecurity.HashPassword(pass.Text);
            User User = new User
            {
                Name = nimi.Text,
                HashPassword = a.Item1,
                Role = "User",
                Salt = a.Item2
            };
            App.Database.SaveUser(User);
            await Navigation.PopAsync();
        }
        catch (Exception d)
        {
            await DisplayAlert("Viga", d.Message, "Tühista");
        }
    }
}