using DGII.Domain.Entities;
using DGII.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Taxpayer> Taxpayers { get; set; }
        public DbSet<TaxReceipt> TaxReceipt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Taxpayer>()
                .HasKey(t => t.RncCedula);

            modelBuilder.Entity<TaxReceipt>(entity =>
            {
                entity.HasKey(r => r.NCF);

                entity.HasOne(r => r.Taxpayer)        
                      .WithMany(t => t.TaxReceipts)   
                      .HasForeignKey(r => r.RncCedula) 
                      .IsRequired();                  
            });

            //Dummy_Data
            modelBuilder.Entity<Taxpayer>().HasData(
                new Taxpayer
                {
                    RncCedula = "98754321012",
                    Name = "JUAN PEREZ",
                    Type = TaxpayerType.PersonaFisica,
                    Status = TaxpayerStatus.Active,
                    CreationDate = new DateTime(2026, 2, 13, 0, 0, 0, DateTimeKind.Utc)
                },
                new Taxpayer
                {
                    RncCedula = "123456789",
                    Name = "FARMACIA TU SALUD",
                    Type = TaxpayerType.PersonaJuridica,
                    Status = TaxpayerStatus.Inactive,
                    CreationDate = new DateTime(2026, 2, 13, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            
            modelBuilder.Entity<TaxReceipt>().HasData(
                new TaxReceipt
                {
                    NCF = "E310000000001",
                    RncCedula = "98754321012",
                    Amount = 200.00m,
                    Itbis18 = 36.00m
                },
                new TaxReceipt
                {
                    NCF = "E310000000002",
                    RncCedula = "98754321012",
                    Amount = 1000.00m,
                    Itbis18 = 180.00m
                }
            );


            base.OnModelCreating(modelBuilder);

           
        }

       
    }
}
