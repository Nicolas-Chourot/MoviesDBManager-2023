using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesDBManager.Models
{
    public class DB
    {
        private static readonly DB instance = new DB();
        public static MoviesRepository Movies { get; set; }
        public static ActorsRepository Actors { get; set; }
        public static CastingsRepository Castings { get; set; }
        public DB()
        {
            Movies = new MoviesRepository();
            Actors = new ActorsRepository();
            Castings = new CastingsRepository();
        }
        public static DB Instance
        {
            get { return instance; }
        }
    }
}