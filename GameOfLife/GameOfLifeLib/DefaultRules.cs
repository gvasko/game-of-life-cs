using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeLib
{
    public sealed class DefaultRules
    {
        private DefaultRules()
        {
            throw new InvalidOperationException("Internal error: no instances allowed.");
        }
    }
}
