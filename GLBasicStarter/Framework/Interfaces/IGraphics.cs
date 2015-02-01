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

namespace SimpleAndroidGame.Framework.Interfaces
{
    public enum PixmapFormat { ARGB8888, ARGB4444, RGB565 }
    
    public interface IGraphics
    {

        int Width{get;}

        int Height { get; }
        
        IPixmap NewPixmap(string fileName, PixmapFormat format);

        void Clear(int color);

        void DrawPixel(int x, int y, int color);

        void DrawLine(int x,int y,int x2,int y2,int color);

        void DrawRect(int x,int y, int width,int height,int color);

        void DrawPixmap(IPixmap pixmap,int x,int y,int srcX,int srcY,int srcWidth,int srcHeight);

        void DrawPixmap(IPixmap pixmap, int x, int y);

	    void DrawTextLine(IPixmap pixmap,string line,int x,int y);
    
    }
}