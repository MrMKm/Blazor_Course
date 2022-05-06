using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Model
{
    public class Movie
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Poster { get; set; }

        public string TitleFit
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                    return null;

                if (Title.Length > 70)
                    return Title.Substring(0, 57) + "...";

                else
                    return Title;
            }
        }

        public ICollection<Actor> Actors { get; set; }
        public ICollection<Gender> Genders { get; set; }

        public Movie(string Title, DateTime ReleaseDate, string Poster)
        {
            this.Title = Title;
            this.ReleaseDate = ReleaseDate;
            this.Poster = Poster;
        }
    }
}
