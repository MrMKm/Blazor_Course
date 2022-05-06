using Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Gender
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }

        public Gender(string name)
        {
            this.Name = name;
            this.Name = name;
        }
    }
}
