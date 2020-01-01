using SinaimgPublisher.Extension;
using SinaimgPublisher.Property;
using SinaimgPublisher.Robot.API;
using SinaimgPublisher.Robot.API.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using System.Windows.Forms;

namespace SinaimgPublisher.Robot.Code
{
    public class _API
    {
        public static void SendMessage(string qq, int msgType, string content, string gdid, string robotQQ, int msgSubType)
        {
            long _qq = 0;
            long _gdid = 0;
            Int64.TryParse(qq, out _qq);
            Int64.TryParse(gdid, out _gdid);

            switch (HandlerProperty.robot)
            {
                case RobotType.MPQ:
                    MPQMessageAPI.Api_SendMsg(robotQQ, msgType, msgSubType, gdid, qq, content);
                    break;
                case RobotType.CQ:
                    if (msgType.In(1, 4))
                    {
                        CQAPI.SendPrivateMessage(HandlerProperty.CQ_AuthCode, _qq, content);
                    }
                    else if (msgType.In(2, 3))
                    {
                        CQAPI.SendGroupMessage(HandlerProperty.CQ_AuthCode, _gdid, content);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void GetGroupList_Async(Action<GroupList> callback)
        {
            try
            {
                callback(GetGroupList());
                return;

                List<string> Lbkn = new List<string>();
                List<string> Lcookies = new List<string>();

                switch (HandlerProperty.robot)
                {
                    case RobotType.MPQ:
                        List<string> qlist = new List<string>();
                        qlist = MPQMessageAPI.Api_GetOnlineQQlist().Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                        qlist.ForEach(f =>
                        {
                            if (f != "")
                            {
                             //   WINAPI.MessageBox(new IntPtr(0), f, "QQ", 40000);
                                Lbkn.Add(MPQMessageAPI.Api_GetGtk_Bkn(f));
                                Lcookies.Add(MPQMessageAPI.Api_GetCookies(f));
                            }
                        });
                        break;
                    case RobotType.CQ:
                        Lbkn.Add(CQAPI.GetCsrfToken(HandlerProperty.CQ_AuthCode).ToString());
                        Lcookies.Add(CQAPI.GetCookies(HandlerProperty.CQ_AuthCode));
                        break;
                    default:
                        break;
                }

                for (int i = 0; i < Lbkn.Count; i++)
                {
                    string bkn = Lbkn[0];
                    string cookies = Lcookies[0];
                    var client = new RestClient("http://qun.qq.com");
                    var request = new RestRequest("cgi-bin/qun_mgr/get_group_list", Method.POST);
                    request.AddParameter("bkn", bkn);

                    try
                    {
                        foreach (var s in cookies.Split(';').Select(x => x.Trim()))
                        {
                            var nameValue = s.Split('=');
                            request.AddCookie(nameValue[0], nameValue[1]);
                        }
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    request.JsonSerializer.ContentType = "application/json; charset=utf-8";
                    request.AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    request.AddHeader("Origin", "http://qun.qq.com");
                    //       request.AddHeader("Referer", "http://qun.qq.com/member.html");
                    client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                    client.ExecuteAsync(request, (response) =>
                    {
                        if (response.Content != null && response.Content != "")
                        {
                            GroupList gl = new GroupList();
                            gl = JsonConvert.DeserializeObject<GroupList>(response.Content);
                            gl.GroupInfos.AddRange(gl.CreateInfos.Select(s => new GroupInfo { GroupName = s.GroupName, GroupNumber = s.GroupNumber, OwnerNumber = s.OwnerNumber }).ToList());
                            callback(gl);
                        }
                    });
                }
            }
            catch { }
        }

        public static GroupList GetGroupList()
        {
            GroupList grouplist = new GroupList();
            grouplist.ec = 0;
            grouplist.CreateInfos = new List<CreateInfo>();
            grouplist.GroupInfos = new List<GroupInfo>();
            try
            {
                switch (HandlerProperty.robot)
                {
                    case RobotType.MPQ:
                        List<string> qlist = new List<string>();
                        qlist = MPQMessageAPI.Api_GetOnlineQQlist().Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                        qlist.ForEach(f =>
                        {
                        });
                        return grouplist;
                    case RobotType.CQ:
                        string base64code = System.Convert.ToString(CQAPI.GetGroupList(HandlerProperty.CQ_AuthCode));
                        MessageBox.Show(base64code);
                        var bytes = Convert.FromBase64String(base64code);
                        var text = System.Text.Encoding.Default.GetString(bytes, 0, bytes.Length);

                        List<GroupInfo> gl = new List<GroupInfo>();
                        gl = CQAPI_Extras.GetGroupList(Converter.ByteArrayToHexString(bytes));
                        grouplist.GroupInfos.AddRange(gl);
                        return grouplist;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                WINAPI.MessageBox(new IntPtr(0), ex.ToString(), "ex", 40000);
            }

            return grouplist;
        }


        public static string GetGroupName(string QQ)
        {
            MPQMessageAPI.Api_GetGroupName(MPQMessageAPI.Api_GetRadomOnlineQQ(), QQ);
            return "";
        }
        public static string GetQQName(string QQ)
        {
            CQAPI.GetStrangerInfo(HandlerProperty.CQ_AuthCode, 9999, 0);
            MPQMessageAPI.Api_GetPersonalProfile(MPQMessageAPI.Api_GetRadomOnlineQQ(), QQ);
            return "";
        }
    }
}
