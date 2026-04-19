using System;

namespace PEMSApi;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string CategoryName { get; set; }
    public DateTime TransactionDate { get; set; }
}
