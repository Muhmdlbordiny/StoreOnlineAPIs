namespace StoreCore.G02.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AppUserId { get; set; } // FK one to one
        public Appuser AppUser { get; set; }

    }
}