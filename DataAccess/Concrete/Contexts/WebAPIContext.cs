using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Contexts
{
    public class WebAPIContext:DbContext
    {
        public WebAPIContext(DbContextOptions<WebAPIContext> options) : base(options)
        {

        }
        public WebAPIContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = "Data Source=DESKTOP-AOMM71G; Initial Catalog = WebAPIContextDb; Integrated Security =true";
            optionsBuilder.UseSqlServer(conStr);
        }
    }
}
