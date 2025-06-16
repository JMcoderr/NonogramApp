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
            string salt = "C00K!3S";
            uint hash = 4629;
            uint hash2 = 9320;
            uint hash3;

            foreach (char c in salt)
            {
                hash = ((hash << 5) + hash) + c; 
            }

            foreach (char c in input)
            {
                hash2 = ((hash2 << 5) + hash2) + c;
            }

            hash3 = hash * hash2;
            return hash3;
        }
    }
}
