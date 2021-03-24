using DistributedLibrary.Books.Service.Models;
using DistributedLibrary.Books.Service.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DistributedLibrary.Books.Service
{
    public class Worker : BackgroundService
    {


   

        private const string FileName = @"books.json";


        private readonly ILogger<Worker> _logger;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;

        public Worker(ILogger<Worker> logger)
        {

            _logger = logger;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
               
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("search-book", false, false, false, null);
            _consumer = new EventingBasicConsumer(_channel);
        }



        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(60000, cancellationToken);
            _consumer.Received += async (model, content) =>
            {
                var body = content.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var result = await SerchAutoresAsync(message);

                WriteAutores(result.ToString());
            };

            _channel.BasicConsume("search-book", true, _consumer);
        }

        public async Task<string> SerchAutoresAsync(string id)
        {



            var get_libroactual = JsonConvert.DeserializeObject<IEnumerable<Libros>>(File.ReadAllText(FileName));
            Libros book = get_libroactual.Where(x => x.Isbn == id).FirstOrDefault();

            string id_autor = book.AuthorId.ToString();

           
            string autor;
        


            string json = JsonConvert.SerializeObject(book, Formatting.Indented);
            string autres_libros ;




            using (var client = new HttpClient())
            {
                autor = await client.GetStringAsync($"http://localhost:54376/Autores/{id}");
             
            }


            if (autor == null)
            {
                using (var client = new HttpClient())
                {
                    autor = await client.GetStringAsync($"http://localhost:11111/weatherforecast/{id}");

                }
            }



            autres_libros = $"{json}{autor}";

           

           



            return autres_libros.ToString();






        }
    

        private void WriteAutores(string autor_book)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("Write-Autor", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(autor_book);

                    channel.BasicPublish("", "Write-Autor", null, body);
                }
            }
        }




        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
