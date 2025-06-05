// Pages/EspeciesPage.xaml.cs
using VetZone.Models;
using VetZone.Services;
using System.Collections.ObjectModel;

namespace VetZone;

public partial class EspeciesPage : ContentPage
{
    private EspecieService _especieService;
    private ObservableCollection<Especie> _especies;

    public EspeciesPage()
    {
        InitializeComponent();
        _especieService = new EspecieService();
        _especies = new ObservableCollection<Especie>();
        EspeciesCollectionView.ItemsSource = _especies; // Associa a cole��o ao CollectionView
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadEspeciesAsync(); // Carrega as esp�cies toda vez que a p�gina aparece
    }

    private async Task LoadEspeciesAsync()
    {
        var especies = await _especieService.GetEspeciesAsync();
        _especies.Clear();
        foreach (var especie in especies)
        {
            _especies.Add(especie);
        }
    }

    private async void OnAdicionarEspecieClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroEspeciePage()); // Navega para a p�gina de cadastro vazia
    }

    private async void OnEditarEspecieClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Especie especie)
        {
            // Navega para a p�gina de cadastro/edi��o, passando a esp�cie para edi��o
            await Navigation.PushAsync(new CadastroEspeciePage(especie));
        }
    }

    private async void OnExcluirEspecieClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Especie especie)
        {
            bool confirm = await DisplayAlert("Confirma��o", $"Tem certeza que deseja excluir a esp�cie {especie.Nome}?", "Sim", "N�o");
            if (confirm)
            {
                await _especieService.DeleteEspecieAsync(especie);
                await LoadEspeciesAsync(); // Recarrega a lista ap�s a exclus�o
            }
        }
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
    }
}