using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace HOTDAL
{
    public abstract class SqlConnector : IDisposable
    {
        protected MySqlConnection conn = new MySqlConnection();
        protected MySqlCommand command = new MySqlCommand();
        protected string connectionString = String.Empty;

        protected bool disposed = false;

        public SqlConnector()
        { }

        public SqlConnector(String connectionStringKey)
        {
            try
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
                if (String.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Invalid connection string.");
                }
                else
                {
                    conn = new MySqlConnection(connectionString);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }
        }

        public MySqlConnection getConnection()
        {
            return conn;
        }

        public void Open()
        {
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }

            //return;
        }

        public void DisposeObjects()
        {
            try
            {
                if (conn != null)
                    conn.Dispose();
                if (command != null)
                    command.Dispose();
                return;
            }
            catch (Exception e)
            {
                //logging all handled exceptions like this as Warning and all unhandled exceptions as Error
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }
        }

        public void Close()
        {
            try
            {
                if (conn != null)
                { conn.Close(); }

                return;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }
        }

        public MySqlParameter makeInputParameter(String parName, MySqlDbType parType, int parSize, String parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Size = parSize;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeInputParameter(String parName, MySqlDbType parType, Int32 parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeInputParameter(String parName, MySqlDbType parType, Int64 parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeInputParameter(String parName, MySqlDbType parType, String parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeInOutParameter(String parName, MySqlDbType parType, int parSize, String parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.InputOutput;
            newParameter.Size = parSize;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeInOutParameter(String parName, MySqlDbType parType, int parSize)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.InputOutput;
            newParameter.Size = parSize;

            return newParameter;
        }

        public MySqlParameter makeReturnParameter(String parName, MySqlDbType parType, int parSize)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.ReturnValue;
            newParameter.Size = parSize;

            return newParameter;
        }

        public MySqlParameter makeReturnParameter(String parName, MySqlDbType parType, int parSize, String parValue)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.ReturnValue;
            newParameter.Size = parSize;
            newParameter.Value = parValue;

            return newParameter;
        }

        public MySqlParameter makeOutputParameter(String parName, MySqlDbType parType, int parSize)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Output;
            newParameter.Size = parSize;

            return newParameter;
        }

        public MySqlParameter makeOutputParameter(String parName, MySqlDbType parType)
        {
            MySqlParameter newParameter = new MySqlParameter();
            newParameter.ParameterName = parName;
            newParameter.MySqlDbType = parType;
            newParameter.Direction = ParameterDirection.Output;

            return newParameter;
        }

        public MySqlCommand getCommand(List<MySqlParameter> parameters, String SPName)
        {
            //Add all of the defined parameters to the command
            command = new MySqlCommand(SPName);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (MySqlParameter par in parameters)
            {
                command.Parameters.Add(par);
            }
            command.Connection = conn;
            return command;
        }

        public DataSet getDataSet(MySqlCommand command)
        {
            DataSet myData = new DataSet();
            //Open connection
            Open();
            MySqlDataReader myReader = command.ExecuteReader();
            //opens the connection, gets the data, and populates a DataSet with it.
            myData.Load(myReader, LoadOption.PreserveChanges, "");
            //Get rid of and close the data connection objects
            DisposeObjects();
            Close();
            return myData;
        }

        public DataTable getDataSet(List<MySqlParameter> parameters, String sqlString) //add ouput parameter
        {
            DataTable myData = new DataTable();

            try
            {
                Open();
                command = new MySqlCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (MySqlParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }
                command.Connection = conn;
                MySqlDataReader myReader = command.ExecuteReader();
                //opens the connection, gets the data, and populates a DataSet with it.
                myData.Load(myReader);
                myReader.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }
            finally
            {
                //Get rid of and close the data connection objects
                DisposeObjects();
                Close();
            }

            return myData;
        }

        public bool modifyData(List<MySqlParameter> parameters, String sqlString) //add ouput parameter
        {
            bool success = false;

            try
            {
                Open();
                command = new MySqlCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (MySqlParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }
                command.Connection = conn;
                command.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                SendErrorMail("SqlConnector.modifyData", ex.StackTrace, ex.Message, sqlString);
            }
            finally
            {
                //Get rid of and close the data connection objects
                DisposeObjects();
                Close();
            }

            return success;
        }

        public long returnLastInsert(List<MySqlParameter> parameters, String sqlString) //add ouput parameter
        {
            long returnValue = 0;

            try
            {
                Open();
                command = new MySqlCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (MySqlParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }
                command.Connection = conn;
                command.ExecuteNonQuery();
                returnValue = command.LastInsertedId;
            }
            catch (Exception ex)
            {
                SendErrorMail("SqlConnector.modifyData", ex.StackTrace, ex.Message, sqlString);
            }
            finally
            {
                //Get rid of and close the data connection objects
                DisposeObjects();
                Close();
            }

            return returnValue;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DisposeObjects();
                }
                this.disposed = true;
            }
        }

        ~SqlConnector()
        {
            Dispose(false);
        }

        public void SendErrorMail(string ErrorClass, string ErrorStack, string ErrorMessage, string ErrorSQL)
        {
            MailMessage objMessage = new MailMessage();
            objMessage.Subject = "Problem in:" + ErrorClass;
            objMessage.From = new MailAddress("problems@hotselfdefense.net");
            objMessage.To.Add("problems@hotselfdefense.net");
            objMessage.Body = "<b>SQL:</b>" + ErrorSQL + "<br><b>Message:</b>" + ErrorMessage + "<br><b>StackTrace:</b>" + ErrorStack;
            objMessage.IsBodyHtml = true;

            //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            SmtpClient smtp = new SmtpClient("web176.dnchosting.com");
            //smtp.EnableSsl = true;
            //NetworkCredential NetworkCred = new NetworkCredential("lowlysacker@gmail.com", "onhnpqjlbqmakcno"); //*wS!UE8GXZFThwC
            NetworkCredential NetworkCred = new NetworkCredential("problems@hotselfdefense.net", "H0tT@n$.");
            //smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 26;
            smtp.Send(objMessage);
        }
    }
}