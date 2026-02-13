using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Application.DTOs
{
    public class TaxReceiptListDto
    {
        public string RncCedula { get; set; } = string.Empty;
        public string NCF { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string Itbis18 { get; set; } = string.Empty;
    }

}
