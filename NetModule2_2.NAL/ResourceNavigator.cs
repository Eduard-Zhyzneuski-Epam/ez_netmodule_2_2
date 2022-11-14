namespace NetModule2_2.NAL
{
    public static class ResourceNavigator
    {
        public static string CategoriesLink() => "/categories";
        public static string CategoryLink(int? id) => id is not null ? $"/category/{id}" : null;
        public static string ItemLink(int id) => $"/item/{id}";
        public static string ItemsLink(int? categoryId, int pageNumber) => categoryId is not null 
            ? $"/items/category/{categoryId}/pages/{pageNumber}" 
            : $"/items/pages/{pageNumber}";
    }
}
