using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using renmengye.Base62;
using RestSharp;
using RestSharp.Authenticators;
using SinaimgPublisher.Robot.Code;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using SinaimgPublisher.Property;
using SinaimgPublisher.Extension;

namespace SinaimgPublisher
{
    public class Sinaimg
    {
        public long FromBase(string input)
        {
            string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            double output = 0;
            var len = input.Length - 1;

            for (int i = 0; i < input.Length; i++)
            {
                output += alphabet.IndexOf(input.Substring(i, 1)) * Math.Pow(62, len - i);
            }
            return (long)output;
        }

        public enum FindUidType : int
        {
            Not_Match = -1,
            Not_Found = 0,
            Found = 1
        }

        public FindUidType Sinaimg_FindUid(string Content, ref string Uid, ref string Imagename, ref string Imageurl)
        {
            try
            {
                Uri uriResult;
                string imageName="";

                bool isuri = Uri.TryCreate(Content, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                if (isuri)
                {
                    if (!uriResult.Host.EndsWith("sinaimg.cn")) return  FindUidType.Not_Match;
                    if (uriResult.AbsolutePath == "") return FindUidType.Not_Match;
                    imageName = System.IO.Path.GetFileNameWithoutExtension(uriResult.AbsolutePath);
                }
                else
                {
                    imageName = System.IO.Path.GetFileNameWithoutExtension(Content);
                }

                if (imageName == "") return FindUidType.Not_Match;
                if (imageName.Length<8) return FindUidType.Not_Match;

                Match match = Regex.Match(imageName, "([A-Za-z0-9])\\w+");
                if (!match.Success) return FindUidType.Not_Match;
                imageName = match.Value;

                //Ref Vaule update
                Imageurl = isuri ? uriResult.AbsolutePath : @"https://wx3.sinaimg.cn/mw690/" + imageName + System.IO.Path.GetExtension(Content);
                Imagename = imageName;

                if (imageName.StartsWith("00"))
                {
                    long result = FromBase(imageName.Substring(0, 8));
                    Uid = result.ToString();
                }
                else
                {
                    uint intresult;
                    bool ishex = UInt32.TryParse(imageName.Substring(0, 8), System.Globalization.NumberStyles.HexNumber, null, out intresult);
                    if (!ishex) return FindUidType.Not_Match;
                    Uid = intresult.ToString();
                }
                return FindUidType.Found;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return FindUidType.Not_Found;
            }
        }

    
        public void Sinaimg_GetUserProfile(string Uid, Action<WeiboData> callback)
        {
            try
            {
                var client = new RestClient("https://m.weibo.cn");
                var request = new RestRequest("api/container/getIndex", Method.GET);
                request.AddParameter("type", "uid");
                request.AddParameter("value", Uid);
                request.JsonSerializer.ContentType = "application/json; charset=utf-8";

                client.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) CriOS/56.0.2924.75 Mobile/14E5239e Safari/602.1";

                client.ExecuteAsync(request, (response) =>
                {
                    callback(JsonConvert.DeserializeObject<WeiboData>(response.Content));
                });
            }
            catch(Exception e) { Console.WriteLine(e); }
        }

        public string GetGenderString(string gender)
        {
            switch (gender)
            {
                case "m":
                    return "男";
                case "f":
                    return "女";
                default:
                    return gender;
            }
        }

        public string GetMbtypeString(long mbtype)
        {
            switch (mbtype)
            {
                case 0:
                    return "非会员";
                case 11:
                    return "微博会员";
                default:
                    return "unknown";
            }
        }

        public string GetVerified_typeString(long verified_type)
        {

            switch (verified_type)
            {
                case -1:
                    return "普通用户";
                case 0:
                    return "名人";
                case 1:
                    return "政府";
                case 2:
                    return "企业";
                case 3:
                    return "媒体";
                case 4:
                    return "校园";
                case 5:
                    return "网站";
                case 6:
                    return "应用";
                case 7:
                    return "机构";
                default:
                    return "unknown";
            }
         }

        public int Run(bool isatme, string qq, int msgType, string msgContent,  string gdid, string robotQQ, int msgSubType)
        {
            Sinaimg s = new Sinaimg();
            string uid = "";
            string imagename = "";
            string imageurl = "";
            QType qtype = QType.Qq;
            ResponseRule rr = new ResponseRule();

            if (msgType == 2 | msgType == 3)
            {
                qtype = QType.Group;
            }

            if (HandlerProperty.SProperty.isActiveAutocreate)
            {
                ResponseRule _rr = new ResponseRule();
                _rr.qq = qq;
                _rr.qtype = (msgType == 2 | msgType == 3) ? QType.Group : QType.Qq;
                _rr.rtype = HandlerProperty.SProperty.isActiveNewUndefinedListToBlackList ? RuleType.Blacklist : RuleType.Undefined;
                _rr.Name = "Loading";
                ResponseRulesController.AddorUpdate(_rr);
            }

            if (HandlerProperty.SProperty.isActiveKeywork)
            {
                if (msgType == 2 | msgType == 3)
                {
                    if (HandlerProperty.SProperty.isAtMe)
                    {
                        if (isatme) return 0;
                    }
                }

                if (HandlerProperty.SProperty.Keyword != null)
                {
                    string _key = msgContent;
                    _key.Replace(" ", "");
                    if (!_key.StartsWith(HandlerProperty.SProperty.Keyword)) return 0;
                }
            }

            if (HandlerProperty.SProperty.isActiveCustomizeRules)
            {
                rr = ResponseRulesController.Select(qq, qtype);
                if (rr.qq != "")
                {
                    if (rr.rtype == RuleType.Blacklist)
                    {
                        return 0;
                    }
                    if (HandlerProperty.SProperty.isActiveUndefinedListNotAccept)
                    {
                        if (rr.rtype == RuleType.Undefined)
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    if (!HandlerProperty.SProperty.isActiveCustomizeRulesOtherwise)
                    {
                        return 0;
                    }
                }
            }

            FindUidType _fut = s.Sinaimg_FindUid(msgContent, ref uid, ref imagename, ref imageurl);

            if (_fut == FindUidType.Found)
            {
                s.Sinaimg_GetUserProfile(uid, (data) =>
                {
                    string content = "";

                    if (msgType == 2 | msgType == 3)
                    {
                        content += _Code.Code_At(qq);
                    }
                    if (HandlerProperty.SProperty.isActiveResponse)
                    {
                        content += HandlerProperty.SProperty.Response;
                    }
                    else
                    {
                        content += HandlerProperty.DefaultResponse;
                        content += HandlerProperty.SProperty.isimg ? "图片：{图片}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isimg_name ? "图片名：{图片名}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isimg_url ? "图片网址：{图片网址}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isuid ? "微博UID：{UID}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isurank ? "微博等级：{微博等级}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isprofile_url ? "微博网址：{微博网址}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isscreen_name ? "微博名称：{微博名称}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isdescription ? "备注：{备注}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isgender ? "性别：{性别}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isfollow ? "关注：{关注}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isfollower ? "粉丝：{粉丝}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.ismbtype ? "会员类型：{会员类型}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.ismbrank ? "会员等级：{会员等级}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isverified ? "是否认证：{是否认证}" + Environment.NewLine : "";
                        content += HandlerProperty.SProperty.isverified_type ? "认证类型：{认证类型}" + Environment.NewLine : "";
                    }

                    content = content.Replace("{UID}", data.userInfo.id.ToString());
                    content = content.Replace("{微博网址}", "https://www.weibo.com/u/" + data.userInfo.id.ToString());
                    content = content.Replace("{图片名}", imagename);
                    content = content.Replace("{图片网址}", imageurl);
                    content = content.Replace("{微博名称}", data.userInfo.screen_name);
                    content = content.Replace("{备注}", data.userInfo.description);
                    content = content.Replace("{性别}", s.GetGenderString(data.userInfo.gender));
                    content = content.Replace("{会员类型}", s.GetMbtypeString(data.userInfo.mbtype));
                    content = content.Replace("{会员等级}", data.userInfo.mbrank.ToString());
                    content = content.Replace("{关注}", data.userInfo.follow_count.ToString());
                    content = content.Replace("{粉丝}", data.userInfo.followers_count.ToString());
                    content = content.Replace("{微博等级}", data.userInfo.urank.ToString());
                    content = content.Replace("{微博等级}", data.userInfo.urank.ToString());
                    content = content.Replace("{是否认证}", data.userInfo.verified ? "是" : "否");
                    content = content.Replace("{认证类型}", s.GetVerified_typeString(data.userInfo.verified_type) + data.userInfo.verified_reason != "" ? String.Format("({0})", data.userInfo.verified_reason) : "");
                    Console.WriteLine(content);
                    try
                    {
                        _API.SendMessage(qq, msgType, content, gdid, robotQQ, msgSubType);
                    }
                    catch (Exception e) { Console.WriteLine(e); }
                });
            }
            else if(_fut == FindUidType.Not_Found)
            {
                if (HandlerProperty.SProperty.isActiveResponseErrorMessage)
                {
                    string content = "";

                    if (msgType == 2 | msgType == 3)
                    {
                        content += _Code.Code_At(qq);
                    }

                    content += HandlerProperty.SProperty.ResponseErrorMessage;
                    try
                    {
                        _API.SendMessage(qq, msgType, content, gdid, robotQQ, msgSubType);
                    }
                    catch (Exception e) { Console.WriteLine(e); }
                }
                return 0;
            }
            else if (_fut == FindUidType.Not_Match)
            {
                if (HandlerProperty.SProperty.isActiveResponseIncorrectMessage)
                {
                    string content = "";

                    if (msgType == 2 | msgType == 3)
                    {
                        content += _Code.Code_At(qq);
                    }

                    content += HandlerProperty.SProperty.ResponseIncorrectMessage;
                    try
                    {
                        _API.SendMessage(qq, msgType, content, gdid, robotQQ, msgSubType);
                    }
                    catch (Exception e) { Console.WriteLine(e); }
                }
                return 0;
            }
            return 0;
        }

        public class WeiboData
        {
            public Userinfo userInfo { get; set; }
            public string fans_scheme { get; set; }
            public string follow_scheme { get; set; }
            public Tabsinfo tabsInfo { get; set; }
            public long ok { get; set; }
            public long showAppTips { get; set; }
            public string scheme { get; set; }
        }

        public class Userinfo
        {
            public long id { get; set; }
            public string screen_name { get; set; }
            public string profile_image_url { get; set; }
            public string profile_url { get; set; }
            public long statuses_count { get; set; }
            public bool verified { get; set; }
            public long verified_type { get; set; }
            public string verified_reason { get; set; }
            public string description { get; set; }
            public string gender { get; set; }
            public long mbtype { get; set; }
            public long urank { get; set; }
            public long mbrank { get; set; }
            public bool follow_me { get; set; }
            public bool following { get; set; }
            public long followers_count { get; set; }
            public long follow_count { get; set; }
            public string cover_image_phone { get; set; }
            public Toolbar_Menus[] toolbar_menus { get; set; }
            public string fans_scheme { get; set; }
            public string follow_scheme { get; set; }
        }

        public class Toolbar_Menus
        {
            public string type { get; set; }
            public string name { get; set; }
            public string pic { get; set; }
            public Params _params { get; set; }
            public string scheme { get; set; }
        }

        public class Params
        {
            public long uid { get; set; }
            public string scheme { get; set; }
        }

        public class Tabsinfo
        {
            public long selectedTab { get; set; }
            public Tab[] tabs { get; set; }
        }

        public class Tab
        {
            public string title { get; set; }
            public string tab_type { get; set; }
            public string containerid { get; set; }
            public string url { get; set; }
        }


    }
}
