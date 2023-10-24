using Boamba_Emilia_Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Boamba_Emilia_Lab2.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Books.Any())
                {
                    return; // BD a fost creata anterior
                }

                Author Author1 = new Author
                {
                    FirstName = "Mihail",
                    LastName = "Sadoveanu"
                };

                Author Author2 = new Author
                {
                    FirstName = "George",
                    LastName = "Calinescu"
                };

                Author Author3 = new Author
                {
                    FirstName = "Mircea",
                    LastName = "Eliade"
                };


                context.Authors.AddRange(Author1, Author2, Author3);

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Baltagul",
                        Author = Author1,
                        Price = Decimal.Parse("22")
                    },
                    new Book
                    {
                        Title = "Enigma Otiliei",
                        Author = Author2,
                        Price = Decimal.Parse("18")
                    },
                    new Book
                    {
                        Title = "Maytrei",
                        Author = Author3,
                        Price = Decimal.Parse("27")
                    },
                    new Book
                    {
                        Title = "Fata de hartie",
                        Author = Author3,
                        Price = Decimal.Parse("37")
                    },
                    new Book
                    {
                        Title = "Panza de paianjen",
                        Author = Author2,
                        Price = Decimal.Parse("37")
                    }, 
                    new Book
                    {
                        Title = "De veghe in lanul de secara",
                        Author = Author1,
                        Price = Decimal.Parse("37")
                    }
                );

                context.Customers.AddRange(
                    new Customer
                    {
                        Name = "Popescu Marcela",
                        Adress = "Str. Plopilor, nr. 24",
                        BirthDate = DateTime.Parse("1979-09-01")
                    },
                    new Customer
                    {
                        Name = "Mihailescu Cornel",
                        Adress = "Str. Bucuresti, nr. 45, ap. 2",
                        BirthDate = DateTime.Parse("1969 - 07 - 08")
                    }
                );

                context.SaveChanges();

                var books = context.Books;
                var customers = context.Customers;

                var orders = new Order[]
                {
                     new Order
                     {
                         BookID = books.Single(b => b.Title == "Baltagul").ID,
                         CustomerID = customers.Single(c => c.Name == "Popescu Marcela").CustomerID,
                         OrderDate = DateTime.Parse("2021-02-25")
                     },
                     new Order{
                         BookID = books.Single(b => b.Title == "Maytrei").ID,
                         CustomerID = customers.Single(c => c.Name == "Mihailescu Cornel").CustomerID,
                         OrderDate = DateTime.Parse("2021-09-28")
                     },
                     new Order{
                         BookID = books.Single(b => b.Title == "Baltagul").ID,
                         CustomerID = customers.Single(c => c.Name == "Popescu Marcela").CustomerID,
                         OrderDate = DateTime.Parse("2021-10-28")
                     },
                     new Order{
                         BookID = books.Single(b => b.Title == "Enigma Otiliei").ID,
                         CustomerID = customers.Single(c => c.Name == "Mihailescu Cornel").CustomerID,
                         OrderDate = DateTime.Parse("2021-09-28")
                     },
                     new Order{
                         BookID = books.Single(b => b.Title == "Fata de hartie").ID,
                         CustomerID = customers.Single(c => c.Name == "Mihailescu Cornel").CustomerID,
                         OrderDate=DateTime.Parse("2021-09-28")
                     },
                     new Order{
                         BookID = books.Single(b => b.Title == "De veghe in lanul de secara").ID,
                         CustomerID = customers.Single(c => c.Name == "Mihailescu Cornel").CustomerID,
                         OrderDate = DateTime.Parse("2021-10-28")
                     },
                 };

                foreach (Order e in orders)
                {
                    context.Orders.Add(e);
                }

                context.SaveChanges();

                var publishers = new Models.Publisher[]
                {

                     new Models.Publisher{PublisherName="Humanitas",Adress="Str. Aviatorilor, nr. 40, Bucuresti"},
                     new Models.Publisher{PublisherName="Nemira",Adress="Str. Plopilor, nr. 35, Ploiesti"},
                     new Models.Publisher{PublisherName="Paralela 45",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
                };
                foreach (Models.Publisher p in publishers)
                {
                    context.Publishers.Add(p);
                }

                context.SaveChanges();

                var newPublishers = context.Publishers;

                var publishedbooks = new PublishedBook[]
                {
                     new PublishedBook {
                         BookID = books.Single(c => c.Title == "Maytrei" ).ID,
                         PublisherID = newPublishers.Single(i => i.PublisherName == "Humanitas").ID
                    },
                     new PublishedBook {
                         BookID = books.Single(c => c.Title == "Enigma Otiliei" ).ID,
                        PublisherID = newPublishers.Single(i => i.PublisherName == "Humanitas").ID
                     },
                     new PublishedBook {
                         BookID = books.Single(c => c.Title == "Baltagul" ).ID,
                        PublisherID = newPublishers.Single(i => i.PublisherName == "Nemira").ID
                     },
                     new PublishedBook {
                         BookID = books.Single(c => c.Title == "Fata de hartie" ).ID,
                         PublisherID = newPublishers.Single(i => i.PublisherName == "Paralela 45").ID
                     },
                     new PublishedBook {
                        BookID = books.Single(c => c.Title == "Panza de paianjen" ).ID,
                        PublisherID = newPublishers.Single(i => i.PublisherName == "Paralela 45").ID
                     },
                     new PublishedBook {
                         BookID = books.Single(c => c.Title == "De veghe in lanul de secara" ).ID,
                         PublisherID = newPublishers.Single(i => i.PublisherName == "Paralela 45").ID
                     }
                };
                foreach (PublishedBook pb in publishedbooks)
                {
                    context.PublishedBooks.Add(pb);
                }
                context.SaveChanges();
            }
        }
    }
}
