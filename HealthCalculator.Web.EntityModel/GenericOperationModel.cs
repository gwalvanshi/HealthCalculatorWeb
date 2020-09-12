using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HealthCalculator.Web.EntityModel
{
    public class ScreenDBMapping //: BaseEntity
    {
        public String ScreenID { get; set; }
        public String TableName { get; set; }
        public String ProcName { get; set; }
        public String ValidateProc { get; set; }
        public bool TableNameIsProc { get; set; }
        public String FilterColumn { get; set; }
        //public int ScreenDBMapping_id { get; set; }
    }

    public class ValidateModeResponse
    {
        public ValidateModeResponse()
        {
            GUID = Guid.NewGuid().ToString();
            this.ReferenceID = -1;
        }

        public String GUID { get; set; }
        public int ReferenceID { get; set; }

        public String FieldName { get; set; }
        public bool IsValid { get; set; }
        public bool UpdateRecord { get; set; }
        public int ErrorCode { get; set; }
        public String ErrorMessage { get; set; }
        public DataTable Data { get; set; }
    }
    public class GenericOperationModel
    {

        public String ScreenID { get; set; }
        public int UserID { get; set; }
        public String Operation { get; set; }
        public String Lang { get; set; }
        public String XML { get; set; }
        public Rows Rows { get; set; }


        //public static Rows GetGlobalRows(GenericOperationModel model)
        //{
        //    Rows rows = new Rows();
        //    List<GenericOperationModelData> temp = new List<GenericOperationModelData>();
        //    GenericOperationModelData dt = new GenericOperationModelData();            

        //    dt = new GenericOperationModelData();
        //    dt.KeyName = "ScreenID".ToLower();
        //    dt.ValueData = model.ScreenID;
        //    temp.Add(dt);

        //    dt = new GenericOperationModelData();
        //    dt.KeyName = "UserID".ToLower();
        //    dt.ValueData = model.UserID.ToString();
        //    temp.Add(dt);

        //    dt = new GenericOperationModelData();
        //    dt.KeyName = "Operation".ToLower();
        //    dt.ValueData = model.Operation;
        //    temp.Add(dt);        

        //    rows.Data = temp.ToArray();
        //    return rows;
        //}

    }

    [XmlRoot("root")]
    public class Rows
    {

        public GenericOperationModelData[] Data { get; set; }
    }

    public class GenericOperationModelData
    {
        public GenericOperationModelData()
        {

        }
        public GenericOperationModelData(String key, String value)
        {
            this.KeyName = key;
            this.ValueData = value;
            this.RowIndex = 0;

        }
        public int RowIndex { get; set; }
        public string KeyName { get; set; }
        public string ValueData { get; set; }


    }

}
