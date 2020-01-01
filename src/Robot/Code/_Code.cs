using SinaimgPublisher.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Robot.Code
{
    public class _Code
    {
        /// <summary>
        /// 獲取 @指定QQ 的操作代碼
        /// </summary>
        /// <param name="qq">指定的QQ號碼</param>
        /// <returns>MPQ @操作代碼</returns>
        public static string Code_At(string qq)
        {
            long _qq = -1;
            Int64.TryParse(qq, out _qq);

            switch (HandlerProperty.robot)
            {
                case RobotType.CQ:
                    return CQCode.CQCode_At(_qq);
                case RobotType.MPQ:
                    return MPCCode.MPQCode_At(qq);
                default:
                    return string.Empty;
            }

        }


}
}
