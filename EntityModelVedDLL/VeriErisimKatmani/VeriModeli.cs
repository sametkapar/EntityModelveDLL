using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Policy;

namespace VeriErisimKatmani
{

    public class VeriModeli
    {
        SqlConnection conn;
        SqlCommand cmd;

        public VeriModeli()
        {
            conn = new SqlConnection(VeriYollari.veriyolu);
            cmd = conn.CreateCommand();
        }

        public void KategoriEkle(Kategori kat)
        {
            cmd.CommandText = "INSERT INTO Categories(CategoryName,Description) VALUES(@categoryName,@description)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@categoryName", kat.Isim);
            cmd.Parameters.AddWithValue("@description", kat.Aciklama);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public List<Kategori> KategoriGoster()
        {
            List<Kategori> kategoriler = new List<Kategori>();
            cmd.CommandText = "SELECT CategoryID, CategoryName, Description FROM Categories ORDER BY CategoryID";
            cmd.Parameters.Clear();
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Kategori kat = new Kategori();
                kat.ID = reader.GetInt32(0);
                kat.Isim = reader.GetString(1);
                kat.Aciklama = reader.GetString(2);
                kategoriler.Add(kat);

            }
            return kategoriler;
        }
    }
}