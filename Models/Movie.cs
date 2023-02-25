using FileKeyReference;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesDBManager.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Titre")]
        public string Name { get; set; }
        public string PosterImageKey { get; set; } = "";

        [JsonIgnore]
        [Display(Name = "Affiche")]
        public string PosterImageData { get; set; }

        [JsonIgnore]
        public List<Actor> Actors
        {
            get
            {
                List<Casting> castings = DB.Castings.ToList().Where(c => c.MovieId == Id).ToList();
                List<Actor> actors = new List<Actor>();
                foreach (var casting in castings)
                {
                    actors.Add(DB.Actors.Get(casting.ActorId));
                }
                return actors.OrderBy(c => c.Name).ToList();
            }
        }

        [JsonIgnore]
        private static ImageFileKeyReference PostersRepository = new ImageFileKeyReference(@"/Images_Data/Movie_Posters/", @"no_poster.png");

        public string GetPosterURL(bool thumbailFormat = false)
        {
            return PostersRepository.GetURL(PosterImageKey, thumbailFormat);
        }
        public void SavePoster()
        {
            PosterImageKey = PostersRepository.Save(PosterImageData, PosterImageKey);
        }
        public void RemovePoster()
        {
            PostersRepository.Remove(PosterImageKey);
        }
    }
}