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
    class SingleTouchHandler : Java.Lang.Object,ITouchHandler
    {

        bool _isTouched;

        int _touchX;

        int _touchY;

        Pool<TouchEvent> _touchEventPool;

        List<TouchEvent> _touchEvents = new List<TouchEvent>();

        List<TouchEvent> _touchEventBuffer = new List<TouchEvent>();

        float _scaleX;

        float _scaleY;

        public SingleTouchHandler(View v,float scaleX, float scaleY)
        {
            _touchEventPool = new Pool<TouchEvent>(new PoolObjectFactory<TouchEvent>(),20);
             v.SetOnTouchListener(this);
             _scaleX = scaleX;
             _scaleY = scaleY;
        }

        public bool IsTouchDown(int pointer)
        {
            lock(this)
            {
                if (pointer == 0)
                {
                    return _isTouched;
                }
                else
                {
                    return false;
                }
            }
        }

        public int TouchX(int pointer)
        {
            lock(this)
            {
                return _touchX;
            }
        }

        public int TouchY(int pointer)
        {
            lock (this)
            {
                return _touchY;
            }
        }

        public List<TouchEvent> GetTouchEvents()
        {
            lock (this)
            {
                int l = _touchEvents.Count;
                for (int i = 0; i < l; i++)
                {
                    _touchEventPool.Add(_touchEvents[i]); 
                    
                }
                _touchEvents.Clear();
                _touchEvents.AddRange(_touchEventBuffer);
                _touchEventBuffer.Clear();
                return _touchEvents;
            }
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            lock (this)
            {
                TouchEvent te = _touchEventPool.NewObject();
                switch (e.Action)
                {
                    case MotionEventActions.Down: te.type = TouchEvent.TOUCH_DOWN; _isTouched=true; break;
                    case MotionEventActions.Move: te.type = TouchEvent.TOUCH_DRAGGED; break;
                    case MotionEventActions.Cancel:
                    case MotionEventActions.Up: { te.type = TouchEvent.TOUCH_UP; _isTouched = false; break; }
                }
                te.x = _touchX = (int)(e.GetX() * _scaleX);
                te.y=_touchY=(int)(e.GetY()*_scaleY);
                _touchEventBuffer.Add(te);
            }
            return true;
        }


    }
}