using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class MessageN : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get; } = MessageType.N;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x4E;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        /// <summary>
        /// Teams names and players names
        /// </summary>
        public MessageN()
        {
            MessageCount++;
            // 412 bytes 1(stx) 2(message type) 3-410(information) 411(etx) 412(crc) 
            //Information
            //Position Code Byte(s) Description
            //3 TNH 12 Name of the LOCAL team (*)
            //15 TNV 12 Name of the VISITOR team (*)
            //27 GH1 12 Name of the player 1 of the LOCAL team
            //39 GH2 12 Name of the player 2 of the LOCAL team
            //…
            //207 GH16 12 Name of the player 16 of the LOCAL team
            //219 GV1 12 Name of the player 1 of the VISITOR team
            //231 GV2 12 Name of the player 2 of the VISITOR team
            //…
            //387 GV15 12 Name of the player 15 of the VISITOR team
            //399 GV16 12 Name of the player 16 of the VISITOR team

            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 1,
                description = "Message Type"
            };
            Information TNH = new Information
            {
                position = 3,
                code = "TNH",
                bytes = 12,
                description = "Name of the LOCAL team (*)"
            };
            InformationList.Add(TNH);
            Information TNV = new Information
            {
                position = 15,
                code = "TNV",
                bytes = 12,
                description = "Name of the VISITOR team (*)"
            };
            InformationList.Add(TNV);
            int pos = 27;
            for (int i = 1; i<= 16; i++)
            {
                Information GH = new Information
                {
                    position = pos,
                    code = "GH" + i,
                    bytes = 12,
                    description = "Name of the player "+i+" of the LOCAL team"
                };
                InformationList.Add(GH);
                pos+=12;
            }
            pos = 219;
            for (int i = 1; i <= 16; i++)
            {
                Information GV = new Information
                {
                    position = pos,
                    code = "GV" + i,
                    bytes = 12,
                    description = "Name of the player " + i + " of the VISITOR team"
                };
                InformationList.Add(GV);
                pos += 12;
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
