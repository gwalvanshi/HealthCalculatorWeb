var dropdownData = [];
var requiredFeild = [];
var dropdownMasterData = [];
var isAllValid = true;
var isSaved = true;
var commonjsURL = '/Scripts/DevScripts/common/Common.js';
var SystemdropdownMasterData = [];
$('#UserSubmit').click(function () {

    var CUserId = $('#CUserId').val();
    isAllValid = checkValidationOnSubmit();
    if (isAllValid) {



        if (ValidationOnSave())
        {
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
            
            CommonAjaxMethod('/User/AddUserDetails', data, true, 'POST');

        }
    }


});
function ValidationOnSave()
{
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
function FormControlValueClear(isValid)
{
    if (isValid)
    {
        $('#CUserId,#UserGroup1401lmdEmailID,#UserGroup1401lmdPassword,#UserGroup1401lmdPhoneNumber,#UserGroup1401lmdLoginID,#UserGroup1401lmdFirstName,#UserGroup1401lmdLastName', '#UserGroup1401lmdconfirmpassword').val('');
        $("#UserGroup1401lmdUserType,#UserGroup1401lmdGroup").val("0").change();
    }    
}
$(document).ready(function ()
{
});





