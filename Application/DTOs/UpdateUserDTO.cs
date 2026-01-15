using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

    public class UpdateUserDTO
    {
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
    public string? Email { get; set; }
}

