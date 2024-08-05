using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PriceSignageSystem.Models.Dto
{
    public class ExportPCAExemptionDto
    {
        public decimal SKU { get; set; }
        public decimal UPC { get; set; }
        //public string Brand { get; set; }
        //public string Model { get; set; }
        public string ItemDesc { get; set; }
        //public string LongDesc { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal StartDate { get; set; }
        public decimal EndDate { get; set; }
        public string DepartmentName { get; set; }
        public string SignType { get; set; }
        //public string Size { get; set; }
        public string ExemptionType { get; set; }
        public decimal ExemptionDate { get; set; }
        public string StoreCodes { get; set; }
        public string CorrectPrice { get; set; }
        public string PriceEventType { get; set; }
        public string CreateCoContract { get; set; }
    }
}