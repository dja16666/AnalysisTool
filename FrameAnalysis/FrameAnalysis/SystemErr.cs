using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameAnalysis
{
    class SystemErr : Errs
    {
        static readonly string[] Task_Name_Array =
        {
            "ATDev_Task",
            "HGModCommRecvTask",
            "HGModCommHandleTask",
            "MotorTask",
            "ESealHandleTask",
            "gnssTask",
            "OWHandleTask",
            "sesorTask",
            "UartDMAHandleTask",
        };

        static readonly string[] Queue_Name_Array =
        {
            "ATDevLock",
            "ATDevQ",
            "AdcLock",
            "BlkLock0",
            "BlkLock1",
            "BlkLock2",
            "BlkLock3",
            "BlkLock4",
            "BlkLock5",
            "BlkLock6",
            "ESealBlkQ",
            "HGComQ",
            "HGReLock",
            "I2CLock",
            "LIS3DHLock",
            "MonitorSem",
            "MotorQ",
            "OneWLock",
            "OneWireQ",
            "RecordLock0",
            "RecordLock1",
            "RecordLock2",
            "SensorQ",
            "TmrQ",
            "UartDmaQ",
            "UartRxLock0",
            "UartRxLock1",
            "UartRxLock2",
            "UartRxLock3",
            "UartTxLock0",
            "UartTxLock1",
            "UartTxLock2",
            "UartTxLock3",

        };
        static string GetNameWithCrc8(string[] array, byte Crc)
        {
            string name_str = null;
            int i = 0;
            do
            {
                byte tmp_crc = Crc8.CalcCRC8(System.Text.Encoding.Default.GetBytes(array[i]), array[i].Length);
                if (Crc == tmp_crc)
                {
                    name_str = array[i];
                }
                i++;
            } while ((name_str == null) && (i < array.Length));
            if (name_str == null)
                name_str = "Not Find";
            return name_str;
        }

        public override string ErrTranslate(byte err)
        {
            throw new NotImplementedException();
        }

        public override string Param1Translate(ushort data)
        {
            throw new NotImplementedException();
        }

        public override string Param2Translate(ushort data)
        {
            return GetNameWithCrc8(Task_Name_Array, (byte)data);
        }

        public override string Param3Translate(ushort data)
        {
            return GetNameWithCrc8(Queue_Name_Array, (byte)data);
        }

        public override string Param4Translate(ushort data)
        {
            throw new NotImplementedException();
        }
    }
}
