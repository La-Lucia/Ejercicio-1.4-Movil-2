using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Imagen.Models;
using SQLite;
using Xamarin.Forms;

namespace Imagen.Controllers
{
    public class DB
    {
        readonly SQLiteAsyncConnection conexion;

        // Constructor Vacio

        public DB(String dbpath)
        {
            conexion = new SQLiteAsyncConnection(dbpath);

            // Creacion de Objetos de Base de dartos
            conexion.CreateTableAsync<Models.Imagenes>().Wait();
        }

        // CRUD - Create / Read / Update/ Delete

        public Task<int> GuardarImagen(Models.Imagenes imagen)
        {
            if (imagen.Id == 0)
            {
                return conexion.InsertAsync(imagen);
            }
            else
            {
                return conexion.UpdateAsync(imagen);
            }
        }

        // Read - List

        public Task<List<Models.Imagenes>> ListaImagen()
        {
            return conexion.Table<Models.Imagenes>().ToListAsync();
        }

        // Get 
        public Task<Models.Imagenes> GetImagen(int pid)
        {
            return conexion.Table<Models.Imagenes>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

    }
}
