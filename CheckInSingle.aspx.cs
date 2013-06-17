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
            string bugName = Request.QueryString.Get("BugID");
            
            Transaction myTransaction = new Transaction();
            myTransaction.BugId = (from bug in myEntity.Bugs
                                   where bug.Bug1 == bugName
                                   select bug).SingleOrDefault().Id;
            myTransaction.ChangedBy = HttpContext.Current.User.Identity.Name;
            myTransaction.ChangedOn = DateTime.Now;
            myTransaction.StatusId = Int32.Parse(((DropDownList)LoginView1.FindControl("ddlResolution")).SelectedValue);
            myTransaction.TimeSpent = Int32.Parse(((TextBox)LoginView1.FindControl("txtTime")).Text);
            myTransaction.LanguageId = Int32.Parse(((DropDownList)LoginView1.FindControl("ddlLanguages")).SelectedValue);
            myTransaction.Note = HttpUtility.HtmlEncode(((TextBox)LoginView1.FindControl("txtNote")).Text);

            myEntity.Transactions.AddObject(myTransaction);
            myEntity.SaveChanges();
        }
        Response.Redirect("~/Default.aspx");
    }
}