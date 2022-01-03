using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void signInBtn_Click(object sender, EventArgs e)
    {
        //אם השם משתמש וסיסמא שהוזנו מתאימים
        if (userNameTB.Text == "hello" && passwordTB.Text == "123")
        {
        Response.Redirect("Default.aspx");
        }

        //אם אחד התיבות טקסט נותרו ריקות
            if (userNameTB.Text == "" || passwordTB.Text == "")
            {

            userNameTB.Text = "";
            passwordTB.Text = "";

            loginInfo.Text = "נא להזין שם משתמש וסיסמא.";

            }

            //אם השם וסיסמא אינם מתאימים
        else
        {
            userNameTB.Text = "";
            passwordTB.Text = "";

            loginInfo.Text = "שם משתמש / סיסמא שגוי.";


        }
    }
}