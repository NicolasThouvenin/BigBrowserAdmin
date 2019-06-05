using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BigBrowser.Modele
{
    class ModeleRequestDB
    {
        //Modifie l'affichage de la datagrid
        //retourne un datatable avec une vue par défaut ou avec la vue demandée via le parametre sql
        static public DataTable FillDataGrid()
        {
            Console.WriteLine("Entering FIllDataGrid");
            var connString = "Host=176.31.121.226;Username=postgres;Password=Windows21;Database=bigbrowsertest";
            string sql = "SELECT firstname, lastname, public.communes.nom as birthtown, fictionnal, birthdate, deathdate FROM public.characters left join public.communes on public.characters.birthplace = public.communes.insee";

            DataTable dt = new DataTable("Characters");

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                da.Fill(dt);   // filling DataSet with result from NpgsqlDataAdapter

                ShowTable(dt);

            }
            return dt;
        }


        /**
         *  Homemade (sorta) function to display DataTable content
         *  TO BE REMOVED
         */ 
        private static void ShowTable(DataTable table)
        {
            foreach (DataColumn col in table.Columns)
            {
                Console.Write("{0,-14}", col.ColumnName);
            }
            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType.Equals(typeof(DateTime)))
                        Console.Write("{0,-14:d}", row[col]);
                    else if (col.DataType.Equals(typeof(Decimal)))
                        Console.Write("{0,-14:C}", row[col]);
                    else
                        Console.Write("{0,-14}", row[col]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}