using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Net.Mail;

namespace HOTDAL
{
    public abstract class ODBCConnector : IDisposable
    {
        protected OdbcConnection conn = new OdbcConnection();
        protected OdbcCommand command = new OdbcCommand();
        protected string connectionString = String.Empty;

        protected bool disposed = false;

        public ODBCConnector()
        { }

        public ODBCConnector(String connectionStringKey)
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
                    conn = new OdbcConnection(connectionString);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString() + "  " + e.StackTrace.ToString());
            }
        }

        public OdbcConnection getConnection()
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

        public OdbcParameter makeInputParameter(String parName, DbType parType, int parSize, String parValue)
        {
            OdbcParameter newParameter = new OdbcParameter();
            newParameter.ParameterName = parName;
            newParameter.DbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Size = parSize;
            newParameter.Value = parValue;

            return newParameter;
        }

        public OdbcParameter makeInputParameter(String parName, DbType parType, Int32 parValue)
        {
            OdbcParameter newParameter = new OdbcParameter();
            newParameter.ParameterName = parName;
            newParameter.DbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public OdbcParameter makeInputParameter(String parName, DbType parType, Int64 parValue)
        {
            OdbcParameter newParameter = new OdbcParameter();
            newParameter.ParameterName = parName;
            newParameter.DbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public OdbcParameter makeInputParameter(String parName, DbType parType, String parValue)
        {
            OdbcParameter newParameter = new OdbcParameter();
            newParameter.ParameterName = parName;
            newParameter.DbType = parType;
            newParameter.Direction = ParameterDirection.Input;
            newParameter.Value = parValue;

            return newParameter;
        }

        public OdbcCommand getCommand(List<OdbcParameter> parameters, String SPName)
        {
            //Add all of the defined parameters to the command
            command = new OdbcCommand(SPName);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (OdbcParameter par in parameters)
            {
                command.Parameters.Add(par);
            }
            command.Connection = conn;
            return command;
        }

        public DataSet getDataSet(OdbcCommand command)
        {
            DataSet myData = new DataSet();
            //Open connection
            Open();
            OdbcDataReader myReader = command.ExecuteReader();
            //opens the connection, gets the data, and populates a DataSet with it.
            myData.Load(myReader, LoadOption.PreserveChanges, "");
            //Get rid of and close the data connection objects
            DisposeObjects();
            Close();
            return myData;
        }

        public DataTable getDataSet(List<OdbcParameter> parameters, String sqlString) //add ouput parameter
        {
            DataTable myData = new DataTable();

            try
            {
                Open();
                command = new OdbcCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (OdbcParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }
                command.Connection = conn;
                OdbcDataReader myReader = command.ExecuteReader();
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

        public bool modifyData(List<OdbcParameter> parameters, String sqlString) //add ouput parameter
        {
            bool success = false;

            try
            {
                Open();
                command = new OdbcCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (OdbcParameter par in parameters)
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

        public long returnLastInsert(List<OdbcParameter> parameters, String sqlString) //add ouput parameter
        {
            long returnValue = 0;

            try
            {
                Open();
                command = new OdbcCommand(sqlString);
                command.CommandType = System.Data.CommandType.Text;

                foreach (OdbcParameter par in parameters)
                {
                    command.Parameters.Add(par);
                }
                command.Connection = conn;
                command.ExecuteNonQuery();
                command = new OdbcCommand(sqlString);
                command.ExecuteReader();
                //returnValue = command.LastInsertedId;
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

        ~ODBCConnector()
        {
            Dispose(false);
        }

        public void SendErrorMail(string ErrorClass, string ErrorStack, string ErrorMessage, string ErrorSQL)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.hottropicaltans.com");

            objMessage.Subject = "Problem in:" + ErrorClass;
            objMessage.From = new MailAddress("hotproblems@hottropicaltans.com");
            objMessage.To.Add("HOTTans@hottropicaltans.com");
            objMessage.Body = "<b>SQL:</b>" + ErrorSQL + "<br><b>Message:</b>" + ErrorMessage + "<br><b>StackTrace:</b>" + ErrorStack;
            objMessage.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("hotproblems@hottropicaltans.com", "H0tTans.");
            smtp.Send(objMessage);
        }
    }
}