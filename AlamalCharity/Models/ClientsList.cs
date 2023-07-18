using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AlamalCharity.Models
{
    public class ClientsList
    {
        public List<CLientModel> Clients { get; set; }
        public List<FamilyModel> Families { get; set; }
    }
}
