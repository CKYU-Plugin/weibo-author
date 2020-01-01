using Newtonsoft.Json;
using System.Collections.Generic;

namespace SinaimgPublisher.Robot.API.Http
{
    public class GroupList
    {
        [JsonProperty("ec")]
        public int ec { get; set; }

        [JsonProperty("create")]
        public List<CreateInfo> CreateInfos { get; set; }

        [JsonProperty("join")]
        public List<GroupInfo> GroupInfos { get; set; }
    }

    public class CreateInfo
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonProperty("gc")]
        public long GroupNumber { get; set; }

        /// <summary>
        /// 群名稱
        /// </summary>
        [JsonProperty("gn")]
        public string GroupName { get; set; }

        /// <summary>
        /// 群主QQ
        /// </summary>
        [JsonProperty("owner")]
        public long OwnerNumber { get; set; }
    }
    public class GroupInfo
    {
        /// <summary>
        /// 群號
        /// </summary>
        [JsonProperty("gc")]
        public long GroupNumber { get; set; }

        /// <summary>
        /// 群名稱
        /// </summary>
        [JsonProperty("gn")]
        public string GroupName { get; set; }

        /// <summary>
        /// 群主QQ
        /// </summary>
        [JsonProperty("owner")]
        public long OwnerNumber { get; set; }
    }

}
