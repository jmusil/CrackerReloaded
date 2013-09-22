using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrackerModel;

/// <summary>
/// Summary description for BugRepository
/// </summary>
public class BugRepository : IBugRepository
{
    private CrackerEntities _context;

    public BugRepository()
	{
        this._context = new CrackerEntities();
	}

    public IEnumerable<Bug> GetAllBugs()
    {
        throw new NotImplementedException();
    }

    public int? GetBugIdByTitle(string title)
    {
        int? bugId = (from bug in _context.Bugs
                     where bug.Bug1 == title
                     select bug.Id).SingleOrDefault();
        return bugId;
    }

    public void InsertBug(Bug bug)
    {
        _context.Bugs.AddObject(bug);
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