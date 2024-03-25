namespace Bortolani_Registro.Pages.Shared
{
    public class configurazioni
    {
        public static String connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
				"AttachDbFilename=D:\\Il mio Drive\\2023-2024\\Classi\\3 IOT\\WebApp\\Bortolani-Registro\\Bortolani-Registro-DB.mdf;" +
                "Integrated Security=True;" +
                "Connect Timeout=30";
        public class Allievo
        {
            public int id;
            public string name;
            public string surname;
            public int idClass;
            public string Class;
        }

        public class Classi
        {
            public int id;
            public string Classe;
        }
    }
}
