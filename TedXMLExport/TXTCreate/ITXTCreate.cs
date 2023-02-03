using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeerTedXML.TXTCreate
{
    internal interface ITXTCreate
    {
        void AppendTxt(int readOption);
        void CreateNewCsv(int readOption);
        void ReadXml(int readOption);
    }
}
