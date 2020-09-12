
///These are common feilds for popup Lookup
//Added by Jiya
//Added wehn 9 April
//start
var LookUpType = 0;
var GridColumnDetails = "";
var GridColumnForRadioButton = "";
var ResxColumnDetails = "";



////End
if ($('#hdnwelcomeMsg').val() != undefined) {
    var userwlc = $('#hdnwelcomeMsg').val();
    var userwlc1 = $('#hdnwelcomeMsg1').val();
    //$('#loggedInUser').text(userwlc + ' ' + sessionStorage.getItem('userName'));
    //$('#loggedInUser1').text(sessionStorage.getItem('userName'));

    $('#loggedInUser').text(userwlc + ' ' + sessionStorage.getItem('DisplayUserName'));
    $('#loggedInUser1').text(sessionStorage.getItem('DisplayUserName'));


    $("#imgUserImage").attr("src", sessionStorage.getItem('ProfilePhoto'));
}

var SystemMasterTableEnum = {

     Role : 1,SystemMaster:2, SystemMasterType : 3,Age:4,Gender:5,

}

var UserPermission =
{
    View:"View",
    Delete:"Delete",
    Update:"Update",
    Add:"Add",
}
///This is common  enum to bind the master dropdown
//// We have to add the masterttype data and value accoding to database ids in enum.
//Data depandancy 
//Hard code
var MasterTypeEnum = {
    CastCategory: 1,
    Age: 2,
    Gender: 3,
    Age_1: 4,
    Qualificaton: 5,
    Religion: 6,
    StudentType: 7,
    Country: 8,
    State: 9,
    City: 10,
    DocuemntMastercategory: 11,
    Docuementcategory: 12,
    DocumentFollowupType: 13,
    ClassClassification: 14,
    SectionsMaster: 15,
    BuildingMaster: 16,
    TeacherClassificationMaster: 17,
    AssessmentTypeGroup: 18,
    AssessmentStructure: 19,
    GradeStructure: 20,
    EmpCategory: 21,
    StaffDesignation: 22,
    Vendor: 23,
    Department: 24,
    IDcardType: 25,
    EmpDegree: 26,
    EmpDegreeMode: 27,
    EmployeeStatus: 28,
    Streams: 29,
    Gender: 30,
    VechicleTrip: 31,
    VechicalManufacturer: 32,
    VechicalMake: 33,
    VechicalModel: 34,
    Fueltype: 35,
    ChartOfAccounts: 36,
    OptionalSubjectGroup: 37,
    AddressArea: 38,
    AdmissionCategory: 39,
    TransportType: 40,
    HostelBedType: 41,
    AdmInteractionType: 42,
    EnquiryType: 43,
    FeeGroup: 45,
    MasterStatus: 46,
    ParentQualification: 47,
    CommunicationMode: 48
};

///This is common  method for load the permission to  control .
//Added by jiya
//Date:29-Mar-2019
function GetPermission(userGroup, menuId, arrrControlIds) {

    $.ajax({
        type: 'GET',
        url: '/CommonMethods/GetPermission',
        data: { userGroup: userGroup, menuId: menuId },
        dataType: "json",
        success: function (response) {
            for (var i = 0; i < arrrControlIds.length; i++) {
                var attribs = $('#' + arrrControlIds[i]).attr("Permission");
                // iterate over each element in the array
                for (var j = 0; j < response.length; j++) {

                    // look for the entry with a matching `Permission` value
                    if (response[j].Permission.toLowerCase() == attribs.toLowerCase()) {
                        $('#' + arrrControlIds[i]).prop('disabled', false);
                    }
                }

            }
        },
        error: function () {
            alert(" An error occurred.3");
        },
    })
}


/*
Function Name               : GetKeyForValidation()
Parameter(s)                : Allows numeric navigationMenu.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This is common  method for load mandotory control .
                              NavigationMenu-this is the page code or menu code which is available in resx file.
*/
function GetKeyForValidation(navigationMenu) {
    $.ajax({
        type: 'GET',
        url: '/CommonMethods/GetIndexScreenDetailByNavigation',
        data: { MenuId: navigationMenu, d: 1 },
        dataType: "json",
        success: function (response) {

            requiredFeild = response;
            $.each(requiredFeild, function (key, value) {

                if ($("#" + value).length > 0) {
                    var lablePrevioue = $("#" + value).prev();
                    lablePrevioue.append("<span class='text-danger ml-1'>*</span>");
                    $("#" + value).addClass("errorvalidation");
                }

            });

        },
        error: function (xhr, status, error) {
            var err = JSON.parse(xhr.responseText);
            alert(err.Message);
        },
    })
}

function getErrorMessage(request, status) {
    if (request.status === 0) {
        return ('We are not able to communicate to  server.\nPlease verify your network connection and ensure  you are connected to internet.');
    } else if (request.status == 404) {
        return ('The requested page not found. [404]');
    } else if (request.status == 500) {
        return ('Internal Server Error [500].');
    } else if (status === 'parsererror') {
        return ('Requested JSON parse failed.');
    } else if (status === 'timeout') {
        return ('Time out error.');
    } else if (status === 'abort') {
        return ('Ajax request aborted.');
    } else {
        return ('Uncaught Error.\n' + request.responseText);
    }
}
function handleError(request, status, error) {

    alert(getErrorMessage(request, status));

}

function processMethodType(methodType) {
    if (methodType == "POST") {
        //alert('Successfully Saved');
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">Saved successfully!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else if (methodType == "PUT") {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">Successfully updated!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else if (methodType == "DELETE") {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">Successfully Deleted!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

}
/*
Function Name               : CommonAjaxMethod()
Parameter(s)                : Allows url, json data, option true or false, method type i.e. post put etc.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This is common post method for save the data .                           
                            URL-this is controller  url to call
                            data: object to bind control value
                            optional: this is for add common fileds into data object, in case false it will not add common value.
                            i.e.Session id school id, group id etc.
                            methodType is having only three type value-POST,PUT,DELETE
*/

function handleErrorResponse(response) {
    if (response.IsSuccessStatusCode == false) {
        alert(response.ErrorMessage);
    }

}
function CommonAjaxMethod(actionMethodUrl, data, optional, methodType, successCallBack) {


    var isSuccess = false;
    var dataObj;
    if (methodType == "GET") {
        dataObj = data;
    }
    else {
        dataObj = JSON.stringify(data);
    }


    if (optional) {
        data = AddCommonFieldsInData(data);
        dataObj = JSON.stringify(data);
    }
    $.ajax({
        type: methodType,
        url: actionMethodUrl,
        data: dataObj,
        contentType: 'application/json',
        beforeSend: function (request) {
            request.setRequestHeader('ASGMapping_Id', sessionStorage.getItem('ASGMapping_Id'));
            request.setRequestHeader('NavMenu_Id', sessionStorage.getItem('MenuId'));
        },
        success: function (response) {


            if (response.IsSuccessStatusCode != undefined) // New implementation 
            {
                if (response.IsSuccessStatusCode) {
                    if (response.Message == undefined) {

                        processMethodType(methodType);
                    }
                    else {
                        var x = document.getElementById("toastmsg");
                        x.innerHTML = '<p class="green">' + response.Message + '!</p>';
                        x.className = "show";
                        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                    }
                    if (successCallBack) {
                        successCallBack(response);
                    }
                }
                else {
                    alert(response.ErrorMessage);
                }


                return;
            }

            if (successCallBack && methodType == "GET") {
                if (Array.isArray(response)) {
                    var responseObj = {
                        dataCollection: response
                    }
                }
                else {
                    var responseObj = {
                        data: response
                    }
                }
                successCallBack(responseObj);
                return;
            }


            if (response.status) {
                if (response.Message == undefined) {

                    processMethodType(methodType);
                }
                else {
                    var x = document.getElementById("toastmsg");
                    x.innerHTML = '<p class="green">' + response.Message + '!</p>';
                    x.className = "show";
                    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
                }

                //This method should be declared in calling page js to get result.
                FormControlValueClear(true);

            }
            else {

                alert('Error -- Change Implementation to new desgin ');
                isSuccess = false;
            }
        },
        error: function (request, status, error) {
            handleError(request, status, error);
            //alert("Hi");
            //alert(error);
            isSuccess = false;
        }
    });
    return isSuccess;
}

function getParam(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    return results[1] || 0;
}


function BuildPage() {

    var div = document.getElementById("dyTable");

    if (div != null) {

        if (div.loaded === undefined) {
            div.loaded = true;
            var asgMappingID = sessionStorage.getItem('ASGMapping_Id');
            var orign = window.location.origin;
            var UserId = sessionStorage.getItem('CUserId');

            var id = getParam("Id")
            var makeIframe = document.createElement("iframe");
            makeIframe.setAttribute("src", orign + "/Scripts/DevScripts/Dynamic/index.html?ASGMapping_Id=" + asgMappingID + "&screenID=" + id + "&UserId=" + UserId

                + "&MenuCode=" + sessionStorage.getItem('MenuCode') + "&refreshToken=" + sessionStorage.getItem('refreshToken')
                + "&accessToken=" + sessionStorage.getItem('accessToken')


            );
            makeIframe.setAttribute("frameborder", "no");
            //makeIframe.setAttribute("border", "0pt");
            //makeIframe.setAttribute("border", "none");
            //makeIframe.setAttribute("left", "-453px");
            //makeIframe.setAttribute("top", "-70px");
            //makeIframe.setAttribute("position", "absolute");
            makeIframe.setAttribute("width", "1400");
            makeIframe.setAttribute("height", "500");
            makeIframe.setAttribute("scrolling", "auto");


            var div = document.getElementById("dyTable");
            div.appendChild(makeIframe);
            window.addEventListener("message", handleMessage, false);
        }
    }

}

function handleMessage(event) {

    if (event.type === "message" && event.origin === window.location.origin) {

        var data = JSON.parse(event.data);

        if (data.nevigate) {
            window.location.href = data.nevigate;

        }


    }
}

BuildPage();

/*
Function Name               : AddCommonFieldsInData()
Parameter(s)                : Allows json data
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to set the value from session to data object to save and update.
*/
function AddCommonFieldsInData(data) {
    data.Sessionyear = sessionStorage.getItem('sessionYear');
    data.SchoolID = sessionStorage.getItem('schoolId');
    data.GroupID = sessionStorage.getItem('groupId');
    data.ASGMapping_Id = sessionStorage.getItem('ASGMapping_Id');
    data.UserGroup = sessionStorage.getItem('UserGroupId');


    return data;
}


/*
Function Name               : checkValidationOnSubmit()
Parameter(s)                : 
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This is common  method for validate  control on submit.
*/

function checkValidationOnSubmit() {


    var isMandatory = true;
    $.each(requiredFeild, function (key, value) {
        if ($("#" + value).length > 0) {
            if ($("#" + value) != null) {
                var text = Trim($("#" + value).val());
                if (text != undefined) {
                    text = text.trim();
                }
                if (text == "" || text == "0" || text == "Select") {
                    isMandatory = false;
                }
            }



        }
    });
    if (!isMandatory) {
        var x = document.getElementById("toastmsg");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        return false;
    }
    return isMandatory;
}


/*
Function Name               : CommonGetAjaxMethod()
Parameter(s)                :Url, data
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This is a common method used for get data.
*/
function CommonGetAjaxMethod(actionMethodUrl, data) {
    $.ajax({
        type: "GET",
        url: actionMethodUrl,
        data: JSON.stringify(data),
        dataType: "json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', sessionStorage.getItem('accessToken'));
        },
        success: function (data) {
            //This method should be declared in calling page js to get result.
            GetDataFromCommonGetAjax(data);
        }
    })
}
/*
Function Name               : PopupLookup()
Parameter(s)                : popupType, RadioColumn, columnList, RColumnDetails
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This is a common method used for open the popup.
*/
function PopupLookup(popupType, RadioColumn, columnList, RColumnDetails) {
    GridColumnForRadioButton = RadioColumn;
    LookUpType = popupType;
    GridColumnDetails = columnList;
    ResxColumnDetails = RColumnDetails;
    var TeamDetailPostBackURL = '/CommonMethods/Details';
    var options = { "backdrop": "static", keyboard: true };

    $.ajax({
        type: "POST",
        url: TeamDetailPostBackURL,
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            $('#myModalContent').html(data);
            $('#myModal').modal(options);
            $('#myModal').modal('show');

        },
        error: function () {
            alert("Dynamic content load failed.");
        }
    });
}

/*
Function Name               : JsonDateFormat()
Parameter(s)                : dateValue
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to date formate
*/
function JsonDateFormat(dateValue) {

    var strDate = "";
    if (dateValue != null) {
        var dateData = new Date(parseInt(dateValue.replace("/Date(", "").replace(")/")));
        var dd = dateData.getDate();
        var mm = dateData.getMonth() + 1; //January is 0! 
        var yyyy = dateData.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        //if (format == 'dd/mm/yyyy')
        //    strDate = mm + ' / ' + dd + ' / ' + yyyy;
        //else if (format == 'mm/dd/yyyy')
        //    strDate = mm + ' / ' + dd + ' / ' + yyyy;
        //else
        strDate = dd + '/' + mm + '/' + yyyy;
    }
    return strDate;

}
function time_format(dateValue) {
    var strtDate = "";
    if (dateValue != null) {
        var d = new Date(parseInt(dateValue.replace("/Date(", "").replace(")/")));
        var hours = format_two_digits(d.getHours());
        var minutes = format_two_digits(d.getMinutes());
        var seconds = format_two_digits(d.getSeconds());
        strtDate = hours + ":" + minutes + ":" + seconds;
    }
    return strtDate;
}

function format_two_digits(n) {
    return n < 10 ? '0' + n : n;
}
/*
Function Name               : CheckInValidSpecialCharacters()
Parameter(s)                : string value
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to check the input data is valid apla numeric with special chars.
*/
function CheckInValidSpecialCharacters(sText) {
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#$_";
    var Char;

    for (i = sText.length - 1; i >= 0; i--) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            return true;
        }
    }
    return false;
}
/*
Function Name               : CheckInValidAlphaNumericCharacters()
Parameter(s)                : string value
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to check the input data is valid apla numeric  chars.
*/
function CheckInValidAlphaNumericCharacters(sText) {
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var Char;

    for (i = sText.length - 1; i >= 0; i--) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            return true;
        }
    }
    return false;
}
/*
Function Name               : CheckInValidCharacters()
Parameter(s)                : string value
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to check the input data is valid  chars only.
*/
function CheckInValidCharacters(sText) {
    var ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var Char;

    for (i = sText.length - 1; i >= 0; i--) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            return true;
        }
    }
    return false;
}

/*
Function Name               : replaceAll()
Parameter(s)                : str, from, to
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to Replace a string.
*/
function replaceAll(str, from, to) {
    var idx = str.indexOf(from);
    while (idx > -1) {
        str = str.replace(from, to);
        idx = str.indexOf(from);
    }

    return str;
}


/*
Function Name               : castToUpper()
Parameter(s)                : current text box obj
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to make upper case of text box char.
*/

function castToUpper(obj) {
    obj.value = obj.value.toUpperCase();
}
/*
Function Name               : castToLower()
Parameter(s)                : current text box obj
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is used to make lower case of text box char.
*/
function castToLower(obj) {
    obj.value = obj.value.toLowerCase();
}
/*
Function Name               : ValidateEmail()
Parameter(s)                : email, emailErrMsg
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : This function is check valid email id.
*/
function ValidateEmail(email, emailErrMsg) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    if (reg.test(email.value) == false && email.value.length != 0) {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">' + emailErrMsg + '!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        email.focus();
        return false;
    }
    return true;
}

/*
Function Name               : Trim()
Parameter(s)                : StringValue
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : Used to remove blank spaces before and after the value in the string being passed.
*/
function Trim(StringValue) {

    return jQuery.trim(StringValue);
}

/*
Function Name               : CheckValidLength()
Parameter(s)                : oCtlToVal, ctlName, maxCharLen, isOnPast, event
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   :
Description                 : Used to validate the number of characters being entered in a textbox. This is also used to check characters being copy pasted.
                               oCtlToVal : is object of the TextBox to be validated.
                               ctrlName  : name of the object to be dispalyed in error message.
                               maxCharLen: valid number of characters that can be entered.
                               isOnPaste : pass true if object needs to be validated onPaste event, else pass false if just control length to be validated.
*/

function CheckValidLength(oCtlToVal, ctlName, maxCharLen, isOnPast, event, ErrMsg) {
    var clipLen = 0, currentFieldLength = 0, resultantLength = 0;
    var clipboardText = "";

    // Get the event object as per the browser.
    var keyCode = GetKeyCodeMain(event);


    if (window.clipboardData != null) {
        clipboardText = window.clipboardData.getData("Text");
    }

    if (isOnPast)//if the data is being pasted then get its length.
    {
        clipLen = clipboardText.length;
    }

    // Get the current and maximum field length
    currentFieldLength = parseInt(oCtlToVal.value.length, 10);

    resultantLength = currentFieldLength + clipLen;

    maxCharLen = parseInt(maxCharLen, 10);
    if (resultantLength > maxCharLen) {
        oCtlToVal.focus();
        //If the user has typed in the data only then truncate it.
        if (!isOnPast) {
            oCtlToVal.value = oCtlToVal.value.substring(0, maxCharLen);
        }

        if (ctlName != "") {
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + ErrMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);


            //alert("Max character length for " + ctlName + " cannot exceed " + maxCharLen + " characters.");
        }
        else {
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + ErrMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
            //alert("Max character length cannot exceed " + maxCharLen + " characters.");
        }
        return false;
    }
    return true;
}

//Used to make sure that the valid characters are passed in the textbox.
//eg if text box allows only alphabets then pass alphabets as validChar.
//objValidate: is the object of TextBox that needs to be validated.
//ValidString: string of special characters that need to be present in the textbox.
//errMsg: error message that needs to be displayed.
//setFocus: pass true if focus needs to be set.
function IsValidCharacters(ctrlClientID, errMsg, setFocus) {
    var ValidString = "abcdefghijklmnopqrstuvwxyzABC DEFGHIJKLMNOPQRSTUVWXYZ1234567890_-";
    var objValidate = $("#" + ctrlClientID);
    var Value = objValidate.val();
    var charCount = 0;
    var oRegex = new RegExp("[^" + ValidString + "]"); //Check for non valid characters.
    if (oRegex.test(Value)) {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">' + errMsg + '!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        if (setFocus == null || setFocus == true)//null will come if setFocus is not passed when calling this method.
            objValidate.focus();
        objValidate.val('');
        return false;
    }
    else
        return true;
}
//Used to make sure that the valid characters are passed in the textbox.
//eg if text box allows only alphabets then pass alphabets as validChar.
//objValidate: is the object of TextBox that needs to be validated.
//ValidString: string of special characters that need to be present in the textbox.
//errMsg: error message that needs to be displayed.
//setFocus: pass true if focus needs to be set.
function CheckValidCharacters(objValidate, ValidString, errMsg, setFocus) {
    var Value = objValidate.value.toUpperCase();
    var charCount = 0;
    for (i = 0; i < Value.length; i++) {
        if (ValidString.indexOf(Value.charAt(i)) > -1) {
            continue;
        }
        else {
            alert(errMsg);
            if (setFocus == null || setFocus == true)//null will come if setFocus is not passed when calling this method.
            {
                objValidate.focus();
            }
            return false;
        }
    }
    return true;
}
//Used to check special characters if any.
//objValidate: is the object of TextBox that needs to be validated.
//SpecialString: string of special characters that need not be present in the textbox.
//objName: name of the object that needs to be displayed in the alert being displayed.
function IsSpecialCharacters(ctrlClientID, SpecialString, objName) {
    var objValidate = $("#" + ctrlClientID);
    var Value = objValidate.val();
    var oRegex = new RegExp("[" + SpecialString + "]");
    if (oRegex.test(Value)) {
        objValidate.focus();
        alert("Special characters like " + SpecialString + " are not allowed in " + objName + " box.");
        return true;
    }
    return false;
}
//Used to check special characters if any.
//objValidate: is the object of TextBox that needs to be validated.
//SpecialString: string of special characters that need not be present in the textbox.
//objName: name of the object that needs to be displayed in the alert being displayed.
function CheckSpecialCharacters(objValidate, SpecialString, objName) {
    var Value = objValidate.value;
    for (i = 0; i < SpecialString.length; i++) {
        for (j = 0; j < Value.length; j++) {
            if (Value.charAt(j) == SpecialString.charAt(i)) {
                iCharPos = j;
                objValidate.focus();
                alert("Special characters like " + SpecialString + " are not allowed in " + objName + " box.");
                return false;
            }
        }
    }
    return true;
}


//Used to check for more than one spaces between two words/characters.
//objValidate: is the object of TextBox that needs to be validated.
//objName: name of the object that needs to be displayed in the alert being displayed.
function CheckValidWhitespaceUse(objValidate, objName) {
    var enteredText = Trim(objValidate.value);
    var spaceChar = " ";
    var spaceCount = 0;

    for (j = 0; j < enteredText.length; j++) {
        if (enteredText.charAt(j) == " ")//Check for space.
        {
            spaceCount += 1; //Increment the space count.
            if (spaceCount >= 2)//check for more than one space.
            {
                objValidate.focus();
                alert("Please enter only one space between two words/characters in " + objName + " box.");
                return false;
            }
        }
        else {
            spaceCount = 0; //Reset the spaceCount.
        }
    }
    return true;
}
//Used to validate if the file being uploaded is a valid file depending upon the extensions being passed.
//objFile: is the object of type FileUpload.
//sAllowedExts: is the extension of the file that is allowed to be uploaded eg .jgp|.jpeg|.gif.
//objName: name of the object that needs to be displayed in the alert message.
function ValidateFile(objFile, sAllowedExts, objName) {

    var sUpload = "";
    var iDot = 0;
    var oRegex = null;

    sUpload = objFile.value;
    iDot = sUpload.indexOf(".");

    if ((iDot < 0)) {
        alert("Invalid file path for upload in " + objName + " box.");
        return false;
    }
    if (iDot > 0) {
        sAllowedExts = sAllowedExts.toUpperCase();
        sUpload = sUpload.toUpperCase();

        oRegex = new RegExp("(" + sAllowedExts + ")$"); //Check for the file ending with the extension(s) passed.

        if (!oRegex.test(sUpload)) {
            // alert("Invalid file type.\nOnly file having ''" + sAllowedExts.replace(/\|/g, ", ") + "'' extension(s) are allowed in " + objName + " box.");
            alert("Only file having .xls extension can be uploaded.");
            return false;
        }
    }
    return true;
}
function ValidateFileXLS(objFile, sAllowedExts, objName) {

    var sUpload = "";
    var iDot = 0;
    var oRegex = null;

    sUpload = objFile.value;
    iDot = sUpload.indexOf(".");

    if ((iDot < 0)) {
        alert("Invalid file path for upload in " + objName + " box.");
        return false;
    }
    if (iDot > 0) {
        sAllowedExts = sAllowedExts.toUpperCase();
        sUpload = sUpload.toUpperCase();

        oRegex = new RegExp("(" + sAllowedExts + ")$"); //Check for the file ending with the extension(s) passed.

        if (!oRegex.test(sUpload)) {
            // alert("Invalid file type.\nOnly file having ''" + sAllowedExts.replace(/\|/g, ", ") + "'' extension(s) are allowed in " + objName + " box.");
            alert("Only file having .xls extension can be uploaded.");
            return false;
        }
    }
    return true;
}
//Used to validate if the file being uploaded is a valid file depending upon the extensions being passed.
//objFile: is the object of type FileUpload.
//sAllowedExts: is the extension of the file that is allowed to be uploaded eg .jgp|.jpeg|.gif.
//objName: name of the object that needs to be displayed in the alert message.
function IsValidateFileUploaded(fileCtrlClientID, sAllowedExts, objName) {
    var oRegex = null;
    var objFile = $("#" + fileCtrlClientID);

    oRegex = new RegExp("(" + sAllowedExts + ")$"); //Check for the file ending with the extension(s) passed.
    if (!oRegex.test(objFile.val())) {
        // alert("Invalid file type.\nOnly file having ''" + sAllowedExts.replace(/\|/g, ", ") + "'' extension(s) are allowed in " + objName + " box.");
        alert("Only file having .xls extension can be uploaded.");
        return false;
    }
    return true;
}

//Used to clear the text inside the fileUpload box.
//oFileUpload: is the object of type FileUpload.
function ClearUploadedFile(ctrlClientID) {
    var oFileUpload = document.getElementById(ctrlClientID);
    oFileUpload.value = ""; //This is used to clear text in Firefox.
    //This is used to clear text in IE.
    var fld2 = oFileUpload.cloneNode(false);
    fld2.onchange = oFileUpload.onchange;
    oFileUpload.parentNode.replaceChild(fld2, oFileUpload);
    //End.
}

/*
Used to check text box is left blank.
objField    : object of the text box that needs to be validated.
fieldName   : Name of the text box that needs to be displayed in the alert message.
*/

function IsBlank(FieldObject, ErrMsg) {
    if (Trim(FieldObject.value) == "") {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">' + ErrMsg + '!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        FieldObject.focus();
        return false;
    }
    else {
        return true;
    }
}
/*
Used to check if an option is selected in the dropdown list or list box.
fieldObject : object of the dropdown list or list box that needs to be validated.
fieldName   : Name of the dropdown list or list box that needs to be displayed in the alert message.
*/
function IsOptionNotSelected(fieldObject, fieldName) {
    if (fieldObject.selectedIndex <= 0) {
        alert("Please select " + fieldName);
        fieldObject.focus();
        return true;
    }
    else
        return false;
}

/*
Used to allow only numeric data to be entered.
This method need to be called on keyPress event of an object in which only numeric data is allowed.
isDecimal : Pass true if number needs to be added in decimal format eg ##.##. Pass false if only numeric data needs to be added.
fieldName: Name of the control that is getting validated.
*/
function AllowNumbersOnly(isDecimal, oTextBox, emailErrMsg) {
    //Here 46 key code is for dot(.) entered.
    //48 - 57 is for numbers between 0 - 9.
    if (isDecimal && event.keyCode == 46)//If decimal number is allowed then allow dot(.) to be entered.
    {
        return true;
    }
    if (event.keyCode >= 48 && event.keyCode <= 57)//Allow only number to be entered.
    {
        return true;
    }
    // alert("Please enter only numeric value in " + fieldName + ".");
    var x = document.getElementById("toastmsg");
    x.innerHTML = '<p class="green">' + emailErrMsg + '!</p>';
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    oTextBox.focus();
    return false;
}
/*
Function Name               : ValidatePasteNumeric()
Parameter(s)                : Allows numeric paste only.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   : 
Description                 : This function allows only numeric values in the CONTROL.
*/
function ValidatePasteNumeric(isDecimal, oTextBox) {
    var emailErrMsg = '';
    if (isDecimal) {
        regex = /^\d+(\.\d{1,2})?$/;
        emailErrMsg = 'Please enter only numeric/decimal values';
    }
    else {
        regex = /^[\d]+$/;
        emailErrMsg = 'Please enter only numeric values';
    }
    var $this = $(oTextBox);
    setTimeout(function () {
        var val = $this.val();

        if (regex.test(val)) {
            $this.val(val);
        }
        else {
            $this.val('');
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + emailErrMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }

    }, 0);



}

function ClearInvalidText(oTextBox) {
    if (!IsNumeric(oTextBox.value, false)) {
        oTextBox.value = "";
        oTextBox.focus();
    }


}
/*
Function Name               : IsNumeric(sText)
Parameter(s)                : sText - String to be checked. Allows numeric paste only.
Created By / On DateTime    : Jiya / 21 Aug 2019
Modified By / On DateTime   : 
Description                 : This function checks if the text has all numbers.
*/
function IsNumeric(sText, isDecimal) {
    var ValidChars = "";
    if (isDecimal) {
        ValidChars = "0123456789.";
    }
    else {
        ValidChars = "0123456789";
    }
    var IsNumber = true;
    var Char;

    for (i = 0; i < sText.length && IsNumber == true; i++) {
        Char = sText.charAt(i);
        if (ValidChars.indexOf(Char) == -1) {
            IsNumber = false;
        }
    }
    return IsNumber;
}

/*
Used to check weather valid number is entered in a textbox.
oTextBox : Object of the textbox that needs to be validated.
isDecimal : Pass true if number can be entered in decimal format.
txtName   : Name of the textbox that needs to be displayed in an error message.
isDisplayAlerMessage: Pass true if message needs to be displayed.
*/
function CheckValidNumber(oTextBox, txtName, isDecimal, isNegativeAllowed, isFocusAllowed, ResExErrMsg) {
    var errMsg = true;

    if (isDecimal == null)//If not passed then allow decimal number.
        isDecimal = true;
    if (isNegativeAllowed == null)//If not passed then allow negative numbers.
        isNegativeAllowed = true;
    if (isFocusAllowed == null)//If not passed then allow focus on the control.
        isFocusAllowed = true;

    if (isNaN(oTextBox.value) || Trim(oTextBox.value) == "")//If not a number.
    {
        errMsg = false;
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">' + ResExErrMsg + '!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    if (errMsg == "" && !isDecimal)//If decimal number is allowed.
    {
        if (oTextBox.value.indexOf(".") > -1)// If decimal point is entered.
        {
            errMsg = false;
            //errMsg = "Please enter numeric data in  " + txtName + " box without decimal point.";
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + ResExErrMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }
    }
    if (!isNegativeAllowed)//If negative number is not allowed.
    {
        if (parseFloat(oTextBox.value) < 0)//if number is less than zero.
        {
            errMsg = false;
            //errMsg = "Please enter positive number only in " + txtName + " box.";
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + ResExErrMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }
    }
    if (errMsg = false) {
        if (isFocusAllowed)
            oTextBox.focus();
        return false;
    }
    return true;
}
/*
Decription: Used to check if a valid decimal number is entered.
digitsBeforeDecimal: the number of digits that can be entered befor a decimal point e.g. 400.00 in this case digitsBeforeDecimal will be 3.
digitsAfterDecimal: the number of digits that can be entered after a decimal point e.g. 400.00 in this case digitsAfterDecimal will be 2.
ctrlClientId: The clientID of the control in which the number is entered and that needs to be validated.
fieldName: Name of the control that is getting validated.
*/
function CheckValidDecimal(digitsBeforeDecimal, digitsAfterDecimal, ctrlClientID, fieldName) {
    var oTextBox = $("#" + ctrlClientID); //Store the control in the object.
    var enteredValue = oTextBox.val(); // Get the text entered in the object.
    var digitsCount = 0;
    var maxRange = "";

    if (isNaN(enteredValue)) {
        alert("Please enter a valid decimal number in " + fieldName + ".");
        oTextBox.focus();
        return false;
    }
    //Get the number of characters after decimal point.
    if (enteredValue.indexOf(".") > -1) {
        if (enteredValue.substring(enteredValue.indexOf(".") + 1).length > parseInt(digitsAfterDecimal, 10)) {
            alert("Please enter only " + digitsAfterDecimal + " digits after decimal point in " + fieldName + ".");
            oTextBox.focus();
            return false;
        }
    }
    //Get the max number that can be entered in the textbox.
    while (digitsCount < parseInt(digitsBeforeDecimal, 10)) {
        maxRange = maxRange + "9";
        digitsCount++;
    }

    maxRange = parseFloat(maxRange) + 1;
    if (parseFloat(enteredValue) >= maxRange) {
        alert("Please enter value less than " + maxRange + " in " + fieldName + ".");
        oTextBox.focus();
        return false;
    }
    return true;
}

//For Check All Emails
function CheckEmail(emailId) {
    var emailPat = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var matchArray = emailId.match(emailPat);
    if (matchArray == null) {
        return false;
    }
    return true;

}

//Check valid Mobile number
function ValidateMobNumber(txtMobId, errorMsg, length) {
    if (txtMobId.value != "") {
        if (isNaN(txtMobId.value)) {
            // alert(errorMsg);
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + errorMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);

            txtMobId.focus();
            return false;
        }
        else if (!(txtMobId.value.length == length)) {
            // alert(errorMsg);
            var x = document.getElementById("toastmsg");
            x.innerHTML = '<p class="green">' + errorMsg + '!</p>';
            x.className = "show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);

            txtMobId.focus();
            return false;
        }
    }
}

/*
Used to clear the text in a textbox depending upon the default text passed.
oTextBox : Object of the textbox that needs to be validated.
defaultText : if the string passed as defaultText is present then clear the textbox field.
*/
function ClearTextOnFocus(ctrlClientID, defaultText) {
    var oTextBox = $("#" + ctrlClientID);
    if (Trim(oTextBox.val()) == defaultText) {
        oTextBox.val("");
        oTextBox.focus();
        oTextBox.select();
    }
}
/*
Used to set the text in a textbox depending upon the default text passed.
oTextBox : Object of the textbox that needs to be validated.
defaultText : The textbox if blank then this string passed as defaultText gets set in it.
*/
function SetTextOnBlur(ctrlClientID, defaultText) {
    var oTextBox = $("#" + ctrlClientID);
    if (Trim(oTextBox.val()) == "") {
        oTextBox.val(defaultText);
    }
}

////Get key code for the event.
//function GetKeyCode(eventObj) {
//    var keyCode = -1;

//    keyCode = GetKeyCode(eventObj);

//    return keyCode;
//}

//Show form in modal dialog.
function ShowModal(url, height, width) {
    var rand = Math.random();
    var winSettings = "center:yes;resizable:no;status:no;edge:Sunken;help:no;scroll:yes;dialogHeight:" + height + "px;dialogWidth:" + width + "px;";
    window.showModalDialog(url + "&RandNum=" + rand, null, winSettings);

    return false;
}
//Show form in modal dialog.
function ShowPopupWindow(url, height, width) {
    var rand = Math.random();
    var winSettings = "resizable=0, scrollbars=yes, toolbar=no, titlebar=no, location=no, ";

    winSettings += GetCenterScreenPositionProperty(height, width); //Get the position of the popup window in the center.

    window.open(url + "&RandNum=" + rand, "ProfileEdit", winSettings);

    return false;
}

//Get the property to position the window.open in the center of the screen.
function GetCenterScreenPositionProperty(windowHeight, windowWidth) {
    var leftprop, topprop, leftvar, rightvar, screenX, screenY, cursorX, cursorY, padAmt;

    var properties = "height = " + windowHeight + "px, width=" + windowWidth + "px";

    var arrWidthHeight = GetBrowserWidthAndHeight(); //Get the height and width of the browser.
    screenY = arrWidthHeight[0];
    screenX = arrWidthHeight[1];

    leftvar = (screenX - windowWidth) / 2;
    rightvar = (screenY - windowHeight) / 2;
    if (navigator.appName == "Microsoft Internet Explorer") {
        leftprop = leftvar;
        topprop = rightvar;
    }
    else {
        leftprop = (leftvar - pageXOffset);
        topprop = (rightvar - pageYOffset);
    }
    properties = properties + ", left = " + leftprop + "px, top = " + topprop + "px";

    return properties;
}

//Get the inner widht and height of the browser.
function GetBrowserWidthAndHeight() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    }
    else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    }
    else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    //window.alert( 'Width = ' + myWidth );
    //window.alert( 'Height = ' + myHeight );
    return new Array(myWidth, myHeight);
}

//Used to validate entered date is not greater than current date.
//dateText		: in the format dd-MMM-yyyy.
//dateFieldName : name of the date field that needs to be validated.
function IsGreaterThanCurrentDate(dateText, dateFieldName) {

    var todaysDate = new Date();
    var arrTodaysDate = new Array();

    var arrFromdaysDate = new Array();

    arrTodaysDate[0] = todaysDate.getDate();
    arrTodaysDate[1] = GetMonth(todaysDate.getMonth(), "Month");
    arrTodaysDate[2] = todaysDate.getFullYear();

    arrFromdaysDate[0] = dateText.getDate();
    arrFromdaysDate[1] = GetMonth(dateText.getMonth(), "Month");
    arrFromdaysDate[2] = dateText.getFullYear();

    daysDiff = GetDateDiffInDays(arrFromdaysDate, arrTodaysDate);
    if (daysDiff < 0) {
        alert("The " + dateFieldName + " Date cannot be greater than Todays Date.");
        return false;
    }
    return true;
}
//Get the number of day bettween fromDate and toDate passed.
//fromDateText: in the format dd-MMM-yyyy.
//toDateText: in the format dd-MMM-yyyy.
function GetDateDiffInDays(fromDateText, toDateText) {
    var arrFromDate = fromDateText;
    var arrToDate = toDateText;
    var fromDate = null;
    var toDate = null;
    var hrs = 0, min = 0, sec = 0, miliSec = 0;

    fromDate = new Date(arrFromDate[2], GetMonth(arrFromDate[1], "Num"), arrFromDate[0], hrs, min, sec, miliSec);
    toDate = new Date(arrToDate[2], GetMonth(arrToDate[1], "Num"), arrToDate[0], hrs, min, sec, miliSec);

    var difference = toDate.getTime() - fromDate.getTime();
    var daysDiff = Math.floor(difference / 1000 / 60 / 60 / 24);

    return daysDiff; //if fromDate is greater than toDate then a negative value is returned else positive value is returned.
}
//Get the number of milliseconds bettween fromDate, fromHrs, fromMin and toDate, toHrs, toMin passed.
//fromDateText: in the format dd-MMM-yyyy.
//fromHrsText: hours selected e.g. 1.
//fromMinText: minutes selected e.g. 1.
//toDateText: in the format dd-MMM-yyyy.
//toHrsText: hours selected e.g. 1.
//toMinText: minutes selected e.g. 1.
function GetDateDiffInMilliseconds(fromDateText, fromHrsText, fromMinText, toDateText, toHrsText, toMinText) {
    var arrFromDate = fromDateText.split("-");
    var arrToDate = toDateText.split("-");
    var fromDate = null;
    var toDate = null;
    var sec = 0, miliSec = 0;

    fromDate = new Date(arrFromDate[2], GetMonth(arrFromDate[1], "Num"), arrFromDate[0], fromHrsText, fromMinText, sec, miliSec);
    toDate = new Date(arrToDate[2], GetMonth(arrToDate[1], "Num"), arrToDate[0], toHrsText, toMinText, sec, miliSec);

    var difference = toDate.getTime() - fromDate.getTime();
    return difference; //if fromDate is greater than toDate then a negative value is returned else positive value is returned.
}
//Here the first argument is monthInitial i.e. the month in initial 3 letters or number to be passed.
//returnMonthOrNum should be "Month" to return month initial else "Num" if month Index needs to be returned.
function GetMonth(monthInitial, returnMonthOrNum) {
    var arrMonth = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    for (var iCount = 0; iCount < arrMonth.length; iCount++) {
        if (monthInitial == arrMonth[iCount] && returnMonthOrNum == "Num") {
            return iCount;
        }
        if (monthInitial == iCount && returnMonthOrNum == "Month") {
            return arrMonth[iCount];
        }
    }
}
function GetKeyCodeMain(eventObject) {
    var keyCode = 0;
    if (eventObject)//If event object is passed.
        if (eventObject.keyCode)//If event object has keyCode as a property.
            keyCode = eventObject.keyCode;
        else
            keyCode = eventObject.which;//This will be if browser is netscape or firefox.
    else
        keyCode = event.keyCode;//If not event object is passed then get keyCode from the event object.
    return keyCode;
}


function GetPageDetails(NavMenuCode) {
    $.ajax({
        type: 'GET',
        url: '/CommonMethods/GetPageDetailsByNavigationCode',
        data: { MenuCode: NavMenuCode },
        dataType: "json",
        success: function (response) {
            //This function should be available in calling page else it will break.
            GetCommonDetailsOfPage();
        },
        error: function () {
            alert(" An error occurred.");
        },
    })
}

//Read grid column from resource
function GetKeyValueFromResxForIndex(columnKey) {
    return $.parseJSON($.ajax({
        type: 'GET',
        url: '/CommonMethods/GetKeyValueFromResx',
        data: { keys: columnKey },
        dataType: "json",
        async: false,
        success: function (response) {

        },
        error: function () {
            alert(" An error occurred.4");
        },
    }).responseText);
}
//This is common function to check the duplicate data

function CheckDuplicateData(collection, oTextBox, errorMsg) {


    var $this = $(oTextBox);
    var val = $this.val();
    var emailErrMsg = val + ' ' + errorMsg;
    var text = Trim(val);
    if (text != '') {
        var returnData = $.parseJSON($.ajax({
            type: 'POST',
            url: '/CommonMethods/CheckDuplicateData',
            data: collection,
            dataType: "json",
            async: false,
            success: function (response) {

            },
            error: function () {
                alert(" An error occurred.4");
            },
        }).responseText);

        setTimeout(function () {

            if (returnData > 0) {
                $this.val('');
                var x = document.getElementById("toastmsg");
                x.innerHTML = '<p class="green">' + emailErrMsg + '!</p>';
                x.className = "show";
                setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);

            }
            else {
                $this.val(val);
            }

        }, 0);
    }
}


function ChangeDateFormatToddMMYYY(dateValue) {


    var strDate = "";
    if (dateValue != null) {
        var strSplit = dateValue.split('/');

        var mm = "";
        if (strSplit[1] == "01")
            mm = "Jan";
        else if (strSplit[1] == "02")
            mm = "Feb";
        else if (strSplit[1] == "03")
            mm = "Mar";
        else if (strSplit[1] == "04")
            mm = "Apr";
        else if (strSplit[1] == "05")
            mm = "May";
        else if (strSplit[1] == "06")
            mm = "Jun";
        else if (strSplit[1] == "07")
            mm = "Jul";
        else if (strSplit[1] == "08")
            mm = "Aug";
        else if (strSplit[1] == "09")
            mm = "Sep";
        else if (strSplit[1] == "10")
            mm = "Oct";
        else if (strSplit[1] == "11")
            mm = "Nov";
        else if (strSplit[1] == "12")
            mm = "Dec";
        strDate = strSplit[0] + '-' + mm + '-' + strSplit[2];

    }
    return strDate;


}
function LoadSystemMasterDropdown(element, typeID,fxdData, selectedValue) {
    if (SystemdropdownMasterData.length == 0) {
        //ajax func
        CommonAjaxMethod('/CommonMethods/GetDropDownData', { tableType: typeID, FixedSearchParam: fxdData}, false, 'GET',
            function (response) {
                SystemdropdownMasterData = response.dataCollection;
                //render catagory
                BindSystemDropDown(element, selectedValue);
            }

            );


    }
    else {
        //render catagory to the element
        BindSystemDropDown(element, selectedValue);
    }
}

function BindSystemDropDown(element, selectedValue) {

    var $ele = $('#' + element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(SystemdropdownMasterData, function (ii, vall) {

        $ele.append($('<option/>').val(vall.Id).text(vall.Name));
    })
    var ddlselectValue = selectedValue == undefined ? "" : selectedValue;
    if (ddlselectValue != "")
        $ele.val(selectedValue);
}

function CheckDOB(trlID, errorMsg) {
    var dateString = document.getElementById(trlID).value;
    var splitString = dateString.split('/');
    var newDAte = splitString[1] + '/' + splitString[0] + '/' + splitString[2];
    var myDate = new Date(newDAte);
    return IsGreaterThanCurrentDate(myDate, errorMsg);

}


