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

        using (CrackerEntities myEntity = new CrackerEntities())
        {
            //Verify that the bug we are going to check in can be checked in
            //Get bug ID
            var result = (from bug in myEntity.Bugs
                          where bug.Bug1 == bugName
                          select bug).SingleOrDefault();

            if (result != null)
            {
                //check if the bug is checked out & if it's checked by current user
                var check = (from transaction in myEntity.Transactions
                             where transaction.BugId == result.Id
                             orderby transaction.ChangedOn descending
                             select new { transaction.ChangedBy, transaction.Status.StatusName, transaction.ChangedOn }).First();

                status = Convert.ToString(check.StatusName);
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
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btnUndo_Click(object sender, EventArgs e)
    {
        using (CrackerEntities myEntity = new CrackerEntities())
        {
            string bugName = Request.QueryString.Get("BugID");

            Transaction myTransaction = new Transaction();
            myTransaction.BugId = (from bug in myEntity.Bugs
                                   where bug.Bug1 == bugName
                                   select bug).SingleOrDefault().Id;
            myTransaction.ChangedBy = HttpContext.Current.User.Identity.Name;
            myTransaction.ChangedOn = DateTime.Now;
            myTransaction.StatusId = 9;
            myTransaction.TimeSpent = Int32.Parse(((TextBox)LoginView1.FindControl("txtTime")).Text);
            myTransaction.LanguageId = 14;

            myEntity.Transactions.AddObject(myTransaction);
            myEntity.SaveChanges();
        }
        Response.Redirect("~/Default.aspx");
    }
}