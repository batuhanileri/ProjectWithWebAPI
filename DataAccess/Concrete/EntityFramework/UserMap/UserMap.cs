using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.UserMap
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", @"dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.FirstName)
              .HasColumnName("FirstName")
              .HasMaxLength(50)
              .IsRequired();

            builder.Property(x => x.LastName)
              .HasColumnName("LastName")
              .HasMaxLength(50)
              .IsRequired();


            builder.Property(x => x.Password)
              .HasColumnName("Password")
              .HasMaxLength(20)
              .IsRequired();

            builder.Property(x => x.Gender)
              .HasColumnName("Gender")
              .IsRequired();


            builder.Property(x => x.DateOfBirth)
              .HasColumnName("DateOfBirth")
              .IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);

            builder.HasData(new User
            {
                Id = 1,
                UserName = "Batuhan",
                FirstName = "Batuhan",
                LastName = "İleri",
                Password = "12345",
                Gender = true,
                DateOfBirth = Convert.ToDateTime("05-08-1999"),
                CreatedDate=DateTime.Now,
                Address ="Bursa",
                CreatedUserId=1,
                Email="batu@gmail.com",

            });

        }
    }
}
