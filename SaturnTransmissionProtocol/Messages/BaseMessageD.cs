using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class BaseMessageD : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get;} = MessageType.D;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x44;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        public BaseMessageD()
        {
            MessageCount++;

            // 27 bytes 1(stx) 2(message type) 3-25(information) 26(etx) 27(crc) 
            //Information
            //Position Code Byte(s) Description
            //3 MM:SS 5 Clock minutes & seconds(or SS.D¬: clock seconds and 1/10s during the last minute of a countdown)
            //8 PH 3 HOME Team score
            //11 PV 3 VISITORS Team score
            //14 FH 1 HOME Team faults
            //15 FV 1 VISITORS Team faults
            //16 TOH 1 HOME Team Time out counter
            //17 TOV 1 VISITORS Team Time out counter
            //18 PER 1 Period(1, 2, 3, 4, 5, 6, … or E for extra time if the “Possession” setting is enabled).
            //Space if day time is displayed.
            //See also PNU from “Date and hour” message.
            //19 SER 1 Services or possession(0 all off, 1 HOME on, 2 VISITORS on, 3 all on)
            //20 S/S 1 Start/Stop(0 stop, 1 start, 2 stop with shot clock point ON, 3 start with shot clock point ON)
            //21 SIR 1 Horn(0 all off, 1 main on, 2 shot clock on, 3 all on)
            //22 TOU 2 Time out activated
            //24 PPD 2 Ball possession time activated

            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 1,
                description = "Message Type"
            };
            InformationList.Add(Type);
            Information item3 = new Information
            {
                position = 3,
                code = "MM:SS",
                bytes = 5,
                description = "Clock minutes & seconds (or SS.D¬: clock seconds and 1/10s during the last minute of a countdown)"
            };
            InformationList.Add(item3);
            Information item8 = new Information
            {
                position = 8,
                code = "PH",
                bytes = 3,
                description = "HOME Team score"
            };
            InformationList.Add(item8);
            Information item11 = new Information
            {
                position = 11,
                code = "PV",
                bytes = 3,
                description = "VISITORS Team score"
            };
            InformationList.Add(item11);
            Information item14 = new Information
            {
                position = 14,
                code = "FH",
                bytes = 1,
                description = "HOME Team faults"
            };
            InformationList.Add(item14);
            Information item15 = new Information
            {
                position = 15,
                code = "FV",
                bytes = 1,
                description = "VISITORS Team faults"
            };
            InformationList.Add(item15);
            Information item16 = new Information
            {
                position = 16,
                code = "TOH",
                bytes = 1,
                description = "HOME Team Time out counter"
            };
            InformationList.Add(item16);
            Information item17 = new Information
            {
                position = 17,
                code = "TOV",
                bytes = 1,
                description = "VISITORS Team Time out counter"
            };
            InformationList.Add(item17);
            Information item18 = new Information
            {
                position = 18,
                code = "PER",
                bytes = 1,
                description = "Period(1, 2, 3, 4, 5, 6, … or E for extra time if the “Possession” setting is enabled).\nSpace if day time is displayed.\nSee also PNU from “Date and hour” message."
            };
            InformationList.Add(item18);
            Information item19 = new Information
            {
                position = 19,
                code = "SER",
                bytes = 1,
                description = "Services or possession(0 all off, 1 HOME on, 2 VISITORS on, 3 all on)"
            };
            InformationList.Add(item19);
            Information item20 = new Information
            {
                position = 20,
                code = "S/S",
                bytes = 1,
                description = "Start/Stop(0 stop, 1 start, 2 stop with shot clock point ON, 3 start with shot clock point ON)"
            };
            InformationList.Add(item20);
            Information item21 = new Information
            {
                position = 21,
                code = "SIR",
                bytes = 1,
                description = "Horn(0 all off, 1 main on, 2 shot clock on, 3 all on)"
            };
            InformationList.Add(item21);
            Information item22 = new Information
            {
                position = 22,
                code = "TOU",
                bytes = 2,
                description = "Time out activated"
            };
            InformationList.Add(item22);
            Information item24 = new Information
            {
                position = 24,
                code = "PPD",
                bytes = 2,
                description = "Ball possession time activated"
            };
            InformationList.Add(item24);
        }

        public string ReadInformation()
        {
            return Saturn.ReadInformation(InformationList, Information);
        }
        public string GetString(string code)
        {
            return Saturn.GetString(InformationList, code);
        }
    }
}
