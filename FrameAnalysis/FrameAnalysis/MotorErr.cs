using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis
{
    internal class MotorErr
    {
        private static readonly Dictionary<Byte, string> res_dict = new Dictionary<Byte, string>
        {
            {0x01, "LOCKSET"},
            {0x02, "LOCKRESET"},
            {0x04, "CURRENTOVER"},
            {0x08, "TIMEOUT"},
            {0x10, "CURRENTDELTA"},
            {0x20, "MOTORNOACTION"},
        };

        public string Translate(Byte err)
        {
            string str = "";
            for(int i = 0; i < 8; i++)
            {
                Byte key = (Byte)(err & (1 << i));
                if (key != 0)
                {
                    if (res_dict.ContainsKey(key))
                    {
                        str += res_dict[key];
                        str += "|";
                    }
                }
            }
            str.TrimEnd('|');
            return str;
        }
    }
    
}
