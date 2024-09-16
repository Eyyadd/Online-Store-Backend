namespace OnlineStore.Domain.Entities
{
    public class OrderItems:BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public int orderId {  get; set; }
        public Order? Order {  get; set; } 
        public ProductVariants? Product { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }  
        public int Quantity { get; set; }
        
    }
}
