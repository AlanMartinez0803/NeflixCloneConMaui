using NetFlixClone.Views;

namespace NetFlixClone
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoriasPage), typeof(CategoriasPage));
            //Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        }
    }
}
