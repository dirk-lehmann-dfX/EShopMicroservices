namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException()
        : base("The product was not found.")
    {            
    }
}
