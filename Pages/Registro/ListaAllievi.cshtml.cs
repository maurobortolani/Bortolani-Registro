using Bortolani_Registro.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Bortolani_Registro.Pages.Registro
{
    public class ListaAllieviModel : PageModel
    {
        public string errorMessage = "";
        
        public List<configurazioni.Allievo> listaAllievi = new List<configurazioni.Allievo>();

        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(configurazioni.connectionString))
                {
                    connection.Open();
                    string sql = "SELECT " +
                        "Allievi.id, Allievi.Nome, Allievi.Cognome, Allievi.Classe, " +
                        "Classi.Classe FROM Allievi INNER JOIN Classi ON Allievi.Classe = Classi.Id " +
                        "ORDER BY Allievi.Cognome";

                    using(SqlCommand command = new  SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                configurazioni.Allievo Allievo = new configurazioni.Allievo();
                                Allievo.id = reader.GetInt32(0);
                                Allievo.name = reader.GetString(1);
                                Allievo.surname = reader.GetString(2);
                                Allievo.idClass = reader.GetInt32(3);
                                Allievo.Class = reader.GetString(4);
                                listaAllievi.Add(Allievo);
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
    }
}
