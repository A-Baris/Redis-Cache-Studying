using Jewellery.Common.IpFinder;
using Jewellery.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.Dal.JewelleryContext
{
    public class ProjectContext:DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get;set;}



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("server=**\\SQLEXPRESS;database=JewelleryDB;uid=sa;pwd=3420;TrustServerCertificate=True");
        //    }
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifierEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach (var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseEntity;
                    if (entityRepository != null)
                    {

                        if (item.State == EntityState.Added)
                        {
                            entityRepository.CreatedDate = DateTime.Now;
                            entityRepository.CreatedByComputer = Environment.MachineName;
                            entityRepository.CreatedByIp = IpFinder.GetIpAddress();


                        }
                        if (item.State == EntityState.Modified)
                        {
                            entityRepository.UpdatedDate = DateTime.Now;
                            entityRepository.UpdatedByComputer = Environment.MachineName;
                            entityRepository.UpdatedByIp = IpFinder.GetIpAddress();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken =default)
        {
            var modifierEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach (var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseEntity;
                    if (entityRepository != null)
                    {

                  
                        if (item.State == EntityState.Modified)
                        {
                            entityRepository.UpdatedDate = DateTime.Now;
                            entityRepository.UpdatedByComputer = Environment.MachineName;
                            entityRepository.UpdatedByIp = IpFinder.GetIpAddress();

                        }
                        if (item.State == EntityState.Added)
                        {
                            entityRepository.CreatedDate = DateTime.Now;
                            entityRepository.CreatedByComputer = Environment.MachineName;
                            entityRepository.CreatedByIp = IpFinder.GetIpAddress();


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return base.SaveChangesAsync();
        }
       
    }
}
