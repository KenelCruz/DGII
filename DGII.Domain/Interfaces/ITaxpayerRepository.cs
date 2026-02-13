using System;
using System.Collections.Generic;
using System.Text;
using DGII.Domain.Entities;

namespace DGII.Domain.Interfaces
{
    public interface ITaxpayerRepository
    {
        Task<IEnumerable<Taxpayer>> GetAllAsync();
        Task<Taxpayer?> GetByDocumentAsync(string rncCedula);

    }
}
