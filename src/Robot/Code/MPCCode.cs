using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Robot.Code
{
    public class MPCCode
    {
        /// <summary>
        /// 獲取 @指定QQ 的操作代碼
        /// </summary>
        /// <param name="qq">指定的QQ號碼</param>
        /// <returns>MPQ @操作代碼</returns>
        public static string MPQCode_At(string qq)
        {
            return "[@" + qq + "]";
        }

    }
}
