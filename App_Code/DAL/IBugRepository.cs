using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrackerModel;

/// <summary>
/// Summary description for IBugRepository
/// </summary>
public interface IBugRepository : IDisposable
{
    IEnumerable<Bug> GetAllBugs();
    int? GetBugIdByTitle(string title);
    void InsertBug(Bug bug);
    void Save();
}