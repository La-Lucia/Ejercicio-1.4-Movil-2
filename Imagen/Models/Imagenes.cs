using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Imagen.Models
{
    public class Imagenes
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }

        [MaxLength(60)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] imagen { get; set; }

    }
}
