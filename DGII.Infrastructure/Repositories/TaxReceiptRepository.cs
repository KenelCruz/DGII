using System;
using System.Collections.Generic;
using System.Text;
using DGII.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DGII.Domain.Interfaces;
using DGII.Infrastructure.Data;

namespace DGII.Infrastructure.Repositories
{
    public class TaxReceiptRepository: ITaxReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaxReceipt>> GetAllAsync()
        {
            return await _context.TaxReceipt.ToListAsync();
        }

        public async Task<IEnumerable<TaxReceipt>> GetTaxpayerRncAsync(string rncCedula)
        {
            return await _context.TaxReceipt.Where(r => r.RncCedula == rncCedula).ToListAsync();
        }

        public async Task AddAsync(TaxReceipt receipt)
        {
            await _context.TaxReceipt.AddAsync(receipt);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
