using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Domain.Exceptions
{
    public class TaxCalculationException: DomainException
    {
        public TaxCalculationException(string document, string message) : base($"Error calculando ITBIS para el RNC/Cedula {document}: {message}") { }
    }
}
