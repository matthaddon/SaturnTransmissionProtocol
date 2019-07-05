using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class Information
    {
        public int position;
        public string code;
        public int bytes;
        public string description;

        public byte[] informationBytes = null;
        public string information = String.Empty;
        
        public string GetString()
        {
            if (informationBytes != null)
            {
                string s = Encoding.Default.GetString(informationBytes);
                return s == "" ? String.Empty : s;
            }

            return String.Empty;
        }
    }
}
