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
        int bugId;

        try
        {
            using (CrackerEntities myEntities = new CrackerEntities())
            {
                ITransactionRepository transactionRepo = new TransactionRepository();

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

                //check if bug was not checked out previously (check if there is any transaction for this bug)
                var testCheckout = transactionRepo.GetLastTransactionForBug(bugId);

                //if there is an transaction
                if (testCheckout != null)
                {
                    //check if bug is not checked out now
                    if (testCheckout.StatusId != 8)
                    {
                        //checkout bug
                        CheckoutBug(bugId, transactionRepo);

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
                    CheckoutBug(bugId, transactionRepo);

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
    /// Checks out bug
    /// </summary>
    /// <param name="bugId">Id of bug</param>
    /// <param name="transactionRepo">Transaction repository object</param>
    private void CheckoutBug(int bugId, ITransactionRepository transactionRepo)
    {
        Transaction myTransaction = new Transaction
        {
            BugId = bugId,
            StatusId = 8, //Checked Out
            ChangedBy = currentUserName,
            ChangedOn = DateTime.Now,
            LanguageId = 14 //unknown for now, will be set after checkin
        };

        transactionRepo.InsertTransaction(myTransaction);
        transactionRepo.Save();
    }


    /// <summary>
    /// Gets checked out bugs and binds them to GridView
    /// </summary>
    /// <param name="gridID">GridView ID</param>
    /// <param name="myBugs">True for bugs for current user only, false for all bugs except those checked by current user</param>
    protected void setGridView(string gridID, bool myBugs)
    {
            ITransactionRepository transactionRepo = new TransactionRepository();

            var checkedOutTransactions = transactionRepo.GetTransactionsByStatus(8);

            var transactions = checkedOutTransactions;
            if (myBugs)
            {
                transactions = checkedOutTransactions.Where(r => r.ChangedBy == currentUserName);
            }
            else
            {
                transactions = checkedOutTransactions.Where(r => r.ChangedBy != currentUserName);
            }
            ((GridView)LoginView1.FindControl(gridID)).DataSource = transactions;
            ((GridView)LoginView1.FindControl(gridID)).DataBind();
    }
}
