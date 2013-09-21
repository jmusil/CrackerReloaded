using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrackerModel;

public partial class Stats : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && Page.IsValid)
        {
            string selectedUser = ((DropDownList)LoginView1.FindControl("ddlUsers")).SelectedItem.Text;
            using (CrackerEntities myEntity = new CrackerEntities())
            {
                ITransactionRepository transactionRepo = new TransactionRepository(myEntity);

                if (((CheckBox)LoginView1.FindControl("on")).Checked)
                {

                    string wtf = ((TextBox)LoginView1.FindControl("onDate")).Text;
                    DateTime myDate = DateTime.Parse(((TextBox)LoginView1.FindControl("onDate")).Text);
                    DateTime myDateNext = myDate.AddDays(1);

                    var result = transactionRepo.GetTransactionsByUserAndDate(selectedUser, myDate, myDateNext);

                    ((GridView)LoginView1.FindControl("gvStats")).DataSource = result;
                    ((GridView)LoginView1.FindControl("gvStats")).DataBind();
                    ((Label)LoginView1.FindControl("lblTest")).Text = "<strong>Total: </strong>" + result.Count();

                }
                else
                {
                    DateTime myStartDate = DateTime.Parse(((TextBox)LoginView1.FindControl("betweenStartDate")).Text);
                    DateTime myEndDate = DateTime.Parse(((TextBox)LoginView1.FindControl("betweenEndDate")).Text).AddHours(23).AddMinutes(59).AddSeconds(59);

                    var result = transactionRepo.GetTransactionsByUserAndDate(selectedUser, myStartDate, myEndDate);

                    ((GridView)LoginView1.FindControl("gvStats")).DataSource = result;
                    ((GridView)LoginView1.FindControl("gvStats")).DataBind();
                    ((Label)LoginView1.FindControl("lblTest")).Text = "<strong>Total: </strong>" + result.Count();

                }
            }
        }
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (((RadioButton)LoginView1.FindControl("on")).Checked)
        {
            DateTime myDateTime = new DateTime();
            if (DateTime.TryParse(((TextBox)LoginView1.FindControl("onDate")).Text, out myDateTime))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (((RadioButton)LoginView1.FindControl("between")).Checked)
        {
            DateTime myDateTime = new DateTime();
            if (DateTime.TryParse(((TextBox)LoginView1.FindControl("betweenStartDate")).Text, out myDateTime))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (((RadioButton)LoginView1.FindControl("between")).Checked)
        {
            DateTime myDateTime = new DateTime();
            if (DateTime.TryParse(((TextBox)LoginView1.FindControl("betweenEndDate")).Text, out myDateTime))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}