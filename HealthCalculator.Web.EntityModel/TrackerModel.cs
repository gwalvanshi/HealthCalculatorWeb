﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCalculator.Web.EntityModel
{
    public class TrackerModel
    {
        public Int64 OrderId { get; set; }
        public Int64 UserId { get; set; }
        public string OrderAmountINR { get; set; }
        public string OrderAmountDoller { get; set; }
        public string CoupenId { get; set; }
        public int ProductId { get; set; }
        public DateTime ? StartActiveDate { get; set; }
        public DateTime ? EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public bool IsActive { get; set; }
        public Int64 GivenDays { get; set; }
    }

    public class Order
    {
        public Int64 OrderId { get; set; }
        public Int64 UserId { get; set; }
        public string OrderAmountINR { get; set; }
        public int OrderAmountDoller { get; set; }      
        public int CoupenId { get; set; }
        public int ProductId { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
    public class OrderDetail
    {
        public Int64 OrderDetail_Id { get; set; }
        public Int64 Order_Id { get; set; }
        public Int64 ProductId { get; set; }
        public string INRRate { get; set; }
        public string DollerRate { get; set; }
        public string Discount { get; set; }
        public DateTime StartActiveDate { get; set; }
        public DateTime EndActiveDate { get; set; }
        public Int64 Totaldays { get; set; }
        public int IsActive { get; set; }             

        public string AddedBy { get; set; }
        public DateTime AddedWhen { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedWhen { get; set; }
    }
}
