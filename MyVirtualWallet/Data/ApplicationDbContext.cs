using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyVirtualWallet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVirtualWallet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AccountDetails> AccountDetails { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<CustomUser>()
            //            .ToTable("CustomUsers")
            //            .HasOne(e => e.AccountDetails)
            //            .WithOne(e => e.CustomUser)
            //            .HasForeignKey<AccountDetails>(e => e.CustomUserID);

            modelBuilder.Entity<AccountDetails>()
                        .ToTable("AccountDetails");
            //.HasOne(e => e.User)
            //.WithOne(e => e.AccountDetails)
            //.HasForeignKey<AccountDetails>(e => e.UserID);
            modelBuilder.Entity<AccountDetails>().Property(p => p.Balance).HasDefaultValue(0M);

            modelBuilder.Entity<AccountTransaction>().ToTable("AccountTransactions");
                        //.HasOne<AccountDetails>(e => e.AccountDetails)
                        //.WithMany(e => e.AccountTransactions);
        }
    }
}
