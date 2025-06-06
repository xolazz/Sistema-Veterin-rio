﻿using SQLite;

namespace VetZone.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}