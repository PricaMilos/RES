using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database
{
    public class CBDataContext : DbContext
    {
        public DbSet<Resurs> Resurs { get; set; }
        public DbSet<Tip> Tip { get; set; }
        public DbSet<TipVeze> TipVeze { get; set; }
        public DbSet<Veza> Veza { get; set; }

        public CBDataContext() : base("CBDataContext")
        {

        }
    }
}
