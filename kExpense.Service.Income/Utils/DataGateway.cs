﻿using KDBAbstractions.Repository;
using KDBAbstractions.Repository.interfaces;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;
using System;

namespace kExpense.Service.Income.Utils
{
    public class DataGateway : IDataGateway
    {
        private string connStr;
        private KMysql_KDBAbstraction dbAbstraction;

        public DataGateway(string connString)
        {
            this.connStr = connString;
          this.dbAbstraction =   new KMysql_KDBAbstraction(connString);
        }


        public KWriteResult ExecuteInsert(string query)
        {
            try
            {
                KWriteResult result = dbAbstraction.ExecuteWriteTransaction(query);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public T ExecuteReadOneQuery<T>(string query)
        {
            try
            {
                List<T> result = ExecuteReadManyResult<T>(query);
                return result.Count<1 ?default(T) : result[0];
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public List<T> ExecuteReadManyResult<T>(string query)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    IEnumerable<T> yielded = conn.Query<T>(query);
                    if (yielded == null) return new List<T>(); 
                    List<T> result = yielded.AsList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KWriteResult Execute(string query)
        {
            try
            {
                KWriteResult result = dbAbstraction.ExecuteWriteTransaction(query);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
