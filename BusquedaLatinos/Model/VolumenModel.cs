using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using BusquedaLatinos.Dto;
using ScjnUtilities;
using System.Collections.ObjectModel;

namespace BusquedaLatinos.Model
{
    public class VolumenModel
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Catalogo"].ConnectionString;
        private readonly string connectionSql = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;



        private string GetVolumenesFaltantes()
        {
            string buscaVolumen = String.Empty;
            string sOleDb;

            OleDbCommand cmd;
            OleDbDataReader reader;
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();

                sOleDb = "SELECT * FROM VolIncluidos ORDER BY Volumen";
                cmd = new OleDbCommand(sOleDb, connection);
                reader = cmd.ExecuteReader();

                List<string> volumenes = new List<string>();

                while (reader.Read())
                {
                    volumenes.Add(reader["Volumen"].ToString());
                }

                if (volumenes.Count > 0)
                    buscaVolumen = string.Join(",", volumenes);
                else
                    buscaVolumen = "0";
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return buscaVolumen;
        }


        public ObservableCollection<Volumen> GetVolumenesForCombo()
        {
            ObservableCollection<Volumen> volumenes = new ObservableCollection<Volumen>();

            string buscaVolumen = String.Empty;
            string sOleDb;

            OleDbCommand cmd;
            OleDbDataReader reader;
            OleDbConnection connection = new OleDbConnection(connectionSql);

            try
            {
                connection.Open();

                sOleDb = String.Format("SELECT * FROM Volumen WHERE Epoca = 256 AND Volumen > 8841 AND Volumen NOT IN ({0}) ORDER BY Volumen",this.GetVolumenesFaltantes());
                cmd = new OleDbCommand(sOleDb, connection);
                reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    volumenes.Add(new Volumen() { Volumen1 = Convert.ToInt32(reader["Volumen"]) , Descripcion = reader["TxtVolumen"].ToString() });
                }

            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return volumenes;
        }



        public bool InsertaVolumen(Volumen volumen)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            bool insertCompleted = false;


            try
            {
                connection.Open();

                string sqlQuery = "INSERT INTO VolIncluidos(Volumen, Descripcion)" +
                                  "VALUES (@Volumen, @Descripcion)";

                OleDbCommand cmd = new OleDbCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@Volumen", volumen.Volumen1);
                cmd.Parameters.AddWithValue("@Descripcion", volumen.Descripcion);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                insertCompleted = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,VolumenModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return insertCompleted;
        }

    }
}
