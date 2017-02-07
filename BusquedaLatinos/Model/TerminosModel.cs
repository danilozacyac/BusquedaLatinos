using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using BusquedaLatinos.Dto;
using ScjnUtilities;

namespace BusquedaLatinos.Model
{
    public class TerminosModel
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Catalogo"].ConnectionString;
        private readonly string connectionSql = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

        public List<Terminos> GetTerminos()
        {
            List<Terminos> catTerminos = new List<Terminos>();
            string sOleDb;

            OleDbCommand cmd;
            OleDbDataReader reader;
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();

                sOleDb = "SELECT * FROM Terminos ORDER BY Termino";
                cmd = new OleDbCommand(sOleDb, connection);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    catTerminos.Add(new Terminos() { IdTermino = Convert.ToInt32(reader["IdTermino"]), Termino = reader["TErmino"].ToString(), TerminoStr = reader["TErminoStr"].ToString() });
                }
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return catTerminos;
        }


       


        public bool InsertaTermino(Terminos termino)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            bool insertCompleted = false;

            termino.IdTermino = DataBaseUtilities.GetNextIdForUse("Terminos", "IdTermino", connection);

            try
            {
                connection.Open();

                string sqlQuery = "INSERT INTO Terminos(IdTermino,Termino,TerminoStr,Definicion,Bibliografia)" +
                                  "VALUES (@IdTermino,@Termino,@TerminoStr,@Definicion,@Bibliografia)";

                OleDbCommand cmd = new OleDbCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                cmd.Parameters.AddWithValue("@Termino", termino.Termino);
                cmd.Parameters.AddWithValue("@TerminoStr", termino.Termino);
                cmd.Parameters.AddWithValue("@Definicion", termino.Definicion);
                cmd.Parameters.AddWithValue("@Bibliografia", termino.Bibliografia);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                insertCompleted = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return insertCompleted;
        }


        public bool UpdateTermino(Terminos termino)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbDataAdapter dataAdapter;
            OleDbCommand cmd;
            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            bool insertCompleted = false;

            try
            {
                connection.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlQuery = "SELECT * FROM Terminos WHERE IdTermino = @IdTermino";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                dataAdapter.Fill(dataSet, "Terminos");

                dr = dataSet.Tables["Terminos"].Rows[0];
                dr.BeginEdit();
                dr["Termino"] = termino.Termino;
                dr["TerminoStr"] = termino.TerminoStr;
                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                dataAdapter.UpdateCommand.CommandText = "UPDATE Terminos SET Termino = @Termino, TerminoStr = @TerminoStr WHERE IdTermino = @IdTermino";

                dataAdapter.UpdateCommand.Parameters.Add("@Termino", OleDbType.VarChar, 0, "Termino");
                dataAdapter.UpdateCommand.Parameters.Add("@TerminoStr", OleDbType.VarChar, 0, "TerminoStr");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTermino", OleDbType.Numeric, 0, "IdTermino");

                dataAdapter.Update(dataSet, "Terminos");

                dataSet.Dispose();
                dataAdapter.Dispose();

                insertCompleted = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return insertCompleted;
        }

        public bool DeleteFuncionario(Terminos termino)
        {
            bool completed = false;
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;

            try
            {
                connection.Open();

                cmd.CommandText = "DELETE FROM Terminos WHERE IdTermino = @IdTermino";
                cmd.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                cmd.ExecuteNonQuery();
                completed = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return completed;
        }





        public bool InsertaRelacion(Terminos termino, int ius)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            bool insertCompleted = false;


            try
            {
                connection.Open();

                string sqlQuery = "INSERT INTO RelTesis(IdTermino,Ius,Pertinencia)" +
                                  "VALUES (@IdTermino,@Ius,1)";

                OleDbCommand cmd = new OleDbCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                cmd.Parameters.AddWithValue("@Ius", ius);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                insertCompleted = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return insertCompleted;
        }


        public void GetTesisRelacionadas(Terminos termino)
        {
            string sOleDb;

            OleDbCommand cmd;
            OleDbDataReader reader;
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();

                sOleDb = "SELECT IUS FROM RelTesis WHERE IdTermino = @IdTermino AND Pertinencia = 1 ORDER BY IUS";
                cmd = new OleDbCommand(sOleDb, connection);
                cmd.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                reader = cmd.ExecuteReader();

                if (termino.Iuses == null)
                    termino.Iuses = new List<int>();

                while (reader.Read())
                {
                    termino.Iuses.Add(Convert.ToInt32(reader["IUS"]));
                }
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

        }


        public bool UpdatePertinencia(Terminos termino, int ius, bool esPertinente)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbDataAdapter dataAdapter;
            OleDbCommand cmd;
            cmd = connection.CreateCommand();
            cmd.Connection = connection;

            bool insertCompleted = false;

            try
            {
                connection.Open();

                DataSet dataSet = new DataSet();
                DataRow dr;

                string sqlQuery = "SELECT * FROM RelTesis WHERE IdTermino = @IdTermino AND Ius = @Ius";

                dataAdapter = new OleDbDataAdapter();
                dataAdapter.SelectCommand = new OleDbCommand(sqlQuery, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@IdTermino", termino.IdTermino);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Ius", ius);
                dataAdapter.Fill(dataSet, "RelTesis");

                dr = dataSet.Tables["RelTesis"].Rows[0];
                dr.BeginEdit();
                dr["Pertinencia"] = (esPertinente) ? 1 : 0;
                dr.EndEdit();

                dataAdapter.UpdateCommand = connection.CreateCommand();

                dataAdapter.UpdateCommand.CommandText = "UPDATE RelTesis SET Pertinencia = @Pertinencia WHERE IdTermino = @IdTermino AND Ius = @Ius";

                dataAdapter.UpdateCommand.Parameters.Add("@Pertinencia", OleDbType.Numeric, 0, "Pertinencia");
                dataAdapter.UpdateCommand.Parameters.Add("@IdTermino", OleDbType.Numeric, 0, "IdTermino");
                dataAdapter.UpdateCommand.Parameters.Add("@Ius", OleDbType.Numeric, 0, "Ius");

                dataAdapter.Update(dataSet, "RelTesis");

                dataSet.Dispose();
                dataAdapter.Dispose();

                insertCompleted = true;
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,TerminosModel", "BusquedaLatinos");
            }
            finally
            {
                connection.Close();
            }

            return insertCompleted;
        }


    }
}
