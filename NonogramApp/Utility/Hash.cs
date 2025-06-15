using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonogramApp.Utility
{
    public static class Hash
    {
        public static uint PassHasher(string input)
        {
            uint hash = 4629;
            foreach (char c in input)
            {
                hash = ((hash << 5) + hash) + c; 
            }
            return hash;
        }
    }
}
