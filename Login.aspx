<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>שלשל את המכתב | כניסה</title>
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
                    <li><a href="#" class="selectedNav">עורך</a></li>
                    <li><a href="Game.html" class="">למשחק</a></li>
                    <li><a href="#" class="about">אודות</a></li>
                    <li><a href="#" class="">עזרה</a></li>
                </ul>
            </nav>
        </div>
    </header>
    <form id="form1" runat="server">
        <div>

            <%-- פאנל כניסה --%>
            <asp:Panel ID="loginPanel" runat="server">
                <p class="t2">צד עורך</p>
                <span id="userName">שם משתמש:</span>
                <asp:TextBox ID="userNameTB" runat="server"></asp:TextBox>
                <br />
                <span id="password">סיסמא:</span>
                <asp:TextBox ID="passwordTB" Type="password" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="loginInfo" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="signInBtn" CssClass="signInBtn" runat="server" Text="כניסה" OnClick="signInBtn_Click" />
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
