using System;
using System.Collections.Generic;
using System.Text;
using DGII.Domain.Enums;

namespace DGII.Domain.Entities
{
    public class Taxpayer
    {
        public string RncCedula { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public TaxpayerType Type { get; set; }

        public TaxpayerStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }


        public virtual ICollection<TaxReceipt> TaxReceipts { get; set; } 

        public Taxpayer()
        {
            CreationDate = DateTime.UtcNow;
            Status = TaxpayerStatus.Active;
        }

        public bool isActive()
        {
            return Status == TaxpayerStatus.Active;
        }

        public void Activate()
        {
            Status = TaxpayerStatus.Active;
            UpdateDate = DateTime.UtcNow;
        }

        public void Desactivate()
        {
            Status = TaxpayerStatus.Inactive;
            UpdateDate = DateTime.UtcNow;
        }
    }
}
