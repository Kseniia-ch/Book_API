using BookAPI.Domain.Models;

namespace BookAPI.Domain.Services.Communication
{
    public class PublisherResponse: BaseResponse
    {
        public Publisher Publisher {get; private set;}
        public PublisherResponse(bool success, string message, Publisher publisher) : base(success, message)
        {
            Publisher = publisher;
        }

        public PublisherResponse(Publisher publisher): this(true, string.Empty, publisher){}

        public PublisherResponse(string message): this(false, message, null) {}
       
    }
}