<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
<body lang="he">
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
        <div id="mainPage">

            <%-- הוספת משחק חדש --%>
            <span class="t2">שם המשחק:</span>
            <br />
            <asp:TextBox ID="addNameTB" item="1" saveBtn="newGameBtn" CharacterLimit="60" CssClass="CharacterCount" runat="server" Width="500px"></asp:TextBox>
            <asp:Button ID="newGameBtn" disabled="true" runat="server" Text="הוספת משחק חדש" OnClick="newGameBtn_Click" />
            <br />

            <asp:Label ID="LabelCounter1" runat="server" Text="הזן עד 60 תווים"></asp:Label>
            <br />

            <%-- גריד-ויו להדפסת פרטי משחקים מהעץ --%>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="XmlDataSource1" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView1_RowCommand" Style="margin-left: 0px" Width="700px">

                <Columns>

                    <asp:TemplateField HeaderText="שם המשחק">
                        <ItemTemplate>
                            <asp:Label ID="NameLabel" Width="500px" runat="server" Text='<%# Server.UrlDecode( XPathBinder.Eval(Container.DataItem, "gameName").ToString())%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="קוד">
                        <ItemTemplate>
                            <asp:Label ID="idLabel" CssClass="Code" ToolTip="קוד אישי" Width="70px" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@gamecode")%>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="הגדרות">
                        <ItemTemplate>
                            <asp:ImageButton ID="settingsImageButton" CssClass="MPButton" Width="30px" Height="30px" CommandName="settingsRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gamecode")%>' runat="server" ImageUrl="~/images/settings.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="עריכה">
                        <ItemTemplate>
                            <asp:ImageButton ID="editImageButton" CssClass="MPButton" Width="30px" Height="30px" CommandName="editRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gamecode")%>' runat="server" ImageUrl="~/images/edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="מחק">
                        <ItemTemplate>
                            <asp:ImageButton ID="deleteImageButton" CssClass="MPButton" Width="30px" Height="30px" CommandName="deleteRow" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gamecode")%>' runat="server" ImageUrl="~/images/delete.png" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="פרסום">
                        <ItemTemplate>

                            <div class="tooltip">
                                <div class="tooltiptext <%#ToolTipLabel((XPathBinder.Eval(Container.DataItem,"@gamecode").ToString()))%>">
                                    <asp:Label ID="tooltipLabel" class="tooltipLabel" runat="server" Text="Label">המשחק אינו עומד בתנאי הפרסום. המשחק צריך לכלול לפחות: 3 קטגוריות ו5 פריטים בכל קטגוריה.</asp:Label>
                                </div>
                                <asp:CheckBox ID="isPassCheckBox" Width="25px" Class='<%#CheckIfCanPublish((XPathBinder.Eval(Container.DataItem,"@gamecode").ToString()))%>' runat="server" AutoPostBack="true" OnCheckedChanged="isPassCheckBox_CheckedChanged" Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem,"@published"))%>' theItemId='<%#XPathBinder.Eval(Container.DataItem,"@gamecode")%>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>


                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

            </asp:GridView>
            <br />
            <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml" XPath="/theMailman/game"></asp:XmlDataSource>
            <br />

            <asp:Panel ID="grayWindows0" CssClass="grayWindow" runat="server">
                <!-- פופ-אפ למחיקת משחק -->
                <asp:Panel ID="DeleteConfPopUp0" CssClass="bounceInDown PopUp" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="האם בטוח תרצה למחוק משחק זה?"></asp:Label>
                    <br />
                    <asp:Button ID="confYesDelete0" runat="server" Text="מחק" OnClick="confYesDelete_Click" />
                    <asp:Button ID="confnNoDelete0" runat="server" Text="בטל" OnClick="confnNoDelete_Click" />
                </asp:Panel>
            </asp:Panel>

            <%-- פאנל אודות --%>
            <asp:Panel ID="aboutPanelmainPage" CssClass="aboutPanelCss bounceInDown hide" runat="server">
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
