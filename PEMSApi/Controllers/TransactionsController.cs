using System.Collections;
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
        public async Task<IActionResult> GetTransactions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var totalIncome = await _context.Transactions
                    .Include(t => t.Category)
                    .Where(t => t.Category!.Type == false)
                    .SumAsync(t => t.Amount);

            var totalExpense = await _context.Transactions
                    .Include(t => t.Category)
                    .Where(t => t.Category!.Type == true)
                    .SumAsync(t => t.Amount);

            var balance = totalIncome - totalExpense;

            var skipAmount = (pageNumber - 1) * pageSize;

            var transactions = await _context.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .Skip(skipAmount)
                .Take(pageSize)
                .Select(t => new TransactionResponseDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    CategoryName = t.Category != null ? t.Category.Name : "Khong xac dinh",
                    TransactionDate = t.TransactionDate
                })
                .ToListAsync();

            var totalRecords = await _context.Transactions.CountAsync();

            var response = new
            {
                Data = transactions,
                TotalRecords = totalRecords,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize),
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance
            };

            return Ok(response);
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

            if (transaction.TransactionDate == default)
            {
                transaction.TransactionDate = DateTime.UtcNow;
            }
            else
            {
                // Ép kiểu về utc để  postgresql chịu lưu
                transaction.TransactionDate = transaction.TransactionDate.ToUniversalTime();
            }

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