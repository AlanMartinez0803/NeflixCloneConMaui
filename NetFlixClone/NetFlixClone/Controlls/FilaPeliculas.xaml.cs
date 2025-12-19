using NetFlixClone.Models;
using System.Windows.Input;

namespace NetFlixClone.Controlls;

public class MediaSelectedEventArgs : EventArgs
{
    public ModeloMedia Media { get; set; }
    public MediaSelectedEventArgs(ModeloMedia media) => Media = media;
}

public partial class FilaPeliculas : ContentView
{
	public static readonly BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), 
		typeof(string), typeof(FilaPeliculas), string.Empty);
    public static readonly BindableProperty MoviesProperty = BindableProperty.Create(nameof(Movies),
        typeof(IEnumerable<ModeloMedia>), typeof(FilaPeliculas), Enumerable.Empty<ModeloMedia>());
    public static readonly BindableProperty IsLargeProperty = BindableProperty.Create(nameof(IsLarge),
        typeof(bool), typeof(FilaPeliculas), false);
    public event EventHandler<MediaSelectedEventArgs> MediaSelected;
    public FilaPeliculas()
	{
		InitializeComponent();
        MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
	}
    public string Heading
    {
        get => (string)GetValue(FilaPeliculas.HeadingProperty);
        set => SetValue(FilaPeliculas.HeadingProperty, value);
    }
    public IEnumerable<ModeloMedia> Movies
    {
        get => (IEnumerable<ModeloMedia>)GetValue(FilaPeliculas.MoviesProperty);
        set => SetValue(FilaPeliculas.MoviesProperty, value);
    }
    public bool IsLarge
    {
        get => (bool)GetValue(FilaPeliculas.IsLargeProperty);
        set => SetValue(FilaPeliculas.IsLargeProperty, value);
    }
    public bool IsNotLarge => !IsLarge;
    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand (object parameters)
    {
        if (parameters is ModeloMedia  media && media is not null)
        {
            MediaSelected?.Invoke(this, new MediaSelectedEventArgs(media));
        }
    }
}