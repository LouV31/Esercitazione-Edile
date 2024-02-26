using System;

namespace Esercitazione_Edile.Models
{
    public class Pagamento
    {
        private int idPagamento;
        public int IdPagamento { get => idPagamento; set => idPagamento = value; }

        private int idDipendente;
        public int IdDipendente { get => idDipendente; set => idDipendente = value; }

        private DateTime dataPagamento;
        public DateTime DataPagamento { get => dataPagamento; set => dataPagamento = value; }

        private double importo;
        public double Importo { get => importo; set => importo = value; }

        private string tipoPagamento;
        public string TipoPagamento { get => tipoPagamento; set => tipoPagamento = value; }


        public Pagamento(int idPagamento, int idDipendente, DateTime dataPagamento, double importo, string tipoPagamento)
        {
            IdPagamento = idPagamento;
            IdDipendente = idDipendente;
            DataPagamento = dataPagamento;
            Importo = importo;
            TipoPagamento = tipoPagamento;
        }

        public Pagamento()
        {

        }
    }
}