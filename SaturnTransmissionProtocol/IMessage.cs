using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnTP
{
    public interface IMessage
    {
        long messageCount { get; }
        MessageType type { get; }
        bool stx { get; set; }
        byte Identification { get; }
        byte[] Information { get; set; }
        bool etx { get; set; }
        List<Information> InformationList { get; set; }
        string ReadInformation();
        string GetString(string code);

    }
    public enum MessageType
    {
        D,
        F1,
        F2,
        F3,
        F4,
        C,
        O,
        T,
        N
    }
}
