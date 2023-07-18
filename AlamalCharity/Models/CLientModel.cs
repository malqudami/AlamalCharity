using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlamalCharity.Models
{
    public class CLientModel
    {
        public int clntID { get; set; }
        public string clntName { get; set; }
        public string clntFamilyValue { get; set; }
        public int clntFamily { get; set; }
        public List<SelectListItem> Families { get; set; }
        public string clntMobile { get; set; }
        public DateTime clntAddDate{ get; set;}
        public int clntStatus { get; set; }
    }
}
