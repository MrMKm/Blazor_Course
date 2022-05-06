using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Actor
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Photo { get; set; }

        public string Bio { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public Actor(string FirstName, string LastName, DateTime BirthDate, string Photo, string Bio)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BirthDate = BirthDate;
            this.Photo = Photo;
            this.Bio = Bio;
        }
    }
}
