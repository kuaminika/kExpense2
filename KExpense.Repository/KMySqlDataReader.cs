﻿using KExpense.Repository.interfaces;
using MySql.Data.MySqlClient;

namespace KExpense.Repository
{
    internal class KMySqlDataReader : IKDataReader
    {
        private MySqlDataReader dt;

        public KMySqlDataReader(MySqlDataReader dt)
        {
            this.dt = dt;
            
        }

        public decimal GetDecimal(string column)
        {
            decimal rslt = dt.GetDecimal(column);
            return rslt;
        }

        public int GetInt(string column)
        {
            int result = dt.GetInt32(column);
            return result;
        }

        public string GetString(string column)
        {
            string result = dt.GetString(column);
            return result;
        }

        public bool Read()
        {
            bool result = this.dt.Read();
            return result;
        }
    }
}