using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Witchblades.Backend.Api.DataContracts.ViewModels.Errors
{
    public class Error : IViewModel
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
