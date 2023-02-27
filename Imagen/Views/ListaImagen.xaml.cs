using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imagen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaImagen : ContentPage
    {
        public ListaImagen()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            lista.ItemsSource = await App.DB.ListaImagen();
        }

        private async void lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.Imagenes imagen = lista.SelectedItem as Models.Imagenes;

            // Verificar que el objeto no es nulo
            if (imagen != null)
            {
                // Navegar a la pantalla DetalleImagen enviando la imagen seleccionada como parámetro
                var pagina = new Views.Imagen();
                pagina.BindingContext = imagen;
                await Navigation.PushAsync(pagina);

            }

        }

    }
}