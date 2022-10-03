namespace PharmacyWebApp.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        public OrderForHeader OrderForHeader { get; set; }
    }
}