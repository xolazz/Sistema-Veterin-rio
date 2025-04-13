namespace VetZone;

public partial class CadastroUsuarioPage : ContentPage
{
    public CadastroUsuarioPage()
    {
        InitializeComponent();
    }

    public DateTime SelectedDate { get; set; } = DateTime.Now;

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}