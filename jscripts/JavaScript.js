
//-------------------------------------------------מצב לא פעיל------------------------------
$(document).ready(function () {

    //תעבור על כל אלמנט שיש לו את הקלאס הזה       
    $(".CheckBoxDisabled").each(function (e) {

        //תגדיר לו שיהיה לא מאופשר              
        $(this).find("input").attr("disabled", true);
        $(this).find("input").addClass("CheckBox");
        //תבטל את תיבת הסימון במידה ויש              
        $(this).find("input").attr("checked", false);
    });


    //תעבור על כל אלמנט שיש לו את הקלאס הזה       
    $(".CheckBoxEnabled").each(function (e) {

        $(this).find("input").addClass("CheckBox");

    });


});


//-------------------------------------------------ספירת תווים------------------------------
$(document).ready(function () {

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {

        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;

        //משתנה המקבל את מספר תיבת הטקסט 
        var itemNumber = myTextBox.attr("item");

        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimitNum = myTextBox.attr("CharacterLimit");

        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > CharacterLimitNum) {

            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimitNum));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimitNum;

        }
        if (countCurrentC > 0) {
            document.getElementById(myTextBox.attr("saveBtn")).disabled = false;
            document.getElementById(myTextBox.attr("saveBtn")).classList.remove("addBtnsDisabled");
            document.getElementById(myTextBox.attr("saveBtn")).classList.add("addBtnsEnabled");
            //myTextBox.classList.add("tbEnabled");
            //document.getElementsByClassName("updateBtn").visible = true;


        } else {
            document.getElementById(myTextBox.attr("saveBtn")).disabled = true;
            document.getElementById(myTextBox.attr("saveBtn")).classList.add("addBtnsDisabled");
            document.getElementById(myTextBox.attr("saveBtn")).classList.remove("addBtnsEnabled");
            //document.getElementsByClassName("updateBtn").classList.add("hide");
            //document.getElementById("GridView2_updateBtn_0").classList.add("hide");
            //document.getElementById(myTextBox.attr("ID")).disabled = true;


        }


        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#LabelCounter" + itemNumber).text(countCurrentC + "/" + CharacterLimitNum);

        if (countCurrentC <= CharacterLimitNum) {
            document.getElementById("LabelCounter" + itemNumber).style.color = "green";
        }

        if (countCurrentC == CharacterLimitNum) {
            document.getElementById("LabelCounter" + itemNumber).style.color = "red";
        }
        if (countCurrentC == 0) {
            $("#LabelCounter" + itemNumber).text("הזן עד " + CharacterLimitNum + " תווים");
            document.getElementById("LabelCounter" + itemNumber).style.color = "black";
        }



    }

});


//-------------------------------------------------פונקציה להעלאת תמונה לפריט חדש------------------------------

function openFileUploader1() {
    $('#FileUpload1').click();
}

$(document).ready(function () {
    $("#FileUpload1").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#SavePic1').click();
            }
            reader.readAsDataURL(this.files[0]);
        }
    });
});

//-------------------------------------------------פונקציה להחלפת תמונה בפריט קיים------------------------------

function openFileUploader2(evt) {
    var x = evt;
    console.log(evt);
    document.getElementById('HiddenField1').value = x;
    $('#FileUpload2').click();
}


$(document).ready(function () {
    $("#FileUpload2").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#SavePic2').click();
            }
            reader.readAsDataURL(this.files[0]);
        }
    });
});


//-------------------------------------------------הצגה והסתרת הפופ-אפים------------------------------

$(document).ready(function () {
    $(".about").click(function () {
        $(".aboutPanelCss").toggle();
    });

    $(".howToPlay").click(function () {
        $(".howToPlayPanelCss").toggle();
    });

    $(".closeAbout").click(function () {
        $(".aboutPanelCss").hide();
        //$("#gameIframe")[0].contentWindow.focus();
    });

    $(".closeHowToPlay").click(function () {
        $(".howToPlayPanelCss").hide();
        //$("#gameIframe")[0].contentWindow.focus();
    });
});