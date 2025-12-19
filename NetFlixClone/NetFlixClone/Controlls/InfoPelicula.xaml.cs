using NetFlixClone.Models;
using System.Windows.Input;

namespace NetFlixClone.Controlls;

public partial class InfoPelicula : ContentView
{
	public static BindableProperty MediaProperty = BindableProperty.Create(nameof(Media), typeof(ModeloMedia), typeof(InfoPelicula), null);
	public event EventHandler Cerrar;
	public InfoPelicula()
	{
		InitializeComponent();
		CloseCommand = new Command(ExecuteCloseCommand);
	}
	public ModeloMedia Media
	{
		get => (ModeloMedia)GetValue(InfoPelicula.MediaProperty);
		set => SetValue(InfoPelicula.MediaProperty, value);
	}
	public ICommand CloseCommand { get; private set; }
	private void ExecuteCloseCommand() =>
		Cerrar?.Invoke(this, EventArgs.Empty);

    private void Button_Clicked(object sender, EventArgs e)
    {
        Cerrar?.Invoke(this, EventArgs.Empty);
    }

    private async void  GestorTap_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync();
    }
}