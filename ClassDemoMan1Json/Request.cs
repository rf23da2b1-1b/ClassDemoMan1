using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoMan1Json
{
    public class Request
    {
        public String Method { get; set; }
        public int Tal1 { get; set; }
        public int Tal2 { get; set; }

        public Request(string method, int tal1, int tal2)
        {
            Method = method;
            Tal1 = tal1;
            Tal2 = tal2;
        }

        public Request():this("Not Defined", 0, 0)
        {
        }

        public override string ToString()
        {
            return $"{{{nameof(Method)}={Method}, {nameof(Tal1)}={Tal1.ToString()}, {nameof(Tal2)}={Tal2.ToString()}}}";
        }
    }
}
