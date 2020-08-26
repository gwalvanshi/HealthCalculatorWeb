var dropdownData = [];
var requiredFeild = [];
var dropdownMasterData = [];
var isAllValid = true;
var isSaved = true;
var commonjsURL = '/Scripts/DevScripts/common/Common.js';
var combojsURL = '/Scripts/DevScripts/common/ComboFill.js';
var SystemdropdownMasterData = [];
$('#UserSubmit').click(function () {

    var CUserId = $('#CUserId').val();
    isAllValid = checkValidationOnSubmit();
    if (isAllValid) {



        if (ValidationOnSave()) {
            var data =
            {

                CUserId: CUserId,
                Email: $('#UserGroup1401lmdEmailID').val() == undefined ? 0 : $('#UserGroup1401lmdEmailID').val().trim(),
                Password: $('#UserGroup1401lmdPassword').val() == undefined ? 0 : $('#UserGroup1401lmdPassword').val().trim(),
                ConfirmPassword: $('#UserGroup1401lmdPassword').val() == undefined ? 0 : $('#UserGroup1401lmdPassword').val().trim(),
                PhoneNumber: $("#UserGroup1401lmdPhoneNumber").val() == undefined ? 0 : $('#UserGroup1401lmdPhoneNumber').val().trim(),
                UserName: $('#UserGroup1401lmdLoginID').val() == undefined ? 0 : $('#UserGroup1401lmdLoginID').val().trim(),
                IsDeleted: false,
                FirstName: $("#UserGroup1401lmdFirstName").val() == undefined ? '' : $("#UserGroup1401lmdFirstName").val().trim(),
                LastName: $("#UserGroup1401lmdLastName").val() == undefined ? '' : $("#UserGroup1401lmdLastName").val().trim(),
                UserGroupId: $("#UserGroup1401lmdGroup").val() == undefined ? '' : $("#UserGroup1401lmdGroup").val().trim(),
                StuStaffTypeId: $("#UserGroup1401lmdUserType").val() == undefined ? '' : $("#UserGroup1401lmdUserType").val().trim(),
                StuStaff_ID: $("#txtStudentId").val(),
                ProfilePhoto: $("#hdnProfilePhotoPath").val()
            }
            var CUserId = $('#CUserId').val() == undefined ? "" : $('#CUserId').val();

            //true: this is for add common fileds into data object, in case false it will not add common value.
            //i.e.Session id school id, group id etc.Method type is post
            if (CUserId != "")
            {
                data.Id = sessionStorage.getItem('UserId');
                data.ModifiedBy_ID = sessionStorage.getItem('CUserId');
                CommonAjaxMethod('/UserCreation/Edit', data, true, 'POST');
            }
            else
            {
                data.CreatedBy_ID = sessionStorage.getItem('CUserId');
                CommonAjaxMethod('/UserCreation/Create', data, true, 'POST');
            }

        }
    }


});
function ValidationOnSave() {
    var UserId = $('#CUserId').val() == undefined ? "" : $('#CUserId').val();
    var passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
    if (ValidateEmail(document.getElementById('UserGroup1401lmdEmailID'), ValidEmail) == false) {
        return false;
    }

    else if (document.getElementById('UserGroup1401lmdPassword').value != document.getElementById('UserGroup1401lmdconfirmpassword').value) {
        alert("Password and confirm password should be matached.")
        return false;
    }
    else if (!document.getElementById('UserGroup1401lmdPassword').value.match(passw) && UserId == "") {
        alert("Passwords must have minimum 6 char and at least one non letter or digit character.Passwords must have at least one lowercase('a' - 'z').Passwords must have at least one uppercase('A' - 'Z').");
        return false;
    }

    return true;
}
//For control clear in case form submitted succesfuly.
function FormControlValueClear(isValid) {
    if (isValid) {
        $('#CUserId,#UserGroup1401lmdEmailID,#UserGroup1401lmdPassword,#UserGroup1401lmdPhoneNumber,#UserGroup1401lmdLoginID,#UserGroup1401lmdFirstName,#UserGroup1401lmdLastName', '#UserGroup1401lmdconfirmpassword').val('');
        $("#UserGroup1401lmdUserType,#UserGroup1401lmdGroup").val("0").change();
    }
    else {
        $('#submit').val('Save');
    }
}
$(document).ready(function () {

    var ctrlArray = new Array("submit");

    $.getScript(commonjsURL, function myfunction() {
        GetKeyForValidation(sessionStorage.getItem('MenuCode'));
        // //GetPermission(sessionStorage.getItem('groupId'), sessionStorage.getItem('MenuId'), ctrlArray);

    });
    $('#lblMenuCode').text("Menu Code:" + ' ' + NavMenuConstants.USER_CREATION_ADD_MENUCODE);
    $("#btnHideModal").click(function () {
        $("#loginModal").modal('hide');
    });
    $.getScript(combojsURL, function myfunction() {

        var CUserId = $('#CUserId').val() == undefined ? "" : $('#CUserId').val();
        if (CUserId != "")
            GetUserDetails();
        else {
            LoadCombo('UserGroup1401lmdGroup', 'UserGroup', 'UserGroupName', 'UserGroupId', 'ISDeleted=0');
            //LoadMasterDropdown('UserGroup1401lmdUserType', null, MasterTypeEnum.UserType, null, null, null, false);
            LoadSystemMasterDropdown('UserGroup1401lmdUserType', SystemMasterTableEnum.UserType);
        }
    });

    function GetUserDetails() {
        var CUserId = $('#CUserId').val();
        CommonAjaxMethod('/UserCreation/EditData', { Id: CUserId }, false, 'GET',
        function (response) {
            console.log("response", response.data);
            var d = response.data;
            BindEdit(d);
        }


        );

    }
    function BindEdit(d) {

        $('#CUserId').val(d.CUserId);
        $('#UserGroup1401lmdEmailID').val(d.Email);
        $("#UserGroup1401lmdPassword").val(d.PasswordHash);
        $("#UserGroup1401lmdconfirmpassword").val(d.PasswordHash);
        $('#UserGroup1401lmdPhoneNumber').val(d.PhoneNumber);
        $('#UserGroup1401lmdLoginID').val(d.UserName);
        $("#UserGroup1401lmdFirstName").val(d.FirstName);
        $("#UserGroup1401lmdLastName").val(d.LastName);
        var stafid = d.StuStaff_ID == undefined ? "" : d.StuStaff_ID;
        if (stafid != "") {
            $("#txtStudentId").val(stafid)
            var fName = d.FirstName + " " + d.LastName + "(" + d.PhoneNumber + ")";
            $("#UserGroup1401lmdStaff_ID_OR_Student_ID").val(fName);
        }

        LoadCombo('UserGroup1401lmdGroup', 'UserGroup', 'UserGroupName', 'UserGroupId', 'ISDeleted=0', d.UserGroupId == undefined ? "" : d.UserGroupId.toString());

        // LoadMasterDropdown('UserGroup1401lmdUserType', null, MasterTypeEnum.UserType, null, null, null, false, d.StuStaffTypeId == undefined ? "" : d.StuStaffTypeId.toString());
        LoadSystemMasterDropdown('UserGroup1401lmdUserType', SystemMasterTableEnum.UserType, d.StuStaffTypeId.toString());

        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").prop("disabled", false);
        $("#btnPopup").prop("disabled", false);


    }


});

function EnablePopupControl() {

    if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {
        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").prop("disabled", false);
        $("#btnPopup").prop("disabled", false);

    }
    else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").prop("disabled", false);
        $("#btnPopup").prop("disabled", false);
    }
    else {
        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").prop("disabled", true);
        $("#btnPopup").prop("disabled", true);

    }

}

function GetlookupData(id) {


    $("#hdnlookupId").val(id);


}

function GetUserDetailsFromPopup(ctrlurl, Uid) {

    CommonAjaxMethod(ctrlurl, { id: Uid }, false, 'GET',
    function (response) {
        console.log("response", response);
        $('#UserGroup1401lmdEmailID').val(response.data.Email);
        $('#UserGroup1401lmdPhoneNumber').val(response.data.ContactMobileNo);
        $('#UserGroup1401lmdLoginID').val(response.data.UserName);
        $("#UserGroup1401lmdFirstName").val(response.data.FirstName);
        $("#UserGroup1401lmdLastName").val(response.data.Lastname);

        var fullName = response.data.FirstName + " " + response.data.Lastname + "(" + response.data.ContactMobileNo + ")";
        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").val(fullName);
    }


    );
}



function AddData() {


    if ($("#hdnlookupId").val() == "") {
        var x = document.getElementById("Ptoastmsg");
        x.innerHTML = '<p class="green">Please select atleast one data!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else {
        var url = "";

        $("#txtStudentId").val($("#hdnlookupId").val());


        $("#loginModal").modal('hide');

        if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {
            url = '/StudentMaster/EditData';

        }
        else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
            url = '/EmployeeMaster/EditData';
        }
        GetUserDetailsFromPopup(url, $("#hdnlookupId").val());

    }

}

function SearchPopupData() {
    Bindpopup();

}
function OpenModelPopup() {
    var x = document.getElementById("Ptoastmsg");
    x.innerHTML = '';
    x.className = "hide";
    if ($("#UserGroup1401lmdUserType").val() == "" || $("#UserGroup1401lmdUserType").val() == "0") {
        var x = document.getElementById("toastmsg");
        x.innerHTML = '<p class="green">Please select user type!</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
    else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {

        $("#loginModal").modal('show');
        Bindpopup();
    }
    else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
        $("#loginModal").modal('show');
        Bindpopup();
    }
    else {
        $("#UserGroup1401lmdStaff_ID_OR_Student_ID").prop("disabled", true);
        $("#btnPopup").prop("disabled", true);
        var x = document.getElementById("Ptoastmsg");
        x.innerHTML = '<p class="green">Candidate and others are not allow to create the users</p>';
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

}
function Bindpopup() {

    $("#hdnlookupId").val('');
    $("#hdnlooupName").val('');
    var x = document.getElementById("headerValue");
    var objData;


    if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {
        x.innerHTML = 'Student Details';

        objData = {
            "PopupType": 6, "SearchValue": $("#txtSearch").val(), "fixedSearch": 6
        };
    }
    else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
        x.innerHTML = 'Staff Details';
        objData = {
            "PopupType": 5, "SearchValue": $("#txtSearch").val(), "fixedSearch": 6
        };
    }




    $("#tblPopup").DataTable({
        "responsive": true,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": false, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "destroy": true,
        "ajax": {
            "url": "/CommonMethods/PupupLookupData",
            "type": "POST",
            "datatype": "json",
            "data": objData,
            'beforeSend': function (request) {
                request.setRequestHeader('ASGMapping_Id', sessionStorage.getItem('ASGMapping_Id'));
            },

        },
        "order": [[3, 'asc']],
        "columnDefs":
            [{
                "targets": [0],
                "visible": false

            }],
        "columns": [
            { "data": "ID", "name": "ID", "autoWidth": true },
            {
                "orderable": false,
                "render": function (data, type, full, meta) {


                    return '<input type="radio" aria-hidden="true" name="id" onclick=GetlookupData("' + full.ID + '")   />';
                }
            },

            { "data": "FirstName", "name": "FirstName", "autoWidth": true },
            { "data": "Lastname", "name": "Lastname", "autoWidth": true },
            { "data": "FatheFirstName", "name": "FatheFirstName", "autoWidth": true },
            { "data": "ContactMobileNo", "name": "ContactMobileNo", "autoWidth": true }



        ]

    });
}

$(function () {
    $("#UserGroup1401lmdStaff_ID_OR_Student_ID").autocomplete(
        {
            source: function (request, response) {
                var obj = {};
                obj.Prefix = request.term;
                var baseURL = "";
                baseURL = '/CommonMethods/AutoLookupData/';
                if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {
                    obj.PopupType = 6;
                }
                else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
                    obj.PopupType = 5;
                }

                CommonAjaxMethod(baseURL, obj, false, 'GET',
                function (responsePost) {

                    response($.map(responsePost.dataCollection, function (item) {
                        return item;
                    }))
                }


                );

                /*  old code
                $.ajax({
                    url: baseURL,
                    data: JSON.stringify(obj),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            return item;
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });*/

            },
            select: function (e, i) {

                $("#txtStudentId").val(i.item.val);
                if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "student") {
                    url = '/EmployeeMaster/EditData';

                }
                else if (($("#UserGroup1401lmdUserType option:selected").text()).toLowerCase() == "staff") {
                    url = '/StudentMaster/EditData';
                }
                GetUserDetailsFromPopup(url, i.item.val);
            },
            minLength: 2
        }).keyup();
});


function IsDataExist(textBox, errorMsg) {

    var updateID = $('#txtStudentId').val();
    var UpdateValue = false;
    if (updateID != '') {
        UpdateValue = true;
    }
    var Collection =
            {
                TableName: "AspNetUsers",
                SelectColumn: "CUserId",//this should be PK
                IsUpdate: UpdateValue,
                UpdateIdData: updateID,//this should be  PK value.
                DuplicateCheckColumnModelList: [],
            }
    var DuplicateCheckColumn =
           {
               ColumnName: "UserName",
               ColumnData: textBox.value,
               ColumnDataType: "string"//this should be data type according to data table

           }
    Collection.DuplicateCheckColumnModelList.push(DuplicateCheckColumn)

    CheckDuplicateData(Collection, textBox, errorMsg);
}

function IsDataExist1(textBox, errorMsg) {

    var updateID = $('#txtStudentId').val();
    var UpdateValue = false;
    if (updateID != '') {
        UpdateValue = true;
    }
    var Collection =
            {
                TableName: "AspNetUsers",
                SelectColumn: "CUserId",//this should be PK
                IsUpdate: UpdateValue,
                UpdateIdData: updateID,//this should be  PK value.
                DuplicateCheckColumnModelList: [],
            }
    var DuplicateCheckColumn =
           {
               ColumnName: "Email",
               ColumnData: textBox.value,
               ColumnDataType: "string"//this should be data type according to data table

           }
    Collection.DuplicateCheckColumnModelList.push(DuplicateCheckColumn)

    CheckDuplicateData(Collection, textBox, errorMsg);
}
