using System;
using BookAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Persistence.Context

{
    public class BookDbContext : DbContext
    {
        
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        
       
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<BookForSale> BooksForSale { get; set; }
        public DbSet<BookOrdered> BooksOrdered { get; set; }
        
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Users

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(x => x.Firstname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Lastname).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(x => x.Login).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasAlternateKey(x => x.Login);
            builder.Entity<User>().Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasMany(x => x.UserRoles).WithOne(x => x.User);

            #endregion

            #region Roles

            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<Role>().HasKey(x => x.Id);
            builder.Entity<Role>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Role>().Property(x => x.Name).IsRequired();
            builder.Entity<Role>().HasMany(x => x.UserRoles).WithOne(x => x.Role);
                      
            #endregion

            #region UserRoles

            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserRole>().HasKey(x => new {x.RoleId, x.UserId});

            #endregion

            builder.Entity<Role>().HasData
            (
                new Role {Id = 1, Name = "Admin"},
                new Role {Id = 2, Name = "User"},
                new Role {Id = 3, Name = "Saler"}
            );


            builder.Entity<User>().HasData
            (
                new User
                {
                    Id = 1,
                    Firstname = "Дмитрий",
                    Lastname = "Преподователь",
                    Login = "Admin",
                    Password = "Admin"
                },
                new User
                {
                    Id = 2,
                    Firstname = "Дмитрий",
                    Lastname = "Пользователь",
                    Login = "User",
                    Password = "User"
                },
                new User
                {
                    Id = 3,
                    Firstname = "Дмитрий",
                    Lastname = "Продавец",
                    Login = "Saler",
                    Password = "Saler"
                }
            );

            builder.Entity<UserRole>().HasData
            (
                new UserRole {UserId = 1, RoleId = 1},
                new UserRole {UserId = 2, RoleId = 2},
                new UserRole {UserId = 3, RoleId = 3}
            );

            builder.Entity<Publisher>().ToTable("Publishers");
            builder.Entity<Publisher>().HasKey(x => x.Id);
            builder.Entity<Publisher>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Publisher>().Property(x => x.Name).IsRequired();
            builder.Entity<Publisher>().Property(x => x.City).IsRequired();

            builder.Entity<Publisher>().HasData
            (
                 new Publisher { Id = 1, Name = "Nebo Booklab Publishing", City="Kyiv" },
                 new Publisher { Id = 2, Name = "New Time", City="Kharkiv"}
            );

            builder.Entity<Genre>().ToTable("Genres");
            builder.Entity<Genre>().HasKey(x => x.Id);
            builder.Entity<Genre>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Genre>().Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Entity<Genre>().HasData
            (
                 new Genre { Id = 1, Name = "History" },
                 new Genre { Id = 2, Name = "Fantasy"}
            );

            builder.Entity<Author>().ToTable("Authors");
            builder.Entity<Author>().HasKey(x => x.Id);
            builder.Entity<Author>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Author>().Property(x => x.FirstName).IsRequired();
            builder.Entity<Author>().Property(x => x.LastName).IsRequired();


            builder.Entity<Author>().HasData
            (
                 new Author { Id = 1, FirstName = "Jurgen", LastName="Muller"},
                 new Author { Id = 2, FirstName = "Andre", LastName="Talley"}
            );

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Book>().HasKey(x => x.Id);
            builder.Entity<Book>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Book>().Property(x => x.Title).IsRequired();
            builder.Entity<Book>()
                            .HasOne(p => p.Publisher)
                            .WithMany(t => t.Books)
                            .HasForeignKey(p => p.PublisherId);
            builder.Entity<Book>()
                            .HasOne(p => p.Genre)
                            .WithMany(t => t.Books)
                            .HasForeignKey(p => p.GenreId);


            builder.Entity<Book>().HasData
            (
                 new Book { Id = 1, Title = "Movies of the 50s", Year=	2018, PublisherId=1, Pages=760, GenreId=1},
                 new Book { Id = 2, Title = "Oscar De La Renta", Year=	2015, PublisherId=1, Pages=176}
            );

            builder.Entity<AuthorBook>().ToTable("AuthorBooks");
            builder.Entity<AuthorBook>().HasKey(x => new {x.AuthorId, x.BookId});

            builder.Entity<AuthorBook>().HasData
            (
                new AuthorBook {AuthorId = 1, BookId = 1},
                new AuthorBook {AuthorId = 2, BookId = 2}
            );

            builder.Entity<BookForSale>().ToTable("BooksForSale");
            builder.Entity<BookForSale>().HasKey(x => x.Id);
            builder.Entity<BookForSale>().Property(x => x.BookId).IsRequired();
            builder.Entity<BookForSale>().Property(x => x.UserId).IsRequired();


            builder.Entity<BookForSale>().HasData
            (
                new BookForSale {Id = 1, BookId = 1, UserId = 3, Price = 1000, Actual = true},
                new BookForSale {Id = 2, BookId = 1, UserId = 3 },
                new BookForSale {Id = 3, BookId = 2, UserId = 3, Price = 2000 }
            );

            builder.Entity<Book>()
                            .HasMany(p => p.BooksForSale)
                            .WithOne(t => t.Book)
                            .HasForeignKey(p => p.BookId);
            builder.Entity<User>()
                            .HasMany(p => p.BooksForSale)
                            .WithOne(t => t.User)
                            .HasForeignKey(p => p.UserId);


            builder.Entity<BookOrdered>().ToTable("BooksOrdered");
            builder.Entity<BookOrdered>().HasKey(x => x.Id);
            builder.Entity<BookOrdered>().Property(x => x.BookForSaleId).IsRequired();
            builder.Entity<BookOrdered>().Property(x => x.UserId).IsRequired();


            builder.Entity<BookOrdered>().HasData
            (
                new BookOrdered {Id = 1, BookForSaleId = 1, UserId = 2, Sold = true},
                new BookOrdered {Id = 2, BookForSaleId = 2, UserId = 2 },
                new BookOrdered {Id = 3, BookForSaleId = 3, UserId = 2 }
            );

            builder.Entity<BookForSale>()
                            .HasOne(p => p.BookOrdered)
                            .WithOne(t => t.BookForSale)
                            .HasForeignKey<BookOrdered>(p => p.BookForSaleId);
            builder.Entity<User>()
                            .HasMany(p => p.BooksOrdered)
                            .WithOne(t => t.User)
                            .HasForeignKey(p => p.UserId);
        }
    }
}