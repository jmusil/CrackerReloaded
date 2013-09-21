using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrackerModel;



public partial class CheckInSingle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string bugName = Request.QueryString.Get("BugID");
        

        string checkedOutBy;
        DateTime changedOn;
        

        using (CrackerEntities myEntity = new CrackerEntities())
        {
            ITransactionRepository transactionRepo = new TransactionRepository(myEntity);

            //Verify that the bug we are going to check in can be checked in
            //Get bug ID
            var result = (from bug in myEntity.Bugs
                        where bug.Bug1 == bugName
                        select bug).SingleOrDefault();

            if (result != null)
            {
                var check = transactionRepo.GetLastTransactionForBug(result.Id);

                changedOn = Convert.ToDateTime(check.ChangedOn);
                checkedOutBy = Convert.ToString(check.ChangedBy);

                if (checkedOutBy !=  HttpContext.Current.User.Identity.Name)
                {
                    ((Label)LoginView1.FindControl("lblWarning")).ForeColor = System.Drawing.Color.Red;
                    ((Label)LoginView1.FindControl("lblWarning")).Text = "You're about to check in a bug that was not checked out by you!";
                }

                //get how long has the bug been checked out
                ((TextBox)LoginView1.FindControl("txtTime")).Text = Math.Round(DateTime.Now.Subtract(changedOn).TotalMinutes, 0).ToString();


                ((Label)LoginView1.FindControl("lblBugId")).Text = bugName;
            }
            else
            {
                ((Label)LoginView1.FindControl("lblBugId")).ForeColor = System.Drawing.Color.Red;
                ((Label)LoginView1.FindControl("lblBugId")).Text = bugName + " does not exist!";
                ((Button)LoginView1.FindControl("btnCheckIn")).Enabled = false;

            }
           
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        using (CrackerEntities myEntity = new CrackerEntities())
        {
            ITransactionRepository transactionRepo = new TransactionRepository(myEntity);
            
            string bugName = Request.QueryString.Get("BugID");

            int bugId = (from bug in myEntity.Bugs
                         where bug.Bug1 == bugName
                         select bug).SingleOrDefault().Id;

            Transaction myTransaction = new Transaction
            {
                BugId = bugId,
                ChangedBy = HttpContext.Current.User.Identity.Name,
                ChangedOn = DateTime.Now,
                StatusId = Int32.Parse(((DropDownList)LoginView1.FindControl("ddlResolution")).SelectedValue),
                TimeSpent = Int32.Parse(((TextBox)LoginView1.FindControl("txtTime")).Text),
                LanguageId = Int32.Parse(((DropDownList)LoginView1.FindControl("ddlLanguages")).SelectedValue),
                Note = HttpUtility.HtmlEncode(((TextBox)LoginView1.FindControl("txtNote")).Text)
            };

            transactionRepo.InsertTransaction(myTransaction);
            transactionRepo.Save();
           
        }
        Response.Redirect("~/Default.aspx");
    }
}