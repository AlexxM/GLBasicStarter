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
using Android.Graphics;
using System.IO;
using SimpleAndroidGame.Framework.Interfaces;
using Android.Content.Res;
using Android.Util;
namespace SimpleAndroidGame.Framework
{
    class AndroidGraphics : IGraphics
    {
        AssetManager _am;
        Bitmap _frameBuffer;
        Canvas _canvas;
        Paint _paint=new Paint();
        Rect _srcRect = new Rect();
        Rect _dstRect = new Rect();


        public AndroidGraphics(AssetManager am, Bitmap frameBuffer)
        {
            _am = am;
            _frameBuffer = frameBuffer;
            _canvas = new Canvas(frameBuffer);
        
        }
        
        public int Width
        {
            get { return _frameBuffer.Width; }
        }

        public int Height
        {
            get { return _frameBuffer.Height; }
        }

        public IPixmap NewPixmap(string fileName, PixmapFormat format)
        {
            Bitmap.Config config = null;
            if (format == PixmapFormat.RGB565)
                config = Bitmap.Config.Rgb565;
            else if (format == PixmapFormat.ARGB4444)
                config = Bitmap.Config.Argb4444;
            else
                config = Bitmap.Config.Argb8888;

            BitmapFactory.Options opt = new BitmapFactory.Options();
            opt.InPreferredConfig = config;

            Stream s = null;
            Bitmap bitmap = null;
            try
            {

                s = _am.Open(fileName);
                bitmap = BitmapFactory.DecodeStream(s);
                if (bitmap == null)
                {
                    throw new AndroidRuntimeException("Couldn't load bitmap from asset" + fileName);
                }

            }
            catch
            {
                throw new AndroidRuntimeException("Couldn't load bitmap from asset" + fileName);

            }
            finally
            {
                if (s != null)
                {
                    try 
                    {
                        s.Close(); 
                    }
                    catch{}
                }
            
            }

            if (bitmap.GetConfig() == Bitmap.Config.Rgb565)
                format = PixmapFormat.RGB565;
            else if (bitmap.GetConfig() == Bitmap.Config.Argb4444)
                format = PixmapFormat.ARGB4444;
            else
                format = PixmapFormat.ARGB8888;

            return new AndroidPixmap(bitmap,format);
            
        }

        public void Clear(int color)
        {
            _canvas.DrawRGB((color & 0xff0000) >> 16, (color & 0xff00) >> 8,(color & 0xff));
        }

        public void DrawPixel(int x, int y, int color)
        {

            //bad
            _paint.Color = new Color(color);
            _canvas.DrawPoint(x,y,_paint);
        }

        public void DrawLine(int x, int y, int x2, int y2, int color)
        {
            _paint.Color = new Color(color);
            _canvas.DrawLine(x, y, x2, y2, _paint);
        }

        public void DrawRect(int x, int y, int width, int height, int color)
        {
            _paint.Color = new Color(color);
            _paint.SetStyle(Paint.Style.Fill);
            _canvas.DrawRect(x,y,x+width-1,y+width-1,_paint);
        }

        public void DrawPixmap(IPixmap pixmap, int x, int y, int srcX, int srcY, int srcWidth, int srcHeight)
        {
            _srcRect.Left = srcX;
            _srcRect.Top = srcY;
            _srcRect.Right = srcX + srcWidth - 1;
            _srcRect.Bottom = srcY + srcHeight - 1;

            _dstRect.Left = x;
            _dstRect.Top = y;
            _dstRect.Right = x + srcWidth - 1;
            _dstRect.Bottom = y + srcHeight - 1;

            _canvas.DrawBitmap(((AndroidPixmap)pixmap).bitmap, _srcRect, _dstRect, null);
			//test;
			pixmap = null;
        }


        public void DrawPixmap(IPixmap pixmap, int x, int y)
        {
            _canvas.DrawBitmap(((AndroidPixmap)pixmap).bitmap, x, y, null);
			//test
			pixmap = null;
        }


		public void DrawTextLine(IPixmap pixmap,string line,int x,int y)
		{
			for (int i = 0; i < line.Length; i++)
			{
				char c = line[i];
				if (c == ' ')
				{
					x += 20;
					continue;
				}

				int srcX = 0;
				int srcWidth = 0;
				if (c == '.')
				{
					srcX = 392;
					srcWidth = 25;
				}
				else
				{
					srcX = (c - '0') * 39;
					srcWidth = 39;

				}

				DrawPixmap(pixmap, x, y, srcX, 0, srcWidth, 55);
				x += srcWidth;
				x += 0;

			}


		}

      
    }
}