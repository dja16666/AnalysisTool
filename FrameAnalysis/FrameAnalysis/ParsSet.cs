using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FrameAnalysis
{
    public class ParseSet
    {
        private enum ID
        {
            NONE = 0,
            AT = 1,
            CELL = 2,
            MOTOR = 3,
            SYSTEM = 4,
            IO = 5,
        }

        private static Dictionary<string, string> ID_dict = new Dictionary<string, string>
        {
            { "00", "NONE"},
            { "01", "AT"},
            { "02", "CELL"},
            { "03", "MOTOR"},
            { "04", "SYSTEM"},
            { "05", "IO"},
        };

        private static Dictionary<string, string> state_dict = new Dictionary<string, string>
        {
            { "01 00", "ATDEV_STATE_UNINIT"},
            { "01 01", "ATDEV_STATE_POWEROFF" },
            { "01 02", "ATDEV_STATE_POWERON" },
            { "01 03", "ATDEV_STATE_INIT" },
            { "01 04", "ATDEV_STATE_NET_ATTACH" },
            { "01 FF", "ATDEV_STATE_UNKNOW" },

            { "02 00", "CELLDEV_STATE_READY" },
            { "02 01", "CELLDEV_STATE_PROGRESS" },
            { "02 10", "CELLDEV_STATE_DONE" },
            { "02 11", "CELLDEV_STATE_FAULT" },

            { "03 01", "MOTOR_SEAL" },
            { "03 02", "MOTOR_UNSEAL" },
            { "03 03", "MOTOR_STOP" },
            { "03 04", "MOTOR_RET_UNSEAL" },
            { "03 05", "MOTOR_FEED" },

            { "04 00", "SYSTEM_RST_NONE"},
            { "04 01", "SYSTEM_RST_OBL"},
            { "04 02", "SYSTEM_RST_PIN"},
            { "04 03", "SYSTEM_RST_POR"},
            { "04 04", "SYSTEM_RST_SFT"},
            { "04 05", "SYSTEM_RST_IWD"},
            { "04 06", "SYSTEM_RST_WWD"},
            { "04 07", "SYSTEM_RST_LPW"},
            { "04 08", "SYSTEM_RST_TWD"},
            //IO
            { "05 00", "IO_SENSOR_ID_CLOSETOP"},
            { "05 01", "IO_SENSOR_ID_CLOSEBOTTOM"},
            { "05 02", "IO_SENSOR_ID_BREAK"},
            { "05 03", "IO_SENSOR_ID_PLUGIN"},
            { "05 04", "IO_SENSOR_ID_SEAL"},
            { "05 05", "IO_SENSOR_ID_KEY"},
            { "05 06", "IO_SENSOR_ID_SELFTEST"},
        };

        private static Dictionary<string, string> err_dict = new Dictionary<string, string>
        {
            { "01 00", "AT_ERR_NONE" },
            { "01 01", "AT_ERR_NEEDWAIT" },
            { "01 02", "AT_ERR_ERROR" },
            { "01 03", "AT_ERR_CMD" },
            { "01 04", "AT_ERR_OS" },
            { "01 05", "AT_ERR_EVENT" },
            { "01 06", "AT_ERR_QUERY" },
            { "01 07", "AT_ERR_TIMEOUT" },
            { "01 08", "AT_ERR_PORT" },
            { "01 09", "AT_ERR_STR" },
            { "01 0A", "AT_ERR_PARA" },
            { "01 0B", "AT_ERR_PARSE" },
            { "01 0C", "AT_ERR_PARSER" },
            { "01 0D", "AT_ERR_PEND" },
            { "01 0E", "AT_ERR_PHY" },
            { "01 20", "AT_ERR_TIMOUT" },
            { "01 21", "AT_ERR_SUPPORT" },
            { "01 22", "AT_ERR_NULL" },
            { "01 23", "AT_ERR_STATE" },
            { "01 24", "AT_ERR_EMPTY" },
            { "01 25", "AT_ERR_LOCK" },
            { "01 26", "AT_ERR_ALLOC" },
            { "01 27", "AT_ERR_OVER" },
            { "01 28", "AT_ERR_FAIL" },
            { "01 80", "AT_ERR_SERIOUS" },
            { "01 81", "AT_ERR_SEND" },
            { "01 82", "AT_ERR_RECV" },
            { "01 83", "AT_ERR_CONNECT" },
            { "01 84", "AT_ERR_CLOSE" },
            { "01 85", "AT_ERR_DEV_STATE" },
            { "01 86", "AT_ERR_SOCKET_STATE" },
            { "01 87", "AT_ERR_CONNECT_RETRY" },
            { "01 A0", "AT_ERR_AT" },
            { "01 A1", "AT_ERR_CONFIG" },
            { "01 A2", "AT_ERR_ATE0" },
            { "01 A3", "AT_ERR_QIMUX" },
            { "01 A4", "AT_ERR_QIFGCNT" },
            { "01 A5", "AT_ERR_QIDNSCFG" },
            { "01 A6", "AT_ERR_ATI" },
            { "01 A7", "AT_ERR_CPIN" },
            { "01 A8", "AT_ERR_QCCID" },
            { "01 A9", "AT_ERR_CFUN" },
            { "01 AA", "AT_ERR_CREG" },
            { "01 AB", "AT_ERR_CGREG" },
            { "01 AC", "AT_ERR_QICSGP" },
            { "01 AD", "AT_ERR_QIREGAPP" },
            { "01 AE", "AT_ERR_QIACT" },
            { "01 AF", "AT_ERR_QILOCIP" },
            { "01 B0", "AT_ERR_QIOPEN" },
            { "01 B1", "AT_ERR_QISEND" },
            { "01 B2", "AT_ERR_QISACK" },
            { "01 B3", "AT_ERR_QIRD" },
            { "01 B4", "AT_ERR_QICLOSE" },
            { "01 B5", "AT_ERR_QIDEACT" },
            { "01 B6", "AT_ERR_QISTAT" },
            { "01 B7", "AT_ERR_CSQ" },
            { "01 B8", "AT_ERR_GETSTATE" },
            { "01 B9", "AT_ERR_PDP_DEACT" },
            { "01 BA", "AT_ERR_CONNECT_TIMEOUT" },
            { "01 BB", "AT_ERR_SEND_TIMEOUT" },
            { "01 BC", "AT_ERR_EVENT_SIM" },
            { "01 BD", "AT_ERR_EVENT_CLOSED" },
            { "01 BE", "AT_ERR_EVENT_DETACH" },
            { "01 BF", "AT_ERR_OP_POWERON" }, 
            { "01 C0", "AT_ERR_OP_POWEROFF" }, 
            { "01 FF", "AT_ERR_UNKNOW" },

            //{ "03 04", "MOTOR_BIT_RES_CURRENTOVER" },
            //{ "03 08", "MOTOR_BIT_RES_TIMEOUT" },
            //{ "03 10", "MOTOR_BIT_RES_CURRENTDELTA" },

            { "04 00", "SYSTEM_ERR_NONE"},
            { "04 01", "SYSTEM_ERR_RESET"},
            { "04 02", "SYSTEM_ERR_TASKWD"},
        };

        private static Dictionary<int, string> AT_para1_dict = new Dictionary<int, string>
        {
            { 0x00,"ATDEV_STEP_ID_NONE"},
            { 0x01,"ATDEV_STEP_ID_POWERON"},
            { 0x02,"ATDEV_STEP_ID_INIT"},
            { 0x03,"ATDEV_STEP_ID_ATTACH"},
            { 0x04,"ATDEV_STEP_ID_CONNECT"},
            { 0x05,"ATDEV_STEP_ID_SEND"},
            { 0x06,"ATDEV_STEP_ID_RECV"},
            { 0x07,"ATDEV_STEP_ID_CLOSE"},
        };

        private static Dictionary<int, string> AT_para2_dict = new Dictionary<int, string>
        {
            { 0x00,"AT"},
            { 0x01,"ATI"},
            { 0x02,"ATE"},
            { 0x03,"AT+IPR=%d&W"},
            { 0x04,"AT+CSQ"},
            { 0x05,"AT+CPIN?"},
            { 0x06,"AT+CREG?"},
            { 0x07,"AT+CCLK?"},
            { 0x08,"AT+CNUM"},
            { 0x09,"AT+CFUN=%s"},
            { 0x0A,"AT+CFUN?"},
            { 0x0B,"AT+QBAND=\"%s\""},
            { 0x0C,"AT+QCCID"},
            { 0x0D,"AT+QCELLLOC=%s"},
            { 0x0E,"AT+QPOWD=%s"},
            { 0x0F,"ATS0=%s"},
            { 0x10,"AT+CGATT?"},
            { 0x11,"AT+CGREG?"},
            { 0x12,"AT+CGREG=%s"},
            { 0x13,"AT+QIFGCNT=%s"},
            { 0x14,"AT+QICSGP=%s"},
            { 0x15,"AT+QIREGAP"},
            { 0x16,"AT+QIACT"},
            { 0x17,"AT+QILOCIP"},
            { 0x18,"AT+QINDI=%s"},
            { 0x19,"AT+QIRD=%d,%d,%d,%d"},
            { 0x1A,"AT+QIHEAD=%s"},
            { 0x1B,"AT+QISHOWRA=%s"},
            { 0x1C,"AT+QISHOWPT=%s"},
            { 0x1D,"AT+QIMUX=%s"},
            { 0x1E,"AT+QIMUX?"},
            { 0x1F,"AT+QISTAT"},
            { 0x20,"AT+QISTAT"},
            { 0x21,"AT+QIDEACT"},
            { 0x22,"AT+QIOPEN=\"%s\",\"%s\",\"%d\""},
            { 0x23,"QIOPEN RESUL"},
            { 0x24,"AT+QICLOSE"},
            { 0x25,"AT+QISEND=%d"},
            { 0x26,"QISEND DATA"},
            { 0x27,"AT+QISACK"},
            { 0x28,"AT+QIOPEN=%d,\"%s\",\"%s\",\"%d\""},
            { 0x29,"AT+QICLOSE=%d"},
            { 0x2A,"AT+QISEND=%d,%d"},
            { 0x2B,"QISEND DATA"},
            { 0x2C,"AT+QISACK=%d"},
            { 0x2D,"AT+QIDNSCFG=\"%s\",\"%s\""},
            { 0x2E,"AT+QIDNSCFG?"},
            { 0x2F,"AT+QIDNSGIP=\"%s\""},
            { 0x30,"QIDNSGIP RESULT"},
            { 0x31,"AT+QIDNSIP=%s"},
            { 0x32,"AT+QISACK=%d"},
            { 0xFF, "UNKNOW"},
        };

        private static Dictionary<int, string> AT_para3_dict = new Dictionary<int, string>
        {
            { 0x00,"UNDER"},
            { 0x01,"OVER"},
            { 0x02,"+CPIN: NOT READY"},
            { 0x03,"+PDP DEACT"},
            { 0x04,"+CMS"},
            { 0x05,"+CME"},
            { 0x06,"CLOSED"},
            { 0x07,"+QIRDI"},
            { 0x08,"RECV FROM"},
            { 0x09,"+RECEIVE"},
            { 0xFF,"UNKNOW"},
        };


        static public string Parse(Byte[] data)
        {
            try
            {
                Param param = new Param();
                Byte id = data[4];
                Byte state = data[5];
                Byte err = data[6];
                string kPhyid = Byte2HexString(new Byte[]{ id });
                string kState = kPhyid + " " + Byte2HexString(new Byte[] { state });
                string kErr = kPhyid + " " + Byte2HexString(new Byte[] { err });
                //phyID analysis
                param.phyID = data[4];
                string pyhIDStr = GetFromDict<string, string>(kPhyid, ID_dict);
                //state anylysis
                param.State = data[5];
                string stateStr = GetFromDict<string, string>(kState, state_dict);
                //err analysis
                param.Err = data[6];
                string errStr = GetFromDict<string, string>(kErr, err_dict);
                //param analysis  
                switch ((ID)id)
                {
                    case ID.NONE:

                        break;
                    case ID.AT:
                        {
                            param.Param1 = GetFromDict(data[8], ParseSet.AT_para1_dict);
                            param.Param2 = GetFromDict(data[9], ParseSet.AT_para2_dict);
                            param.Param3 = GetFromDict(data[10], ParseSet.AT_para3_dict);
                        }
                        break;
                    case ID.CELL:
                        {
                            string cell_data = Convert.ToString(data[8], 16);
                            param.Param1 = "0x" + cell_data;
                            cell_data = Convert.ToString(data[9], 16);
                            param.Param2 = "0x" + cell_data;
                        }
                        break;
                    case ID.MOTOR:
                        {
                            MotorErr motorErr = new MotorErr();
                            errStr = "";
                            errStr = motorErr.ErrTranslate(data[6]);

                            UInt16 tmp;
                            tmp = Convert.ToUInt16(data[8] + (data[9] * 256));
                            param.Param1 = motorErr.Param1Translate(tmp);
                            tmp = Convert.ToUInt16(data[10] + (data[11] * 256));
                            param.Param2 = motorErr.Param2Translate(tmp);
                            tmp = Convert.ToUInt16(data[12] + (data[13] * 256));
                            param.Param3 = motorErr.Param3Translate(tmp);
                        }
                        break;
                    case ID.SYSTEM:
                        {
                            SystemErr systemErr = new SystemErr();
                            if (stateStr == "SYSTEM_RST_TWD")
                            {
                                if ((data[8] != 0) || (data[9] != 0)) {
                                    param.Param1 = data[8].ToString();
                                    param.Param2 = systemErr.Param2Translate(data[9]);//GetNameWithCrc(data[9]);
                                    param.Param3 = systemErr.Param3Translate(data[10]);
                                }
                            }
                            else
                            {
                                param.Param1 = null;
                                param.Param2 = null;
                            }
                        }
                        break;
                    case ID.IO:
                        {
                            IOErr ioerr = new IOErr();
                            errStr = "";
                            errStr = ioerr.ErrTranslate(data[6]);
                            UInt16 tmp;
                            tmp = Convert.ToUInt16(data[8] + (data[9] * 256));
                            param.Param1 = ioerr.Param1Translate(tmp);
                        }
                        break;
                    default:
                        break;

                }
                //param4 version message
                if (data.Length >= 16)
                    param.Param4 = "V:" + data[15].ToString();
                else
                    param.Param4 = "V:0";

                //description
                param.Description = string.Format("ID:{0}#{1}#,State:{2}#{3}#,Err:{4}#{5}#", id, pyhIDStr, state, stateStr, err, errStr);
                return JsonConvert.SerializeObject(param);

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static T2 GetFromDict<T1, T2>(T1 key, Dictionary<T1, T2> Dict)
        {
            if (Dict.ContainsKey(key))
            {
                return Dict[key];
            }
            else
            {
                return default(T2);
            }
        }

        static string Byte2HexString(Byte[] data)
        {
            string str = "";
            string bytestr = "";
            foreach(var item in data)
            {
                bytestr = Convert.ToString(item, 16);
                if(item <= 0x0f)
                {
                    bytestr = bytestr.Insert(0, "0");
                }
                str += bytestr;
            }
            return str.ToUpper();
        }

        
    }

    internal class Param
    {
        public int phyID { get; set; }
        public int State { get; set; }
        public int Err   { get; set; }

        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
        public string Description { get; set; }

        public Param()
        {
            Param1 = null;
            Param2 = null;
            Param3 = null;
            Param4 = null;
            Description = null;
        }
    }
}
