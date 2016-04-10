using System;
using System.Collections.Generic;
using System.Drawing;
using SmartAnalytics.Services.HeatMap;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Snapshot;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using SmartAnalytics.Services.Extendsions;

namespace SmartAnalytics.Services.Impl
{
    public class HeatMapService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(HeatMapService));

        private const string ApiFormat = "http://tongjiapi.wybi.net/thermodynamicDiagram/index.jsp?protype=1&url={0}&beginDate={1}&endDate={2}&screenWidth={3}&screenHeight={4}";

        public byte[] GetHeatMapImage(string beginTime, string endTime, string url, string urlEncoding, int screenWidth, int screenHeight)
        {
            /*
             * 1.获取Url网页快照
             * 2.查询数据库访客点击表记录
             * 3.校正偏移访客点击坐标点数据
             * 4.绘制访客点击热力图
             * 5.缓存查询结果并返回热力图
             */

            try
            {
                //获取网页快照
                var webPageImage = WebPreview.GetWebPreview(new Uri(url), 30000, screenWidth, screenHeight, true);

                //设置heatPoint
                var apiUrl = string.Format(ApiFormat, urlEncoding, beginTime, endTime, screenWidth, screenHeight);

                var json = GetHtmlSource(apiUrl);

                var heatMapMessage = JsonConvert.DeserializeObject<List<HeatMapMessage>>(json);

                var heatPoints = new List<HeatPoint>();
                if (heatMapMessage != null)
                {
                    heatPoints = heatMapMessage.Select(m => new HeatPoint { W = 1, X = m.X, Y = m.Y }).ToList();
                }

                //生成热力图
                var heatMapMaker = new HeatMapMaker
                {
                    Width = webPageImage.Width,
                    Height = webPageImage.Height,
                    Radius = 20,
                    ColorRamp = ColorRamp.THERMAL,
                    HeatPoints = heatPoints,
                    Opacity = 0.8f
                };

                var heatMapImage = heatMapMaker.MakeHeatMap().Result;

                //场景图与热力图合并
                using (var g = Graphics.FromImage(webPageImage))
                {
                    var rectangle = new Rectangle(0, 0, webPageImage.Width, webPageImage.Height);
                    g.DrawImage(heatMapImage, rectangle);
                }

                //加入缓存
                var imageBytes = webPageImage.ToImageBytes();

                webPageImage.Dispose();

                return imageBytes;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                return null;
            }
        }

        private static string GetHtmlSource(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var response = (HttpWebResponse)request.GetResponse();

            using (var stream = response.GetResponseStream())
            {
                if (stream != null)
                {
                    var reader = new StreamReader(stream, Encoding.Default);
                    return reader.ReadToEnd();
                }
            }

            return String.Empty;
        }
    }
}
