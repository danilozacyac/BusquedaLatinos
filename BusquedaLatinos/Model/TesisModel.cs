using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using BusquedaLatinos.Indices;
using MantesisApi.Dto;
using ScjnUtilities;

namespace BusquedaLatinos.Model
{
    public class TesisModel
    {
        private readonly string connectionDvd = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

        private readonly string connectionSql = ConfigurationManager.ConnectionStrings["MantesisSql"].ConnectionString;

        /// <summary>
        /// Obtiene la información de las tesis necesaria para generar los índices
        /// </summary>
        /// <returns></returns>
        public List<TesisIndx> GetInfoForIndex()
        {
            List<TesisIndx> iuses = new List<TesisIndx>();
            string sSql;

            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection(connectionSql);

            try
            {
                connection.Open();

                sSql = "SELECT IUS, Rubro,Texto FROM Tesis WHERE Parte <> 99 AND Epoca = 1";
                cmd = new SqlCommand(sSql, connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    iuses.Add(new TesisIndx()
                    {
                        Ius = Convert.ToInt32(reader["IUS"]),
                        Rubro = reader["Rubro"].ToString(),
                        Texto = reader["Texto"].ToString(),
                        RubroIndx = StringUtilities.PrepareToAlphabeticalOrder( reader["Rubro"].ToString()),
                        TextoIndx = StringUtilities.PrepareToAlphabeticalOrder(reader["Texto"].ToString())
                    });
                    
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return iuses;
        }

        /// <summary>
        /// Obtiene la información de las tesis necesaria para generar los índices del volumen especificado
        /// </summary>
        /// <returns></returns>
        public List<TesisIndx> GetInfoForIndex(int volumen)
        {
            List<TesisIndx> iuses = new List<TesisIndx>();
            string sSql;

            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection(connectionSql);

            try
            {
                connection.Open();

                sSql = "SELECT IUS, Rubro,Texto FROM Tesis WHERE Parte <> 99 AND Volumen = @Volumen";
                cmd = new SqlCommand(sSql, connection);
                cmd.Parameters.AddWithValue("@Volumen", volumen);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    iuses.Add(new TesisIndx()
                    {
                        Ius = Convert.ToInt32(reader["IUS"]),
                        Rubro = reader["Rubro"].ToString(),
                        Texto = reader["Texto"].ToString(),
                        RubroIndx = StringUtilities.PrepareToAlphabeticalOrder(reader["Rubro"].ToString()),
                        TextoIndx = StringUtilities.PrepareToAlphabeticalOrder(reader["Texto"].ToString())
                    });

                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return iuses;
        }

        /// <summary>
        /// Obtiene los números ius en los cuales se incluye el termino especificado
        /// </summary>
        /// <param name="termino"></param>
        /// <returns></returns>
        public List<int> GetIuses(string termino)
        {
            List<int> iuses = new List<int>();
            string sSql;

            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection(connectionDvd);

            try
            {
                connection.Open();

                sSql = String.Format("SELECT IUS FROM Tesis WHERE (RIndx Like '% {0} %' OR TIndx Like '% {0} %') AND Parte <> 99", termino);
                cmd = new SqlCommand(sSql, connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    iuses.Add(Convert.ToInt32(reader["IUS"]));
                    // Console.WriteLine(reader["IUS"].ToString());
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "PadronApi");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "PadronApi");
            }
            finally
            {
                connection.Close();
            }

            return iuses;
        }


        public ObservableCollection<TesisDto> GetDetalleTesisRel(List<int> iuses)
        {
            string sSql;

            ObservableCollection<TesisDto> listaTesis = new ObservableCollection<TesisDto>();

            List<string> iusString = iuses.ConvertAll<string>(x => x.ToString());

            string numsIus = string.Join(",", iusString.ToArray() );


            SqlCommand cmd;
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection(connectionSql);

            try
            {
                connection.Open();

                sSql = String.Format("SELECT IUS,Rubro FROM Tesis WHERE IUS In ({0}) ORDER BY ConsecIndx", numsIus);
                cmd = new SqlCommand(sSql, connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TesisDto tesisDto = new TesisDto() { Ius = Convert.ToInt32(reader["ius"]), Rubro = reader["rubro"].ToString() };

                    listaTesis.Add(tesisDto);

                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,AccesoModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return listaTesis;
        }

    }
}
