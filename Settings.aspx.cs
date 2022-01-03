using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Settings : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();

    string gameSelected;
    string SelectedCategory;
    string SelectedValue;


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));
        //שמירת מספר מזהה של המשחק שנבחר
        gameSelected = Session["selGame"].ToString();

        //הדפסת שם המשחק לתוך תיבת הטקסט
        string gName = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + gameSelected + "']/gameName").InnerXml;
        gameNameTB.Attributes.Add("placeholder", Server.UrlDecode(gName));

        //הדפסת הנחיות לשחקן לתוך תיבת הטקסט
        string gameDirections = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + gameSelected + "']/gameDirections").InnerXml;
        directionsTB.Attributes.Add("placeholder", Server.UrlDecode(gameDirections));

        //הצגת הדפסת זמן בהתאם למה שהעורך בחר
        string timeValue = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + gameSelected + "']").Attributes["gameTime"].Value;

        timeLimitDropDownList.Items.FindByValue(timeValue).Selected = true;
        timeLimitDropDownList.DataBind();

    }

    protected void saveBtn_Click(object sender, EventArgs e)
    {
        SelectedValue = timeLimitDropDownList.SelectedValue;

        //אם תיבת הטקסט של שם המשחק ריק תשמור לפי השם הקודם
        if(((TextBox)FindControl("gameNameTB")).Text != "")
        {
            myDoc.SelectSingleNode("//game[@gamecode=" + gameSelected + "]/gameName").InnerXml = Server.UrlEncode(((TextBox)FindControl("gameNameTB")).Text);
        }

        //אם התיבת טקסט של שם המשחק לא ריק תשמור את הערך החדש
        if (((TextBox)FindControl("directionsTB")).Text != "")
        {
            myDoc.SelectSingleNode("//game[@gamecode=" + gameSelected + "]/gameDirections").InnerXml = Server.UrlEncode(((TextBox)FindControl("directionsTB")).Text);
        }

        //שמירת הגבלת זמן
        myDoc.SelectSingleNode("//game[@gamecode=" + gameSelected + "]").Attributes["gameTime"].Value = timeLimitDropDownList.SelectedValue;

        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        //ריענון עמוד
        Response.Redirect("Default.aspx");

    }

    protected void cancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


}