namespace BookAPI.Resources
{
    public class UserResource
    {
        public int Id {get;set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        public string[] Role { get; set; }
        public BookForSaleResource[] BooksForSale {get;set;}
    }
}