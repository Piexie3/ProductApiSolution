using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Mappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(ProductDto product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price
        };

        public static (ProductDto?, IEnumerable<ProductDto>?) FromEntity(Product product, IEnumerable<Product>? products)
        {
            // return single
            if (product is not null || products is null)
            {
                var singleProduct = new ProductDto(
                    Id: product!.Id,
                    Name: product.Name!,
                    Quantity: product.Quantity,
                    Price: product.Price
                    );
                return (singleProduct, null);
            }
            if (product is null || products is not null)
            {
                var _products = products.Select(pDto => new ProductDto(pDto.Id, pDto.Name!, pDto.Quantity, pDto.Price)).ToList();
                return (null, _products);
            }
            return (null, null);
        }
    }
}
