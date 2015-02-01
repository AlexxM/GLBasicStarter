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
    public interface IInput
    {
         List<KeyEvent> KeyEvents { get;  }
         List<TouchEvent> TouchEvents {get;}
         bool IsKeyPressed(int keyCode);
         bool IsTouchDown(int pointer);
         int TouchX(int pointer);
         int TouchY(int pointer);
         float AccelX();
         float AccelY();
         float AccelZ();
    }

    public struct KeyEvent
    {
         public const int KEY_DOWN = 0;
         public const int KEY_UP = 1;
         public int type;
         public int keyCode;
         public char keyChar;
    }

    public struct TouchEvent
    {
        public const int TOUCH_DOWN = 0;
        public const int TOUCH_UP = 1;
        public const int TOUCH_DRAGGED = 2;
        public int type;
        public int x, y;
        public int pointer;
    }
}