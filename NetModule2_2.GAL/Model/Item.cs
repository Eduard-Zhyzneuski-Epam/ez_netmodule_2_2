namespace NetModule2_2.GAL.Model
{
    public class Item
    {
        [GraphQLType(typeof(IdType))]
        public int Id { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
