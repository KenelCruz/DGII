using DGII.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Domain.Interfaces
{
    public interface ITaxReceiptRepository
    {
        Task<IEnumerable<TaxReceipt>> GetAllAsync();
        Task<IEnumerable<TaxReceipt>> GetTaxpayerRncAsync(string rncCedula);
        Task AddAsync(TaxReceipt receipt);
        Task<bool> SaveChangesAsync();
    }
}
