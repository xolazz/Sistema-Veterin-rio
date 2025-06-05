// Models/Cliente.cs
using SQLite;
using System;

namespace VetZone.Models
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; } // Alterado para DateTime
    }
}