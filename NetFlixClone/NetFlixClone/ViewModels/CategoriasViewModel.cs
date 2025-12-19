using CommunityToolkit.Mvvm.ComponentModel;
using NetFlixClone.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlixClone.ViewModels
{
    public partial class CategoriasViewModel : ObservableObject
    {
        private readonly tmdbService _tmdbService;
        public ObservableCollection<string> Categorias { get; set; } = new();
        private IEnumerable<Genre> _genreslist;
        public CategoriasViewModel( tmdbService tmdbService) 
        {
          
            _tmdbService = tmdbService;
        }
       

        public async Task InitializeAsync()
        {
            _genreslist ??= await _tmdbService.GetGenreAsync();
            Categorias.Clear();
            Categorias.Add("Mis listas");
            Categorias.Add("Descargas");

            foreach ( var genero in _genreslist)
            {
                Categorias.Add(genero.Name);
            }
        }
    }
}
