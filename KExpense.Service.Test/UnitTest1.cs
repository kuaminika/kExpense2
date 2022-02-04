using KExpense.Repository;
using KExpense.Repository.interfaces;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
namespace KExpense.Service.Test
{
    //todo: test KExpenseRepository
    //todo: test OrgRepository
    public class Tests
    {
        AKDBAbstraction db;
        int orgId;
         [SetUp]
        public void Setup()
        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("kExpenseConfig.json", optional: false, reloadOnChange: false).Build();

            string connString = config["connectionString"].ToString();
             orgId = 2;
            db = new KMysql_KDBAbstraction(connString);
        }

        [Test]
        public void TestGettingAllOrgs()
        {
            OrgRepository or = new OrgRepository(db);
            var r = or.GetAll();
            Assert.IsTrue(r.Count > 0);
        }

        [Test]
        public void TestGettingExpenses()
        {
            IKExpenseRepository ker = new KExpenseRepository(orgId,db);
            var r = ker.GetAllKExpenses();
            Assert.IsTrue(r.Count > 0);
        }
        [Test]
        public void TestAddingExpense()
        {
            //CALL `houseofm_kExpense`.`record_expense`(<{IN for_produc_id int}>, <{IN expense_year int}>, <{IN expense_month int}>, <{IN expense_day int}>, <{IN cost decimal(10,2)}>, <{IN reason varchar(500)}>, <{IN merchant_id int}>, <{IN spending_org_id int}>);

            IKExpenseRepository ker = new KExpenseRepository(orgId,db);
            var r = ker.GetAllKExpenses();
            int countBefore = r.Count;

            Model.IKExpense newExpense = new Model.ExpenseModel { BriefDescription = "this is a test:" + System.DateTime.Now.ToString(), ExpenseDate = System.DateTime.Now, Cost = 100, SpentOnName = "test",SpendingOrgId=1 };
            ker.RecordExpense(newExpense);
            r = ker.GetAllKExpenses();


            int countAfter = r.Count;

            Assert.IsTrue(countAfter > countBefore);

        }


        [Test]
        public void TestingGettingExpensesForMonth()
        {

            IKExpenseRepository ker = new KExpenseRepository(orgId,db);
            var r = ker.GetAllKExpensesForMonth(2021, 8, 2);
            Assert.IsTrue(r.Count > 0);
        }
    }
}