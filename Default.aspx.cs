using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrackerModel;

public partial class _Default : System.Web.UI.Page
{
    string gridIdBugsMe = "gvMe";
    string gridIdBugsOthers = "gvOthers";
    string currentUserName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            setGridView(gridIdBugsMe, true);
            setGridView(gridIdBugsOthers, false);
        }
    }

    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        string bug = ((TextBox)LoginView1.FindControl("txtBugId")).Text;
        //string currentUserName = HttpContext.Current.User.Identity.Name;
        int bugId;

        try
        {
            using (CrackerEntities myEntities = new CrackerEntities())
            {
                var result = (from bugs in myEntities.Bugs
                              where bugs.Bug1 == bug
                              select bugs).Count();

                if (result == 0)
                {
                    Bug newBug = new Bug();
                    newBug.Bug1 = bug;
                    myEntities.Bugs.AddObject(newBug);
                    myEntities.SaveChanges();

                }

                bugId = (from bugs in myEntities.Bugs
                         where bugs.Bug1 == bug
                         select bugs.Id).Single();

                //check if bug was not checked out previously
                var testCheckout = (from transactions in myEntities.Transactions
                                    where transactions.BugId == bugId
                                    orderby transactions.Id descending
                                    select transactions).FirstOrDefault();

                if (testCheckout != null)
                {
                    //check if bug is not checked out now
                    if (testCheckout.StatusId != 8)
                    {
                        Transaction myTransaction = new Transaction();

                        myTransaction.BugId = bugId;
                        myTransaction.StatusId = 8; //Checked Out
                        myTransaction.ChangedBy = currentUserName;
                        myTransaction.ChangedOn = DateTime.Now;
                        myTransaction.LanguageId = 14; // Unknown

                        myEntities.Transactions.AddObject(myTransaction);
                        myEntities.SaveChanges();

                        ((Label)LoginView1.FindControl("lblAlreadyCheckedOut")).Visible = false;

                        setGridView(gridIdBugsMe, true);
                        setGridView(gridIdBugsOthers, false);
                    }
                    else
                    {
                        ((Label)LoginView1.FindControl("lblAlreadyCheckedOut")).Visible = true;
                    }
                }
                else
                {
                    Transaction myTransaction = new Transaction();
                    //TODO: extract function
                    myTransaction.BugId = bugId;
                    myTransaction.StatusId = 8; //Checked Out
                    myTransaction.ChangedBy = currentUserName;
                    myTransaction.ChangedOn = DateTime.Now;
                    myTransaction.LanguageId = 14; // Unknown

                    myEntities.Transactions.AddObject(myTransaction);
                    myEntities.SaveChanges();

                    ((Label)LoginView1.FindControl("lblAlreadyCheckedOut")).Visible = false;

                    setGridView(gridIdBugsMe, true);
                    setGridView(gridIdBugsOthers, false);
                }
            }

        }
        catch (DataException ex)
        {
            ((Label)LoginView1.FindControl("lblException")).Visible = true;
            ((Label)LoginView1.FindControl("lblException")).Text = "Data not available, please try again later (" + ex.Message + ").";

        }
        
    }


    /// <summary>
    /// Gets checked out bugs and binds them to GridView
    /// </summary>
    /// <param name="gridID">GridView ID</param>
    /// <param name="myBugs">True for bugs for current user only, false for all bugs except those checked by current user</param>
    protected void setGridView(string gridID, bool myBugs)
    {
        using (CrackerEntities myEntities = new CrackerEntities())
        {
            var allTransactions = (from transaction in myEntities.Transactions
                                  group transaction by transaction.BugId into g
                                  select g.OrderByDescending(t => t.ChangedOn).FirstOrDefault()).Where(r => r.StatusId == 8);
            var transactions = allTransactions;
            if (myBugs)
            {
                transactions = allTransactions.Where(r => r.ChangedBy == currentUserName);
            }
            else
            {
                transactions = allTransactions.Where(r => r.ChangedBy != currentUserName);
            }
            ((GridView)LoginView1.FindControl(gridID)).DataSource = transactions;
            ((GridView)LoginView1.FindControl(gridID)).DataBind();
        }
    }
}
