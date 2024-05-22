using PillMe.Models;
using Plugin.LocalNotification;

namespace PillMe.Views;

public partial class MainPage : ContentPage
{
    Entry password, login;
    CheckBox passwordVisibleBox;
    Label passwordVisible, passwordLabel, loginLabel;
    Button sisse, withoutacc, reg;
    public static User user { get; set; }

    [Obsolete]
    public MainPage()
    {
        loginLabel = new Label { Text = "Kasutaja nimi"};
        passwordLabel = new Label{ Text = "Parool"};
        passwordVisible = new Label { Text = "Näita parool" };
        login = new Entry();
        password = new Entry { IsPassword = true };
        passwordVisibleBox = new CheckBox { };
        passwordVisibleBox.CheckedChanged += PasswordVisibleBox_CheckedChanged;
        sisse = new Button { Text = "Logi sisse" };
        withoutacc = new Button { Text = "Külaline" };
        reg = new Button { Text = "Registreerimine" };
        sisse.Clicked += Sisse_Clicked;
        withoutacc.Clicked += Withoutacc_ClickedAsync;
        reg.Clicked += Reg_Clicked;
        Content = new StackLayout
        {
            Children = { loginLabel, login, passwordLabel, password, new HorizontalStackLayout { Children = { passwordVisible, passwordVisibleBox } }, sisse, withoutacc,reg }
        };
	}

    private async void Reg_Clicked(object? sender, EventArgs e) =>
        await Navigation.PushAsync(new RegisterPage());

    private async void Withoutacc_ClickedAsync(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ReminderListPage());
        user = new User
        {
            Name = "null"
        };
    }

    [Obsolete]
    private async void Sisse_Clicked(object sender, EventArgs e)
    {
        try
        {
            user = App.Database.GetUsers().Where(x => x.Name == login.Text).ToArray()[0];
            if (user != null && PasswordSecurity.VerifyPassword(password.Text, user.HashPassword, user.Salt))
                await Navigation.PushAsync(new ReminderListPage());
        }
        catch (Exception)
        {
            await DisplayAlert("Viga", "Vale kasutaja nimi või parool", "Tühista");
        }
    }

    private void PasswordVisibleBox_CheckedChanged(object sender, CheckedChangedEventArgs e) => password.IsPassword = !passwordVisibleBox.IsChecked;
}