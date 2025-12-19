

using NetFlixClone.Models;
using System.Net.Http.Json;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace NetFlixClone.Services
{
    public class tmdbService
    {
        private const string ApiKey = "d68de34312ac5ec0c6078d15e87532c6";//Insertar key de tmdb Api
        public const string TmdbClientHttps = "TmdbClient"; //LogClient
        private readonly IHttpClientFactory _httpClientFactory;
        public tmdbService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
      


        private HttpClient httpcliente => _httpClientFactory.CreateClient(TmdbClientHttps);
        public async Task <IEnumerable<Genre>> GetGenreAsync()
        {
            var genrewrapped = await httpcliente.GetFromJsonAsync<GenreWrapper>($"{tmdbUrl.MovieGenres}&api_key={ApiKey}");
            return genrewrapped.Genres;
        }

        public async Task<IEnumerable<ModeloMedia>> GetTrendingAscyc() => await GetMediaAsync(tmdbUrl.Tendencias);

        public async Task<IEnumerable<ModeloMedia>> GetOriginal() => await GetMediaAsync(tmdbUrl.OriginalesNetflix);

        public async Task<IEnumerable<ModeloMedia>> GetDestacados() => await GetMediaAsync(tmdbUrl.Destacado);
      
        public async Task<IEnumerable<ModeloMedia>> GetAction()=> await GetMediaAsync(tmdbUrl.Action);

        private async Task<IEnumerable <ModeloMedia>> GetMediaAsync(string url)
        {
            var trendingMovieCollection = await httpcliente.GetFromJsonAsync<Movie>($"{url}&api_key={ApiKey}");
            return trendingMovieCollection.results.Select(r => r.ObjetosMedia());   
        }

    }
   
    public static class tmdbUrl 
    {
        public const string Tendencias = "3/trending/all/week?language=en-US";
        public const string OriginalesNetflix = "3/discover/tv?language=en-US&with_networks=213";
        public const string Destacado = "3/movie/top_rated?language=en-US";
        public const string Action = "3/discover/movie?language=en-US&with_genres=28";
        public const string MovieGenres = "3/genre/movie/list?language=en-US";

        public static string GetTrailers(int movieID, string Type="movie")=> $"3/{Type ?? "movie"}/{movieID}/videos?language=en-US";
        public static string GetMovieDetails(int movieID, string Type = "movie") => $"3/{Type ?? "movie"}/{movieID}?language=en-US";
        public static string GetSimilar(int movieID, string Type = "movie") => $"3/{Type ?? "movie"}/{movieID}/similar?language=en-US";
        
    }
    public class Movie
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result
    {
        public string backdrop_path { get; set; }
        public int[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; } // "movie" or "tv"
        public string ThumbnailPath => poster_path ?? backdrop_path;
        public string Thumbnail => $"https://image.tmdb.org/t/p/w600_and_h900_bestv2/{ThumbnailPath}";
        public string ThumbnailSmall => $"https://image.tmdb.org/t/p/w220_and_h330_face/{ThumbnailPath}";
        public string ThumbnailUrl => $"https://image.tmdb.org/t/p/original/{ThumbnailPath}";
        public string DisplayTitle => title ?? name ?? original_title ?? original_name;

        public ModeloMedia ObjetosMedia() =>
            new ()
            {
                ID = id,
                DisplayTitlex = DisplayTitle,
                MediaType = media_type,
                Overview = overview,
                ReleaseDate = release_date,
                Thumbnail = Thumbnail,
                ThumbnailSmall = ThumbnailSmall,
                ThumbnailUrl = ThumbnailUrl
            };
       
          
    }


    public class VideosWrapper
    {
        public int id { get; set; }
        public Video[] results { get; set; }

        public static Func<Video, bool> FilterTrailerTeasers => v =>
            v.official
            && v.site.Equals("Youtube", StringComparison.OrdinalIgnoreCase)
            && (v.type.Equals("Teaser", StringComparison.OrdinalIgnoreCase) || v.type.Equals("Trailer", StringComparison.OrdinalIgnoreCase));
    }

    public class Video
    {
        public string name { get; set; }
        public string key { get; set; }
        public string site { get; set; }
        public string type { get; set; }
        public bool official { get; set; }
        public DateTime published_at { get; set; }
        public string Thumbnail => $"https://i.ytimg.com/vi/{key}/mqdefault.jpg";
    }


    public class MovieDetail
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string english_name { get; set; }
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }
    public class GenreWrapper
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
    public record struct Genre(int Id, string Name);
}
