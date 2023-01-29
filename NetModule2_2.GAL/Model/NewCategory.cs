namespace NetModule2_2.GAL.Model
{
    public class NewCategory
    {
        [GraphQLNonNullType]
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
