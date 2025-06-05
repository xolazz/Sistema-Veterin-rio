using SQLite;
using System;

namespace VetZone.Models
{
    public class Especie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

    }
}