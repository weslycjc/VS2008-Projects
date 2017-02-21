using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
namespace shaiXuan
{
    /**//// <summary>
    /// Summary description for DataHelper.
    /// </summary>
    public class SqlDB
    {
        public SqlDB()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd =new SqlCommand(sql,conn);
                SqlDataReader reader =cmd.ExecuteReader();
                return reader;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //conn.Close();
            }
        }

        public static void ExecuteNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }

        #region GetDataSet
        public static DataSet GetDataSet(string sql)
        {
            SqlDataAdapter sda = new SqlDataAdapter(sql, ConnectionString);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        #endregion

       #region ExecCommand
        public static int ExecCommand(SqlCommand sqlcom)
        {
            SqlConnection conn=new SqlConnection(ConnectionString);
            sqlcom.Connection =conn;
            conn.Open();
            try
            {
                int rtn=sqlcom.ExecuteNonQuery();
                return rtn;
            }
            catch(Exception ex) 
            {
                throw ex;                
            }
            finally
            {
                conn.Close();
            }
            //return 0;

        }
        public static int ExecCommand(string sql)
        {
            if (sql.EndsWith(",")) sql=sql.Substring(0,sql.Length-1);
        
            SqlCommand sqlcom=new SqlCommand(sql);
            return ExecCommand(sqlcom);                
        }
        #endregion
        
        #region ExecuteScalar
        public static object ExecuteScalar(string sql)
        {
            SqlConnection conn=new SqlConnection(ConnectionString);
            SqlCommand sqlcom=new SqlCommand(sql,conn);
            conn.Open();
            try
            {
                object rtn=sqlcom.ExecuteScalar ();
                return rtn;
            }
            catch(Exception ex) 
            {
                throw ex;                
            }
            finally
            {
                conn.Close();
            }
            //return null;
        }
        #endregion

        #region ExecSPCommand
        public static void ExecSPCommand(string sql,System.Data.IDataParameter[] paramers)
        {
            SqlConnection conn=new SqlConnection(ConnectionString);
            SqlCommand sqlcom=new SqlCommand(sql,conn);
            sqlcom.CommandType= CommandType.StoredProcedure ;

            foreach(System.Data.IDataParameter paramer in paramers)
            {
                sqlcom.Parameters.Add(paramer);
            }            
            conn.Open();
            try
            {
                sqlcom.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                string s=ex.Message ;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        #region ExecSPDataSet
        public static DataSet ExecSPDataSet(string sql,System.Data.IDataParameter[] paramers)
        {
            SqlConnection conn=new SqlConnection(ConnectionString);
            SqlCommand sqlcom=new SqlCommand(sql,conn);
            sqlcom.CommandType= CommandType.StoredProcedure ;

            foreach(System.Data.IDataParameter paramer in paramers)
            {
                sqlcom.Parameters.Add(paramer);
            }            
            conn.Open();
            
            SqlDataAdapter da=new SqlDataAdapter();
            da.SelectCommand=sqlcom;
            DataSet ds=new DataSet();
            da.Fill(ds);
        
            conn.Close();
            return ds;
        }

        #endregion

        #region DbType
        private static System.Data.DbType GetDbType(Type type)
        {
            DbType result = DbType.String;
            if( type.Equals(typeof(int)) ||  type.IsEnum)
                result = DbType.Int32;
            else if( type.Equals(typeof(long)))
                result = DbType.Int32;
            else if( type.Equals(typeof(double)) || type.Equals( typeof(Double)))
                result = DbType.Decimal;
            else if( type.Equals(typeof(DateTime)))
                result = DbType.DateTime;
            else if( type.Equals(typeof(bool)))
                result = DbType.Boolean;
            else if( type.Equals(typeof(string) ) )
                result = DbType.String;
            else if( type.Equals(typeof(decimal)))
                result = DbType.Decimal;
            else if( type.Equals(typeof(byte[])))
                result = DbType.Binary;
            else if( type.Equals(typeof(Guid)))
                result = DbType.Guid;
        
            return result;
            
        }

        #endregion

        #region UpdateTable
        public static void UpdateTable(DataTable dt,string TableName,string KeyName)
        {
            foreach(DataRow dr in dt.Rows)
            {
                updateRow(dr,TableName,KeyName);
            }
        }
        #endregion

        #region InsertTable
        //用于主键是数据库表名+ID类型的
        public static void InsertTable(DataTable dt)
        {
            string TableName="["+dt.TableName+"]";
            string KeyName=dt.TableName+"ID";
            foreach(DataRow dr in dt.Rows)
            {
                insertRow(dr,TableName,KeyName);
            }
        }
        //用于主键是任意类型的
        public static void InsertTable(DataTable dt,string KeyName)
        {
            string TableName="["+dt.TableName+"]";
            foreach(DataRow dr in dt.Rows)
            {
                insertRow(dr,TableName,KeyName);
            }
        }
        #endregion

        #region DeleteTable
        public static void DeleteTable(DataTable dt,string KeyName)
        {
            string TableName="["+dt.TableName+"]";
            foreach(DataRow dr in dt.Rows)
            {
                deleteRow(dr,TableName,KeyName);
            }
        }
        #endregion

        #region updateRow
        private static void  updateRow(DataRow dr,string TableName,string KeyName)
        {
            if (dr[KeyName]==DBNull.Value ) 
            {
                throw new Exception(KeyName +"的值不能为空");
            }
            
            if (dr.RowState ==DataRowState.Deleted)
            {
                deleteRow(dr,TableName,KeyName);
 
            }
            else if (dr.RowState ==DataRowState.Modified )
            {
                midifyRow(dr,TableName,KeyName);
            }
            else if (dr.RowState ==DataRowState.Added  )
            {
                insertRow(dr,TableName,KeyName);
            }
            else if (dr.RowState ==DataRowState.Unchanged )
            {
                midifyRow(dr,TableName,KeyName);
            }           
        }

        #endregion

        #region deleteRow
        private static void  deleteRow(DataRow dr,string TableName,string KeyName)
        {
            string sql="Delete {0} where {1} =@{1}";
            DataTable dtb=dr.Table ;
            sql=string.Format(sql,TableName,KeyName);

            SqlCommand sqlcom=new SqlCommand(sql);
            System.Data.IDataParameter iparam=new  SqlParameter();
            iparam.ParameterName    = "@"+ KeyName;
            iparam.DbType            = GetDbType(dtb.Columns[KeyName].DataType);
            iparam.Value            = dr[KeyName];
            sqlcom.Parameters .Add(iparam);
            
            ExecCommand(sqlcom);
        }
        #endregion

        #region midifyRow
        private static void  midifyRow(DataRow dr,string TableName,string KeyName)
        {
            string UpdateSql            = "Update {0} set {1} {2}";
            string setSql="{0}= @{0}";
            string wherSql=" Where {0}=@{0}";
            StringBuilder setSb    = new StringBuilder();

            SqlCommand sqlcom=new SqlCommand();
            DataTable dtb=dr.Table;
        
            for (int k=0; k<dr.Table.Columns.Count; ++k)
            {
                System.Data.IDataParameter iparam=new  SqlParameter();
                iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
                iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
                iparam.Value            = dr[k];
                sqlcom.Parameters .Add(iparam);

                if (dtb.Columns[k].ColumnName==KeyName)
                {
                    wherSql=string.Format(wherSql,KeyName);
                }
                else
                {
                    setSb.Append(string.Format(setSql,dtb.Columns[k].ColumnName));    
                    setSb.Append(",");
                }
                
            }
            
            string setStr=setSb.ToString();
            setStr=setStr.Substring(0,setStr.Length -1); //trim ,
            
            string sql = string.Format(UpdateSql, TableName, setStr,wherSql);
            sqlcom.CommandText =sql;    
            try
            {
                ExecCommand(sqlcom);
            }
            catch(Exception ex)
            {
                throw ex;            
            }
        }
        #endregion

        #region insertRow
        private static void  insertRow(DataRow dr,string TableName,string KeyName)
        {
            string InsertSql = "Insert into {0}({1}) values({2})";
            SqlCommand sqlcom=new SqlCommand();
            DataTable dtb=dr.Table ;
            StringBuilder insertValues    = new StringBuilder();
            StringBuilder cloumn_list    = new StringBuilder();
            for (int k=0; k<dr.Table.Columns.Count; ++k)
            {
                //just for genentae，
                if (dtb.Columns[k].ColumnName==KeyName) continue;
                System.Data.IDataParameter iparam=new  SqlParameter();
                iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
                iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
                iparam.Value            = dr[k];
                sqlcom.Parameters .Add(iparam);

                cloumn_list.Append(dtb.Columns[k].ColumnName);
                insertValues.Append("@"+dtb.Columns[k].ColumnName);

                cloumn_list.Append(",");
                insertValues.Append(",");
            }
            
            string cols=cloumn_list.ToString();
            cols=cols.Substring(0,cols.Length -1);

            string values=insertValues.ToString();
            values=values.Substring(0,values.Length -1);
            
            string sql = string.Format(InsertSql, TableName,cols ,values);
            sqlcom.CommandText =sql;    
            try
            {
                ExecCommand(sqlcom);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
//2..调用范例
        //#region Insert
        //private void InsertUserInfo()
        //{
        //    DataTable dt=ds.Tables[0];
        //    dt.TableName="UserInfo";
        //        string keyname="UserInfoID";
        //    DataRow dr=dt.NewRow();
        //    dr["LoginName"]=this.txtUserName.Value;
        //    dr["Pass"]=this.txtPassword.Value;
        //    dr["NickName"]=this.txtNickName.Value;
        //    dr["UserType"]=1;
        //    dr["IsActive"]=false;
        //    dr["RegisterDate"]=System.DateTime.Now;
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    DataHelper.InsertTable(dt,keyname);
        //}
        //#endregion

        //#region Update
        //private void UpdateUserInfo(string UserID)
        //{            
        //    DataSet ds=GetUserOther(UserID);
        //    DataTable dt=ds.Tables[0];
        //    dt.TableName="UserInfo";
        //        string keyname="UserID";
        //    DataRow dr=dt.Rows[0];
        //    dr["LoginName"]=this.txtUserName.Value;
        //    dr["Pass"]=this.txtPassword.Value;
        //    dr["NickName"]=this.txtNickName.Value;
        //    dr["UserType"]=1;
        //    dr["IsActive"]=false;
        //    dr["RegisterDate"]=System.DateTime.Now;
        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //    DataHelper.UpdateTable(dt,dt.TableName,keynanme);
        //}

        //#endregion

        //#region Delete
        //private void DeleteUserInfo(string UserID)
        //{            
        //    DataSet ds=GetUserOther(UserID);
        //    DataTable dt=ds.Tables[0];
        //    dt.TableName="UserInfo";            
        //        string keyname="UserID";
        //    DataHelper.DeleteTable(dt,keyname);
        //}
        //#endregion