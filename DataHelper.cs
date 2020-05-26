using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace EmployeeRecordsDesktop
{
    class DataHelper
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        SqlConnection cnn;

        private bool isConnected()

        {
            bool connected = false;





            cnn = new SqlConnection(ConnectionString);
            try
            {
                cnn.Open();
                connected = true;
            }
            catch (Exception ex)
            {
                connected = false;
            }

            return connected;

        }

        public string GetData()

        {

            if (isConnected())

                return "great";

            else
                return "not good";

        }
        public void Execute(string StoredProcedure, int Param)
        {
            SqlCommand command;
            if (isConnected())
            {

                try
                {
                    command = new SqlCommand();
                    command.Connection = cnn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedure;
                    command.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = Param;


                    command.ExecuteNonQuery();


                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


        }

        public void Execute(string StoredProcedure,string ParamName, string Param)
        {
            SqlCommand command;
            if (isConnected())
            {

                try
                {
                    command = new SqlCommand();
                    command.Connection = cnn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedure;
                    command.Parameters.Add(ParamName, SqlDbType.VarChar).Value = Param;
                    

                    command.ExecuteNonQuery();


                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


        }
        public void Execute (string StoreProcedure,string[] Param)
        {
            SqlCommand command;
            var strRetVal = string.Empty;



            if (isConnected())
            {   

                try
                {
                    command = new SqlCommand();
                    command.Connection = cnn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoreProcedure;
                    command.Parameters.Add("@empid", SqlDbType.VarChar).Value = Param[0];
                    command.Parameters.Add("@firstname", SqlDbType.VarChar).Value = Param[1];
                    command.Parameters.Add("@middlename", SqlDbType.VarChar).Value = Param[2];
                    command.Parameters.Add("@lastname", SqlDbType.VarChar).Value = Param[3];
                    command.Parameters.Add("@gender", SqlDbType.NChar).Value = Param[4];
                    command.Parameters.Add("@dob", SqlDbType.Date).Value = Param[5];
                    command.Parameters.Add("@dtehire", SqlDbType.Date).Value = Param[6];
                    command.Parameters.Add("@status", SqlDbType.Int).Value =Convert.ToInt32( Param[7]);
                    command.Parameters.Add("@address", SqlDbType.VarChar).Value = Param[8];

                    command.ExecuteNonQuery();

                    
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");

        }
        public void Execute(string Sql)
        {

            SqlCommand command;
            var strRetVal = string.Empty;
            var data = string.Empty;

            if (isConnected())
            {


                try
                {
                    command = new SqlCommand(Sql, cnn); ;

                    command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            else
            { throw new Exception("Cannot Connect to Server!"); }
        }
        public string GetData(string StoredProcedure)

        {
            SqlCommand command;
            var strRetVal = string.Empty;
            var data = string.Empty;

            if (isConnected())
            {


                try
                {
                    command = new SqlCommand(StoredProcedure, cnn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var datareader = command.ExecuteReader();

                    while (datareader.Read())
                    {

                        data = datareader.GetValue(0).ToString();

                    }

                    datareader.Close();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            else
            { throw new Exception("Cannot Connect to Server!"); }


            return  (data);
        }

        public IEnumerable<String> GetAllData(string StoredProcedureName, string Param)
        {
            List<string> AllData = new List<string>();

            SqlCommand command;
            var strRetVal = string.Empty;



            if (isConnected())
            {

                try
                {
                    command = new SqlCommand();
                    command.Connection = cnn;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureName;
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = Param;

                    var datareader = command.ExecuteReader();

                    while (datareader.Read())
                    {

                        for (var i = 0; i <= datareader.FieldCount; i++)
                        {
                            AllData.Add(datareader.GetValue(i).ToString());
                        }
                    }

                    datareader.Close();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


            return AllData;





        }
        public IEnumerable<String> GetAllData(string sql)
        {
            List<string> AllData = new List<string>();

            SqlDataAdapter sa = new SqlDataAdapter();



            SqlCommand command;
            var strRetVal = string.Empty;



            if (isConnected())
            {

                try
                {
                    command = new SqlCommand(sql, cnn);

                    var datareader = command.ExecuteReader();

                    while (datareader.Read())
                    {

                        for (var i = 0; i < datareader.FieldCount; i++)
                        {
                            AllData.Add(datareader.GetValue(i).ToString());
                        }
                    }

                    datareader.Close();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


            return AllData;





        }

        public DataSet GetAllDataTable(string StoredProcedureName, int Param)
        {

            DataSet ds = new DataSet();

            


            if (isConnected())
            {

                try
                {


                    SqlDataAdapter sa = new SqlDataAdapter(StoredProcedureName, cnn);
                    //sa.SelectCommand.Connection = cnn;
                    sa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sa.SelectCommand.CommandText = StoredProcedureName;
                    if(Param>0)
                    sa.SelectCommand.Parameters.Add("@empID", SqlDbType.Int).Value = Param;
                    sa.Fill(ds);


                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


            return ds;





        }
        public DataSet GetAllDataTable(string StoredProcedureName, string Param)
        {

            DataSet ds = new DataSet();


            if (isConnected())
            {

                try
                {


                    SqlDataAdapter sa = new SqlDataAdapter(StoredProcedureName, cnn);
                    //sa.SelectCommand.Connection = cnn;
                    sa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sa.SelectCommand.CommandText = StoredProcedureName;
                    sa.SelectCommand.Parameters.Add("@empid", SqlDbType.VarChar).Value = Param;
                    sa.Fill(ds);


                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception();


            return ds;





        }
        public DataSet GetAllDataTable(string sql)
        {

            DataSet ds = new DataSet();


            if (isConnected())
            {

                try
                {


                    SqlDataAdapter sa = new SqlDataAdapter(sql, cnn);
                    sa.Fill(ds);


                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            else

                throw new Exception("Problem with the connection to the server");


            return ds;





        }


        

      

       
    }
}
