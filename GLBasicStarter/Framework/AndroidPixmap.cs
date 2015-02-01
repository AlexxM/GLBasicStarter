using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimpleAndroidGame.Framework.Interfaces;
using Android.Graphics;
namespace SimpleAndroidGame.Framework
{
    class AndroidPixmap : IPixmap
    {
        public Bitmap bitmap;
        PixmapFormat _format;

        public AndroidPixmap(Bitmap bitmap, PixmapFormat format)
        {
            this.bitmap = bitmap;
            _format = format;
        }


        public int Width
        {
            get { return bitmap.Width; }
        }

        public int Height
        {
            get { return bitmap.Height; }
        }

        public PixmapFormat Format
        {
            get { return _format; }
        }

        public void Dispose()
        {
            bitmap.Recycle();
        }
    }
}