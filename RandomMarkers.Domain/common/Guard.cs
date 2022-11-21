using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomMarkers.Domain.common
{
    public static class Guard
    {
        public static void Require(bool precondition, string exceptionMessage)
        {
            if (!precondition)
                throw new ArgumentException(exceptionMessage);
        }
    }
}
