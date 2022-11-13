namespace NetModule2_2.BAL
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}