using SinaimgPublisher.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SinaimgPublisher.Extension
{
    public static class ResponseRulesController
    {
        private static object ResponseRulesLock = new object();

        public static bool AddorUpdate(ResponseRule _rR)
        {

            lock (ResponseRulesLock)
            {
                List<ResponseRule> tmp = new List<ResponseRule>();
                tmp = HandlerProperty.SProperty.ResponseRules.Where(w => w.qq == _rR.qq && w.qtype == _rR.qtype).ToList();
                if (tmp != null)
                {
                    if (tmp.Count > 0)
                    {
                        tmp.ForEach(f =>
                        {
                            f.rtype = _rR.rtype;
                        });
                        return true;
                    }
                    else
                    {
                        HandlerProperty.SProperty.ResponseRules.Add(_rR);
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool Remove(ResponseRule _rR)
        {
            lock (ResponseRulesLock)
            {
                HandlerProperty.SProperty.ResponseRules.RemoveAll(r => r.qq == _rR.qq & r.qtype == _rR.qtype);
            }
            return true;
        }
        public static bool RemoveAll()
        {
            lock (ResponseRulesLock)
            {
                HandlerProperty.SProperty.ResponseRules = new List<ResponseRule>();
            }
            return true;
        }
        public static ResponseRule Select(string qq, QType qtype)
        {
            lock (ResponseRulesLock)
            {
                return HandlerProperty.SProperty.ResponseRules.Where(w=>w.qq == qq & w.qtype == qtype).DefaultIfEmpty(new ResponseRule { qq = ""}).FirstOrDefault();
            }
        }

        public static List<ResponseRule> Get()
        {
            lock (ResponseRulesLock)
            {
                List<ResponseRule> rr = new List<ResponseRule>();
                if (HandlerProperty.SProperty.ResponseRules != null)
                {
                    if (HandlerProperty.SProperty.ResponseRules.Count > 0)
                    {
                        rr = HandlerProperty.SProperty.ResponseRules.OrderBy(o => o.qtype).OrderBy(o => o.rtype).ToList();
                        return rr;
                    }
                }
                return null;
            }
        }

    }
}
