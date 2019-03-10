using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    public class FetchDataFromAPIException:Exception
    {
        public FetchDataFromAPIException()
        {
        }

        public FetchDataFromAPIException(string message)
            : base(message)
        {
        }

        public FetchDataFromAPIException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
