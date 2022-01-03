using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument backUP = new XmlDocument();

        //טעינת מסמך 
        backUP.Load(Server.MapPath("trees/XMLFile.xml"));
        //העתקת מסמך ראשי למסמך גיבוי לצורך ביטול שינויים
        backUP.Save(Server.MapPath("trees/XMLFile2.xml"));
    }

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void newGameBtn_Click(object sender, EventArgs e)
    {

        //טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        int gameCounter = Convert.ToInt16(xmlDoc.SelectSingleNode("/theMailman/gameCounter").InnerXml);
        gameCounter++;
        xmlDoc.SelectSingleNode("/theMailman/gameCounter").InnerXml = gameCounter.ToString();


        //יצירת ענף משחק
        XmlElement myNewGameNode = xmlDoc.CreateElement("game");
        myNewGameNode.SetAttribute("gamecode", gameCounter.ToString());
        myNewGameNode.SetAttribute("published", "false");
        myNewGameNode.SetAttribute("gameTime", "30");
        

        //יצירת ענף עם שם המשחק
        XmlElement myNewNameNode = xmlDoc.CreateElement("gameName");
        myNewNameNode.InnerXml =Server.UrlEncode(addNameTB.Text);
        myNewGameNode.AppendChild(myNewNameNode);

        //יצירת ענף הנחיות לשחקן
        XmlElement myGameDirections = xmlDoc.CreateElement("gameDirections");
        myGameDirections.InnerXml = "גרור את המכתב אל התיבה המתאימה.";
        myNewGameNode.AppendChild(myGameDirections);

        //יצירת ענף סופר קטגוריות
        XmlElement categoriesCounterNode = xmlDoc.CreateElement("categoryCounter");
        categoriesCounterNode.InnerXml = "0";
        myNewGameNode.AppendChild(categoriesCounterNode);

        //יצירת ענף קטגוריות
        XmlElement myCategoriesNode = xmlDoc.CreateElement("categories");
        myNewGameNode.AppendChild(myCategoriesNode);

        //יצירת ענף סופר פריטים
        XmlElement itemsCounterNode = xmlDoc.CreateElement("itemCounter");
        itemsCounterNode.InnerXml = "0";
        myNewGameNode.AppendChild(itemsCounterNode);

        //יצירת ענף פריטים
        XmlElement myItemsNode = xmlDoc.CreateElement("items");
        myNewGameNode.AppendChild(myItemsNode);


        //הוספת משחק לראש הרשימה
        XmlNode FirstGame = xmlDoc.SelectNodes("/theMailman/game").Item(0);
        xmlDoc.SelectSingleNode("/theMailman").InsertBefore(myNewGameNode, FirstGame);
        XmlDataSource1.Save();
        GridView1.DataBind();

        addNameTB.Text = "";

    }



    protected void isPassCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        CheckBox myCheckBox = (CheckBox)sender;
       
        //משתנה למספר המשחק
        string theId = myCheckBox.Attributes["theItemId"];

        //משתנה הבוחר את המשחק שנבחר
        XmlNode theGame = xmlDoc.SelectSingleNode("/theMailman/game[@gamecode='" + theId + "']");

        //משתנה הבודק אם הצ'ק בוקס מסומן או לא
        bool NewIsPass = myCheckBox.Checked;

        //עדכון העץ במצב בצ'ק בוקס- מסומן או לא
        theGame.Attributes["published"].InnerText = NewIsPass.ToString();

        XmlDataSource1.Save();
        GridView1.DataBind();

    }

    public string ToolTipLabel(string gameID)
    {

        string returnClass = "hide";

        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument(); //טעינה של העץ

        //ספירת מספר קטגוריות קיימות
        int categoryCounter = xmlDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + gameID + "']/categories").ChildNodes.Count;

        //תנאי- אם יש לפחות 3 קטגוריות
        if (categoryCounter >= 3)
        {
            for (int i = 0; i < categoryCounter; i++) //תעבור על כל הקטגוריות
            {
                XmlNode existingCategories = xmlDoc.SelectNodes("/theMailman/game[@gamecode= '" + gameID + "']/categories/category").Item(i);

                //משתנה השומר את המספר ID של קטגוריות קיימות
                int categoryID = Convert.ToInt16(existingCategories.Attributes["categoryNum"].Value);


                //ספירה כמה פריטים יש בכל קטגוריה
                int itemsCounter = xmlDoc.SelectNodes("/theMailman/game[@gamecode= '" + gameID + "']/items/item[@categoryNum='" + categoryID + "']").Count;

                if (itemsCounter < 5) //אם יש פחות מ5 פריטים בקטגוריה שנבחרה
                {
                    returnClass = "";
                }

            }
        }
        else //אם יש פחות מ3 קטגוריות
        {
            returnClass = "";
        }
        return returnClass;

    }

    public string CheckIfCanPublish(string gameID)
    {
        string returnClass = "CheckBoxEnabled";

        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument(); //טעינה של העץ

        //ספירת מספר קטגוריות קיימות
        int categoryCounter = xmlDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + gameID + "']/categories").ChildNodes.Count;

        //תנאי- אם יש לפחות 3 קטגוריות
        if (categoryCounter >= 3)
        {
            for (int i=0; i<categoryCounter; i++) //תעבור על כל הקטגוריות
            {
                XmlNode existingCategories = xmlDoc.SelectNodes("/theMailman/game[@gamecode= '" + gameID + "']/categories/category").Item(i);

                //משתנה השומר את המספר ID של קטגוריות קיימות
                int categoryID = Convert.ToInt16(existingCategories.Attributes["categoryNum"].Value);


                //ספירה כמה פריטים יש בכל קטגוריה
                int itemsCounter = xmlDoc.SelectNodes("/theMailman/game[@gamecode= '" + gameID + "']/items/item[@categoryNum='" + categoryID + "']").Count;

                if(itemsCounter < 5) //אם יש פחות מ5 פריטים בקטגוריה שנבחרה
                {
                    returnClass = "CheckBoxDisabled";
                    XmlNode myGame = xmlDoc.SelectSingleNode("/theMailman/game[@gamecode=" + gameID + "]"); //קבלת המשחק שאנו רוצים
                    myGame.Attributes["published"].InnerText = "false"; //במידה ולא ניתן לפרסם יש להגדיר בעץ כfalse
                    XmlDataSource1.Save();
                }

            }
        }
        else //אם יש פחות מ3 קטגוריות
        {
            returnClass = "CheckBoxDisabled";
            XmlNode myGame = xmlDoc.SelectSingleNode("/theMailman/game[@gamecode=" + gameID + "]"); //קבלת המשחק שאנו רוצים
            myGame.Attributes["published"].InnerText = "false"; //במידה ולא ניתן לפרסם יש להגדיר בעץ כfalse
            XmlDataSource1.Save();
        }
        return returnClass;

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //יביא את האלמנט שהפעיל את הפונקציה הזאת
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["selGame"] = i.Attributes["theItemId"];


        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                deleteConf();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":


                Session["selCategory"] = null;
                Response.Redirect("Edit.aspx");
                break;

            case "settingsRow":
                Response.Redirect("Settings.aspx");
                break;
        }

    }

    void deleteConf()
    {
        //הצגה של המסך האפור
        grayWindows0.Style.Add("display", "block");
        //הצגת הפופ-אפ של המחיקה
        DeleteConfPopUp0.Style.Add("display", "block");
    }

   

    protected void confYesDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows0.Style.Add("display", "none");

        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void confnNoDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows0.Style.Add("display", "none");

    }
}