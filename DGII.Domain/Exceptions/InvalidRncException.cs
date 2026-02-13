using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Domain.Exceptions
{
    public class InvalidRncException: DomainException
    {
        public InvalidRncException(string document): base($"El RNC/Cedula '{document}' no tiene un formato valido para la DGII.") { }
    }
}
