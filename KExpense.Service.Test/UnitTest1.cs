using KExpense.Repository;
using KExpense.Repository.interfaces;
using NUnit.Framework;
namespace KExpense.Service.Test
{
    public class Tests
    {
        AKDBAbstraction db;
         [SetUp]
        public void Setup()
        {
            string connString = "server=localhost;user id=kExpense;persistsecurityinfo=True;database=kExpense; password=kExpense1000";
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
            IKExpenseRepository ker = new KExpenseRepository(db);
            var r = ker.GetAllKExpenses();
            Assert.IsTrue(r.Count > 0);
        }

        [Test]
        public void TestingGettingExpensesForMonth()
        {

            IKExpenseRepository ker = new KExpenseRepository(db);
            var r = ker.GetAllKExpensesForMonth(2021, 8, 2);
            Assert.IsTrue(r.Count > 0);
        }
    }
}