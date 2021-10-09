using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class UserTrackerReport
    {
        public string UserID { get; set; }
        public string UserCode { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public string IsActive { get; set; }
        public string IS_FIRST_LOGIN { get; set; }
        public string IS_LOCKED { get; set; }
        public string FAILED_ATTEMPTS { get; set; }
        public string PasswordChangedDate { get; set; }
        public string IsExternalUser { get; set; }
        public string AddedWhen { get; set; }
        public string code { get; set; }
            

    }
}
