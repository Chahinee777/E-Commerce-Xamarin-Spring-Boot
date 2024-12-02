   public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; }
        public long CategoryId { get; set; } 

        // Property to get the image source for Xamarin
        public string ImageSource => $"resource://{ImageName}";
    }