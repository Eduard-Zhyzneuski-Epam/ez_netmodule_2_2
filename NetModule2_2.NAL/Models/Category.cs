namespace NetModule2_2.NAL.Models
{
    public class Category
    {
        public string Link { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ParentCategoryLink { get; set; }
        public string CategoryItemsLink { get; set; }
    }
}
