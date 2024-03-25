using Bortolani_Registro.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace Bortolani_Registro.Pages.Registro
{
    public class addModel : PageModel
    {
		public string errorMessage = "";
		public List<SelectListItem> listaClassi = new List<SelectListItem>();
        
		public void OnGet()
        {
			try
			{
				using (SqlConnection connection = new SqlConnection(configurazioni.connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM Classi";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								configurazioni.Classi Classi = new configurazioni.Classi();
								Classi.id = reader.GetInt32(0);
								Classi.Classe = reader.GetString(1);
								SelectListItem Classe = new SelectListItem(Classi.Classe, ""+Classi.id);
								listaClassi.Add(Classe);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}
        public void OnPost()
        {
			configurazioni.Allievo Allievo = new configurazioni.Allievo();
			Allievo.name = Request.Form["nome"];
			Allievo.surname = Request.Form["cognome"];
			Allievo.Class = Request.Form["classe"];

			if (Allievo.name.Length == 0 || Allievo.surname.Length == 0 || Allievo.Class.Length == 0)
			{
				errorMessage = "Tutti i campi devo essere compilati";
				return;
			}

			try
			{
				using(SqlConnection connection = new SqlConnection(configurazioni.connectionString))
				{
					connection.Open();
					string sql = $"INSERT INTO Allievi " +
						$"(Nome, Cognome, Classe) VALUES " +
						$"('{Allievo.name}', '{Allievo.surname}', {Allievo.Class});";

					using(SqlCommand command = new SqlCommand(sql, connection))
					{
						command.ExecuteNonQuery();
					}
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			if (errorMessage == "") 
			{
				Response.Redirect("/Registro/ListaAllievi");
			}
        }
    }
}
