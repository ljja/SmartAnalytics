using System;
using System.Drawing;
using System.Threading;

namespace SmartAnalytics.Services.Snapshot
{
    public class WebPreview
    {
        private readonly Uri _uri;
        private Exception _ex;
        private Bitmap _bitmap;
        private readonly int _timeout = 50 * 1000;//设置线程超时时长
        private readonly int _width = 200;//缩图宽
        private readonly int _height = 150;//缩图高
        private readonly bool _fullPage = true;

        private WebPreview(Uri uri, int timeout = 50000, int width = 1024, int height = 768, bool fullPage = true)
        {
            _uri = uri;
            _timeout = timeout;
            _width = width;
            _height = height;
            _fullPage = fullPage;
        }

        internal Bitmap GetWebPreview()
        {
            //Asp.Net引用Winform（类似ActiveX）控件，必须开单线程
            var t = new Thread(StaRun);
            t.SetApartmentState(ApartmentState.STA);
            t.Start(this);

            if (!t.Join(_timeout))
            {
                t.Abort();
                throw new TimeoutException();
            }

            if (_ex != null) throw _ex;
            if (_bitmap == null) throw new ExecutionEngineException();

            return _bitmap;
        }

        public static Bitmap GetWebPreview(Uri uri)
        {
            var wp = new WebPreview(uri);
            return wp.GetWebPreview();
        }

        public static Bitmap GetWebPreview(Uri uri, int timeout, int width, int height, bool fullPage)
        {
            var wp = new WebPreview(uri, timeout, width, height, fullPage);
            return wp.GetWebPreview();
        }

        /// <summary>
        /// 为WebBrowser所开线程的启动入口函数，相当于Winform里面的Main()
        /// </summary>
        /// <param name="_wp"></param>
        private static void StaRun(object _wp)
        {
            if (_wp == null) throw new ArgumentNullException("_wp");
            var wp = (WebPreview)_wp;
            try
            {
                var wpb = new WebPreviewBase(wp._uri, wp._width, wp._height, wp._fullPage);
                wp._bitmap = wpb.GetWebPreview();
            }
            catch (Exception ex)
            {
                wp._ex = ex;
            }
        }
    }
}
