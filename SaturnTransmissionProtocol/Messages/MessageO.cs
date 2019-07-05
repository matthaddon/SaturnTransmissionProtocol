using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class MessageO : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get; } = MessageType.O;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x4F;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        /// <summary>
        /// Expulsions timings setting message (standard version)
        /// </summary>
        public MessageO()
        {
            MessageCount++;
            // 30 bytes 1(stx) 2(message type) 3-28(information) 29(etx) 30(crc) 
            //Information
            //Position Code Byte(s) Description
            //5 DM1 1 Ten of the minutes for the timer no. 1
            //6 UM1 1 Unit of the minutes for the timer no. 1
            //8 DS1 1 Ten of the seconds for the timer no. 1
            //9 US1 1 Unit of the seconds for the timer no. 1
            //…
            //40 DM6 1 Ten of the minutes for the timer no. 6
            //41 UM6 1 Unit of the minutes for the timer no. 6
            //43 DS6 1 Ten of the seconds for the timer no. 6
            //44 US6 1 Unit of the seconds for the timer no. 6
            //45 A 1 Penalty of 10 min for the LOCAL team (1 first, 2 second, 3 both)
            //46 B 1 Penalty of 10 min for the VISITORS team (1 first, 2 second, 3 both)


            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 1,
                description = "Message Type"
            };
            int pos = 3;
            for (int i = 1; i<= 6; i++)
            {
                Information DM = new Information
                {
                    position = pos,
                    code = "DM"+i,
                    bytes = 1,
                    description = "Ten of the minutes for the timer no. "+ i
                };
                InformationList.Add(DM);
                pos++;
                Information UM = new Information
                {
                    position = pos,
                    code = "UM" + i,
                    bytes = 1,
                    description = "Unit of the minutes for the timer no. " + i
                };
                InformationList.Add(UM);
                pos++;
                Information DS = new Information
                {
                    position = pos,
                    code = "DS" + i,
                    bytes = 1,
                    description = "Ten of the seconds for the timer no. " + i
                };
                InformationList.Add(DS);
                pos++;
                Information US = new Information
                {
                    position = pos,
                    code = "US" + i,
                    bytes = 1,
                    description = "Unit of the seconds for the timer no. " + i
                };
                InformationList.Add(US);
                pos++;
            }

            Information A = new Information
            {
                position = 27,
                code = "A",
                bytes = 1,
                description = "Penalty of 10 min for the LOCAL team (1 first, 2 second, 3 both)"
            };
            InformationList.Add(A);
            Information B = new Information
            {
                position = 28,
                code = "B",
                bytes = 1,
                description = "Penalty of 10 min for the VISITORS team (1 first, 2 second, 3 both)"
            };
            InformationList.Add(B);
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
