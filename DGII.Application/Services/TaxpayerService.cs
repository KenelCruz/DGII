using System;
using System.Collections.Generic;
using System.Text;
using DGII.Domain.Interfaces;
using DGII.Application.DTOs;
using DGII.Domain.Entities;

namespace DGII.Application.Services
{
    public class TaxpayerService
    {
        private readonly ITaxpayerRepository _repository;
        private const decimal ITBIS_RATE = 0.18m;

        public TaxpayerService(ITaxpayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxpayerReportDto?> GetTaxpayerReportAsync(string document)
        {
            var taxpayer = await _repository.GetByDocumentAsync(document);
            if (taxpayer == null) return null;

            return new TaxpayerReportDto
            {
                RncCedula = taxpayer.RncCedula,
                Name = taxpayer.Name,
                TotalItbis = taxpayer.TaxReceipts.Sum(r => r.Itbis18),

                vouchers = taxpayer.TaxReceipts.Select(r => new TaxReceiptDto
                {
                    NCF = r.NCF,
                    Amount = r.Amount,
                    Itbis = r.Itbis18

                }).ToList()
            };
        }

        public TaxReceipt CreateReceiiptCalculated(string ncf, string document, decimal amount) 
        {
            return new TaxReceipt
            {
                NCF = ncf,
                Amount = amount,
                RncCedula = document,
                Itbis18 = amount * ITBIS_RATE
            };
        }
    }
}
