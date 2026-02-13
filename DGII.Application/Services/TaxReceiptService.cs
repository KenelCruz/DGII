using System;
using System.Collections.Generic;
using System.Text;
using DGII.Domain.Interfaces;
using DGII.Application.DTOs;
using DGII.Domain.Exceptions;
using DGII.Domain.Entities;
namespace DGII.Application.Services
{
    public class TaxReceiptService
    {
        private readonly ITaxReceiptRepository _repository;
        private readonly ITaxpayerRepository _taxpayerRepository;
        private const decimal ITBIS_RATE = 0.18m;

        public TaxReceiptService(ITaxReceiptRepository repository, ITaxpayerRepository taxpayerRepository)
        {
            _repository = repository;
            _taxpayerRepository = taxpayerRepository;
        }

        public async Task<IEnumerable<TaxReceiptListDto>> GetAllAsync()
        {
            var receipts = await _repository.GetAllAsync();

            return receipts.Select(r => new TaxReceiptListDto
            {
                RncCedula = r.RncCedula,
                NCF = r.NCF,
                Amount = r.Amount.ToString("F2"),
                Itbis18 = r.Itbis18.ToString("F2")
            });
        }

        public async Task<IEnumerable<TaxReceiptListDto>> GetByTaxpayerAsync(string rncCedula)
        {
            var receipts = await _repository.GetTaxpayerRncAsync(rncCedula);

            return receipts.Select(r => new TaxReceiptListDto
            {
                RncCedula = r.RncCedula,
                NCF = r.NCF,
                Amount = r.Amount.ToString("F2"),
                Itbis18 = r.Itbis18.ToString("F2")
            });
        }

        public async Task<TaxReceiptListDto> CreateAsync(string rnc, string ncf, decimal amount)
        {
            var taxpayer = await _taxpayerRepository.GetByDocumentAsync(rnc);
            if (taxpayer == null) throw new EntityNotFoundException("Taxpayer", rnc);

            var receipt = new TaxReceipt
            {
                RncCedula = rnc,
                NCF = ncf,
                Amount = amount,
                Itbis18 = amount * ITBIS_RATE
            };

        await _repository.AddAsync(receipt);
            await _repository.SaveChangesAsync();
            return new TaxReceiptListDto
            {
                RncCedula = receipt.RncCedula,
                NCF = receipt.NCF,
                Amount = receipt.Amount.ToString("F2"),
                Itbis18 = receipt.Itbis18.ToString("F2")
            };
        }
    }
}
