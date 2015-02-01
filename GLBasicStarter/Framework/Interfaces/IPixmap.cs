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
    //модификация
    public interface IPixmap : IDisposable
    {
        int Width { get; }
        int Height { get; }
        PixmapFormat Format { get; }
    
    
    }
}