using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    /// <summary>
    /*
     * Transmission: Serial asynchronous unidirectional 
     * Type: ASCII, 1 start bit, 8 data bits, no parity, 1 stop bit 
     *Speed:9600 Bauds  
     * Electrical standard:RS422  
     */
    /// </summary>
    public class Saturn
    {
        //Start
        private const byte STX = 0x02;
        //End
        private const byte ETX = 0x03 ;

        //Identification char(s)
        private const byte D = 0x44 ; //base message
        private const byte F = 0x46 ; //start of F message
        private const byte F1 = 0x31 ; //end of F message
        private const byte F2 = 0x32; //end of F message
        private const byte F3 = 0x33; //end of F message
        private const byte F4 = 0x34; //end of F message
        private const byte C =  0x43 ; 
        private const byte O =  0x4F ;
        private const byte T =  0x54 ;
        private const byte N =  0x4E ;
        
        int messageCount = 0;
        List<byte> currentMessage = new List<byte>();
        long bytesReceived = 0;

        public Action<IMessage> messageCallback;

        public Saturn(Action<IMessage> action)
        {
            messageCallback += action;
        }

        public byte[] GetCurrentMessage()
        {
            return currentMessage.ToArray();
        }
        public long GetBytesReceived()
        {
            return bytesReceived;
        }

        public void StreamChunks(byte[] chunk, int index = 0)
        {
            bytesReceived += chunk.Length;

            int stx = CheckSTX(chunk, index);
            int etx = CheckETX(chunk, index);

            if(etx != -1)
            {
                //Include CRC
                etx++;
            }

            if (stx != -1 && etx != -1)
            {
                if (etx < stx)
                {
                    if (currentMessage.Count > 0)
                    {
                        for (int i = index; i <= etx; i++)
                        {
                            currentMessage.Add(chunk[i]);
                        }
                        currentMessage.Insert(0, STX);
                        ReadMessage(currentMessage.ToArray());
                        currentMessage.Clear();
                    }

                    messageCount++;
                    currentMessage.Clear();
                    for (int i = stx; i < chunk.Length; i++)
                    {
                        currentMessage.Add(chunk[i]);
                    }
                }
                else
                {
                    if (currentMessage.Count > 0)
                    {
                        messageCount++;
                        currentMessage.Clear();
                        for (int i = stx; i <= etx; i++)
                        {
                            currentMessage.Add(chunk[i]);
                        }
                        currentMessage.Insert(0, STX);
                        ReadMessage(currentMessage.ToArray());
                        currentMessage.Clear();
                    }
                }
            }
            else if (stx != -1)
            {
                messageCount++;
                currentMessage.Clear();
                for (int i = stx; i < chunk.Length; i++)
                {
                    currentMessage.Add(chunk[i]);
                }
            }
            else if (etx != -1)
            {
                if (currentMessage.Count > 0)
                {
                    for (int i = index; i <= etx; i++)
                    {
                        currentMessage.Add(chunk[i]);
                    }
                    currentMessage.Insert(0, STX);
                    ReadMessage(currentMessage.ToArray());
                    currentMessage.Clear();
                }
            }

            if (etx != -1)
            {
                if (etx < chunk.Length)
                {
                    StreamChunks(chunk, etx+1);
                }
            }
        }

        private void ReadMessage(byte[] messageBytes)
        {
            bytesReceived = 0;
            int msgIdx = 0;
            if (CheckSum(messageBytes, messageBytes.Length-1) == messageBytes[messageBytes.Length-1])
            {
                IMessage message = GetMessageType(messageBytes, ref msgIdx);
                if (message != null)
                {
                    message.Information = messageBytes;
                    message.ReadInformation();
                    messageCallback(message);
                }
            }
        }
        
        public byte CheckSum(byte[] _PacketData, int PacketLength)
        {
            Byte _CheckSumByte = 0x00;
            for (int i = 0; i < PacketLength; i++)
                _CheckSumByte ^= _PacketData[i];
            return _CheckSumByte;
        }

        public int CheckSTX(byte[] bytes, int startIndex = 0)
        {
            for(int i = startIndex; i < bytes.Length; i++)
            {
                switch (bytes[i])
                {
                    case STX:
                        return i;
                }
            }
            return -1;
        }

        public int CheckETX(byte[] bytes, int startIndex = 0)
        {
            for (int i = startIndex; i < bytes.Length; i++)
            {
                switch (bytes[i])
                {
                    case ETX:
                        return i;
                }
            }
            return -1;
        }

        public IMessage GetMessageType(byte[] bytes, ref int index)
        {
            bool isF = false;
            for (int i = index; i < bytes.Length; i++)
            {
                switch(bytes[i])
                {
                    case D:
                        index = i;
                        return new BaseMessageD();
                    case F:
                        isF = true;
                        break;
                    case F1:
                        if(isF)
                        {
                            index = i;
                            return new MessageF1();
                        }
                        break;
                    case F2:
                        if (isF)
                        {
                            index = i;
                            return new MessageF2();
                        }
                        break;
                    case F3:
                        if (isF)
                        {
                            index = i;
                            return new MessageF3();
                        }
                        break;
                    case F4:
                        if (isF)
                        {
                            index = i;
                            return new MessageF4();
                        }
                        break;
                    case C:
                        index = i;
                        return new MessageC();
                    case O:
                        index = i;
                        return new MessageO();
                    case T:
                        index = i;
                        return new MessageT();
                    case N:
                        index = i;
                        return new MessageN();
                    default:
                        break;
                }
            }

            return null;
        }
        
        public static string ReadInformation(List<Information> InformationList, byte[] Information)
        {
            foreach (Information info in InformationList)
            {
                try
                {
                    if (info.position + info.bytes < Information.Length)
                    {
                        byte[] informationBytes = new byte[info.bytes];
                        Buffer.BlockCopy(Information, info.position, informationBytes, 0, info.bytes);
                        info.informationBytes = informationBytes;
                    }
                }
                catch { }
            }

            return "";
        }

        public static string GetString(List<Information> InformationList, string code)
        {
            Information info = InformationList.Find(x => x.code == code);

            if (info != null)
            {
                return info.GetString();
            }

            return String.Empty;
        }

    }


}
