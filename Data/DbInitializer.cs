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
                    Price=Decimal.Parse("22")
                },
               
                new Book
                {
                    Title = "Enigma Otiliei",
                    Author = Author2,
                    Price =Decimal.Parse("18")},
               
                new Book
                {
                    Title = "Maytrei",
                    Author = Author3,
                    Price =Decimal.Parse("27")}
              
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
                    BirthDate=DateTime.Parse("1969 - 07 - 08")}
               
                );

                context.SaveChanges();
            }
        }
    }
}
