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
namespace SimpleAndroidGame.Framework
{
    class AndroidInput : IInput
    {
        AccelerometerHandler _accelHandler;
        KeyboardHandler _kbHandler;
        ITouchHandler _touchHandler;
        
        //модификация
        public AndroidInput(Context c, View v, float scaleX, float scaleY)
        {
            _accelHandler = new AccelerometerHandler(c);
            _kbHandler = new KeyboardHandler(v);
            _touchHandler = new MultiTouchHandler(v, scaleX, scaleY);
        
        
        }

        public List<Interfaces.KeyEvent> KeyEvents
        {
            get
            {
               return _kbHandler.GetKeyEvents();
            }
        }

        public List<TouchEvent> TouchEvents
        {
            get
            {
                return _touchHandler.GetTouchEvents();
            }
        }

        public bool IsKeyPressed(int keyCode)
        {
            return _kbHandler.IsKeyPressed(keyCode);
        }

        public bool IsTouchDown(int pointer)
        {
            return _touchHandler.IsTouchDown(pointer);
        }

        public int TouchX(int pointer)
        {
            return _touchHandler.TouchX(pointer);
        }

        public int TouchY(int pointer)
        {
            return _touchHandler.TouchY(pointer);
        }

        public float AccelX()
        {
            return _accelHandler.GetX;
        }

        public float AccelY()
        {
            return _accelHandler.GetY;
        }

        public float AccelZ()
        {
            return _accelHandler.GetZ;
        }
    }
}