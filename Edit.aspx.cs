using System;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;
using System.Web.UI;

public partial class Edit : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();

    //הגדרת נתיב לשמירת הקובץ
    string imagesLibPath = "uploadedFiles/";
    string imageNewName;




    protected void Page_Load(object sender, EventArgs e)
    {

        if ((string)Session["selCategory"] != null) //במידה וזו לא הטעינה הראשונה של העמוד- קיבל כבר ערך לקטגוריה שנבחרה
        {
            //החזרת הצבע לפאנל הפריטים
            itemsPanel.CssClass = "itemsPanelenabled";

            //רדיו-באטן במצב לא פעיל
            ItemTypeRBT.Enabled = true;

            //החזרת צבע הפונט לפריטים
            ItemsBigPanel.CssClass = ItemsBigPanel.CssClass.Replace("greyfont", "blackfont");


            //ספירת מספר פריטים בכל קטגוריה
            int itemCounter = myDoc.SelectNodes("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/items/item[@categoryNum='" + (string)Session["selCategory"] + "']").Count;

            if (itemCounter >= 10)
            {
                newItemTxt.Attributes.Add("disabled", "true");
                newItemBtn.Attributes.Add("disabled", "true");
                LabelCounter5.Text = "ניתן להוסיף עד 10 פריטים.";
            }

                //המרת מספר קטגוריה נבחרה לערך מספרי
                int catID = Convert.ToInt16((string)Session["selCategory"]);
                colorCategory(catID); //הפעלת פונקציה שצובעת את כפתור הקטגוריה שנבחרה



            //הדפסת הפריטים המעודכנים של הקטגוריה שנלחצה
            showItems((string)Session["selGame"], (string)Session["selCategory"]);

        }
        else //טעינה ראשונה של העמוד- עוד לא קיבל ערך לקטגוריה שנבחרה
        {

            //פאנל פריטים אפור
            itemsPanel.CssClass = "itemsPaneldisabled";

            //טקסט בוקס במצב לא פעיל
            newItemTxt.Attributes.Add("disabled", "true");

            //רדיו באטן מצב לא פעיל
            ItemTypeRBT.Enabled = false;

            //לולאה שעוברת על כל השורות בגריד-ויו
            for (int j = 0; j < GridView2.Rows.Count; j++)
            {
                //הסתרת כפתורי מחיקה
                ((ImageButton)GridView2.Rows[j].FindControl("deleteImageButton")).Visible = false;
                //הסתרת כפתורי עריכה
                ((ImageButton)GridView2.Rows[j].FindControl("editImageButton")).Visible = false;
            }

        }

    }


    protected void Page_Init(object sender, EventArgs e)
    {

        //-------------------------הדפסת שם המשחק הכותרת העמוד------------------------------

        //הטענת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        //הגדרת מקור לאקס-פט
        XmlDataSource1.XPath = "/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/categories/category";

        //הדפסת שם המשחק
        XmlNode node = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/gameName");
        gameName.Text = Server.UrlDecode(node.InnerXml);

        //------------------------------הגבלת מספר קטגוריות ופריטים------------------------------

        //ספירת מספר קטגוריות
        int categoryTotal = myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/categories").ChildNodes.Count;

        if (categoryTotal >= 5) //עד 5 קטגוריות
        {
            newCategoryTxt.Attributes.Add("disabled", "true");
            LabelCounter4.Text = "ניתן להוסיף עד 5 קטגוריות.";
        }



        //------------------------------יצירה של הפריטים------------------------------

        //קטגוריות של המשחק שנבחר בעץ
        XmlNodeList myNodes1;
        myNodes1 = myDoc.SelectNodes("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/categories/category");

        //לכל קטגוריה תעשה את הפכולות הבאות
        foreach (XmlNode myNode1 in myNodes1)
        {
            string categoryID = myNode1.Attributes["categoryNum"].Value; //מספר קטגוריה אליה הפריט שייך

            //מספר ייחודי ללייבלים שסופרים תווים
            int LCounter = 6;

            //שליפת פריטים השייכים לקטגוריה מהעץ
            XmlNodeList myNodes;
            myNodes = myDoc.SelectNodes("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/items/item[@categoryNum='" + categoryID + "']");


            //יצירת פאנלים ייחודים לכל קטגוריה- ובתוכו הפריטים שלו
            Panel categoryPanel = new Panel();
            categoryPanel.ID = "categoryPanel" + categoryID;
            categoryPanel.Style.Add("display", "none");


            //יצירת הפריטים
            foreach (XmlNode myNode in myNodes)
            {
                string itemNum = myNode.Attributes["itemNum"].Value; //מספר ייחודי לכל פריט


                if (myNode.Attributes["itemType"].Value == "text") //אם הפריט טקסט
                {
                    //יצירת לייבל להדפסת הטקסט של הפריט
                    Label newItemLabel = new Label();
                    newItemLabel.ID = "itemNum" + categoryID + myNode.Attributes["itemNum"].Value;
                    newItemLabel.CssClass = "itemsGroup";
                    newItemLabel.Text = Server.UrlDecode(myNode.InnerXml);

                    //יצירת טקסט-בוקס לעריכת הטקסט של הפריט
                    TextBox editTB = new TextBox();
                    editTB.ID = "editTB" + categoryID + myNode.Attributes["itemNum"].Value;
                    editTB.TextMode = TextBoxMode.MultiLine;
                    editTB.CssClass = "editTBs CharacterCount";
                    editTB.Text = Server.UrlDecode(myNode.InnerXml);
                    editTB.Attributes.Add("item", categoryID + myNode.Attributes["itemNum"].Value);
                    editTB.Attributes.Add("CharacterLimit", "30");
                    editTB.Attributes.Add("saveBtn", "savePageBtn");

                    //יצירת לייבל לספירת התווים כאשר עורכים טקסט
                    Label LabelCounter = new Label();
                    LabelCounter.ID = "LabelCounter" + categoryID + myNode.Attributes["itemNum"].Value;
                    LabelCounter.CssClass = "LabelCounter";
                    LabelCounter.Text = "הזן עד 30 תווים";

                    //יצירת כפתור לעריכת הפריט
                    ImageButton editItemBtn = new ImageButton();
                    editItemBtn.ImageUrl = "~/images/edit.png";
                    editItemBtn.ID = "editBtn" + categoryID + myNode.Attributes["itemNum"].Value;
                    editItemBtn.CssClass = "edit_delete_Group";
                    editItemBtn.Click += new ImageClickEventHandler(editItemBtn_Click);

                    //יצירת כפתור למחיקת הפריט
                    ImageButton deleteItemBtn = new ImageButton();
                    deleteItemBtn.ImageUrl = "~/images/delete.png";
                    deleteItemBtn.ID = "deleteBtn" + categoryID + myNode.Attributes["itemNum"].Value;
                    deleteItemBtn.CssClass = "edit_delete_Group";
                    deleteItemBtn.Click += new ImageClickEventHandler(deleteItemConf_Click);

                    //יצירת פאנל לכפתורי "עריכה" ו"מחק"
                    Panel itemButtonsPanel = new Panel();
                    itemButtonsPanel.CssClass = "itemButtonsPanel";
                    itemButtonsPanel.ID = "itemButtonsPanel" + categoryID + myNode.Attributes["itemNum"].Value;

                    //יצירת פאנל עוטף לכל פריט
                    Panel smallItemPanel = new Panel();
                    smallItemPanel.ID = "smallItemPanel" + categoryID + myNode.Attributes["itemNum"].Value;
                    smallItemPanel.CssClass = "itemsPan";

                    //משתנה לזיהוי ייחודי לליבל ספירת תויים
                    LCounter++;

                    //הוספת פקדים לפאנל
                    itemButtonsPanel.Controls.Add(editItemBtn);
                    itemButtonsPanel.Controls.Add(deleteItemBtn);
                    smallItemPanel.Controls.Add(editTB);
                    smallItemPanel.Controls.Add(newItemLabel);
                    smallItemPanel.Controls.Add(itemButtonsPanel);
                    smallItemPanel.Controls.Add(LabelCounter);
                    categoryPanel.Controls.Add(smallItemPanel);

                }

                else //אם הפריט תמונה
                {
                    //יצירת תמונה
                    System.Web.UI.WebControls.Image picitem = new System.Web.UI.WebControls.Image();
                    picitem.ImageUrl = Server.UrlDecode(myNode.InnerXml);
                    picitem.CssClass = "picItemsGroup";

                    //יצירת כפתור לשינוי התמונה
                    ImageButton uploadImageItemBtn = new ImageButton();
                    uploadImageItemBtn.ImageUrl = "~/images/camera.png";
                    uploadImageItemBtn.ID = "uploadImageItemBtn" + categoryID + myNode.Attributes["itemNum"].Value;
                    uploadImageItemBtn.CssClass = "edit_delete_Group";
                    uploadImageItemBtn.OnClientClick = "openFileUploader2(" + myNode.Attributes["itemNum"].Value + "); return false;";

                    //יצירת כפתור למחיקת התמונה
                    ImageButton deleteItemBtn = new ImageButton();
                    deleteItemBtn.ImageUrl = "~/images/delete.png";
                    deleteItemBtn.ID = "deleteBtn" + categoryID + myNode.Attributes["itemNum"].Value; ;
                    deleteItemBtn.CssClass = "edit_delete_Group";
                    deleteItemBtn.Click += new ImageClickEventHandler(deleteItemConf_Click);

                    //יצירת פאנל לכפתורי "עריכה" ו"מחק"
                    Panel itemButtonsPanel = new Panel();
                    itemButtonsPanel.CssClass = "itemButtonsPanel";

                    //יצירת פאנל עוטף לכל פריט
                    Panel smallItemPanel = new Panel();
                    smallItemPanel.ID = "smallItemPanel" + categoryID + myNode.Attributes["itemNum"].Value;
                    smallItemPanel.CssClass = "itemsPan";
                    
                    //הוספת פקדים לפאנל שעל הבמה
                    itemButtonsPanel.Controls.Add(uploadImageItemBtn);
                    itemButtonsPanel.Controls.Add(deleteItemBtn);
                    smallItemPanel.Controls.Add(picitem);
                    smallItemPanel.Controls.Add(itemButtonsPanel);
                    categoryPanel.Controls.Add(smallItemPanel);


                }
            }

            itemsPanel.Controls.Add(categoryPanel);
            itemsPanel.DataBind();

        }

    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "deleteRow")//אם נלחץ על כפתור המחיקה
        {
            //יביא את האלמנט שהפעיל את הפונקציה הזאת
            ImageButton i = (ImageButton)e.CommandSource;

            // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
            Session["selCategory"] = i.Attributes["theItemId"];

            //הפעלת פונקציית וידוי מחיקה
            deleteCategoryConf();

        }

        if (e.CommandName == "CategoryIdCheck")//אם נלחץ כפתור הקטגוריה
        {
            //יביא את האלמנט שהפעיל את הפונקציה הזאת
            Button i = (Button)e.CommandSource;
            // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
            Session["selCategory"] = i.Attributes["theItemId"];
            GridViewRow gvrow = (GridViewRow)(i.Parent.Parent);
            int index = gvrow.RowIndex;

            //ריענון עמוד
            Response.Redirect("Edit.aspx");

        }

        if (e.CommandName == "Update")
        {
            Button i = (Button)e.CommandSource;
            Session["selCategory"] = i.Attributes["theItemId"];
            string theIteID = (string)Session["selCategory"];
            GridViewRow gvrow = (GridViewRow)(i.Parent.Parent);
            int index = gvrow.RowIndex;

            approveUpdate(theIteID, index);
            
        }


    }
    protected void colorCategory(int categoryID)
    {

        string rowSelected = null;


        //לולאה שעוברת על כל השורות בגריד-ויו
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            //הוספת קלאס לרקע לבן
            if(((Button)GridView2.Rows[j].FindControl("CategoryBtn")) != null)
            {
                ((Button)GridView2.Rows[j].FindControl("CategoryBtn")).CssClass = "CategoryBtns";

                //הסתרת כפתורי מחיקה
                ((ImageButton)GridView2.Rows[j].FindControl("deleteImageButton")).Visible = false;
                //הסתרת כפתורי עריכה
                ((ImageButton)GridView2.Rows[j].FindControl("editImageButton")).Visible = false;

                if (((Button)GridView2.Rows[j].FindControl("CategoryBtn")).Attributes["theItemId"] == categoryID.ToString())
                {
                    rowSelected = j.ToString();
                }

            }
            

        }

        if(rowSelected != null)
        {
            int myRow = Convert.ToInt16(rowSelected);

            //הוספת רקע כחול לכפתור שנלחץ
            ((Button)GridView2.Rows[myRow].FindControl("CategoryBtn")).CssClass = "blueBackground";
        //הצגת כפתור מחיקה לקטגוריה שנלחצה
        ((ImageButton)GridView2.Rows[myRow].FindControl("deleteImageButton")).Visible = true;
        //הצגת כפתור עריכה לקטגורה שנבחרה
        ((ImageButton)GridView2.Rows[myRow].FindControl("editImageButton")).Visible = true;
        }


        //שינוי צבע פאנל של הפריטים
        itemsPanel.CssClass = itemsPanel.CssClass.Replace("itemsPaneldisabled", "itemsPanelenabled");

    }




    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        DataBind();

        //לולאה שעוברת על כל השורות בגריד-ויו
        for (int j = 0; j < GridView2.Rows.Count; j++)
        {
            //הוספת קלאס לרקע לבן
            if (((Button)GridView2.Rows[j].FindControl("CategoryBtn")) != null)
            {
                //הסתרת כפתורי מחיקה
                
                ((ImageButton)GridView2.Rows[j].FindControl("deleteImageButton")).CssClass = ((ImageButton)GridView2.Rows[j].FindControl("deleteImageButton")).CssClass.Replace("edit_delete_categoryBtns", "hide");
            }
        }

       
    }



    protected void approveUpdate(string theItemID, int RowIndex)
    {

        string newCatName = ((TextBox)GridView2.Rows[RowIndex].FindControl("CategoryEditTB")).Text;


        int a = Convert.ToInt16((string)Session["selCategory"]);

        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        Document.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/categories/category[@categoryNum='" + theItemID + "']").InnerText = newCatName;


        XmlDataSource1.Save();
        GridView2.EditIndex = -1;
        GridView2.DataBind();

        
        Response.Redirect("Edit.aspx");
    }



    protected void newCategoryBtn_Click(object sender, EventArgs e)
    {
        //טעינה של העץ
        XmlDocument myDoc = XmlDataSource1.GetXmlDocument();

        //שליפת סופר קטגוריות ועדכון ערך חדש
        int categoryCounter = Convert.ToInt16(myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/categoryCounter").InnerXml);
        categoryCounter++;
        myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/categoryCounter").InnerXml = categoryCounter.ToString();

        //יצירת קטגוריה
        XmlElement myCategoryNode = myDoc.CreateElement("category");
        myCategoryNode.SetAttribute("categoryNum", categoryCounter.ToString());
        myCategoryNode.InnerXml = Server.UrlEncode(newCategoryTxt.Text);

        //הוספת הקטגוריה לראש הרשימה
        XmlNode FirstGame = myDoc.SelectNodes("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//category").Item(0);

        if (myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//categories").ChildNodes.Count == 0)
        {
            myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//categories").AppendChild(myCategoryNode);
        }
        else
        {
            myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//categories").InsertBefore(myCategoryNode, FirstGame);
        }

        //שמירה ועדכון שינויים בעץ ובגריד-ויו
        XmlDataSource1.Save();
        GridView2.DataBind();


        //איפוס תיבת הטקסט להוספת קטגוריה חדשה
        newCategoryTxt.Text = "";

        //שמירת מספר הקטגוריה
        Session["selCategory"] = categoryCounter.ToString();

        //ריענון העמוד
        Response.Redirect("Edit.aspx");

    }


    protected void NewItemBtn_Click(object sender, EventArgs e)
    {
        //טעינה של העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        //שליפת סופר פריטים ועדכון ערך חדש
        int itemCounter = Convert.ToInt16(myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/itemCounter").InnerXml);
        itemCounter++;
        myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/itemCounter").InnerXml = itemCounter.ToString();

        //יצירת פריט
        XmlElement myItemNode = myDoc.CreateElement("item");
        myItemNode.SetAttribute("itemNum", itemCounter.ToString());
        if (ItemTypeRBT.SelectedValue == "text") //אם הפריט מסוג טקסט
        {
            myItemNode.SetAttribute("itemType", "text");
            myItemNode.InnerXml = Server.UrlEncode(newItemTxt.Text);
        }
        if (ItemTypeRBT.SelectedValue == "pic")//אם הפריט מסוג תמונה
        {
            myItemNode.SetAttribute("itemType", "picture");
            myItemNode.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName);
        }
        myItemNode.SetAttribute("categoryNum", (string)Session["selCategory"]);


      

        //הוספת פריט חדש לעץ
        myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//items").AppendChild(myItemNode);

        //שמירה של הפריטים החדשים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        //איפוס תיבת טקסט להוספת פריט חדש
        newItemTxt.Text = "";

        //ריענון העמוד
        Response.Redirect("Edit.aspx");
    }


    void showItems(string gamNum, string CatNum)
    {
        //שליפת קטגוריות מעץ
        XmlNodeList myNodes1;
        myNodes1 = myDoc.SelectNodes("/theMailman/game[@gamecode='" + gamNum + "']/categories/category");


        foreach (XmlNode myNode1 in myNodes1)
        {
            //שליפת מספר מזהה של הקטגוריה
            string categoryID = myNode1.Attributes["categoryNum"].Value;
            //הסתרת הפאנלים של כל הקטגוריות
            ((Panel)FindControl("categoryPanel" + categoryID)).Style.Add("display", "none");

        }
            //תצוגת הפאנל של הקטגוריה שנבחרה
            ((Panel)FindControl("categoryPanel" + CatNum)).Style.Add("display", "block");

    }


    protected void editItemBtn_Click(object sender, ImageClickEventArgs e)
    {
        //שליפת מספר פריט מהאי-די של הכפתור
        var button = (ImageButton)sender;
        string buttonName = button.ID;
        string CategoryandItemId = buttonName.Substring(7);

        //הצגת טקסט-בוקס לעריכת פריט
        ((TextBox)FindControl("editTB" + CategoryandItemId)).Style.Add("display", "block");
        //הצגת לייבל של ספירת תווים
        ((Label)FindControl("LabelCounter" + CategoryandItemId)).Style.Add("display", "block");
        //הסתרת לייבל של טקסט הפריט
        ((Label)FindControl("itemNum" + CategoryandItemId)).Style.Add("display", "none");
        //הסתרת כפתור עריכת פריט
        ((ImageButton)FindControl("editBtn" + CategoryandItemId)).Style.Add("display", "none");

       



    }




    protected void ItemTypeRBT_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SelectedValue = ItemTypeRBT.SelectedValue;
        if (ItemTypeRBT.SelectedValue == "text") //אם הערך שנבחר הוא מסוג טקסט
        {
            //הצגת הפקדים המתאימים
            newItemBtn.Visible = true;
            newItemTxt.Visible = true;
            newImageBtn.Visible = false;
            LabelCounter5.Visible = true;
        }

        if (ItemTypeRBT.SelectedValue == "pic") //אם הערך שנבחר הוא תמונה
        {
            //הצגת הפקדים המתאימים
            newImageBtn.Visible = true;
            newItemTxt.Visible = false;
            newItemBtn.Visible = false;
            LabelCounter5.Visible = false;

        }
    }




    void deleteCategoryConf()
    {

        //הצגה של המסך האפור
        grayWindows.Style.Add("display", "block");
        //הצגת הפופ-אפ של המחיקה
        categoryDeletePopUp.Style.Add("display", "block");
        //הסתרת פופ-אפ מחיקת פריט
        itemDeletePopUp.Style.Add("display", "none");

    }

    protected void YesDeleteCategory_Click(object sender, EventArgs e)
    {

        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");

        //מחיקת הקטגוריה מהעץ
        XmlDocument Document = XmlDataSource1.GetXmlDocument();
        XmlNode node = Document.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/categories/category[@categoryNum='" + (string)Session["selCategory"] + "']");
        node.ParentNode.RemoveChild(node);

        //מחיקת הפריטים השייכים לקטגוריה מהעץ
        XmlNodeList myNodes;
        myNodes = Document.SelectNodes("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/items/item[@categoryNum='" + (string)Session["selCategory"] + "']");
        foreach (XmlNode myNode in myNodes)
        {
            myNode.ParentNode.RemoveChild(myNode);
        }

        //שמירה ועדכון לתוך העץ ולגריד ויו
        XmlDataSource1.Save();
        GridView2.DataBind();

        //איפוס הדף ללא הצגת פריטים
        Session["selCategory"] = null;
        Response.Redirect("Edit.aspx");

    }


    protected void deleteItemConf_Click(object sender, ImageClickEventArgs e)
    {

        //שליפת מספר פריט מהאי-די של הכפתור
        var button = (ImageButton)sender;
        string buttonName = button.ID;
        string CategoryandItemId = buttonName.Substring(10);

        Session["itemSelected"] = CategoryandItemId;


        //הצגה של המסך האפור
        grayWindows.Style.Add("display", "block");
        //הצגת הפופ-אפ של המחיקה
        itemDeletePopUp.Style.Add("display", "block");
        //הסתרת פופ-אפ מחיקת קטגוריה
        categoryDeletePopUp.Style.Add("display", "none");

    }

    protected void YesDeleteItem_Click(object sender, EventArgs e)
    {

        //מציאת הפריט בעץ
        XmlNode myNode = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']//items/item[@itemNum='" + (string)Session["itemSelected"] + "']");

        //משתנה לשם התמונה
        string oldFileName = Server.UrlDecode(myNode.InnerXml);


        if (Server.UrlDecode(myNode.Attributes["itemType"].Value) == "picture") //אם הפריט טקסט
        {
            var filePath = Server.MapPath(oldFileName); //מחיקת התמונה מקובץ ההעלאות

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        //מחיקת התמונה מהעץ
        myNode.ParentNode.RemoveChild(myNode); //מחיקת הפריט

        //שמירת שינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");

        //ריענון הדף
        Response.Redirect("Edit.aspx");

    }

    protected void NoDelete_Click(object sender, EventArgs e)
    {
        //הסתרה של המסך האפור
        grayWindows.Style.Add("display", "none");
    }

    protected void savePageBtn_Click(object sender, EventArgs e)
    {
        //קטגוריות של המשחק שנבחר בעץ
        XmlNodeList myNodes1;
        myNodes1 = myDoc.SelectNodes("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/categories/category");

        //לכל קטגוריה תעשה את הפעולות הבאות
        foreach (XmlNode myNode1 in myNodes1)
        {
            string categoryID = myNode1.Attributes["categoryNum"].Value; //משתנה למספר הקטגוריה

            //שליפת פריטים השייכים לקטגוריה מהעץ
            XmlNodeList myNodes;
            myNodes = myDoc.SelectNodes("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/items/item[@categoryNum='" + categoryID + "']");

            //לולאה פנימית של כל הפריטים מסוג טקסט
            foreach (XmlNode myNode in myNodes)
            {
                string itemNum = myNode.Attributes["itemNum"].Value; //משתנה למספר פריט

                if (myNode.Attributes["itemType"].Value == "text") //אם הפריט טקסט
                {
                    myNode.InnerXml = Server.UrlEncode(((TextBox)FindControl("editTB" + categoryID + itemNum)).Text); //עדכון העץ לפי התיבות טקסט
                }

            }
        }

        //שמירת השינויים בעץ
        myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        //העברה לעמוד הראשי
        Response.Redirect("Default.aspx");

    }

    protected void cancelBtn_Click(object sender, EventArgs e)
    {

        XmlDocument backUp = new XmlDocument();

        //טעינת מסמך 
        backUp.Load(Server.MapPath("~/trees/XMLFile2.xml"));

        //העתקת מסמך ראשי למסמך גיבוי לצורך ביטול שינויים
        backUp.Save(Server.MapPath("~/trees/XMLFile.xml"));

        //העברה לעמוד הראשי
        Response.Redirect("Default.aspx");

    }

    protected void SavePic1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string filetype = FileUpload1.PostedFile.ContentType;//איתור סוג הקובץ 

            //בדיקה האם הקובץ הנקלט הוא מסוג תמונה
            if (filetype.Contains("image"))
            {
                //איתור שם הקובץ
                string fileName = FileUpload1.PostedFile.FileName;
                //איתור סיומת הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //איתור זמן העלת הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                //הגדרת שם חדש לקובץ
                imageNewName = myTime + endOfFileName;


                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload1.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 107, 137);


                //שמירת הקובץ בגודלו החדש בתיקייה
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);


                //שליפת סופר פריטים ועדכון ערך חדש
                int itemCounter = Convert.ToInt16(myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/itemCounter").InnerXml);
                itemCounter++;
                myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']/itemCounter").InnerXml = itemCounter.ToString();

                //  יצירת פריט חדש
                XmlElement myItemNode = myDoc.CreateElement("item");
                myItemNode.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName); //הוספת שם ופרטי התמונה
                myItemNode.SetAttribute("itemNum", itemCounter.ToString()); //הוספת מספר הפריט
                myItemNode.SetAttribute("itemType", "picture"); //הוספת סוג הפריט
                myItemNode.SetAttribute("categoryNum", (string)Session["selCategory"]); //הוספת מספר קטגוריה שאליו הפריט שייך


             

                //הוספת הפריט לעץ
                myDoc.SelectSingleNode("/theMailman/game[@gamecode= '" + (string)Session["selGame"] + "']//items").AppendChild(myItemNode);

                //שמירה של הפריטים החדשים בעץ
                myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

                Response.Redirect("Edit.aspx");

            }

            else
            {
                //// הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('הקובץ אינו תמונה ולכן לא ניתן להעלות אותו.')", true);
            }
        }

    }


    protected void SavePic2_Click(object sender, EventArgs e)
    {

        if (FileUpload2.HasFile)
        {
            string filetype = FileUpload2.PostedFile.ContentType;//איתור סוג הקובץ 

            //בדיקה האם הקובץ הנקלט הוא מסוג תמונה
            if (filetype.Contains("image"))
            {
                //איתור שם הקובץ
                string fileName = FileUpload2.PostedFile.FileName;
                //איתור סיומת הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                //איתור זמן העלת הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                //הגדרת שם חדש לקובץ
                imageNewName = myTime + endOfFileName;


                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileUpload2.PostedFile.InputStream);

                //קריאה לפונקציה המקטינה את התמונה
                //אנו שולחים לה את התמונה שלנו בגירסאת הביטמאפ ואת האורך והרוחב שאנו רוצים לתמונה החדשה
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 100, 100);


                //שמירת הקובץ בגודלו החדש בתיקייה
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);


                //הוצאת מספר פריט שנבחר
                string itemSeleceted = HiddenField1.Value;


                XmlNode node = myDoc.SelectSingleNode("/theMailman/game[@gamecode='" + (string)Session["selGame"] + "']/items/item[@itemNum='" + itemSeleceted + "']");
                string oldFileName = node.InnerXml;



                //מחיקת התמונה הישנה מקובץ ההעלאות
                var filePath = Server.MapPath(oldFileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                //הוספת השם של התמונה החדשה לעץ
                node.InnerXml = Server.UrlEncode(imagesLibPath + imageNewName); //הוספת שם ופרטי התמונה

                //שמירה של הפריטים החדשים בעץ
                myDoc.Save(Server.MapPath("trees/XMLFile.xml"));


                Response.Redirect("Edit.aspx");
            }

            else
            {
                // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('הקובץ אינו תמונה ולכן לא ניתן להעלות אותו.')", true);
            }
        }

    }

    //פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }
}