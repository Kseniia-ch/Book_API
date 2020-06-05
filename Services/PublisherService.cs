using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;
        private readonly IUnitOfWork unitOfWork;
        public PublisherService(IPublisherRepository publisherRepository, IUnitOfWork unitOfWork)
        {
            this.publisherRepository = publisherRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PublisherResponse> DeleteAsync(int id)
        {
            var existingPublisher = await publisherRepository.FindByIdAsync(id);
            if (existingPublisher == null)
                return new PublisherResponse("Publisher not found");
             if (existingPublisher.Books.Count > 0)
                return new PublisherResponse("Publisher is used in books");
            try
            {
                publisherRepository.Remove(existingPublisher);
                await unitOfWork.CompleteAsync();

                return new PublisherResponse(existingPublisher);
            }
            catch (Exception ex)
            {
                return new PublisherResponse($"Error when deleting Publisher: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Publisher>> ListAsync()
        {
            return await publisherRepository.ListAsync();
        }

        public async Task<PublisherResponse> SaveAsync(Publisher publisher)
        {
            try
            {
                await publisherRepository.AddAsync(publisher);
                await unitOfWork.CompleteAsync();

                return new PublisherResponse(publisher);
            }
            catch (Exception ex)
            {
                return new PublisherResponse($"Error when saving the Publisher: {ex.Message}");
            }
        }

        public async Task<PublisherResponse> UpdateAsync(int id, Publisher publisher)
        {
            var existingPublisher = await publisherRepository.FindByIdAsync(id);
            if (existingPublisher == null)
                return new PublisherResponse("Publisher not found");
            
            existingPublisher.Name = publisher.Name;
            existingPublisher.City = publisher.City;

            try
            {
                publisherRepository.Update(existingPublisher);
                await unitOfWork.CompleteAsync();

                return new PublisherResponse(existingPublisher);
            }
            catch (Exception ex)
            {
                return new PublisherResponse($"Error when updating Publisher: {ex.Message}");
            }
        }
    }
}