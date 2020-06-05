using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookAPI.Domain.Models;
using BookAPI.Domain.Repositories;
using BookAPI.Domain.Services;
using BookAPI.Domain.Services.Communication;
using BookAPI.Resources;

namespace BookAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IPublisherRepository publisherRepository;
        private readonly IUnitOfWork unitOfWork;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, IPublisherRepository publisherRepository, IUnitOfWork unitOfWork)
        {
            this.bookRepository      = bookRepository;
            this.authorRepository    = authorRepository;
            this.genreRepository     = genreRepository;
            this.publisherRepository = publisherRepository;
            this.unitOfWork          = unitOfWork;
        }

        public async Task<BookResponse> DeleteAsync(int id)
        {
            var existingBook = await bookRepository.FindByIdAsync(id);
            if (existingBook == null)
                return new BookResponse("Book not found");
            if (existingBook.BooksForSale.Count > 0)
                return new BookResponse("Book on sale");
            try
            {
                bookRepository.Remove(existingBook);
                await unitOfWork.CompleteAsync();

                return new BookResponse(existingBook);
            }
            catch (Exception ex)
            {
                return new BookResponse($"Error when deleting Book: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await bookRepository.ListAsync();
        }

        public async Task<BookResponse> SaveAsync(Book book)
        {
            try
            {
                if(!string.IsNullOrEmpty(book.Genre.Name))
                {
                    var genre = await genreRepository.FindByKeyFieldsAsync(book.Genre.Name);
                    if (genre == null)
                    {
                        await genreRepository.AddAsync(book.Genre);
                        book.GenreId = book.Genre.Id;
                    }
                    else
                    {
                        book.GenreId = genre.Id;
                    }
                }

                if(!string.IsNullOrEmpty(book.Publisher.Name))
                {
                    var publisher = await publisherRepository.FindByKeyFieldsAsync(book.Publisher.Name, book.Publisher.City);
                    if (publisher == null)
                    {
                        await publisherRepository.AddAsync(book.Publisher);
                        book.PublisherId = book.Publisher.Id;
                    }
                    else
                    {
                        book.PublisherId = publisher.Id;
                    }
                }

                foreach(var author in book.Authors)
                {
                    var authorInBase = await authorRepository.FindByKeyFieldsAsync(author.Author.LastName, author.Author.FirstName, author.Author.Patronymic);
                    if (authorInBase == null)
                    {
                        await authorRepository.AddAsync(author.Author);
                        author.AuthorId = author.Author.Id;
                    }
                    else
                    {
                        author.AuthorId = authorInBase.Id;
                    }
                }

                await bookRepository.AddAsync(book);

                await unitOfWork.CompleteAsync();

                return new BookResponse(book);
            }
            catch (Exception ex)
            {
                return new BookResponse($"Error when saving the book: {ex.Message}");
            }
        }

        public async Task<BookResponse> UpdateAsync(int id, Book book)
        {
            
            var existingBook = await bookRepository.FindByIdAsync(id);
            if (existingBook == null)
                return new BookResponse("Book not found");
            if (existingBook.BooksForSale.Count > 0)
                return new BookResponse("Book on sale");
            
            existingBook.Title = book.Title;
            existingBook.Pages = book.Pages;
            existingBook.Year = book.Year;

            try
            {
                if(!string.IsNullOrEmpty(book.Genre.Name))
                {
                    var genre = await genreRepository.FindByKeyFieldsAsync(book.Genre.Name);
                    if (genre == null)
                    {
                        await genreRepository.AddAsync(book.Genre);
                        existingBook.GenreId = book.Genre.Id;
                    }
                    else
                    {
                        existingBook.GenreId = genre.Id;
                    }
                }

                if(!string.IsNullOrEmpty(book.Publisher.Name))
                {
                    var publisher = await publisherRepository.FindByKeyFieldsAsync(book.Publisher.Name, book.Publisher.City);
                    if (publisher == null)
                    {
                        await publisherRepository.AddAsync(book.Publisher);
                        existingBook.PublisherId = book.Publisher.Id;
                    }
                    else
                    {
                        existingBook.PublisherId = publisher.Id;
                    }
                }

                existingBook.Authors.Clear();

                foreach(var author in book.Authors)
                {
                    var authorInBase = await authorRepository.FindByKeyFieldsAsync(author.Author.LastName, author.Author.FirstName, author.Author.Patronymic);
                    if (authorInBase == null)
                    {
                        await authorRepository.AddAsync(author.Author);
                        existingBook.Authors.Add(new AuthorBook(){AuthorId = author.Author.Id, BookId = id});
                    }
                    else
                    {
                        existingBook.Authors.Add(new AuthorBook(){AuthorId = authorInBase.Id, BookId = id});
                    }
                }

                bookRepository.Update(existingBook);
                await unitOfWork.CompleteAsync();

                return new BookResponse(existingBook);
            }
            catch (Exception ex)
            {
                return new BookResponse($"Error when updating Book: {ex.Message}");
            }
        }
    }
}