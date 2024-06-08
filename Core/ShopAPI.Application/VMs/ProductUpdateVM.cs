namespace ShopAPI.Web.Controllers
{
    public class ProductUpdateVM
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public int ProductStock { get; set; }
        public float ProductPrice { get; set; }
    }
}
