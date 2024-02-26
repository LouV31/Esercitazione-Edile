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

        [HttpGet]
        /*public ActionResult Details()
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                string query = "SELECT * FROM Pagamenti AS P JOIN Dipendenti AS D ON P.idDipendente = D.idDipendente";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    
                
                }
            }

            return View();
        }*/

    }
}