using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Robot.Code
{
    public class CQAPI
    {

        /// <summary>
        /// 酷Q提供的API，實現發送私聊消息的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="qqid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_sendPrivateMsg", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendPrivateMessage(int ac, long qqid, [MarshalAs(UnmanagedType.LPStr)] [In] string msg);

        /// <summary>
        /// 酷Q提供的API，實現發送群消息的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="grougid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_sendGroupMsg", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendGroupMessage(int ac, long grougid, [MarshalAs(UnmanagedType.LPStr)] [In] string msg);

        /// <summary>
        /// 酷Q提供的API，實現發送討論組消息的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="discussid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_sendDiscussMsg", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendDiscussMessage(int ac, long discussid, [MarshalAs(UnmanagedType.LPStr)] [In] string msg);

        /// <summary>
        /// 酷Q提供的API，實現發贊的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="QQID"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_sendLike", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendLike(int ac, long QQID);

        /// <summary>
        /// 酷Q提供的API，實現發贊的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="QQID"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_sendLikeV2", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendLikeV2(int ac, long groupid, long QQID, bool state);

        /// <summary>
        /// 酷Q提供的API，實現設置管理員的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="QQID"></param>
        /// <param name="setadmin"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupAdmin", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupAdmin(int ac, long groupid, long QQID, int setadmin);

        /// <summary>
        /// 酷Q提供的API，實現群踢人的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="QQID"></param>
        /// <param name="rejectaddrequest"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupKick", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupKick(int ac, long groupid, long QQID, int rejectaddrequest);

        /// <summary>
        /// 酷Q提供的API，實現群成員禁言的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="QQID"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupBan", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupBan(int ac, long groupid, long QQID, long duration);

        /// <summary>
        /// 酷Q提供的API，實現全員禁言的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="enableban"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupWholeBan", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupWholeBan(int ac, long groupid, int enableban);

        /// <summary>
        /// 酷Q提供的API，實現匿名聊天禁言的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="anomymous"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupAnonymousBan", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupAnonymousBan(int ac, long groupid, [MarshalAs(UnmanagedType.LPStr)] [In] string anomymous, long duration);

        /// <summary>
        /// 酷Q提供的API，實現匿名功能設置的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="enableanomymous"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupAnonymous", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupAnonymous(int ac, long groupid, int enableanomymous);

        /// <summary>
        /// 酷Q提供的API，實現修改群名片的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="QQID"></param>
        /// <param name="newcard"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupCard", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupCard(int ac, long groupid, long QQID, [MarshalAs(UnmanagedType.LPStr)] [In] string newcard);

        /// <summary>
        /// 酷Q提供的API，實現退群的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="isdismiss"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupLeave", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupLeave(int ac, long groupid, int isdismiss);

        /// <summary>
        /// 酷Q提供的API，實現群頭銜的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupid"></param>
        /// <param name="QQID"></param>
        /// <param name="newspecialtitle"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupSpecialTitle", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupSpecialTitle(int ac, long groupid, long QQID, [MarshalAs(UnmanagedType.LPStr)] [In] string newspecialtitle, long duration);

        /// <summary>
        /// 酷Q提供的API，實現退出討論組的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="discussid"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setDiscussLeave", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetDiscussLeave(int ac, long discussid);

        /// <summary>
        /// 酷Q提供的API，實現加好友請求處理的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="responseflag"></param>
        /// <param name="responseoperation"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setFriendAddRequest", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetFriendAddRequest(int ac, [MarshalAs(UnmanagedType.LPStr)] [In] string responseflag, int responseoperation, [MarshalAs(UnmanagedType.LPStr)] [In] string remark);

        /// <summary>
        /// 酷Q提供的API，實現加群請求處理的功能。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="responseflag"></param>
        /// <param name="requesttype"></param>
        /// <param name="responseoperation"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setGroupAddRequestV2", CallingConvention = CallingConvention.StdCall)]
        public static extern int SetGroupAddRequestV2(int ac, [MarshalAs(UnmanagedType.LPStr)] [In] string responseflag, int requesttype, int responseoperation, [MarshalAs(UnmanagedType.LPStr)] [In] string reason);

        /// <summary>
        /// CQCProxy類中導出的獲取群成員信息的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="groupNumber"></param>
        /// <param name="qqNumber"></param>
        /// <param name="nocache"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getGroupMemberInfoV2", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetGroupMemberInfo(int ac, long groupNumber, long qqNumber, int nocache);

        /// <summary>
        /// CQCProxy類中導出的獲取陌生人信息的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="qqNumber"></param>
        /// <param name="nocache"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getStrangerInfo", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetStrangerInfo(int ac, long qqNumber, int nocache);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="priority"></param>
        /// <param name="category"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_addLog", CallingConvention = CallingConvention.StdCall)]
        public static extern int AddLog(int ac, long priority, [MarshalAs(UnmanagedType.LPStr)] [In] int category, string content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getRecord", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetRecord(int ac);

        /// <summary>
        /// CQCProxy類中導出的獲取Cookies的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getCookies", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetCookies(int ac);

        /// <summary>
        /// CQCProxy類中導出的獲取Token的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getCsrfToken", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetCsrfToken(int ac);

        /// <summary>
        /// CQCProxy類中導出的獲取登錄QQ的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getLoginQQ", CallingConvention = CallingConvention.StdCall)]
        public static extern long GetLoginQQ(int ac);

        /// <summary>
        /// CQCProxy類中導出的獲取登錄昵稱的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getLoginNick", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetLoginNick(int ac);

        /// <summary>
        /// CQCProxy類中導出的獲取Cookies的方法。
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getAppDirectory", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetCQAppFolder(int ac);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_setFatal", CallingConvention = CallingConvention.StdCall)]
        public static extern string SetGroupFatal(int ac, long GroupID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getGroupMemberList", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetGroupMemberList(int ac, long GroupID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ac"></param>
        /// <returns></returns>
        [DllImport("CQP.dll", EntryPoint = "CQ_getGroupList", CallingConvention = CallingConvention.StdCall)]
        public static extern string GetGroupList(int ac);


    }
}
