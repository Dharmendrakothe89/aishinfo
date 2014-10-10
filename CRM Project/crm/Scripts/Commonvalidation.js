//***************************** SHOW HIDE DIV ************************************************//
function Showdiv(id) {
    document.getElementById(id).style.display = "block";
}
function HideDiv(id) {
    document.getElementById(id).style.display = "none";
}

function DateFormatValidation(id, val) {

    if (val.length == 2) {
        document.getElementById(id).value = val + "/";
    }

    if (val.length == 5) {
        document.getElementById(id).value = val + "/";
    }
}

// ***************************** NUMERIC DATA ************************************************ //

function mobile(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 107 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else
        return true;
}

function landline(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode != 109 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    else
        return true;
}



function isNumberWthOutDot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode < 48 || charCode > 57) {
        return false;
    }
    else
        return true;
} 
// ***************  WITH DECIMAL POINT  ***************************** //

var i = 1;
var allow;
function isNumberWthDot(evt, val) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode == 46) {

        if (val.indexOf(".") == -1) {
            allow = val.length + 3;
            i = 1;
        }
        if (i > 1) {
            return false;
        }
        else {
            i++;
            return true;
        }
    }
    else if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (val.indexOf(".") == -1) {
        allow = val.length + 100;
        i = 1;
    }
    if (allow != undefined) {
        if (allow == val.length) {
            return false;
        }
    }

    return true;
}   
/**********************************  DATE VALIDATION   **********************************************/
function DateFormatValidation(id, val) {

    if (val.length == 2) {
        document.getElementById(id).value = val + "/";
    }

    if (val.length == 5) {
        document.getElementById(id).value = val + "/";
    }
}
function ValidateDate(id, value) {

    if (isValidDate(value)) {
        FinalValidationDate(id);
    }
    else {
        if (value != "") {
            inValidAlert(id);

        }
    }

}

function FinalValidationDate(id) {
    var txtDate = document.getElementById(id).value;

    if ((txtDate.substring(0, 2) == "30" || txtDate.substring(0, 2) == "31") && txtDate.substring(3, 5) == "02") {
        inValidAlert(id, txtDate);
    }
}

//function datevalidate(inputText, id) {
//    var idd = document.getElementById(id);
//    
//    if (inputText != "") {
//        var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
//        
//        if (inputText.match(dateformat)) {

//            var opera1 = inputText.value.split('/');
//           
//          //  var opera2 = inputText.value.split('-');
//            lopera1 = opera1.length;
//            //  lopera2 = opera2.length;
//           

//            if (lopera1 > 1) {
//                var pdate = inputText.split('/');
//                
//            }
//            alert("date is" + inputText);
//            var current_year = new Date().getFullYear();
//            var cy = current_year - 10;
//            var date=new Date().getDate();
//            var month=new Date().getMonth();
//            var dd = parseInt(pdate[0]);
//            var mm = parseInt(pdate[1]);
//            var yy = parseInt(pdate[2]);
//          var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
//         
////            if (dd > date && mm > month && yy > current_year) {
////                alert("Invalid date format!");
////                idd.value = "";
////                idd.focus();
////            }

////            if (yy > cy) {
////                alert("Invalid date format!");
////                idd.value = "";
////                idd.focus();

////                return false;
////            }
//           
//           
//            if (mm == 2) {
//                var lyear = false;
//                if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
//                    lyear = true;
//                }
//                if ((lyear == false) && (dd >= 29)) {
//                    alert("Invalid date format!");
//                    idd.value = "";
//                    idd.focus();
//                    return false;
//                }
//                if ((lyear == true) && (dd > 29)) {
//                    alert("Invalid date format!");
//                    idd.value = "";
//                    idd.focus();
//                    return false;
//                }
//            }



////            if (mm == 1 || mm > 2) {
////                if (dd > ListofDays[mm - 1]) {
////                    alert("Invalid date format!");
////                    idd.value = "";
////                    idd.focus();
////                    return false;
////                }
////            }
//           
//        }
//        else {
////            alert("Invalid date format!");
////            idd.value = "";
////            idd.focus();
//            return false;
//        }
//        return true;
//    }
//}


function checkdate(input, id) {
    var date1 = new Date().getDate();
    var month1 = new Date().getMonth();
    var year1 = new Date().getFullYear();

    var idd = document.getElementById(id);
    var validformat = /^\d{2}\/\d{2}\/\d{4}$/ ; //Basic check for format validity
var returnval = false;
if (!validformat.test(input)) {
    idd.value = "";
    alert("Invalid Date Format. Please correct and submit again.");
    
}
else { //Detailed check for valid date ranges
    var monthfield = input.split("/")[1];
    var dayfield = input.split("/")[0];
    var yearfield = input.split("/")[2];
    var dayobj = new Date(yearfield, monthfield - 1, dayfield);

    if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield)) {
        alert("Invalid Day, Month, or Year range detected. Please correct and submit again.");
        idd.value = "";
        idd.focus();
        return false;
    }
    else if ((dayfield > date1) && (monthfield > month1) && (yearfield == year1)) {
        alert("Invalid Day, Month, or Year range detected. Please correct and submit again.");
        idd.value = "";
        idd.focus();
        return false;
    }
    else
        returnval = true;
}
if (returnval==false)
return returnval;
}



/***************************************** EMAIL VALIDATION  *********************************************************************/
function validateEmail(elementValue, id) {
    var idd = document.getElementById(id);
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (emailPattern.test(elementValue))
    { }
    else {
        if (idd.value != "") {
            alert("Invalid Email-ID");
            idd.value = "";
            idd.focus();
            return false;
        }
    }
    return true;
}
/*************************************** SAMI ID VALIDATION ************************************************************************/


 /*************************************** MOBILE VALIDATION ************************************************************************/
function validateMobileNo(evt, id) {
    var id = document.getElementById(id).value;
    var charCode = (evt.which) ? evt.which : event.keyCode;
    
    if (((charCode >= 48) && (charCode <= 57)) || (charCode == 44) || (charCode == 107) || (charCode == 43)) 
      {
         return true;
      }
     else
      {

         return false;
      }
 }


 /*************************************** URL VALIDATION ************************************************************************/
 // function learnRegExp() {
 //     return /((http|https):\/\/(\w+:{0,1}\w*@)?(\S+)|)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(learnRegExp.arguments[0]);
 // }

 function validateURL(elementValue, id) {

     var idd = document.getElementById(id);
     var urlPattern = /((http|https):\/\/(\w+:{0,1}\w*@)?(\S+)|)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
     if (urlPattern.test(elementValue))
     { }
     else {
         if (idd.value != "") {
             alert("Invalid Website");
             idd.value = "";
             idd.focus();
             return false;
         }
     }
     return true;
 }

 /***************************************CHAR ONLY VALIDATION ************************************************************************/


 function isCharacterWithSpace(evt) {

     var charCode = (evt.which) ? evt.which : event.keyCode
  
     if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32) || (charCode == 8)) 
     {
      return true;
       }
     else
     {
         return false;
     }
 }