using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;

namespace BookAPI.Services
{
    public class BookOrderedService: IBookOrderedService
    {
      private readonly IBookOrderedRepository bookOrderedRepository;
      private readonly IBookForSaleRepository bookForSaleRepository;
        private readonly IUnitOfWork unitOfWork;
        public BookOrderedService(IBookOrderedRepository bookOrderedRepository, IBookForSaleRepository bookForSaleRepository, IUnitOfWork unitOfWork)
        {
            this.bookOrderedRepository = bookOrderedRepository;
            this.bookForSaleRepository = bookForSaleRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BookOrderedResponse> DeleteAsync(int id)
        {
            var existingBookOrdered = await bookOrderedRepository.FindByIdAsync(id);
            if (existingBookOrdered == null)
                return new BookOrderedResponse("Book ordered not found");
            if (existingBookOrdered.Sold)
                return new BookOrderedResponse("Book was sold");
            try
            {
                bookOrderedRepository.Remove(existingBookOrdered);
                await unitOfWork.CompleteAsync();

                return new BookOrderedResponse(existingBookOrdered);
            }
            catch (Exception ex)
            {
                return new BookOrderedResponse($"Error when deleting BookOrdered: {ex.Message}");
            }
        }

        public async Task<IEnumerable<BookOrdered>> ListAsync()
        {
            return await bookOrderedRepository.ListAsync();
        }

        public async Task<BookOrderedResponse> SaveAsync(BookOrdered bookOrdered)
        {
            var existingBookForSale = await bookForSaleRepository.FindByIdAsync(bookOrdered.BookForSaleId);

            if (existingBookForSale == null)
                return new BookOrderedResponse("Book for sale not found");
            if (existingBookForSale.BookOrdered != null)
                return new BookOrderedResponse("Book for sale was sold or reserved");

            try
            {
                await bookOrderedRepository.AddAsync(bookOrdered);
                await unitOfWork.CompleteAsync();

                return new BookOrderedResponse(bookOrdered);
            }
            catch (Exception ex)
            {
                return new BookOrderedResponse($"Error when saving the BookOrdered: {ex.Message}");
            }
        }

        public async Task<BookOrderedResponse> UpdateAsync(int id, BookOrdered bookOrdered)
        {
            var existingBookOrdered = await bookOrderedRepository.FindByIdAsync(id);
            if (existingBookOrdered == null)
                return new BookOrderedResponse("Book ordered not found");
            if (existingBookOrdered.BookForSaleId != bookOrdered.BookForSaleId)
                return new BookOrderedResponse("The book for sale can`t be changed in book ordered");
            if (existingBookOrdered.BookForSale.BookOrdered != null)
                return new BookOrderedResponse("Book was sold or reserved");
            
            existingBookOrdered.Sold = bookOrdered.Sold;

            try
            {
                bookOrderedRepository.Update(existingBookOrdered);
                await unitOfWork.CompleteAsync();

                return new BookOrderedResponse(existingBookOrdered);
            }
            catch (Exception ex)
            {
                return new BookOrderedResponse($"Error when updating BookOrdered: {ex.Message}");
            }
        }
    }
}