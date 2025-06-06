using SQLite;
using System;

namespace VetZone.Models
{
    public class Animal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }

        public string FotoPath { get; set; }
        public int EspecieId { get; set; }
        [Ignore]
        public Especie Especie { get; set; }

        public int ClienteId { get; set; } 
        [Ignore] 
        public Cliente Cliente { get; set; }

        public int IdadeAproximada { get; set; }
        public string SituacaoClinica { get; set; }
    }
}