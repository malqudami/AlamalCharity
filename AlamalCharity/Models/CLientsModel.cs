using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlamalCharity.Models
{
    public class CLientsModel
    {
        public int clntID { get; set; }
        public string clntName { get; set; }
        public string clntFamily{ get; set; }
        public List<SelectListItem> Families { get; set; }
        public string clntMobile { get; set; }
        public int clntStatus { get; set; }
    }
}
