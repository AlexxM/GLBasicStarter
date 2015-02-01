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
    class KeyboardHandler : Java.Lang.Object,View.IOnKeyListener
    {

        bool[] _pressedKeys = new bool[128];

        Pool<Interfaces.KeyEvent> _keyEventPool;
        List<Interfaces.KeyEvent> _keyEventsBuffer = new List<Interfaces.KeyEvent>();
        List<Interfaces.KeyEvent> _keyEvents = new List<Interfaces.KeyEvent>();
        
        public KeyboardHandler(View v)
        {
            _keyEventPool = new Pool<SimpleAndroidGame.Framework.Interfaces.KeyEvent>(new PoolObjectFactory<Interfaces.KeyEvent>(), 10);
            v.SetOnKeyListener(this);
            v.Focusable = true;
            v.RequestFocus();
        
        }

        public bool OnKey(View v, Keycode keyCode, Android.Views.KeyEvent e)
        {
            if (e.Action == KeyEventActions.Multiple)
                return false;
            
            lock (this)
            {
                Interfaces.KeyEvent ke = _keyEventPool.NewObject();
                int kcode=(int)keyCode;
                ke.keyCode = kcode;
                ke.keyChar = (char)e.UnicodeChar;
                if (e.Action == KeyEventActions.Down)
                {
                    ke.type = Interfaces.KeyEvent.KEY_DOWN;
                    if (kcode > 0 && kcode < 128)
                    {
                        _pressedKeys[kcode] = true;
                    }
                }

                if (e.Action == KeyEventActions.Up)
                {
                    ke.type = Interfaces.KeyEvent.KEY_UP;
                    if (kcode > 0 && kcode < 128)
                    {
                        _pressedKeys[kcode] = false;
                    }
                }

                _keyEventsBuffer.Add(ke);
            
            }

            return false;
        }

        public bool IsKeyPressed(int kcode)
        {
            if (kcode < 0 || kcode > 127)
                return false;
            return _pressedKeys[kcode];
        
        }

        public List<Interfaces.KeyEvent> GetKeyEvents()
        {
            lock (this)
            {
                int len = _keyEvents.Count;
                for (int i = 0; i < len; i++)
                {
                    _keyEventPool.Add(_keyEvents[i]);
                }
                _keyEvents.Clear();
                _keyEvents.AddRange(_keyEventsBuffer);
                _keyEventsBuffer.Clear();
                return _keyEvents;

            
            }
        
        }

    }
}