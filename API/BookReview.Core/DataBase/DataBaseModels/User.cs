﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookReview.DataBase.DataBaseModels
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Review> Reviews { get; set; }
        public List<MyShelf> MyShelfs { get; set; }
    }
}