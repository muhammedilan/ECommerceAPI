namespace ECommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductsImageQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public Guid ProductId { get; set; }
    }
}
