using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Application.DTOs
{
    public class TaxReceiptDto
    {
        public string NCF { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Itbis { get; set; }
    }
}
