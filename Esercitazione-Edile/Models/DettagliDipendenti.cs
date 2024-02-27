using System;

namespace Esercitazione_Edile.Models
{
    public class DettagliDipendenti
    {
        // SELECT D.nome, D.cognome, P.dataPagamento, P.importo, P.tipoPagamento
        // scrivimi le priprietà della classe in base ai campi della query
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Importo { get; set; }
        public string TipoPagamento { get; set; }

        public DettagliDipendenti(string nome, string cognome, DateTime dataPagamento, decimal importo, string tipoPagamento)
        {
            Nome = nome;
            Cognome = cognome;
            DataPagamento = dataPagamento;
            Importo = importo;
            TipoPagamento = tipoPagamento;
        }

        public DettagliDipendenti()
        {
        }
    }
}