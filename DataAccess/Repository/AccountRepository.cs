using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;

namespace DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;

        public AccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void CreateAccount(AccountDTO acc)
        {
            var account = _mapper.Map<Account>(acc);
            AccountDAO.CreateAccount(account);
        }

        public void DeleteAccount(AccountDTO acc)
        {
            var account = _mapper.Map<Account>(acc);
            AccountDAO.DeleteAccount(account);
        }

        public List<AccountDTO> GetAccount()
        {
            return _mapper.Map<List<AccountDTO>>(AccountDAO.GetAccount());
        }

        public AccountDTO GetAccountByEmailAndPassword(string email, string pwd)
        {
            return _mapper.Map<AccountDTO>(AccountDAO.GetAccountByEmailAndPassword(email, pwd));
        }

        public AccountDTO GetAccountById(int id)
        {
            return _mapper.Map<AccountDTO>(AccountDAO.GetAccountById(id));
        }

        public AccountDTO GetInfoAccount(string email)
        {
            return _mapper.Map<AccountDTO>(AccountDAO.GetInfoAccount(email));
        }

        public AccountDTO GetRoleAccount(string email)
        {
            return _mapper.Map<AccountDTO>(AccountDAO.GetRoleAccount(email));
        }

        public void UpdateAccount(AccountDTO acc)
        {
            var account = _mapper.Map<Account>(acc);
            AccountDAO.UpdateAccount(account);
        }
    }
}
