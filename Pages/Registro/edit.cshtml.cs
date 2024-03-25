using Bortolani_Registro.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Data.SqlClient;

namespace Bortolani_Registro.Pages.Registro
{
    public class editModel : PageModel
    {
		public string errorMessage = "";
        public string idAllievo;
		public configurazioni.Allievo Allievo;
		public List<SelectListItem> listaClassi = new List<SelectListItem>();

		public editModel()
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(configurazioni.connectionString))
				{
					connection.Open();
					string sql = $"SELECT * FROM Classi";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while(reader.Read())
							{
								configurazioni.Classi Classe = new configurazioni.Classi();
								Classe.id = reader.GetInt32(0);
								Classe.Classe = reader.GetString(1);
								SelectListItem classe = new SelectListItem(Classe.Classe, ""+Classe.id);
								listaClassi.Add(classe);
							}
							
						}
					}
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}

		public void OnGet()
        {
            idAllievo = Request.Query["id"];

			try
			{
				using (SqlConnection connection = new SqlConnection(configurazioni.connectionString))
				{
					connection.Open();
					string sql = $"SELECT " +
						$"Allievi.id, Allievi.Nome, Allievi.Cognome, Allievi.Classe, " +
						$"Classi.Classe FROM Allievi INNER JOIN Classi ON Allievi.Classe = Classi.Id " +
						$"WHERE Allievi.id like '{idAllievo}'";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							reader.Read();
							Allievo = new configurazioni.Allievo();
							Allievo.id = reader.GetInt32(0);
							Allievo.name = reader.GetString(1);
							Allievo.surname = reader.GetString(2);
							Allievo.idClass = reader.GetInt32(3);
							Allievo.Class = reader.GetString(4);								
						}
					}
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

		}
    }
}
