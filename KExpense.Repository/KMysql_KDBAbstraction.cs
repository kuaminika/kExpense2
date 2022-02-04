﻿using KExpense.Repository.interfaces;
using KExpense.Repository.kModelMappers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KExpense.Repository
{
    public class KMysql_KDBAbstraction : AKDBAbstraction
    {
        public KMysql_KDBAbstraction(string connString) : base(connString) { }

        public override void ExecuteReadTransaction(string query, IKModelMapper mapper)
        {
            try
            {//"server=localhost;user id=kExpense;persistsecurityinfo=True;database=kExpense; password=kExpense1000");
                MySqlConnection conn = new MySqlConnection(this.ConnectionString);
                conn.Open();


                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader dt = cmd.ExecuteReader())
                {
                    IKDataReader mysqlDatareader = new KMySqlDataReader(dt);
                    mapper.map(mysqlDatareader);
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MySqlCommand prepareSP(string name, List<KSP_Param> paramers)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            paramers.ForEach(p =>
            {
                cmd.Parameters.AddWithValue(p.Name, p.Value);
                switch (p.Direction)
                {
                    case (KSP_ParamDirection.In):
                        cmd.Parameters[p.Name].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case (KSP_ParamDirection.Out):
                        cmd.Parameters[p.Name].Direction = System.Data.ParameterDirection.Output;
                        break;
                    default:
                        cmd.Parameters[p.Name].Direction = System.Data.ParameterDirection.InputOutput;
                        break;
                }

                switch (p.Type)
                {
                    case (KSP_ParamType.Bool):
                        cmd.Parameters[p.Name].DbType = System.Data.DbType.Boolean;
                        break;
                    case (KSP_ParamType.Int):
                        cmd.Parameters[p.Name].DbType = System.Data.DbType.Int32;
                        break;
                    case (KSP_ParamType.Str):
                        cmd.Parameters[p.Name].DbType = System.Data.DbType.String;
                        break;
                    case (KSP_ParamType.Decimal):
                        cmd.Parameters[p.Name].DbType = System.Data.DbType.Decimal;
                        break;
                }
            });

            return cmd;
        }

        public override void ExecuteReadSPTranasaction(string name, List<KSP_Param> paramers, IKModelMapper mapper)
        {

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = this.prepareSP(name, paramers))
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = name;
                        using (MySqlDataReader dt = cmd.ExecuteReader())
                        {
                            IKDataReader mysqlDatareader = new KMySqlDataReader(dt);
                            mapper.map(mysqlDatareader);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        public override void ExecuteWriteTransaction(string query, IKModelMapper mapper)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(this.ConnectionString);
                conn.Open();


                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
