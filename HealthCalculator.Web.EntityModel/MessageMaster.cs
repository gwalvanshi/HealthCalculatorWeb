using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class MessageMaster
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Attachement { get; set; }
        public int MessageId { get; set; }
        public string Message { get; set; }
        public int AddedBy { get; set; }
        public int ConID { get; set; }
        public int RoleID { get; set; }
        public DateTime Addedwhen { get; set; }
        public string UserName { get; set; }
        public List<MessageConversation> MessageConversationList { get; set; }

    }
    public class MessageConversation
    {      
        public int MessageId { get; set; }
        public string Attachement { get; set; }
    }
    public class MessageMasterData
    {
      
        public int UserID { get; set; }
        public string Message { get; set; }
        public int MessageId { get; set; }
        public DateTime Addedwhen { get; set; }
        public int AddedBy { get; set; }
        public string UserName { get; set; }
        public List<MessageMasterDataList> MessageMasterDataList { get; set; }
    }
    public class MessageMasterDataList
    {
        public string Attachement { get; set; }
        public int MessageId { get; set; }
    }

}
