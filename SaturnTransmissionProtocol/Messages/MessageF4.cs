using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class MessageF4 : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get; } = MessageType.F4;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x34;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        /// <summary>
        /// Points of each VISITOR player message
        /// </summary>
        public MessageF4()
        {
            MessageCount++;
            // 37 bytes 1(stx) 2,3(message type) 4-35(information) 36(etx) 37(crc) 
            //Information
            //Position Code Byte(s) Description
            //4 DP1 1 Ten of the points for the player no. 1
            //5 UP1 1 Unit of the points for the player no. 1
            //…
            //…
            //34 DP16 1 Ten of the points for the player no. 16
            //35 UP16 1 Unit of the points for the player no. 16

            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 2,
                description = "Message Type"
            };
            int pos = 4;
            for (int i = 1; i <= 16; i++)
            {
                Information DP = new Information
                {
                    position = pos,
                    code = "DP" + i,
                    bytes = 1,
                    description = "Ten of the points for the player no. " + i
                };
                InformationList.Add(DP);
                pos++;
                Information UP = new Information
                {
                    position = pos,
                    code = "UP" + i,
                    bytes = 1,
                    description = "Unit of the points for the player no. " + i
                };
                InformationList.Add(UP);
                pos++;
            }
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
