<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>שלשל את המכתב | עורך</title>
    <meta name="description" content="שלשל את המכתב" />
    <meta name="keywords" content="שלשל את המכתב, משחק, משחק לימודי, משחק מיון, קטגוריות, הילה, שירה, הילה פיטם, שירה פיקר" />
    <meta name="author" content="Heela Pitem, Shira Picker" />

    <%-- קישור לדף CSS --%>
    <link href="styles/StyleSheet.css" rel="stylesheet" />
    <%-- קישור ל scripts --%>
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/JavaScript.js"></script>
    <style type="text/css">
        .CharacterCount {
        }
    </style>
    <link rel="icon" type="image/png" sizes="96x96" href="images/mail.png" />
</head>
<body>
    <header>
        <div class="innerHeader">
            <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img id="logo" src="images/mail.png" />
                <!--הלוגו של המשחק שלכם-->
                <p class="t0">שלשל את המכתב!</p>
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                    <li><a href="Default.aspx" class="selectedNav">עורך</a></li>
                    <li><a href="Game.html" class="">למשחק</a></li>
                    <li><a href="#" class="about">אודות</a></li>
                    <li><a href="#" class="">עזרה</a></li>
                </ul>
            </nav>
        </div>
    </header>
    <form id="form1" runat="server">
        <div id="EditContainer">

            <div class="pageTitle">
                עמוד עריכה:
            <asp:Label ID="gameName" runat="server" Text="Label"></asp:Label>
            </div>
            <br />

            <%-- צד ימין- קטגגוריות --%>
            <div id="CategoryDIV">
                <span class="t2">קטגוריות</span>
                <br />
                <span class="t3">הזן 3-5 קטגוריות.</span>
                <br />
                <asp:TextBox ID="newCategoryTxt" saveBtn="newCategoryBtn" item="4" CharacterLimit="30" CssClass="CharacterCount" runat="server" Width="200px"></asp:TextBox>
                <asp:Button ID="newCategoryBtn" disabled="true" CssClass="addBtnsDisabled" OnClick="newCategoryBtn_Click" runat="server" Text="הוסף" />
                <br />
                <asp:Label ID="LabelCounter4" runat="server" Text="הזן עד 30 תווים"></asp:Label>

                <br />
                <asp:GridView ID="GridView2" CssClass="CategoryGrid" runat="server" DataSourceID="XmlDataSource1" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView2_RowCommand" OnRowEditing="GridView2_RowEditing" Style="margin-left: 0px; margin-bottom: 0px;" Width="220px">

                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="CategoryBtn" CssClass="CategoryBtns" OnClientClick="inableItems(); return false;" runat="server" CommandName="CategoryIdCheck" Text='<%# Server.UrlDecode( XPathBinder.Eval(Container.DataItem, "@categoryNum/..").ToString())%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@categoryNum")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CategoryEditTB" saveBtn="updateBtn" item="6" CharacterLimit="30" CssClass="CharacterCount tbEditBtn" runat="server" Text='<%# Server.UrlDecode( XPathBinder.Eval(Container.DataItem, "@categoryNum/..").ToString())%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@categoryNum")%>'></asp:TextBox>
                            </EditItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="editImageButton" CssClass="edit_delete_categoryBtns" Width="25px" Height="25px" CommandName="Edit" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@categoryNum")%>' runat="server" ImageUrl="~/images/edit.png" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="LabelCounter6" runat="server" Text=""></asp:Label>
                                <asp:Button ID="updateBtn" Class="updateBtn" Text="שמור" CommandName="Update" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@categoryNum")%>' runat="server"></asp:Button>

                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="deleteImageButton" CssClass="edit_delete_categoryBtns" Width="25px" Height="25px" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@categoryNum")%>' runat="server" ImageUrl="~/images/delete.png" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br />
                <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml"></asp:XmlDataSource>
                <br />
            </div>

            <div id="seperatorDIV"></div>


            <%-- צד שמאל - פריטים --%>
            <asp:Panel ID="ItemsBigPanel" CssClass="greyfont" runat="server">
                <span id="itemstitle" class="t2">פריטים</span>
                <br />
                <span class="t3">הוסף 5-10 פריטים.</span>
                <br />
                <asp:RadioButtonList ID="ItemTypeRBT" AutoPostBack="true" OnSelectedIndexChanged="ItemTypeRBT_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Value="text" Selected="true">טקסט</asp:ListItem>
                    <asp:ListItem Value="pic">תמונה</asp:ListItem>
                </asp:RadioButtonList>

                <asp:TextBox ID="newItemTxt" saveBtn="newItemBtn" item="5" CharacterLimit="30" CssClass="CharacterCount" runat="server" Width="200px"></asp:TextBox>

                <asp:Button ID="newItemBtn" disabled="true" CssClass="addBtnsDisabled" runat="server" Text="הוספת פריט" OnClick="NewItemBtn_Click" />


                <asp:Button ID="newImageBtn" CssClass="addBtnsEnabled" Visible="false" runat="server" Text="העלת תמונה" OnClientClick="openFileUploader1(); return false;" />
                <br />
                <asp:Label ID="LabelCounter5" runat="server" Text="הזן עד 30 תווים"></asp:Label>
                <br />

                <asp:Panel ID="itemsPanel" runat="server"></asp:Panel>
            </asp:Panel>

            <%-- פקדים בלתי נראים --%>
            <asp:FileUpload ID="FileUpload1" runat="server" onChange="this.form.submit()" />
            <asp:FileUpload ID="FileUpload2" runat="server" onChange="this.form.submit()" />
            <asp:Button ID="SavePic1" runat="server" Text="Button" OnClick="SavePic1_Click" />
            <asp:Button ID="SavePic2" runat="server" Text="SavePic2" OnClick="SavePic2_Click" />
            <asp:HiddenField ID="HiddenField1" runat="server" />

            <%-- כפתורי שמור וחזור בתחתית העמוד --%>
            <asp:Button ID="savePageBtn" class="saveBtn" runat="server" Text="שמור וחזור" OnClick="savePageBtn_Click" />
            <asp:Button class="cancelBtn" runat="server" Text="בטל וחזור" OnClick="cancelBtn_Click" />




            <asp:Panel ID="grayWindows" CssClass="grayWindow" runat="server">
                <!-- פופ-אפ למחיקת קטגוריה -->
                <asp:Panel ID="categoryDeletePopUp" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="האם בטוח תרצה למחוק קטגוריה זו?"></asp:Label>
                    <br />
                    <asp:Button ID="YesDeleteCategory" runat="server" Text="מחק" OnClick="YesDeleteCategory_Click" />
                    <asp:Button ID="NoDeleteCategory" runat="server" Text="בטל" OnClick="NoDelete_Click" />
                </asp:Panel>

                <!-- פופ-אפ למחיקת פריט -->
                <asp:Panel ID="itemDeletePopUp" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="האם בטוח תרצה למחוק פריט זה?"></asp:Label>
                    <br />
                    <asp:Button ID="YesDeleteItem" runat="server" Text="מחק" OnClick="YesDeleteItem_Click" />
                    <asp:Button ID="NoDeleteItem" runat="server" Text="בטל" OnClick="NoDelete_Click" />
                </asp:Panel>
            </asp:Panel>

            <!-- פופ-אפ כשהקובץ שהועלה לא מסוג תמונה  -->
            <asp:Panel ID="Panel1" CssClass="bounceInDown PopUp" runat="server">
                <asp:Label ID="Label3" runat="server" Text="האם בטוח תרצה למחוק פריט זה?"></asp:Label>
                <br />
                <asp:Button ID="Button1" runat="server" Text="מחק" OnClick="YesDeleteItem_Click" />
                <asp:Button ID="Button2" runat="server" Text="בטל" OnClick="NoDelete_Click" />
            </asp:Panel>
            </asp:Panel>



            <%-- פאנל אודות --%>
            <asp:Panel ID="aboutPanel" CssClass="aboutPanelCss bounceInDown hide" runat="server">
                <a class="closeAbout">X</a>
                <p>שלשל את המכתב!</p>
                <p>אפיון ופיתוח: הילה פיטם ושירה פיקר</p>
                <p>פותח במסגרת פרויקט בקורסים:</p>
                <p>סביבות לימוד אינטראקטיביות, תכנות & אנימציה, תשע"ט</p>
                <a href="https://www.hit.ac.il/telem/overview">הפקולטה לטכנולוגיות למידה, המכון הטכנולוגי חולון</a>
                <img src="images/hitlogo.jpg" width="180" height="180" />
            </asp:Panel>




        </div>

    </form>

</body>
</html>
