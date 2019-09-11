using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis
{
    abstract class Errcs
    {
        public abstract string ErrTranslate(Byte err);
        public abstract string Param1Tanslate(UInt16 data);
        public abstract string Param2Tanslate(UInt16 data);
        public abstract string Param3Tanslate(UInt16 data);
        public abstract string Param4Tanslate(UInt16 data);
    }
}
