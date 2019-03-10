using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    public class DatabaseAccesException:Exception
    {
        public DatabaseAccesException()
        {
        }

        public DatabaseAccesException(string message)
            : base(message)
        {
        }

        public DatabaseAccesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
