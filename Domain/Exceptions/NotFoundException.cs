using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message = "Recurso não encontrado.")
            : base(StatusCodes.Status404NotFound, message) { }
    }
