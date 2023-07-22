using BusinessObject.DTO;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace PetStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountRepository repository;
        public AccountController(IAccountRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountDTO>> GetAccounts() => repository.GetAccount();

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(loginInfo.Email) ||
                    string.IsNullOrEmpty(loginInfo.Password))
                {
                    throw new ApplicationException("Login Information is invalid!! Please check again...");
                }

                AccountDTO loginAccount = repository.GetAccountByEmailAndPassword(loginInfo.Email, loginInfo.Password);
                if (loginAccount == null)
                {
                    throw new ApplicationException("Failed to login! Please check the information again...");
                }
                return StatusCode(200);
            }
            catch (ApplicationException ae)
            {
                return StatusCode(400, ae.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("role")]
        public IActionResult GetRole(string email)
        {
            // Lấy thông tin tài khoản từ cơ sở dữ liệu dựa trên địa chỉ email
            var account = repository.GetRoleAccount(email);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account.Role);
        }


        [HttpGet("Infomation")]
        public IActionResult GetInfomation(string email)
        {
            // Lấy thông tin tài khoản từ cơ sở dữ liệu dựa trên địa chỉ email
            var account = repository.GetInfoAccount(email);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpGet("id")]
        public ActionResult<AccountDTO> GetAccountById(int id) => repository.GetAccountById(id);

        [HttpPost]
        public IActionResult CreateAccount(AccountDTO m)
        {
            repository.CreateAccount(m);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAccount(int id)
        {
            var Account = repository.GetAccountById(id);
            if (Account == null) return NotFound();
            repository.DeleteAccount(Account);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, AccountDTO m)
        {
            var mTmp = repository.GetAccountById(id);
            if (mTmp == null) return NotFound();
            m.AccountId = id; 
            repository.UpdateAccount(m);
            return NoContent();
        }
    }
}
