namespace NetModule2_2.GAL.Model
{
    public class Category
    {
        [GraphQLType(typeof(IdType))]
        public int Id { get; set; }
        [GraphQLNonNullType]
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
