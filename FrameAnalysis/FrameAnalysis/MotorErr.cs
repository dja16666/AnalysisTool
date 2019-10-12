using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis
{
    internal class MotorErr: Errs
    {
        readonly static UInt16 MOTOR_ERR_RECORD_PARAM3_PREFIX_MASK = 0xF000;
        readonly static UInt16 MOTOR_ERR_RECORD_PARAM3_ERR_MASK = 0x0FFF;

        private enum Motor_Param3_Prefix
        {
            MOTOR_ERR_RECORD_PARAM3_PREFIX_NONE = 0x0000,
            MOTOR_ERR_RECORD_PARAM3_PREFIX_IOSEAL = 0x1000,
        };

        private static readonly Dictionary<Byte, string> res_dict = new Dictionary<Byte, string>
        {
            {0x01, "LOCKSET"},
            {0x02, "LOCKRESET"},
            {0x04, "CURRENTOVER"},
            {0x08, "TIMEOUT"},
            {0x10, "CURRENTDELTA"},
            {0x20, "MOTORNOACTION"},
        };

        public override string ErrTranslate(Byte err)
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
            str = str.TrimEnd('|');
            return str;
        }

        public override string Param1Translate(UInt16 data)
        {
            string str = "Max_I:";
            str += data.ToString();
            return str;
        }

        public override string Param2Translate(UInt16 data)
        {
            string str = "Cell_V:";
            str += data.ToString();
            return str;
        }

        public override string Param3Translate(UInt16 data)
        {
            string str = "Seal_IO:";

            if ((data & MOTOR_ERR_RECORD_PARAM3_PREFIX_MASK) == (UInt16)Motor_Param3_Prefix.MOTOR_ERR_RECORD_PARAM3_PREFIX_IOSEAL)
            {
                if ((data & MOTOR_ERR_RECORD_PARAM3_ERR_MASK) == 1)
                {
                    str += "Seal Off";
                }
                else if ((data & MOTOR_ERR_RECORD_PARAM3_ERR_MASK) == 0)
                {
                    str += "Seal On";
                }
                else
                {
                    str += "Seal Unknow";
                }
            }
            else
            {
                str = null;
            }

            return str;
        }
        public override string Param4Translate(UInt16 data)
        {
            string str = null;
            return str;
        }
    }
    
}
