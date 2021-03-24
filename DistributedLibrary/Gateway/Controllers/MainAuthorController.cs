using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainAuthorController : ControllerBase
    {


        [HttpGet("{isbn}")]
        public async Task<ActionResult> Getisbn(string isbn)
        {


            DeliveryAutor(isbn);

            return Ok("creado");



        }




        private static void DeliveryAutor(string isbn)
        {


            var factory = new ConnectionFactory
            {
                HostName = "localhost",

            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("search-book", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(isbn);

                    channel.BasicPublish("", "search-book", null, body);


                    channel.QueueDeclare("Write-Autor", false, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, content) =>
                    {
                        var body = content.Body.ToArray();
                        var mmessage = Encoding.UTF8.GetString(body);

                        using (var writer = System.IO.File.CreateText(Guid.NewGuid().ToString()))
                        {
                            writer.WriteLine(mmessage);
                        }

                    };

                    channel.BasicConsume("Write-Autor", true, consumer);
                }

            }
        }
    }


}
