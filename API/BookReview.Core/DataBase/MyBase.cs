
using BookReview.DataBase.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace BookReview.DataBase
{
    public class MyBase : DbContext
    {

        public MyBase(DbContextOptions<MyBase> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<MyShelf> MyShelf { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Books> Books { get; set; }
    }
    }
