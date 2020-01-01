using SinaimgPublisher.Extension;
using SinaimgPublisher.Robot.API.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Robot.API
{
    public static class CQAPI_Extras
    {
        public static List<GroupInfo> GetGroupList(string Hex)
        {
            List<GroupInfo> Ginfo = new List<GroupInfo>();

            int numofGroup = Converter.HexStringToInt(Hex.Substring(0, 8));
            int pointer_start = 8;
            int pointer_end = 8;
            string lineHex = "";
            int lineGroupNum = 0;
            int GroupNamelength = 0;
            string lineGroupName = "";

            if (numofGroup > 0)
            {
                for(int row = 1; row <= numofGroup; row++)
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //    [00000001000c0000000020cf24ac00023232]
                    //    =[00000001]  numofGroup
                    //    #[000c] lineLength
                    //    +[00000000 20cf24ac 0002 3232] Data
                    //     -[00000000] GroupNumLength ; 0=ch+ ; 1=ch-
                    //     -[20cf24ac] GroupNum
                    //     -[0002]GroupNameLength
                    //     -[3232] GroupName
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////

                    pointer_end = pointer_start + 4 + (Converter.HexStringToInt(Hex.Substring(pointer_start, 4)) * 2);
                    lineHex = Hex.Substring(pointer_start + 4, pointer_end - (pointer_start + 4));
                    lineGroupNum = Converter.HexStringToInt(lineHex.Substring(8, 8));
                    GroupNamelength = Converter.HexStringToInt(lineHex.Substring(16, 4)) * 2;
                    lineGroupName = Converter.HexadecimalEncoding.FromHexString(lineHex.Substring(20, GroupNamelength));
                    GroupInfo gi = new GroupInfo();
                    gi.GroupNumber = lineGroupNum;
                    gi.GroupName = lineGroupName;
                    Ginfo.Add(gi);

                    pointer_start = pointer_end;
                }
            }
            return Ginfo;
        }
    }
}
