using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.HeatMap
{
    /*
       var hmMaker = new HeatMapMaker
       {
            Width = 800,
            Height = 600,
            Radius = 30,
            ColorRamp = cr,
            HeatPoints = points,
            Opacity = 1
        };
        hmMaker.MakeHeatMap();
        hmMaker.GrayMap;
     */

    /// <summary>
    /// 热力图生成类
    /// </summary>
    public class HeatMapMaker
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Radius { get; set; }
        public float Opacity { get; set; }
        public ColorRamp ColorRamp { get; set; }
        public List<HeatPoint> HeatPoints { get; set; }
        public Bitmap GrayMap { get; private set; }

        public Task<Bitmap> MakeHeatMap()
        {
            return Task.Run(() =>
            {
                var result = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

                this.GrayMap = this.MakeGrayMap().Result; //*****

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        var grayVal = this.GrayMap.GetPixel(x, y);
                        var index = grayVal.A;
                        var color = ColorUtil.GetColorInRamp(index, this.ColorRamp);
                        result.SetPixel(x, y, color);
                    }
                }

                return ColorUtil.AdjustOpacity(result, this.Opacity);
            });
        }

        private Task<Bitmap> MakeGrayMap()
        {
            return Task.Run(() =>
            {
                var result = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
                var graphics = Graphics.FromImage(result);

                var grayRamp = ColorUtil.GetGrayRamp();
                foreach (var point in this.HeatPoints)
                {
                    var r = this.Radius;
                    var rect = new Rectangle((int)point.X - (int)r, (int)point.Y - (int)r, (int)r * 2, (int)r * 2);

                    var path = new GraphicsPath();
                    path.AddEllipse(rect);

                    var brush = new PathGradientBrush(path)
                    {
                        InterpolationColors = grayRamp
                    };

                    graphics.FillEllipse(brush, rect);
                }
                graphics.Dispose();

                return result;
            });
        }
    }
}
