using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SinaimgPublisher.Property
{
    public class SinaimgPublisherConfig
    {

        public static string GetAppFolder()
        {
            switch (HandlerProperty.robot)
            {
                case RobotType.MPQ:
                    return "Plugin";
                case RobotType.CQ:
                    return "app";
                default:
                    return "";
            }
        }

        public static void LoadProperty()
        {
            try
            {
                if (File.Exists(GetAppFolder() + @"\SinaimgPublisherConfig.json"))
                {
                    string readText = File.ReadAllText(GetAppFolder() + @"\SinaimgPublisherConfig.json");
                    List<SetProperty> tmp = JsonConvert.DeserializeObject<List<SetProperty>>(readText);
                    if (tmp.Count > 0)
                    {
                            HandlerProperty.SProperty = tmp.FirstOrDefault();
                    }
                }
                else
                {
                    SaveProperty();
                }
            }
            catch(Exception e) { Console.WriteLine(e); }
        }

        public static void SaveProperty()
        {
            try
            {
                List<SetProperty> tmp = new List<SetProperty>();
                tmp.Add(HandlerProperty.SProperty);
                var json = JsonConvert.SerializeObject(tmp);
                File.WriteAllText(GetAppFolder() + @"\SinaimgPublisherConfig.json", json);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }


        }
}
