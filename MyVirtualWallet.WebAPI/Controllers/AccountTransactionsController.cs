using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyVirtualWallet.Data;
using MyVirtualWallet.Models;
using MyVirtualWallet.WebAPI.Models;

namespace MyVirtualWallet.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountTransactionsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //// GET: api/AccountTransactions
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AccountTransaction>>> GetAccountTransactions()
        //{
        //    return await _context.AccountTransactions.ToListAsync();
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountTransactionDTO>>> GetAccountTransactions()
        {
            return Ok(_mapper.Map<List<AccountTransaction>, List<AccountTransactionDTO>>(_context.AccountTransactions.ToList()));
        }

        // GET: api/AccountTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountTransaction>> GetAccountTransaction(Guid id)
        {
            var accountTransaction = await _context.AccountTransactions.FindAsync(id);

            if (accountTransaction == null)
            {
                return NotFound();
            }

            return accountTransaction;
        }

        // PUT: api/AccountTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountTransaction(Guid id, AccountTransaction accountTransaction)
        {
            if (id != accountTransaction.ObjectID)
            {
                return BadRequest();
            }

            _context.Entry(accountTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AccountTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountTransactionDTO>> PostAccountTransaction(AccountTransactionDTO accountTransaction)
        {
            _context.AccountTransactions.Add(_mapper.Map<AccountTransactionDTO, AccountTransaction>(accountTransaction));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountTransaction", new { id = accountTransaction.ObjectID }, accountTransaction);
        }

        // DELETE: api/AccountTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountTransaction(Guid id)
        {
            var accountTransaction = await _context.AccountTransactions.FindAsync(id);
            if (accountTransaction == null)
            {
                return NotFound();
            }

            _context.AccountTransactions.Remove(accountTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("Deposit")]
        public async Task<IActionResult> DepositMoney(AccountTransactionDTO accountTransaction)
        {
            if (accountTransaction == null)
            {
                return BadRequest($"No account transaction details. Please check with administrator");
            }
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(i => i.UserID == accountTransaction.currentUserID);
            var mappedObj = _mapper.Map<AccountTransactionDTO, AccountTransaction>(accountTransaction);
            mappedObj.TransactionType = (int)AccountTransactionTypeEnum.Deposit;
            
            if (accountDetails != null)
            {
                accountDetails.Balance += mappedObj.DebitAmount;
                mappedObj.DestinationAccountID = accountDetails.ObjectID;
                mappedObj.CreatedDateTime = DateTime.Now;
            }

            _context.AccountTransactions.Add(mappedObj);
            await _context.SaveChangesAsync();

            return Ok($"Amount deposited: {mappedObj.DebitAmount?.ToString("c")}");
        }

        [HttpPost]
        [Route("Withdraw")]
        public async Task<IActionResult> WithdrawMoney(AccountTransactionDTO accountTransaction)
        {
            if(accountTransaction == null)
            {
                return BadRequest($"No account transaction details. Please check with administrator");
            }
            var accountDetails = await _context.AccountDetails.FirstOrDefaultAsync(i => i.UserID == accountTransaction.currentUserID);
            var mappedObj = _mapper.Map<AccountTransactionDTO, AccountTransaction>(accountTransaction);
            mappedObj.TransactionType = (int)AccountTransactionTypeEnum.Withdraw;
            
            if (accountDetails != null)
            {
                if(mappedObj.CreditAmount > accountDetails.Balance)
                {
                    return BadRequest($"Not enough amount balance to withdraw.");
                }
                accountDetails.Balance -= mappedObj.CreditAmount;
                mappedObj.SourceAccountID = accountDetails.ObjectID;
                mappedObj.CreatedDateTime = DateTime.Now;
            }

            _context.AccountTransactions.Add(mappedObj);
            await _context.SaveChangesAsync();

            return Ok($"Amount withdrawn: {mappedObj.CreditAmount?.ToString("c")}");
        }

        [HttpPost]
        [Route("Transfer")]
        public async Task<IActionResult> TransferMoney(AccountTransactionDTO accountTransaction)
        {
            if (accountTransaction == null)
            {
                return BadRequest($"No account transaction details. Please check with administrator");
            }
            var sourceAccountDetails = await _context.AccountDetails.FirstOrDefaultAsync(i => i.UserID == accountTransaction.currentUserID);
            var destinationAccountDetails = await _context.AccountDetails.FirstOrDefaultAsync(i => i.ObjectID == accountTransaction.DestinationAccountID);
            var mappedObj = _mapper.Map<AccountTransactionDTO, AccountTransaction>(accountTransaction);

            if (sourceAccountDetails != null && destinationAccountDetails != null)
            {
                mappedObj.TransactionType = (int)AccountTransactionTypeEnum.Transfer;
                mappedObj.SourceAccountID = sourceAccountDetails.ObjectID;
                mappedObj.CreditAmount = accountTransaction.DebitAmount;
                sourceAccountDetails.Balance -= mappedObj.CreditAmount;
                destinationAccountDetails.Balance += mappedObj.DebitAmount;

                mappedObj.CreatedDateTime = DateTime.Now;
            }

            _context.AccountTransactions.Add(mappedObj);
            await _context.SaveChangesAsync();

            return Ok($"Amount transferred: {mappedObj.DebitAmount?.ToString("c")}");
        }

        [HttpGet]
        [Route("GetUserTransactionsByUserID")]
        public async Task<IActionResult> GetUserTransactionsByUserID(Guid userID)
        {
            Guid? userAccountID = _context.AccountDetails.Where(i => i.UserID == userID).Select(i => i.ObjectID).FirstOrDefault();
            var at = _context.AccountTransactions.Where(i => i.SourceAccountID == userAccountID || i.DestinationAccountID == userAccountID).OrderByDescending(i => i.CreatedDateTime).ToList();
            if(at != null)
            {
                return Ok(_mapper.Map<List<AccountTransaction>, List<AccountTransactionDTO>>(at));
            }

            return NotFound("User account transactions not found.");
        }

        private bool AccountTransactionExists(Guid id)
        {
            return _context.AccountTransactions.Any(e => e.ObjectID == id);
        }
    }
}
