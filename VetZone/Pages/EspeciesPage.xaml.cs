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
        EspeciesCollectionView.ItemsSource = _especies; // Associa a coleção ao CollectionView
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadEspeciesAsync(); // Carrega as espécies toda vez que a página aparece
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
        await Navigation.PushAsync(new CadastroEspeciePage()); // Navega para a página de cadastro vazia
    }

    private async void OnEditarEspecieClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Especie especie)
        {
            // Navega para a página de cadastro/edição, passando a espécie para edição
            await Navigation.PushAsync(new CadastroEspeciePage(especie));
        }
    }

    private async void OnExcluirEspecieClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Especie especie)
        {
            bool confirm = await DisplayAlert("Confirmação", $"Tem certeza que deseja excluir a espécie {especie.Nome}?", "Sim", "Não");
            if (confirm)
            {
                await _especieService.DeleteEspecieAsync(especie);
                await LoadEspeciesAsync(); // Recarrega a lista após a exclusão
            }
        }
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
    }
}