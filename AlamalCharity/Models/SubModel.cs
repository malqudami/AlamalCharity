using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlamalCharity.Models
{
    public class SubModel
    {
        public int subID { get; set; }
        public int subClientID { get; set; }
        public string subClientName { get; set; }
        public List<CLientModel> Clients { get; set; }
        public int subYear { get; set; }
        public List<SelectListItem> Years { get; set; }
        public int subJAN { get; set; }
        public DateTime subJAN_DATE { get; set; }
        public int subFEB { get; set; }
        public DateTime subFEB_DATE { get; set; }
        public int subMAR { get; set; }
        public DateTime subMAR_DATE { get; set; }
        public int subAPR { get; set; }
        public DateTime subAPR_DATE { get; set; }
        public int subMAY { get; set; }
        public DateTime subMAY_DATE { get; set; }
        public int subJUN { get; set; }
        public DateTime subJUN_DATE { get; set; }
        public int subJUL { get; set; }
        public DateTime subJUL_DATE { get; set; }
        public int subAUG { get; set; }
        public DateTime subAUG_DATE { get; set; }
        public int subSEP { get; set; }
        public DateTime subSEP_DATE { get; set; }
        public int subOCT { get; set; }
        public DateTime subOCT_DATE { get; set; }
        public int subNOV { get; set; }
        public DateTime subNOV_DATE { get; set; }
        public int subDEC { get; set; }
        public DateTime subDEC_DATE { get; set; }
        public int subTotal { get; set; }

        public bool _subJAN { get; set; }
        public bool _subFEB { get; set; }
        public bool _subMAR { get; set; }
        public bool _subAPR { get; set; }
        public bool _subMAY { get; set; }
        public bool _subJUN { get; set; }
        public bool _subJUL { get; set; }
        public bool _subAUG { get; set; }
        public bool _subSEP { get; set; }
        public bool _subOCT { get; set; }
        public bool _subNOV { get; set; }
        public bool _subDEC { get; set; }
    }
}
