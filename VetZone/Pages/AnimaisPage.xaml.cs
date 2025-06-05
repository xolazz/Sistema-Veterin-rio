using VetZone.Models;
using VetZone.Services;
using System.Collections.ObjectModel;

namespace VetZone;

public partial class AnimaisPage : ContentPage
{
    private AnimalService _animalService;
    private ObservableCollection<Animal> _animais;

    public AnimaisPage()
    {
        InitializeComponent();
        _animalService = new AnimalService();
        _animais = new ObservableCollection<Animal>();
        AnimaisCollectionView.ItemsSource = _animais;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAnimaisAsync();
    }

    private async Task LoadAnimaisAsync()
    {
        var animais = await _animalService.GetAnimaisAsync();
        _animais.Clear();
        foreach (var animal in animais)
        {
            _animais.Add(animal);
        }
    }

    private async void OnAdicionarAnimalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroAnimalPage());
    }

    private async void OnEditarAnimalClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Animal animal)
        {
            await Navigation.PushAsync(new CadastroAnimalPage(animal));
        }
    }

    private async void OnExcluirAnimalClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Animal animal)
        {
            bool confirm = await DisplayAlert("Confirmação", $"Tem certeza que deseja excluir o animal {animal.Nome}?", "Sim", "Não");
            if (confirm)
            {
                await _animalService.DeleteAnimalAsync(animal);
                await LoadAnimaisAsync();
            }
        }
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}