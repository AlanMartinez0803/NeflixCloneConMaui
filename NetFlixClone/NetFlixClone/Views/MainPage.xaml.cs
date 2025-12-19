using NetFlixClone.Services;
using NetFlixClone.ViewModels;
using System.Threading.Tasks;

namespace NetFlixClone.Views;

public partial class MainPage : ContentPage
{
    
    private readonly HomeViewModel _homeviewmodel;
    public MainPage(HomeViewModel homeviewmodel)
    {
        InitializeComponent();
        _homeviewmodel = homeviewmodel;
        BindingContext = _homeviewmodel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _homeviewmodel.InitializeAsync();

    }

  

    private void FilaPeliculas_MediaSelected(object sender, Controlls.MediaSelectedEventArgs e)
    {
        _homeviewmodel.PeliculaSeleccionadaCommand.Execute(e.Media);
    }

    private void MovieInfoBox_Closed(object sender, EventArgs e)
    {
        _homeviewmodel.PeliculaSeleccionadaCommand.Execute(null);
    }

    private async void CategoriasMenu_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CategoriasPage));
    }
}
