using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class BookForSaleService: IBookForSaleService
    {
      private readonly IBookForSaleRepository bookForSaleRepository;
        private readonly IUnitOfWork unitOfWork;
        public BookForSaleService(IBookForSaleRepository bookForSaleRepository, IUnitOfWork unitOfWork)
        {
            this.bookForSaleRepository = bookForSaleRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BookForSaleResponse> DeleteAsync(int id)
        {
            var existingBookForSale = await bookForSaleRepository.FindByIdAsync(id);
            if (existingBookForSale == null)
                return new BookForSaleResponse("Book for sale not found");
            if (existingBookForSale.BookOrdered != null)
                return new BookForSaleResponse("Book for sale is ordered");
            try
            {
                bookForSaleRepository.Remove(existingBookForSale);
                await unitOfWork.CompleteAsync();

                return new BookForSaleResponse(existingBookForSale);
            }
            catch (Exception ex)
            {
                return new BookForSaleResponse($"Error when deleting BookForSale: {ex.Message}");
            }
        }

        public async Task<IEnumerable<BookForSale>> ListAsync()
        {
            return await bookForSaleRepository.ListAsync();
        }

        public async Task<BookForSaleResponse> SaveAsync(BookForSale bookForSale)
        {
            try
            {
                await bookForSaleRepository.AddAsync(bookForSale);
                await unitOfWork.CompleteAsync();

                return new BookForSaleResponse(bookForSale);
            }
            catch (Exception ex)
            {
                return new BookForSaleResponse($"Error when saving the BookForSale: {ex.Message}");
            }
        }

        public async Task<BookForSaleResponse> UpdateAsync(int id, BookForSale bookForSale)
        {
            var existingBookForSale = await bookForSaleRepository.FindByIdAsync(id);
            if (existingBookForSale == null)
                return new BookForSaleResponse("Book for sale not found");
            if (existingBookForSale.BookId != bookForSale.BookId)
                return new BookForSaleResponse("The book can`t be changed in book for sale");
            if (existingBookForSale.BookOrdered != null)
                return new BookForSaleResponse("Book for sale is ordered");
            
            existingBookForSale.Price = bookForSale.Price;
            existingBookForSale.Place = bookForSale.Place;
            existingBookForSale.Actual = bookForSale.Actual;

            try
            {
                bookForSaleRepository.Update(existingBookForSale);
                await unitOfWork.CompleteAsync();

                return new BookForSaleResponse(existingBookForSale);
            }
            catch (Exception ex)
            {
                return new BookForSaleResponse($"Error when updating BookForSale: {ex.Message}");
            }
        }
    }
}