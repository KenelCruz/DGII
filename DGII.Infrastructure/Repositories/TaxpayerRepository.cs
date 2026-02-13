using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DGII.Domain.Entities;
using DGII.Domain.Interfaces;
using DGII.Infrastructure.Data;

namespace DGII.Infrastructure.Repositories
{
    public class TaxpayerRepository: ITaxpayerRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxpayerRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Taxpayer>> GetAllAsync() 
        {
            return await _context.Taxpayers.ToListAsync();
        }

        public async Task<Taxpayer?> GetByDocumentAsync(string rncCedula)
        {
            return await _context.Taxpayers
                .Include(t => t.TaxReceipts)
                .FirstOrDefaultAsync(t => t.RncCedula == rncCedula);
        }
    }
}
