using Esercitazione_Edile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Esercitazione_Edile.Controllers
{
    public class DipendenteController : Controller
    {
        // GET: Dipendente
        public ActionResult Index()
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            List<Dipendente> dipendenti = new List<Dipendente>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Dipendenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())

                {
                    Dipendente dipendente = new Dipendente(
                        Convert.ToInt32(reader["idDipendente"].ToString()),
                        reader["nome"].ToString(),
                        reader["cognome"].ToString(),
                        reader["indirizzo"].ToString(),
                        reader["codiceFiscale"].ToString(),
                        Convert.ToBoolean(reader["spostato"].ToString()),
                        Convert.ToInt32(reader["figliACarico"].ToString())
                    );

                    dipendenti.Add(dipendente);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(dipendenti);
        }

        [HttpGet]
        public ActionResult Create()
        {


            return View();

        }

        [HttpPost]
        public ActionResult Create(Dipendente dipendente)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                string query = "Insert into Dipendenti (nome, cognome, indirizzo, codiceFiscale, spostato, figliACarico) values (@nome, @cognome, @indirizzo, @codiceFiscale, @spostato, @figliACarico)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@codiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@spostato", dipendente.Spostato);
                cmd.Parameters.AddWithValue("@figliACarico", dipendente.FigliACarico);
                cmd.ExecuteNonQuery();



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return View();
        }




        public ActionResult Delete(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                string query = "DELETE FROM Dipendenti WHERE idDipendente = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            Dipendente dipendente = new Dipendente();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Dipendenti WHERE idDipendente = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dipendente.IdDipendente = Convert.ToInt32(reader["idDipendente"].ToString());
                    dipendente.Nome = reader["nome"].ToString();
                    dipendente.Cognome = reader["cognome"].ToString();
                    dipendente.Indirizzo = reader["indirizzo"].ToString();
                    dipendente.CodiceFiscale = reader["codiceFiscale"].ToString();
                    dipendente.Spostato = Convert.ToBoolean(reader["spostato"].ToString());
                    dipendente.FigliACarico = Convert.ToInt32(reader["figliACarico"].ToString());

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return View(dipendente);

        }

        [HttpPost]
        public ActionResult Edit(Dipendente dipendente)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();

            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                string query = "UPDATE Dipendenti SET nome = @nome, cognome = @cognome, indirizzo = @indirizzo, codiceFiscale = @codiceFiscale, spostato = @spostato, figliACarico = @figliACarico WHERE idDipendente = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", dipendente.IdDipendente);
                cmd.Parameters.AddWithValue("@nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@codiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@spostato", dipendente.Spostato);
                cmd.Parameters.AddWithValue("@figliACarico", dipendente.FigliACarico);
                Response.Write(dipendente.IdDipendente);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return View();
        }

        public ActionResult Dettagli(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            List<DettagliDipendenti> listaDettagli = new List<DettagliDipendenti>();
            try
            {
                conn.Open();
                string query = "SELECT D.nome, D.cognome, P.dataPagamento, P.importo, P.tipoPagamento FROM Dipendenti D JOIN Pagamenti P ON D.idDipendente = P.idDipendente WHERE D.idDipendente = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DettagliDipendenti dettaglio = new DettagliDipendenti(
                        reader["nome"].ToString(),
                        reader["cognome"].ToString(),
                       Convert.ToDateTime(reader["dataPagamento"]),
                       Convert.ToDecimal(reader["importo"]),
                       reader["tipoPagamento"].ToString()
                        );

                    listaDettagli.Add(dettaglio);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }


            return View(listaDettagli);
        }

    }
}