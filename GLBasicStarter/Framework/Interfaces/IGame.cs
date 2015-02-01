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
    public interface IGame
    {
        IInput Input {get;}
        IFileIO FileIO { get; }
        IGraphics Graphics { get; }
        IAudio Audio { get; }
        void SetScreen(Screen screen);
        Screen CurrentScreen { get; }
        Screen GetStartScreen();
    }
}