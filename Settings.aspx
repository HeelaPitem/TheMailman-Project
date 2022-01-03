<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="styles/StyleSheet.css" rel="stylesheet" />
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/JavaScript.js"></script>
    <style type="text/css">
        .CharacterCount {
        }
    </style>
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
                    <li><a href="#" class="howToPlay">עזרה</a></li>
                </ul>
            </nav>
        </div>
    </header>
    <form id="form1" runat="server">


        <div class="pageTitle">
            עמוד הגדרות
        </div>

        <%-- עריכת הגדרות משחק --%>
        <div id="settingsDIV">

            <span class="t2">שם המשחק: </span>
            <asp:TextBox ID="gameNameTB" item="2" CharacterLimit="60" CssClass="CharacterCount" runat="server" Width="390px"></asp:TextBox>
            <br />
            <asp:Label ID="LabelCounter2" runat="server" Text="הזן עד 60 תווים"></asp:Label>
            <br />
            <span class="t2">הנחייה לשחקן: </span>
            <asp:TextBox ID="directionsTB" item="3" CharacterLimit="80" CssClass="CharacterCount" runat="server" Width="500px"></asp:TextBox>
            <br />
            <asp:Label ID="LabelCounter3" runat="server" Text="הזן עד 80 תווים"></asp:Label>
            <br />
            <span class="t2">הגבלת זמן :</span>
            <asp:DropDownList ID="timeLimitDropDownList" runat="server" AutoPostBack="True" Width="106px">
                <asp:ListItem Value="30">30 שניות</asp:ListItem>
                <asp:ListItem Value="60">60 שניות</asp:ListItem>
                <asp:ListItem Value="90">90 שניות</asp:ListItem>
                <asp:ListItem Value="0">ללא הגבלה</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Button CssClass="saveBtn SCsettings" runat="server" Text="שמור שינויים" OnClick="saveBtn_Click" />
            <asp:Button CssClass="cancelBtn SCsettings" runat="server" Text="בטל וחזור" OnClick="cancelBtn_Click" />

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
