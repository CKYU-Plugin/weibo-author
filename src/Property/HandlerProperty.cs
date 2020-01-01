using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Property
{
    public class HandlerProperty
    {
        public static BindingList<ResponseRule> BindingSetRR = new BindingList<ResponseRule>();
        public static SetProperty SProperty = new SetProperty();
        public static RobotType robot = RobotType.MPQ;
        public static int CQ_AuthCode = 0;
        public static string DefaultResponse =
@"反查圖片結果
-----------------------------
";
    }

    public class ResponseRule
    {
        private string _rtypeString;
        private string _qtypeString;

        [System.ComponentModel.DisplayName("規則E")]
        public RuleType rtype { get; set; }
        [System.ComponentModel.DisplayName("來源E")]
        public QType qtype { get; set; }

        [System.ComponentModel.DisplayName("規則")]
        public string rtypeString
        {
            get
            {
                switch (rtype)
                {
                    case RuleType.All:
                        return "所有";
                    case RuleType.Whitelist:
                        return "白名單";
                    case RuleType.Blacklist:
                        return "黑名單";
                    case RuleType.Undefined:
                        return "未定義";
                }
                return "";
            }
            set
            {
                _rtypeString = value;
            }
        }
        [System.ComponentModel.DisplayName("來源")]
        public string qtypeString
        {
            get
            {
                switch (qtype)
                {
                    case QType.All:
                        return "所有";
                    case QType.Qq:
                        return "個人";
                    case QType.Group:
                        return "群組";
                    case QType.Undefined:
                        return "未定義";
                }
                return "";
            }
            set
            {
                _qtypeString = value;
            }
        }

        [System.ComponentModel.DisplayName("號碼")]
        public string qq { get; set; }
        [System.ComponentModel.DisplayName("名稱")]
        public string Name { get; set; }

    }

    public enum QType : int
    {
        [Description("未定義")]
        Undefined = -1,
        [Description("個人")]
        Qq = 0,
        [Description("群組")]
        Group = 1,
        [Description("所有")]
        All = 99
    }

    public enum RuleType : int
    {
        [Description("未定義")]
        Undefined = -1,
        [Description("黑名單")]
        Blacklist = 0,
        [Description("白名單")]
        Whitelist = 1,
        [Description("所有")]
        All = 99
    }

    public enum RuleTypeColor : uint
    {
        Undefined = 0xFFE8F539,
        Blacklist = 0xFFF34A83,
        Whitelist = 0xFF89FF89,
    }


    public enum RobotType : int
    {
        CQ = 0,
        MPQ = 1,
        Amanda = 2,
        IRQQ = 3,
        Huajing = 4,
        QY = 5
    }

    public  class SetProperty
    {
        public string CurrentQObject { get; set; } = "";
        public QType CustomizeRules_QType { get; set; } = QType.All;
        public QType CustomizeRulesCurrentObject_QType { get; set; } = QType.Qq;
        public RuleType CustomizeRules_RuleType { get; set; } = RuleType.All;
        public RuleType CustomizeRulesCurrentObject_RuleType { get; set; } = RuleType.Undefined;
        public List<ResponseRule> ResponseRules { get; set; } = new List<ResponseRule>();
        public bool isActiveKeywork { get; set; } = false;
        public bool isActiveResponse { get; set; } = false;
        public bool isActiveResponseErrorMessage { get; set; } = false;
        public bool isActiveResponseIncorrectMessage { get; set; } = false;
        public bool isActiveCustomizeRules { get; set; } = false;
        public bool isActiveCustomizeRulesOtherwise { get; set; } = false;

        public bool isActiveAutocreate { get; set; } = false;
        public bool isActiveNewUndefinedListToBlackList { get; set; } = false;
        public bool isActiveUndefinedListNotAccept { get; set; } = false;

        public bool isAtMe { get; set; } = false;
        public bool isuid { get; set; } = false;
        public bool isimg { get; set; } = false;
        public bool isimg_name { get; set; } = true;
        public bool isimg_url { get; set; } = false;
        public bool isprofile_url { get; set; } = true;
        public bool isscreen_name { get; set; } = true;
        public bool isdescription { get; set; } = true;
        public bool isgender { get; set; } = true;
        public bool isfollow { get; set; } = true;
        public bool isfollower { get; set; } = true;
        public bool isurank { get; set; } = false;
        public bool ismbtype { get; set; } = false;
        public bool ismbrank { get; set; } = false;
        public bool isverified { get; set; } = false;
        public bool isverified_type { get; set; } = false;
        public string Keyword { get; set; } = "#微博圖片反查";
        public string Response { get; set; }= 
@"反查圖片結果
-----------------------------
圖片名：{圖片名}
微博網址：{微博網址}
微博名稱：{微博名稱}
備注：{备注}
性别：{性别}
关注：{关注}
粉丝：{粉丝}
";
        public string ResponseErrorMessage { get; set; } = "查询失败";
        public string ResponseIncorrectMessage { get; set; } = "查询格式不正确";
    }
}
