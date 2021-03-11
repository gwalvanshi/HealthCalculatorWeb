using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace HealthCalculator.Web.EntityModel

{

    public class LoginEntity

    {
        public DateTime? StartActiveDate { get; set; }
        public DateTime? EndActiveDate { get; set; }
        public string OrderAmountINR { get; set; }
        public string EncriptUserId { get; set; }
        public int ? Totaldays { get; set; }
        public int order_id { get; set; }
        public int ProductID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCompleted { get; set; }
        public string OrderAmountDoller { get; set; }
        public string Coupen { get; set; }
        public string program { get; set; }
        public string ProductName { get; set; }
	  
        public int tokenId { get; set; }

        public int LoginType { get; set; }

        public int Days { get; set; }

        public Int64 userId { get; set; }

        public Int64 OrderId { get; set; }
        public Int64 OrderDetail_Id { get; set; }

        public string authToken { get; set; }

        public System.DateTime issuedOn { get; set; }
        public System.DateTime ? AddedWhen { get; set; }
        public System.DateTime expiresOn { get; set; }

        public string EmployeeCode

        {

            get;

            set;

        }

        public string UserName

        {

            get;

            set;

        }



        public string OldPassword

        {

            get;

            set;

        }



        public string Password

        {

            get;

            set;

        }

        public Int64 RoleID

        {

            get;

            set;

        }

        public Int64 EmployeeID

        {

            get;

            set;

        }

        public string EmployeeName

        {

            get;

            set;

        }

        public Int64 DesignationID

        {

            get;

            set;

        }

        public string Email

        {

            get;

            set;

        }

        public Int64 result

        {

            get;

            set;

        }

        public string ErrorMessage

        {

            get;

            set;

        }

        public Int64 ? ModifiedBy

        {

            get;

            set;

        }

        public Int64 LocationID

        {

            get;

            set;

        }

        public Int64 DepartmentID

        {

            get;

            set;

        }

        public string RoleName

        {

            get;

            set;

        }
        public string FirstName

        {

            get;

            set;

        }




        public string LastName

        {

            get;

            set;

        }
        public int CreatedBy

        {

            get;

            set;

        }
        public bool IsExternalUser

        {

            get;

            set;

        }
        public string MobileNo

        {

            get;

            set;

        }
        public string PhoneNo

        {

            get;

            set;

        }
        public string ConfirmPassword

        {

            get;

            set;

        }
        

    }

}