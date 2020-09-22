using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TP01SWII06;


namespace TP01SWII06
{
    class Startup
    {


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("ShowBookById/{bookId}", ShowBookById); //ok
            builder.MapRoute("ShowNameOfBookById/{bookId}", ShowNameOfBookById); //ok
            builder.MapRoute("BookAuthorsById/{bookId}", ShowBookAuthorsByIdBook); //ok
            builder.MapRoute("Book/ShowLivro/{bookId}", ShowBookHtml); //ok

            //criar rota home
            builder.MapRoute("", Home); 

            var rotas = builder.Build();
            app.UseRouter(rotas);
        }

        public Task  Home (HttpContext context)
        {
            return context.Response.WriteAsync("Rotas: \nShowBookById/{id} Mostra a descrição completa do livro. \nShowNameOfBookById{id}  Mostra o nome do livro \nBookAuthorsById/{id} Mostra os autores com o Id passado \nBook/ShowLivro/{id} Mostra a página HTML do livro");
        }


        public Task ShowBookAuthorsByIdBook(HttpContext context)
        {
            using (var db = new BookContext())
            {
                var books = db.Books
                    .Include(book => book.authors)
                    .ToList();

                int bookId = Convert.ToInt32(context.GetRouteValue("bookId").ToString()) - 1;
                var book = books[bookId];

                return context.Response.WriteAsync(book.getAuthorNames());
            }
        }

        public Task ShowNameOfBookById(HttpContext context)
        {
            using ( var db = new BookContext())
            {
                var books = db.Books
                    .Include(book => book.authors)
                    .ToList();

                int bookId = Convert.ToInt32(context.GetRouteValue("bookId").ToString()) - 1;
                var book = books[bookId];

                return context.Response.WriteAsync(book.name);
            }
        }


        public Task ShowBookById(HttpContext context)
        {
            using ( var db = new BookContext())
            {
                var books = db.Books
                    .Include(book => book.authors)
                    .ToList();

                int bookId = Convert.ToInt32(context.GetRouteValue("bookId").ToString()) - 1;
                var book = books[bookId];

                return context.Response.WriteAsync(book.ToString());
            } 
        }

        public Task ShowBookHtml(HttpContext context)
        {
            using (var db = new BookContext())
            {
                var books = db.Books
                    .Include(book => book.authors)
                    .ToList();

                int bookId = Convert.ToInt32(context.GetRouteValue("bookId").ToString()) - 1;
                var book = books[bookId];

                var conteudoArquivo = CarregaArquivoHTML("ShowBook");

                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<h4>{book.ToString()}<h4>");

                return context.Response.WriteAsync(conteudoArquivo);
            }
        }

        private string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"View/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
