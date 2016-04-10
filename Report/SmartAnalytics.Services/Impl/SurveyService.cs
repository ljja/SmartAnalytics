using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class SurveyService : ServiceContext, ISurveyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="day">
        /// 格式：yyyy-MM-dd
        /// </param>
        /// <returns></returns>
        public List<SurveyDomainByDayPageItem> GetSurveyDomainByDayList(DateTime day)
        {
            var query = from p in DbContext.SurveyDomain
                        where p.TotalDate == day
                        group p by p.SiteDomain into tempGroup
                        orderby tempGroup.Sum(p => p.PageView) descending
                        select new SurveyDomainByDayPageItem
                        {
                            SiteDomain = tempGroup.Key,
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            UniqueIp = tempGroup.Sum(p => p.UniqueUser)
                        };

            return query.ToList();
        }

        public List<SurveyDomainByHourPageItem> GetSurveyDomainByHourList(DateTime day)
        {
            var query = from p in DbContext.SurveyDomain
                        where p.TotalDate == day
                        group p by new { p.SiteDomain, p.TotalHour } into tempGroup
                        orderby tempGroup.Sum(p => p.PageView) descending
                        select new SurveyDomainByHourPageItem
                        {
                            SiteDomain = tempGroup.Key.SiteDomain,
                            TotalHour = tempGroup.Key.TotalHour,
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            UniqueIp = tempGroup.Sum(p => p.UniqueUser)
                        };

            var pageResult = query.ToList();

            //循环验证每个Domain
            var allDomain = DbContext.Domain.ToList();
            foreach (var m in allDomain)
            {
                var currentDomain = m.SiteDomain;
                //补齐24个小时段空数据
                for (var i = 0; i <= 23; i++)
                {
                    if (pageResult.Count(p => p.TotalHour == i && p.SiteDomain == currentDomain) == 0)
                    {
                        pageResult.Add(new SurveyDomainByHourPageItem
                        {
                            SiteDomain = currentDomain,
                            TotalHour = i,
                            IsEmpty = true
                        });
                    }
                }
            }

            return pageResult;
        }

        public List<SurveyPageByDayPageItem> GetSurveyPageByDayList(DateTime day, string siteDomain)
        {
            var query = from p in DbContext.SurveyPage
                        where p.TotalDate == day && p.SiteDomain == siteDomain
                        group p by p.Url into tempGroup
                        orderby tempGroup.Sum(p => p.PageView) descending
                        select new SurveyPageByDayPageItem
                        {
                            Url = tempGroup.Key,
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp)
                        };

            return query.ToList();
        }
        /// <summary>
        /// 调用http接口取得数据
        /// </summary>
        private const string StrFomart = "http://tongjiapi.wybi.net/ActualTimeVisitor/getJson?page={0}&pageNum={1}&webName={2}";
        public PagedResult<UserLocalPageItem> GetList(Paging paging, string domain)
        {
            var apiUrl = string.Format(StrFomart, paging.PageIndex+1, paging.PageSize, domain);
            var queryPageResult = new PagedResult<UserLocalPageItem>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
            };
            var json = GetHtmlSource(apiUrl);

            var userLocalMessage = JsonConvert.DeserializeObject<UserLocalMessage>(json);
            queryPageResult.SizeCount = userLocalMessage.sum;

            //queryPageResult.Result = query.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize).ToList();
            queryPageResult.Result = userLocalMessage.data;
            if (queryPageResult.Result == null)
            {
                queryPageResult.Result = new List<UserLocalPageItem>();
            }

            return queryPageResult;
        }
        private static string GetHtmlSource(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        var reader = new StreamReader(stream, Encoding.Default);
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return String.Empty;
        }
    }
}
