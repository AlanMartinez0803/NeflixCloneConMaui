using NetFlixClone.ViewModels;


namespace NetFlixClone.Views;

public partial class CategoriasPage : ContentPage
{
	private readonly CategoriasViewModel _categoriaViewModel;

	public CategoriasPage(CategoriasViewModel categoriaViewModel)
	{
		InitializeComponent();
		_categoriaViewModel = categoriaViewModel;
		BindingContext = _categoriaViewModel;
	}
	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _categoriaViewModel.InitializeAsync();
	}

    private async void Cerrar_Categoria(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}