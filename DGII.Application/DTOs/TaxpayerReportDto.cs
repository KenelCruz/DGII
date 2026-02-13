using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Application.DTOs
{
    public class TaxpayerReportDto
    {
        public string RncCedula { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal TotalItbis { get; set; }
        public List<TaxReceiptDto> vouchers { get; set; } = new();
    }
}
