using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrackerModel;

/// <summary>
/// Summary description for TransactionRepository
/// </summary>
public class TransactionRepository : ITransactionRepository, IDisposable
{
    private CrackerEntities _context;

    public TransactionRepository(CrackerEntities crackerEntities)
    {
        this._context = crackerEntities;
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        return _context.Transactions.ToList();
    }

    public IEnumerable<Transaction> GetTransactionsByStatus(int statusId)
    {
        var transactionsById = (from transaction in _context.Transactions
                                group transaction by transaction.BugId into g
                                select g.OrderByDescending(t => t.ChangedOn).FirstOrDefault()).Where(r => r.StatusId == statusId);
        return transactionsById;
    }

    public IEnumerable<Transaction> GetTransactionsByUserAndDate(string selectedUser, DateTime fromDate, DateTime toDate)
    { 
        var result = (from transactions in _context.Transactions
                     select transactions)
                         .Where(t => t.ChangedBy == selectedUser)
                         .Where(t => t.ChangedOn >= fromDate && t.ChangedOn < toDate);
        return result;
    }

    public Transaction GetLastTransactionForBug(int bugId)
    {
        var lastTransaction = (from transactions in _context.Transactions
                               where transactions.BugId == bugId
                               orderby transactions.Id descending
                               select transactions).FirstOrDefault();

        return lastTransaction;
    }

    public void InsertTransaction(Transaction transaction)
    {
        _context.Transactions.AddObject(transaction);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}