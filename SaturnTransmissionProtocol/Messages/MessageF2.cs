using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class MessageF2 : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get; } = MessageType.F2;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x32;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        /// <summary>
        /// Shirt number and personal faults for VISITORS players message
        /// </summary>
        public MessageF2()
        {
            MessageCount++;
            // 53 bytes 1(stx) 2,3(message type) 4-51(information) 52(etx) 53(crc) 
            //Information
            //Position Code Byte(s) Description
            //4 DM1 1 Ten of shirt for player no. 1 (if the 7th bit is set to 1, it shows that the player is on the playground)
            //5 UM1 1 Unit of shirt for player no. 1
            //6 P1 1 Faults number for player no. 1
            //…
            //… repeat until player 16.
            //…
            //49 DM16 1 Ten of shirt for player no. 16 (if the 7th bit is set to 1, it shows that the player is on the playground)
            //50 UM16 1 Unit of shirt for player no. 16
            //51 P16 1 Faults number for player no. 16

            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 2,
                description = "Message Type"
            };
            int pos = 4;
            for (int i = 1; i<= 16; i++)
            {
                Information DM = new Information
                {
                    position = pos,
                    code = "DM"+i,
                    bytes = 1,
                    description = "Ten of shirt for player no. "+i+" (if the 7th bit is set to 1, it shows that the player is on the playground)"
                };
                InformationList.Add(DM);
                pos++;
                Information UM = new Information
                {
                    position = pos,
                    code = "UM" + i,
                    bytes = 1,
                    description = "Unit of shirt for player no. "+i
                };
                InformationList.Add(UM);
                pos++;
                Information P = new Information
                {
                    position = pos,
                    code = "P" + i,
                    bytes = 1,
                    description = "Faults number for player no. " + i
                };
                InformationList.Add(P);
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
