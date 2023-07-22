using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        public static List<Account> GetAccount()
        {
            var listAccount = new List<Account>();
            try
            {
                using (var context = new ProjectContext())
                {
                    listAccount = context.Accounts.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAccount;
        }

        public static Account GetRoleAccount(string email)
        {
            Account Account = new Account();
            try
            {
                using (var context = new ProjectContext())
                {
                    Account = context.Accounts.SingleOrDefault(x => x.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Account;
        }

        public static Account GetInfoAccount(string email)
        {
            Account Account = new Account();
            try
            {
                using (var context = new ProjectContext())
                {
                    Account = context.Accounts.SingleOrDefault(x => x.Email == email);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Account;
        }

        public static Account GetAccountById(int id)
        {
            Account Account = new Account();
            try
            {
                using (var context = new ProjectContext())
                {
                    Account = context.Accounts.SingleOrDefault(x => x.AccountId == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Account;
        }

        public static Account GetAccountByEmailAndPassword(string email, string pwd)
        {
            Account Account = new Account();
            try
            {
                using (var context = new ProjectContext())
                {
                    Account = context.Accounts.SingleOrDefault(x => x.Email == email && x.Password == pwd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Account;
        }

        public static void CreateAccount(Account Account)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Accounts.Add(Account);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateAccount(Account Account)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    context.Entry(Account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteAccount(Account Account)
        {
            try
            {
                using (var context = new ProjectContext())
                {
                    var u = context.Accounts.SingleOrDefault(c => c.AccountId == Account.AccountId);
                    context.Accounts.Remove(u);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
