using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public class MessageT : IMessage
    {
        private static long MessageCount;
        public long messageCount { get { return MessageCount; } }
        public MessageType type { get; } = MessageType.T;
        public bool stx { get; set; }
        public byte Identification { get; } = 0x54;
        public byte[] Information { get; set; }
        public bool etx { get; set; }
        public List<Information> InformationList { get; set; }

        /// <summary>
        /// Date and hour
        /// </summary>
        public MessageT()
        {
            MessageCount++;
            // 28 bytes 1(stx) 2(message type) 3-26(information) 27(etx) 28(crc) 
            //Information
            //Position Code Byte(s) Description
            // 3 DD 1 Ten of the day date
            // 4 DU 1 Unit of the day date
            // 5 / 1 Separator character (2Fh)
            // 6 MOD 1 Ten of the month date
            // 7 MOU 1 Unit of the month date
            // 8 / 1 Separator character (2Fh)
            // 9 YD 1 Ten of the year date
            // 10 YU 1 Unit of the year date
            // 11 HD 1 Ten of the hour (time of day)
            // 12 HU 1 Unit of the hour (time of day)
            // 13 : 1 Separator character (3Ah)
            // 14 MD 1 Ten of the minutes (time of day)
            // 15 MU 1 Unit of the minutes (time of day)
            // 16 . 1 Separator character (2Eh)
            // 17 SD 1 Ten of the seconds (time of day)
            // 18 SU 1 Unit of the seconds (time of day)
            // 19 CFG 1 Console configuration, Byte from 0x80 to 0x9F with bit set if the corresponding setting is
            // Enabled in the Advance console setting: bit 0 for “Tennis Orion”, bit 1 for “Olympics”, bit 2 for
            // “Hockey outdoor”, bit 3 for “Possession” and bit 4 for “604”
            // 20 SPORT 1 Sport selected: 0 for Basket and Netball, 1 for Volley, 2 for Football, 3 for Handball, 4 for Hockey,
            // 5 for Water polo, 6 for Tennis and 7 for Custom
            // 21 PNU 1 Period number (always from 0 to 9, never “E” for extra period); only valid from software version 5.12.
            // Set to 0 during game intermission. Decrease from 3 to 1 during the hour before an ice hockey game.
            // 22 LUM 1 Scoreboard luminosity (only managed by Saturn V2 scoreboards) [from software version 5.37]:
            // ¬ (space) or 0: Luminosity selected inside the scoreboard by switch.
            // 1: Lowest scoreboard luminosity
            // 2:Medium low scoreboard luminosity
            // 3: Medium high scoreboard luminosity
            // 4: Highest scoreboard luminosity
            // 23 SSI 1 Start/Stop Indication [from software version 5.37]:
            // ¬ (space): No Hockey or no Whistle detection.
            // 1: Manual Start (hockey whistle detection only).
            // 2: Manual Stop (hockey whistle detection only).
            // 3: Automatic Stop (hockey whistle detection only).
            // 24 LAN 1 Bit 0 & 1: Language character set to display on alphanumeric scoreboard:
            // Bit 1 Bit 0 Char. 32-127 Char. 128-255
            // 0 0 Char 32-127 of Font
            // 1 (Latin/standard)
            // Char 128-255 of Font 2 (Cyrillic)
            // 0 1 Char 128-255 of Font 3 (not yet defined)
            // 1 0 Char 128-255 of Font 4 (not yet defined)
            // 1 1

            InformationList = new List<Information>();
            Information Type = new Information
            {
                position = 2,
                code = "Type",
                bytes = 1,
                description = "Message Type"
            };
            InformationList.Add(Type);
            Information DD = new Information
            {
                position = 3,
                code = "DD",
                bytes = 1,
                description = "Ten of the day date"
            };
            InformationList.Add(DD);
            Information DU = new Information
            {
                position = 4,
                code = "DU",
                bytes = 1,
                description = "Unit of the day date"
            };
            InformationList.Add(DU);
            Information SEP1 = new Information
            {
                position = 5,
                code = "/",
                bytes = 1,
                description = "Separator character (2Fh)"
            };
            InformationList.Add(SEP1);
            Information MOD = new Information
            {
                position = 6,
                code = "MOD",
                bytes = 1,
                description = "Ten of the month date"
            };
            InformationList.Add(MOD);
            Information MOU = new Information
            {
                position = 7,
                code = "MOU",
                bytes = 1,
                description = "Unit of the month date"
            };
            InformationList.Add(MOU);
            Information SEP2 = new Information
            {
                position = 8,
                code = "/",
                bytes = 1,
                description = "Separator character (2Fh)"
            };
            InformationList.Add(SEP2);
            Information YD = new Information
            {
                position = 9,
                code = "YD",
                bytes = 1,
                description = "Ten of the year date"
            };
            InformationList.Add(YD);
            Information YU = new Information
            {
                position = 10,
                code = "YU",
                bytes = 1,
                description = "Unit of the year date"
            };
            InformationList.Add(YU);
            Information HD = new Information
            {
                position = 11,
                code = "HD",
                bytes = 1,
                description = "Ten of the hour (time of day)"
            };
            InformationList.Add(HD);
            Information HU = new Information
            {
                position = 12,
                code = "HU",
                bytes = 1,
                description = "Unit of the hour (time of day)"
            };
            InformationList.Add(HU);
            Information Sep3 = new Information
            {
                position = 13,
                code = ":",
                bytes = 1,
                description = "Separator character (3Ah)"
            };
            InformationList.Add(Sep3);
            Information MD = new Information
            {
                position = 14,
                code = "MD",
                bytes = 1,
                description = "Ten of the minutes (time of day)"
            };
            InformationList.Add(MD);
            Information MU = new Information
            {
                position = 15,
                code = "MU",
                bytes = 1,
                description = "Unit of the minutes (time of day)"
            };
            InformationList.Add(MU);
            Information Sep4 = new Information
            {
                position = 16,
                code = ".",
                bytes = 1,
                description = "Separator character (2Eh)"
            };
            InformationList.Add(Sep4);
            Information SD = new Information
            {
                position = 17,
                code = "SD",
                bytes = 1,
                description = "Ten of the seconds (time of day)"
            };
            InformationList.Add(SD);
            Information SU = new Information
            {
                position = 18,
                code = "SU",
                bytes = 1,
                description = "Unit of the seconds (time of day)"
            };
            InformationList.Add(SU);
            Information CFG = new Information
            {
                position = 19,
                code = "CFG",
                bytes = 1,
                description = "Console configuration, Byte from 0x80 to 0x9F with bit set if the corresponding setting is Enabled in the Advance console setting: bit 0 for “Tennis Orion”, bit 1 for “Olympics”, bit 2 for “Hockey outdoor”, bit 3 for “Possession” and bit 4 for “604”"
            };
            InformationList.Add(CFG);
            Information SPORT = new Information
            {
                position = 20,
                code = "SPORT",
                bytes = 1,
                description = "Sport selected: 0 for Basket and Netball, 1 for Volley, 2 for Football, 3 for Handball, 4 for Hockey, 5 for Water polo, 6 for Tennis and 7 for Custom"
            };
            InformationList.Add(SPORT);
            Information PNU = new Information
            {
                position = 21,
                code = "PNU",
                bytes = 1,
                description = "Period number (always from 0 to 9, never “E” for extra period); only valid from software version 5.12."
            };
            InformationList.Add(PNU);
            Information LUM = new Information
            {
                position = 22,
                code = "LUM",
                bytes = 1,
                description = "Scoreboard luminosity (only managed by Saturn V2 scoreboards) [from software version 5.37]: ¬ (space) or 0: Luminosity selected inside the scoreboard by switch." +
                "\n1: Lowest scoreboard luminosity\n2:Medium low scoreboard luminosity\n3: Medium high scoreboard luminosity\n4: Highest scoreboard luminosity"
            };
            InformationList.Add(LUM);
            Information SSI = new Information
            {
                position = 23,
                code = "SSI",
                bytes = 1,
                description = "Start/Stop Indication [from software version 5.37]:" +
                "\n¬ (space): No Hockey or no Whistle detection." +
                "\n1: Manual Start (hockey whistle detection only)." +
                "\n2: Manual Stop (hockey whistle detection only)." +
                "\n3: Automatic Stop (hockey whistle detection only)."
            };
            InformationList.Add(SSI);
            Information LAN = new Information
            {
                position = 24,
                code = "LAN",
                bytes = 1,
                description = "LAN 1 Bit 0 & 1: Language character set to display on alphanumeric scoreboard: " +
                "\nBit 1 Bit 0 Char. 32-127 Char. 128-255" +
                "\n0 0 Char 32-127 of Font" +
                "\n1 (Latin/standard)" +
                "\nChar 128-255 of Font 2 (Cyrillic)" +
                "\n0 1 Char 128-255 of Font 3 (not yet defined)" +
                "\n1 0 Char 128-255 of Font 4 (not yet defined)" +
                "\n1 1"
            };
            InformationList.Add(LAN);
            
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
