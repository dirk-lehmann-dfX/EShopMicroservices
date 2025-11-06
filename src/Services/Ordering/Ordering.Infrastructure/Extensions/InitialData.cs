namespace Ordering.Infrastructure.Extensions;

internal class InitialData
{
    private static Guid _customerId1 = new Guid("B0A8BFE0-DC20-43A0-88F6-0E459ABA3C14");
    private static Guid _customerId2 = new Guid("44464351-C089-4D08-8352-8BE811228A72");

    private static Guid _product1 = new Guid("7AE46A2C-2237-4B9A-8D9A-9C311A2E6712");
    private static Guid _product2 = new Guid("579BCB1E-8471-4E82-A88D-B96989F82E67");
    private static Guid _product3 = new Guid("3901B778-ED4D-4781-8254-FCB07982FF5E");
    private static Guid _product4 = new Guid("E43DA995-A8A2-418B-A178-86E195D28A0E");

    public static IEnumerable<Customer> Customers =>
        new List<Customer>
        {
            Customer.Create(CustomerId.Of(_customerId1), "mehmet", "mehmet@gmail.com"),
            Customer.Create(CustomerId.Of(_customerId2), "john", "john@gmail.com"),
        };

    public static IEnumerable<Product> Products =>
        new List<Product>
        {
            Product.Create(ProductId.Of(_product1), "IPhone X", 500),
            Product.Create(ProductId.Of(_product2), "Samsung 10", 400),
            Product.Create(ProductId.Of(_product3), "Huawei Plus", 650),
            Product.Create(ProductId.Of(_product4), "Xiaomi Mi", 450)
        };

    public static IEnumerable<Order> OrdersWithOrderItems
    {
        get
        {
            var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turjey", "Istanbul", "38050");
            var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

            var payment1 = Payment.Of("mehmet", "55555555555555544", "12/25", "123", 1);
            var payment2 = Payment.Of("john", "55555555555555544", "06/30", "222", 2);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(_customerId1),
                OrderName.Of("Ord 1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);
            order1.Add(
                ProductId.Of(_product1), 2, 500);
            order1.Add(
                ProductId.Of(_product2), 1, 400);

            var order2 = Order.Create(
               OrderId.Of(Guid.NewGuid()),
               CustomerId.Of(_customerId2),
               OrderName.Of("Ord 2"),
               shippingAddress: address2,
               billingAddress: address2,
               payment2);
            order2.Add(
                ProductId.Of(_product3), 1, 650);
            order2.Add(
                ProductId.Of(_product4), 2, 450);

            return new List<Order> { order1, order2 };
        }
    }
}
