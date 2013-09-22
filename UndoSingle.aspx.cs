using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrackerModel;

public partial class UndoSingle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string bugName = Request.QueryString.Get("BugID");
        ((Label)LoginView1.FindControl("lblBugID")).Text = bugName;

        string checkedOutBy;
        DateTime changedOn;
        string status;


        ITransactionRepository transactionRepo = new TransactionRepository();
        IBugRepository bugRepo = new BugRepository();

        //Verify that the bug we are going to check in can be checked in
        //Get bug ID
        var result = bugRepo.GetBugIdByTitle(bugName);

        if (result != null)
        {
            //check if the bug is checked out & if it's checked by current user
            var check = transactionRepo.GetLastTransactionForBug((int)result);

            status = Convert.ToString(check.Status.StatusName);
            changedOn = Convert.ToDateTime(check.ChangedOn);
            checkedOutBy = Convert.ToString(check.ChangedBy);

            if (checkedOutBy != HttpContext.Current.User.Identity.Name)
            {
                ((Label)LoginView1.FindControl("lblWarning")).ForeColor = System.Drawing.Color.Red;
                ((Label)LoginView1.FindControl("lblWarning")).Text = "You're about to undo a bug that was not checked out by you!";
            }

            //get how long has the bug been checked out
            ((TextBox)LoginView1.FindControl("txtTime")).Text = Math.Round(DateTime.Now.Subtract(changedOn).TotalMinutes, 0).ToString();


            ((Label)LoginView1.FindControl("lblBugID")).Text = bugName;
        }
        else
        {
            ((Label)LoginView1.FindControl("lblBugID")).ForeColor = System.Drawing.Color.Red;
            ((Label)LoginView1.FindControl("lblBugID")).Text = bugName + " does not exist!";
            ((Button)LoginView1.FindControl("btnUNdo")).Enabled = false;

        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btnUndo_Click(object sender, EventArgs e)
    {

        ITransactionRepository transactionRepo = new TransactionRepository();
        IBugRepository bugRepo = new BugRepository();

        string bugName = Request.QueryString.Get("BugID");
        var bugId = bugRepo.GetBugIdByTitle(bugName);

        Transaction myTransaction = new Transaction
        {
            ChangedBy = HttpContext.Current.User.Identity.Name,
            ChangedOn = DateTime.Now,
            BugId = (int)bugId,
            StatusId = 9,
            TimeSpent = Int32.Parse(((TextBox)LoginView1.FindControl("txtTime")).Text),
            LanguageId = 14
        };

        transactionRepo.InsertTransaction(myTransaction);
        transactionRepo.Save();

        Response.Redirect("~/Default.aspx");
    }
}