// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

using System;

public class Transactions
{
    public static void MainTest(string[] args)
    {
        List<TransactionTest> listTrans = new List<TransactionTest>(){
          new TransactionTest {id=1,tien=500,laChi = true},
          new TransactionTest {id=2,tien=1000,laChi = false},
          new TransactionTest {id=3,tien=200,laChi=true},
          new TransactionTest {id=4,tien=300,laChi =false},
          new TransactionTest{id=5,tien=100,laChi=true}
        };

        decimal sumThu = 0;
        decimal sumChi = 0;

        for (var index = 0; index < listTrans.Count; index++)
        {
            if (listTrans[index].laChi)
            {
                sumChi += listTrans[index].tien;
            }
            else
            {
                sumThu += listTrans[index].tien;
            }
        }

        Console.WriteLine($"Tong thu la {sumThu}");
        Console.WriteLine($"Tong chi la {sumChi}");
    }
}

public class TransactionTest
{
    public int id { get; set; }
    public decimal tien { get; set; }
    public bool laChi { get; set; }
}