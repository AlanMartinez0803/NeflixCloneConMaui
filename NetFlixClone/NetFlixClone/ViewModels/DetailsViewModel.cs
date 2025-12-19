using CommunityToolkit.Mvvm.ComponentModel;
using NetFlixClone.Models;
using NetFlixClone.Services;


namespace NetFlixClone.ViewModels
{
    [QueryProperty (nameof(Media), nameof(Media))]
    public partial class DetailsViewModel : ObservableObject
    {
        private readonly tmdbService _tmdbService;
        public ModeloMedia Media { get; set; }
        public DetailsViewModel(tmdbService tmdbservice)
        {
            _tmdbService= tmdbservice;
        }
    }

}
