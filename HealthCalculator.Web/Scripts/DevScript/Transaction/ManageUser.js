var dropdownData = [];
var requiredFeild = [];
var dropdownMasterData = [];
var isAllValid = true;
var isSaved = true;
var commonjsURL = '/Scripts/DevScripts/Common.js';
var SystemdropdownMasterData = [];
$('#UserSubmit').click(function ()
{

    if (ValidationOnSave()) {
        var data =
        {

         
            Email: $('#txtEmailID').val() == undefined ? 0 : $('#txtEmailID').val().trim(),
            Password: $('#txtPassword').val() == undefined ? 0 : $('#txtPassword').val().trim(),
            ConfirmPassword: $('#txtconfirmpassword').val() == undefined ? 0 : $('#txtconfirmpassword').val().trim(),
            PhoneNo: $("#txtPhoneNumber").val() == undefined ? 0 : $('#txtPhoneNumber').val().trim(),
            UserName: $('#txtLoginId').val() == undefined ? 0 : $('#txtLoginId').val().trim(),
            RoleId: 3,
            FirstName: $("#txtFirstName").val() == undefined ? '' : $("#txtFirstName").val().trim(),
            LastName: $("#txtLastName").val() == undefined ? '' : $("#txtLastName").val().trim(),
            MobileNo: $("#txtMobileNumber").val() == undefined ? 0 : $('#txtMobileNumber').val().trim(),
        }

        CommonAjaxMethod('/User/AddUserDetails', data, false, 'POST');

    }
    //isAllValid = checkValidationOnSubmit();
    //if (isAllValid) {



    //    if (ValidationOnSave())
    //    {
    //        var data =
    //        {

    //            CUserId: CUserId,
    //            Email: $('#UserGroup1401lmdEmailID').val() == undefined ? 0 : $('#UserGroup1401lmdEmailID').val().trim(),
    //            Password: $('#UserGroup1401lmdPassword').val() == undefined ? 0 : $('#UserGroup1401lmdPassword').val().trim(),
    //            ConfirmPassword: $('#UserGroup1401lmdPassword').val() == undefined ? 0 : $('#UserGroup1401lmdPassword').val().trim(),
    //            PhoneNumber: $("#UserGroup1401lmdPhoneNumber").val() == undefined ? 0 : $('#UserGroup1401lmdPhoneNumber').val().trim(),
    //            UserName: $('#UserGroup1401lmdLoginID').val() == undefined ? 0 : $('#UserGroup1401lmdLoginID').val().trim(),
    //            IsDeleted: false,
    //            FirstName: $("#UserGroup1401lmdFirstName").val() == undefined ? '' : $("#UserGroup1401lmdFirstName").val().trim(),
    //            LastName: $("#UserGroup1401lmdLastName").val() == undefined ? '' : $("#UserGroup1401lmdLastName").val().trim(),
    //            MobileNumber: $("#txtMobileNumber").val() == undefined ? 0 : $('#txtMobileNumber').val().trim(),
    //        }
                        
    //        CommonAjaxMethod('/User/AddUserDetails', data, false, 'POST');

    //    }
    //}


});
function ValidationOnSave()
{
  
    var passw = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/;
  if (document.getElementById('txtPassword').value != document.getElementById('txtconfirmpassword').value) {
        alert("Password and confirm password should be matached.")
        return false;
    }
    else if (!document.getElementById('txtPassword').value.match(passw))
    {
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
        $('#txtEmailID,#txtPassword,#txtconfirmpassword,#txtPhoneNumber,#txtLoginId,#txtFirstName', '#txtLastName','#txtMobileNumber').val('');
        
    }    
}
$(document).ready(function ()
{
});





