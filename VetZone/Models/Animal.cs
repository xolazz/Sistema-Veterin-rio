using SQLite;
using System;

namespace VetZone.Models
{
    public class Animal
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }

        // Caminho da imagem no armazenamento local
        public string FotoPath { get; set; }

        // Id da espécie relacionada (chave estrangeira)
        public int EspecieId { get; set; }

        [Ignore] // Indica ao SQLite-net-pcl para ignorar esta propriedade ao mapear para a tabela.
                 // Usaremos esta propriedade para carregar o objeto Especie completo.
        public Especie Especie { get; set; }

        public int IdadeAproximada { get; set; } // Em anos, meses, ou como preferir
        public string SituacaoClinica { get; set; } // Texto descritivo

        // Você pode adicionar um campo para o ClienteId aqui se cada animal pertencer a um cliente
        // public int ClienteId { get; set; }
        // [Ignore]
        // public Cliente Cliente { get; set; }
    }
}