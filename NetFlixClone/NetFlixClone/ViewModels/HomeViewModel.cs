using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetFlixClone.Models;
using NetFlixClone.Services;
using System.Collections.ObjectModel;


namespace NetFlixClone.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly tmdbService _tmdService;
        public HomeViewModel(tmdbService tmdbService )
        { 
        _tmdService = tmdbService;
        }
        [ObservableProperty]
        private ModeloMedia _trendingMovie;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MostrarInfoBox))]
        private ModeloMedia? _seleccionPelicula;

        public bool MostrarInfoBox => SeleccionPelicula is not null;


        public ObservableCollection<ModeloMedia> Trending { get; set; } = new();
        public ObservableCollection<ModeloMedia> Destacado {  get; set; } = new();
        public ObservableCollection<ModeloMedia> OriginalesNetflix { get; set; } = new();
        public ObservableCollection<ModeloMedia> PeliculasAccion { get; set; } = new();
        public async Task InitializeAsync()
        {
            var trendingTask = _tmdService.GetTrendingAscyc();
            var destacadoTask = _tmdService.GetDestacados();
            var OriginalTask = _tmdService.GetOriginal();
            var AccionTask = _tmdService.GetAction();
            var media = await Task.WhenAll(trendingTask, destacadoTask, OriginalTask, AccionTask);

            var trandingList = media[0];
            var DestacadoList = media[1];
            var OriginalList = media[2];
            var AccionList = media[3];

            //Genera peliculas trending a la lista de tranding.
            TrendingMovie = trandingList.OrderBy(t => Guid.NewGuid())
                .FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.DisplayTitlex) &&
                    !string.IsNullOrWhiteSpace(x.Thumbnail));


            SetMediaCollection(trandingList, Trending);
            SetMediaCollection(DestacadoList, Destacado);
            SetMediaCollection(OriginalList, OriginalesNetflix);
            SetMediaCollection(AccionList, PeliculasAccion);

            //SeleccionPelicula = TrendingMovie;
        }


        private void SetMediaCollection(IEnumerable<ModeloMedia> medias, ObservableCollection<ModeloMedia> collection)
        {
            collection.Clear();
            foreach (var media in medias)
            {
                collection.Add(media);
            }

        }
        [RelayCommand]
        private void PeliculaSeleccionada(ModeloMedia? Media = null) 
        
        {
            if (Media is not null)
            {
                if (Media.ID == SeleccionPelicula?.ID)
                {
                    Media = null;
                }
            }

            SeleccionPelicula = Media;
        }  
    }

}
