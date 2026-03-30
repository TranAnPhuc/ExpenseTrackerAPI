using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PEMSApi.Data;
using Transaction = PEMSApi.Models.Transaction;

namespace PEMSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransactionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            return Ok(transactions);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction.Amount <= 0) return BadRequest("Số tiền phải > 0");

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId);
            if (!categoryExists) return BadRequest("Danh mục không tồn tại");

            if (transaction.TransactionDate == default) transaction.TransactionDate = DateTime.Now;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return StatusCode(201, transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return NotFound("Không tìm thấy");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return Ok("Xóa thành công");
        }
    }
}