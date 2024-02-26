using Esercitazione_Edile.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Esercitazione_Edile.Controllers
{
    public class PagamentoController : Controller
    {
        // GET: Pagamento
        public ActionResult Index()
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            List<Pagamento> pagamenti = new List<Pagamento>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Pagamenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pagamento pagamento = new Pagamento(
                        Convert.ToInt32(reader["idPagamento"].ToString()),
                        Convert.ToInt32(reader["idDipendente"].ToString()),
                        Convert.ToDateTime(reader["dataPagamento"].ToString()),
                        Convert.ToDouble(reader["importo"].ToString()),
                        reader["tipoPagamento"].ToString()
                    );
                    pagamenti.Add(pagamento);
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

            return View(pagamenti);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pagamento pagamento)
        {
            string connection = ConfigurationManager.ConnectionStrings["Edile"].ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                string query = "INSERT INTO Pagamenti (idDipendente, dataPagamento, importo, tipoPagamento) VALUES (@idDipendente, @dataPagamento, @importo, @tipoPagamento)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idDipendente", pagamento.IdDipendente);
                cmd.Parameters.AddWithValue("@dataPagamento", pagamento.DataPagamento);
                cmd.Parameters.AddWithValue("@importo", pagamento.Importo);
                cmd.Parameters.AddWithValue("@tipoPagamento", pagamento.TipoPagamento);
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Response.Write("Error: ");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
