using EFGetStarted.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP01SWII06
{
    public class teste
    {
        public void testexecutar()
        {
            using (var db = new BookContext())
            {
                //Create 

                Console.WriteLine("Insert");
                db.Add(new Book("Guia do mochileiro das galáxias", 19.00));
                db.Add(new Book("Scott Pilgrim vs the world", 35.00));
                db.Add(new Book("Umbrella Academy", 60.00));
                db.SaveChanges();

                //Read
                Console.WriteLine("Query");
                var books = db.Books
                    .Include(book => book.authors)
                    .ToList();

                //Update 
                Console.WriteLine("Updating");
                books[0].authors.Add(
                    new Author
                    {
                        Name = "Douglas Adams",
                        gender = 'M',
                        email = "douglinha@hotmail.com"
                    });
                books[0].authors.Add(
                    new Author
                    {
                        Name = "Douglas Adams",
                        gender = 'M',
                        email = "douglinha@hotmail.com"
                    });
                books[1].authors.Add(
                    new Author
                    {
                        Name = "Bryan Lee O'Malley",
                        gender = 'M',
                        email = "ilovelisa@hotmail.com"
                    });
                books[1].authors.Add(
                    new Author
                    {
                        Name = "Gerard Way",
                        gender = 'M',
                        email = "mychemicalromance@hotmail.com"
                    });

                db.SaveChanges();
                
                var book = db.Books
                    .Include(book => book.authors)
                    .First();

                Console.WriteLine("Pegando o primeiro autor: " + book.authors[0].Name);
                //utilizando o toString
                Console.WriteLine("metodo ToString: " + book.ToString());
                //utilizando o getAuthorName
                Console.WriteLine("metodo getAuthorName: " + book.getAuthorNames());

            }
        }
    }
}
