namespace NetModule2_2.GAL.Model
{
    public class NewItem
    {
        [GraphQLNonNullType]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
