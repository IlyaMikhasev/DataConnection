using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Microsoft.Data.SqlClient;

namespace DataConnection
{
    public abstract class DB_Connected
    {
        private string _connectSTR;
        public string ConnectSTR { get { return _connectSTR; } }
        public DbConnection Connection;
        private DbCommand _command;
        public DbCommand Command { get { return _command; } set { _command = value; } }
        private DbDataReader _reader;
        public DbDataReader DataReader { get {return _reader; } set { _reader = value; } }
        private DbDataAdapter _adapter;
        public DbDataAdapter DataAdapter { get { return _adapter; } set { _adapter = value; } }
        public  DB_Connected() { }
        public DB_Connected(string pathToBase) {
            _connectSTR = pathToBase;
        }
        public abstract DbConnection GetConnection();
        public abstract DbDataAdapter GetAdapter(string sqlText);
        public abstract DbCommand GetComand(string sqlText);
        public abstract void ConnectOpen();
        public abstract void ConnectClose();

    }
    public class DataSQLITE:DB_Connected {
        private SQLiteConnection connected;
        public SQLiteCommand command;
        public SQLiteDataAdapter adapter;
        public DataSQLITE(string pathToBase): base(pathToBase) { }
        public override DbConnection GetConnection()
        {
            connected = new SQLiteConnection(base.ConnectSTR);
            Connection = connected;
            return Connection;

        }
        public override DbDataAdapter GetAdapter(string sqlText) { 
            adapter = new SQLiteDataAdapter(sqlText,connected);
            DataAdapter = adapter;
            return DataAdapter;
        }
        public override DbCommand GetComand(string sqlText) {
            command = new SQLiteCommand(sqlText,connected);
            Command = command;
            return Command;
        }
        public override void ConnectOpen() { 
            connected.Open();
        }
        public override void ConnectClose() { 
            connected.Close();
        }
    }
    public class MSSQL : DB_Connected
    {
        private SqlConnection connected;
        public SqlCommand command;
        public SqlDataAdapter adapter;
        public MSSQL(string pathToBase) : base(pathToBase) { }
        public override DbConnection GetConnection()
        {
            connected = new SqlConnection(base.ConnectSTR);
            Connection = connected;
            return Connection;

        }
        public override DbDataAdapter GetAdapter(string sqlText)
        {
            adapter = new SqlDataAdapter(sqlText, connected);
            DataAdapter = adapter;
            return DataAdapter;
        }
        public override DbCommand GetComand(string sqlText)
        {
            command = new SqlCommand(sqlText, connected);
            Command = command;
            return Command;
        }
        public override void ConnectOpen()
        {
            connected.Open();
        }
        public override void ConnectClose()
        {
            connected.Close();
        }
    }
}
