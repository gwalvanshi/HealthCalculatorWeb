using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
   public class GenericGetRecordModel
    {
    }

    public class IndexScreenParameterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<IndexScreenSelectParameterModel> IndexScreenSelectParameterModel { get; set; }
        public List<IndexScreenSearchParameterModel> IndexScreenSearchParameterModel { get; set; }
        public string SelectColumnToGrid { get; set; }
        public int SchoolId { get; set; }
        public int GroupId { get; set; }
        public int ReferenceID { get; set; }
        public String TableName { get; set; }
        public int ASGMapping_Id { get; set; }
        public int SgMapping_id { get; set; }
        public String ScreenID { get; set; }
        public String Language { get; set; }
        public int UserId { get; set; }
    }
    public class IndexScreenSelectParameterModel
    {
        public string SelectParameter { get; set; }
    }
    public class IndexScreenSearchParameterModel
    {
        public string SearchParameter { get; set; }
        public string SearchParameterValue { get; set; }
        public string SearchParameterDataType { get; set; }
    }
}
