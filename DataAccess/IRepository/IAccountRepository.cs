using BusinessObject.DTO;

namespace DataAccess.IRepository
{
    public interface IAccountRepository
    {
        List<AccountDTO> GetAccount();
        AccountDTO GetAccountByEmailAndPassword(string email, string pwd);
        AccountDTO GetAccountById(int id);
        AccountDTO GetRoleAccount(string email);
        AccountDTO GetInfoAccount(string email);
        void CreateAccount(AccountDTO acc);
        void UpdateAccount(AccountDTO acc);
        void DeleteAccount(AccountDTO acc);
    }
}
