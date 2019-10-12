using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis 
{
    class IOErr : Errs
    {
        public string Err { get; set; }
        private static readonly Dictionary<Byte, string> err_dict = new Dictionary<Byte, string>
        {
            {0, "IO_ERR_NONE"},
            {1, "IO_ERR_STATE"},
        };

        public override string ErrTranslate(byte err)
        {
            if (err_dict.ContainsKey(err))
            {
                Err = err_dict[err];
                return err_dict[err];

            }
            else
            {
                Err = null;
                return null;
            }
        }

        public override string Param1Translate(ushort data)
        {
            if (Err == "IO_ERR_STATE")
            {
                string str = "ioState:";
                str += data.ToString();
                return str;
            }
            else
            {
                return null;
            }
        }

        public override string Param2Translate(ushort data)
        {
            return null;
        }

        public override string Param3Translate(ushort data)
        {
            return null;
        }

        public override string Param4Translate(ushort data)
        {
            return null;
        }
    }
}
