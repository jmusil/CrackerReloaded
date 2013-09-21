using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrackerModel;

/// <summary>
/// Summary description for ITransactionRepository
/// </summary>

public interface ITransactionRepository : IDisposable
{
    IEnumerable<Transaction> GetAllTransactions();
    IEnumerable<Transaction> GetTransactionsByStatus(int StatusId);
    Transaction GetLastTransactionForBug(int bugId);
    IEnumerable<Transaction> GetTransactionsByUserAndDate(string selectedUser, DateTime fromDate, DateTime toDate);
    void InsertTransaction(Transaction transaction);
    void Save();

}