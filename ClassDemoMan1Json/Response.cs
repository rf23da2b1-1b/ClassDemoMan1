using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoMan1Json
{
    public class Response
    {
        public bool Ok { get; set; }
        public int? Result { get; set; }
        public String? ErrorMessage { get; set; }

        public Response(int result)
        {
            Ok = true;
            Result = result;
            ErrorMessage = null;
        }

        public Response(string? errorMessage = null) // virker også som default konstruktor
        {
            Ok = false;
            Result = null;
            ErrorMessage = errorMessage;
        }

        public override string ToString()
        {
            return $"{{{nameof(Ok)}={Ok.ToString()}, {nameof(Result)}={Result.ToString()}, {nameof(ErrorMessage)}={ErrorMessage}}}";
        }
    }
}
