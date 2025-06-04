namespace VetZone;

public partial class CadastroEspeciePage : ContentPage
{
    public CadastroEspeciePage()
    {
        InitializeComponent();
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

}