using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedLibrary.Books.Service.Models
{
    public class Libros
    {

        public string Isbn { get; set; }
        public string Title { get; set; }


        public string Subtitle { get; set; }


        public string Author { get; set; }


        public string Published { get; set; }


        public string Publisher { get; set; }


        public long Pages { get; set; }

        public string Description { get; set; }


        public Uri Website { get; set; }


        public long AuthorId { get; set; }


      
    }
    public class Autores
    {


        public int Id { get; set; }
        public string First_name { get; set; }

        public string Last_name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string Ip_address { get; set; }



    }

}
