using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopMicro6.Services.ShoppingCartApi.DTOs;

public class ResponseDTO
{
    public bool IsSuccess { get; set; } = true;
    public object Result { get; set; }
    public string Message { get; set; } = "";
    public IEnumerable<string> ErrorMessages { get; set; }
}
