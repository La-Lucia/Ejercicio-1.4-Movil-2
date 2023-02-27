using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Imagen.Models;
using Plugin.Media;
using Imagen.Views;

namespace Imagen
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        byte[] imageToSave;

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_foto.jpg",
                    SaveToAlbum = true
                });

                await DisplayAlert("Foto Guardada", "Foto Guardada en la Galeria", "Aceptar");
                
                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    img.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                    
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Se ha generado el siguiente error al agregar la imagen " + ex, "Aceptar");
            }

        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (imageToSave == null)
            {
                await DisplayAlert("Aviso", "Capture una imagen", "OK");
            }
            else if (Descripcion.Text == null)
            {
                await DisplayAlert("Aviso", "Ingrese una descripcion", "OK");
            }
            else if (Nombre.Text == null)
            {
                await DisplayAlert("Aviso", "Ingrese un nombre", "OK");
            }
            else
            {
                var imagen = new Imagenes { Nombre = Nombre.Text, Descripcion = Descripcion.Text, imagen = imageToSave };
                var resultado = await App.DB.GuardarImagen(imagen);

                if (resultado != 0)
                {
                    await DisplayAlert("Aviso", "¡Imagen ingresada correctamente!", "OK");
                    Nombre.Text = "";
                    Descripcion.Text = "";
                    img.Source = "dslrcamera.png";
                    imageToSave = null;

                }
                else
                {
                    await DisplayAlert("Aviso", "Ha Ocurrido un Error", "OK");
                }


                await Navigation.PopAsync();
            }

        }

        private async void Lista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaImagen());
        }
    }
}
