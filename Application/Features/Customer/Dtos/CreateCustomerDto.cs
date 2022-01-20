using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customer.Dtos
{
    public record CreateCustomerDto(string Name, string Phone, string Email);
}
