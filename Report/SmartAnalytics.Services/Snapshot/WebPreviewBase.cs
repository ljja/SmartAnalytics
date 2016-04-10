using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartAnalytics.Services.Snapshot
{
    class WebPreviewBase : IDisposable
    {
        Uri _uri = new Uri("about:blank");//原始uri
        int _thumbW = 1024;     //期望的缩略图高度
        int _thumbH = 768;      //期望的缩略图宽度
        int _width;                  //实际抓图宽度
        int _height;                  //实际抓图高度
        readonly WebBrowser _wb;         //浏览器对象
        readonly bool _fullpage = false; //是否抓取全图


        public WebPreviewBase(Uri uri, int thumbW, int thumbH, bool fullpage)
        {
            _wb = new WebBrowser
            {
                ScriptErrorsSuppressed = false,
                ScrollBarsEnabled = false,
                Size = new Size(1024, 768)
            };

            _wb.NewWindow += CancelEventHandler;
            _wb.DocumentCompleted += DocCompletedEventHandler;

            _thumbW = thumbW;
            _thumbH = thumbH;
            _uri = uri;
            _fullpage = fullpage;
        }

        /// <summary>
        /// URI 地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int ThumbWidth { get; set; }

        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int ThumbHeight { get; set; }

        //防止弹窗
        private static void CancelEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        //如果是全屏模式，则缩略图调整为网页实际高度和宽度
        private void DocCompletedEventHandler(object sender, EventArgs e)
        {
            _width = _wb.Width;
            _height = _wb.Height;
            var sz = _wb.Size;

            if (_fullpage && _wb.Document != null && _wb.Document.Body != null)
            {
                _height = _wb.Document.Body.ScrollRectangle.Height;
                _width = _wb.Document.Body.ScrollRectangle.Width;
            }
            // 最小宽度不能小于缩略图宽度
            if (_width < _thumbW)
                _width = _thumbW;

            // 调整最小高度，充满浏览器
            if (_height < sz.Height)
                _height = sz.Height;

            _wb.Size = new Size(_width, _height);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected void InitComobject()
        {
            try
            {
                _wb.Navigate(this._uri);
                //因为没有窗体，所以必须如此
                while (_wb.ReadyState != WebBrowserReadyState.Complete)
                {
                    //立即重绘
                    Application.DoEvents();
                }
                //这句最好注释，不然网页上的动画都抓不到了
                _wb.Stop();
                if (_wb.ActiveXInstance == null) throw new Exception("实例不能为空");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Web预览图
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetWebPreview()
        {
            InitComobject();//这个过程是需要等待的
            //构造snapshot类，抓取浏览器ActiveX的图象
            var snap = new Snapshot();

            var thumBitmap = snap.TakeSnapshot(_wb.ActiveXInstance, new Rectangle(0, 0, _width, _height));

            //调整图片大小，这里选择以宽度为标准，高度保持比例
            thumBitmap = (Bitmap)ImageLibrary.ResizeImageToAFixedSize(thumBitmap, _thumbW, _thumbH, ImageLibrary.ScaleMode.W);

            return thumBitmap;
        }

        public void Dispose()
        {
            _wb.Dispose();
        }

    }
}
