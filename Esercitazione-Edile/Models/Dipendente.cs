namespace Esercitazione_Edile.Models
{
    public class Dipendente
    {
        private int idDipendente;
        public int IdDipendente { get => idDipendente; set => idDipendente = value; }

        private string nome;
        public string Nome { get => nome; set => nome = value; }

        private string cognome;
        public string Cognome { get => cognome; set => cognome = value; }

        private string indirizzo;
        public string Indirizzo { get => indirizzo; set => indirizzo = value; }

        private string codiceFiscale;
        public string CodiceFiscale { get => codiceFiscale; set => codiceFiscale = value; }

        private bool spostato;
        public bool Spostato { get => spostato; set => spostato = value; }

        private int figliACarico;
        public int FigliACarico { get => figliACarico; set => figliACarico = value; }

        public Dipendente(int idDipendente, string nome, string cognome, string indirizzo, string codiceFiscale, bool spostato, int figliACarico)
        {
            IdDipendente = idDipendente;
            Nome = nome;
            Cognome = cognome;
            Indirizzo = indirizzo;
            CodiceFiscale = codiceFiscale;
            Spostato = spostato;
            FigliACarico = figliACarico;
        }

        public Dipendente()
        {

        }
    }
}