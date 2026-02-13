using System;
using System.Collections.Generic;
using System.Text;

namespace DGII.Domain.Exceptions
{
    public class EntityNotFoundException: DomainException
    {
        public EntityNotFoundException(string entityName, object key): base($"El '{entityName}' con el documento ({key}) no fue encontrado.") { }
    }
}
