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
    public class CQ
    {
        static string _ApiVer = "9";
        static string _AppID = "link.toroko.SinaimgPublisher";
        static int _AuthCode = 0;
        static frmSet fs = new frmSet();
        static frmAbout fa = new frmAbout();

        [DllExport("AppInfo", CallingConvention = CallingConvention.StdCall)]
        public static string appInfo()
        {
            return string.Concat(_ApiVer, ",", _AppID);
        }

        [DllExport("Initialize", CallingConvention = CallingConvention.StdCall)]
        public static Int32 Initialize(int AuthCode)
        {
            HandlerProperty.robot = RobotType.CQ;
            HandlerProperty.CQ_AuthCode = AuthCode;
            SinaimgPublisherConfig.LoadProperty();
            _AuthCode = AuthCode;
            return 0;
        }

        [DllExport("_eventStartup", CallingConvention = CallingConvention.StdCall)]
        public static Int32 AppStartup()
        {
            return 0;
        }

        [DllExport("_eventExit", CallingConvention = CallingConvention.StdCall)]
        public static Int32 AppExit()
        {
            return 0;
        }

        [DllExport("_eventEnable", CallingConvention = CallingConvention.StdCall)]
        public static Int32 AppEnable()
        {
            return 0;
        }

        [DllExport("_eventDisable", CallingConvention = CallingConvention.StdCall)]
        public static Int32 AppDisable()
        {
            return 0;
        }

        [DllExport("_eventPrivateMsg", CallingConvention = CallingConvention.StdCall)]
        public static Int32 PrivateMessage(int subType, int sendTime, long fromQQ, string msg, int font)
        {
                Sinaimg s = new Sinaimg();
                bool isatme;

                Match match_atme;
                match_atme = Regex.Match(msg, @"\[CQ:at,qq=" + CQAPI.GetLoginQQ(HandlerProperty.CQ_AuthCode) + @"\]");
                isatme = match_atme.Success;

                if (isatme) { msg = msg.Replace(match_atme.Value, ""); }

                return s.Run(isatme, fromQQ.ToString(), 1, msg, fromQQ.ToString(), CQAPI.GetLoginQQ(HandlerProperty.CQ_AuthCode).ToString(), subType);
        }

        [DllExport("_eventGroupMsg", CallingConvention = CallingConvention.StdCall)]
        public static Int32 GroupMessage(int subType, int sendTime, long fromGroup, long fromQQ, string fromAnonymous, string msg, int font)
        {
            Sinaimg s = new Sinaimg();
            bool isatme;

            Match match_atme;
            match_atme = Regex.Match(msg, @"\[CQ:at,qq=" + CQAPI.GetLoginQQ(HandlerProperty.CQ_AuthCode) + @"\]");
            isatme = match_atme.Success;

            if (isatme) { msg = msg.Replace(match_atme.Value, ""); }

            return s.Run(isatme, fromQQ.ToString(), 2, msg, fromGroup.ToString(), CQAPI.GetLoginQQ(HandlerProperty.CQ_AuthCode).ToString(), subType);
        }

        [DllExport("_eventDiscussMsg", CallingConvention = CallingConvention.StdCall)]
        public static Int32 DiscussMessage(int subType, int sendTime, long fromDiscuss, long fromQQ, string msg, int font)
        {
            return 0;
        }

        [DllExport("_eventGroupUpload", CallingConvention = CallingConvention.StdCall)]
        public static Int32 GroupUpload(int subType, int sendTime, long fromGroup, long fromQQ, string file)
        {
            // 處理群文件上傳事件。
            //  CQ.SendGroupMessage(fromGroup, String.Format("[{0}]{1}你上傳了一個文件：{2}", CQ.ProxyType, CQ.CQCode_At(fromQQ), file));
            return 0;
        }

        [DllExport("_eventSystem_GroupAdmin", CallingConvention = CallingConvention.StdCall)]
        public static Int32 GroupAdmin(int subType, int sendTime, long fromGroup, long beingOperateQQ)
        {
            // 處理群事件-管理員變動。
            //CQ.SendGroupMessage(fromGroup, String.Format("[{0}]{2}({1})被{3}管理員權限。", CQ.ProxyType, beingOperateQQ, CQ.GetQQName(beingOperateQQ), subType == 1 ? "取消了" : "設置為"));
            return 0;
        }

        [DllExport("_eventSystem_GroupMemberDecrease", CallingConvention = CallingConvention.StdCall)]
        public static Int32 GroupMemberDecrease(int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
        {
            // 處理群事件-群成員減少。
            //CQ.SendGroupMessage(fromGroup, String.Format("[{0}]群員{2}({1}){3}", CQ.ProxyType, beingOperateQQ, CQ.GetQQName(beingOperateQQ), subType == 1 ? "退群。" : String.Format("被{0}({1})踢除。", CQ.GetQQName(fromQQ), fromQQ)));
            return 0;
        }

        [DllExport("_eventSystem_GroupMemberIncrease", CallingConvention = CallingConvention.StdCall)]
        public static Int32 GroupMemberIncrease(int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
        {
            // 處理群事件-群成員增加。
            //CQ.SendGroupMessage(fromGroup, String.Format("[{0}]群裡來了新人{2}({1})，管理員{3}({4}){5}", CQ.ProxyType, beingOperateQQ, CQ.GetQQName(beingOperateQQ), CQ.GetQQName(fromQQ), fromQQ, subType == 1 ? "同意。" : "邀請。"));
            return 0;
        }

        [DllExport("_eventFriend_Add", CallingConvention = CallingConvention.StdCall)]
        public static Int32 FriendAdded(int subType, int sendTime, long fromQQ)
        {
            // 處理好友事件-好友已添加。
            // CQ.SendPrivateMessage(fromQQ, String.Format("[{0}]你好，我的朋友！", CQ.ProxyType));
            return 0;
        }

        [DllExport("_eventRequest_AddFriend", CallingConvention = CallingConvention.StdCall)]
        public static Int32 RequestAddFriend(int subType, int sendTime, long fromQQ, string msg, string responseFlag)
        {
            // 處理請求-好友添加。
            // CQ.SetFriendAddRequest(responseFlag, CQReactType.Allow, "新來的朋友");
            return 0;
        }

        [DllExport("_eventRequest_AddGroup", CallingConvention = CallingConvention.StdCall)]
        public static Int32 RequestAddGroup(int subType, int sendTime, long fromGroup, long fromQQ, string msg, string responseFlag)
        {
            // 處理請求-群添加。
            // CQ.SetGroupAddRequest(responseFlag, CQRequestType.GroupAdd, CQReactType.Allow, "新群友");
            return 0;
        }

        [DllExport("_menuA", CallingConvention = CallingConvention.Cdecl)]
        public static Int32 _menuA()
        {
            return 0;
        }

        [DllExport("_menuA", CallingConvention = CallingConvention.Cdecl)]
        public static Int32 _menuB()
        {
            return 0;
        }

        [DllExport("_set", CallingConvention = CallingConvention.Cdecl)]
        public static Int32 set()
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
        //  CQAPI.AddLog(_AuthCode, 1, 10, "啟動設置頁");
            return 0;
        }

        [DllExport("_about", CallingConvention = CallingConvention.Cdecl)]
        public static Int32 about()
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
          //  CQAPI.AddLog(_AuthCode, 1, 10, "启动关于页");
            return 0;
        }
    }
}
