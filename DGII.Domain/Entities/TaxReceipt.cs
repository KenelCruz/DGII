using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DGII.Domain.Entities
{
    public class TaxReceipt
    {
        public string RncCedula { get; set; } = string.Empty;
        [Key]
        public string NCF {  get; set; } =  string.Empty;
        public decimal Amount { get; set; }
        public decimal Itbis18 {  get; set; }

        [ForeignKey("RncCedula")]
        public virtual Taxpayer? Taxpayer { get; set; }
    }
}
