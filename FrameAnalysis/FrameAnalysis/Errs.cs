using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis
{
    abstract class Errs
    {
        public abstract string ErrTranslate(Byte err);
        public abstract string Param1Translate(UInt16 data);
        public abstract string Param2Translate(UInt16 data);
        public abstract string Param3Translate(UInt16 data);
        public abstract string Param4Translate(UInt16 data);
    }
}
