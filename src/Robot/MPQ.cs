using SinaimgPublisher;
using SinaimgPublisher.Extension;
using SinaimgPublisher.Property;
using SinaimgPublisher.Robot.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SinaimgPublisher
{
    public class MPQ
    {
        static frmSet fs = new frmSet();
        static frmAbout fa = new frmAbout();

        //Disable
        [DllExport("Message", CallingConvention = CallingConvention.StdCall)]
        public static Int32 Message(string robotQQ, Int32 msgType, string msgRaw, string cookies, string SessionKey, string ClinetKey)
        {
            return 1;
        }

        [DllExport("info", CallingConvention = CallingConvention.StdCall)]
        public static string info()
        {
            HandlerProperty.robot = RobotType.MPQ;
            SinaimgPublisherConfig.LoadProperty();
            return "微博圖片反查(查找圖片發布人)";
        }

        /// <summary>
        /// Event定義
        ///<para>會話事件返回值定義:</para>
        ///<para>0 繼續鏈表 1 執行完畢且繼續鏈表  2執行完畢 阻斷鏈表</para>
        ///<para>特別注意:</para>
        ///<para>在特殊事件(需要同意或拒絕的事件)中 返回值1 = 默認確定\同意  2=默認取消\拒絕</para>
        ///<para>因此 插件未處理或不需要的信息請返回0以免造成默認允許添加好友或入群</para>
        ///<para>會話信息事件</para>
        ///<para>1 好友信息;2 群信息;3 討論組信息;4 臨時會話信息</para>
        /// </summary>
        /// <param name="robotQQ"></param>
        /// <param name="msgType"></param>
        /// <param name="msgSubType"></param>
        /// <param name="msgSrc"></param>
        /// <param name="targetActive"></param>
        /// <param name="targetPassive"></param>
        /// <param name="msgContent"></param>
        /// <param name="msgRaw"></param>
        /// <param name="mPointer"></param>
        /// <returns>0</returns>
        [DllExport("EventFun", CallingConvention = CallingConvention.StdCall)]
        public static int EventFun(string robotQQ, Int32 msgType, Int32 msgSubType, string msgSrc, string targetActive, string targetPassive, string msgContent, string msgRaw, IntPtr mPointer)
        {

            //if (msgSrc == "2818618094")
            //{
            //    MPQMessageAPI.Api_SendMsg(robotQQ, msgType, msgSubType, targetActive, msgSrc, "MusicAPI");

            //    MPQMessageAPI.Api_SendMusic(robotQQ, msgType, targetActive, msgSrc, "Tex", "", "http://192.166.5.231/set2.png", "http://192.166.5.231/music.mp3", "", "", "", "");
            //    MPQMessageAPI.Api_SendMsg(robotQQ, msgType, msgSubType, targetActive, msgSrc, "seee.");
            //}

           MPQMessageAPI.Api_OutPut(msgContent);


            //if (msgType.In(1,2,3,4))
            //{
            //    Sinaimg s = new Sinaimg();
            //    bool isatme;

            //    Match match_atme;
            //    match_atme = Regex.Match(msgContent, @"\[\@" + robotQQ + @"\]");
            //    isatme = match_atme.Success;

            //    if (isatme) { msgContent = msgContent.Replace(match_atme.Value, ""); }

            //    return s.Run(isatme, targetActive, msgType, msgContent, msgSrc, robotQQ, msgSubType);

            //}//msgType is 1,2,3,4

            return 0;
        }

        [DllExport("about", CallingConvention = CallingConvention.StdCall)]
        public static void about()
        {
            if (!fa.Visible)
            {
                fa.Show();
            }
            else
            {
                try
                {
                    fa.Activate();
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            try
            {
                MPQMessageAPI.Api_OutPut("啟動:微博圖片反查-關於頁");
            }
            catch { }
        }

        [DllExport("set", CallingConvention = CallingConvention.StdCall)]
        public static void set()
        {
            if (!fs.Visible)
            {
                fs.Show();
            }
            else
            {
                try
                {
                    fs.Activate();
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            try

            {
                MPQMessageAPI.Api_OutPut("启动:微博图片反查-设置页");
            }
            catch { }
        }

        [DllExport("end", CallingConvention = CallingConvention.StdCall)]
        public static Int32 end()
        {
            return 0;
        }

    }
}
