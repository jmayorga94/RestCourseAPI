using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses
{
    public class ValidationFailureResponse
    {
        public IEnumerable<ValidationResponse> Errors { get; set; }
    }

    public class ValidationResponse
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
