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
    class MultiTouchHandler : Java.Lang.Object,ITouchHandler
    {

        bool[] _isTouched = new bool[20];
        int[] _touchX = new int[20];
        int[] _touchY = new int[20];
        Pool<TouchEvent> _touchEventPool;

        List<TouchEvent> _touchEvents = new List<TouchEvent>();
        List<TouchEvent> _touchEventsBuffer = new List<TouchEvent>();
        float _scaleX;
        float _scaleY;

        public MultiTouchHandler(View v, float scaleX, float scaleY)
        {
            _touchEventPool = new Pool<TouchEvent>(new PoolObjectFactory<TouchEvent>(), 20);
            v.SetOnTouchListener(this);
            _scaleX = scaleX;
            _scaleY = scaleY;
        }
        
      

        public bool OnTouch(View v, MotionEvent e)
        {
            lock (this)
            {
                MotionEventActions action = e.Action & MotionEventActions.Mask;
                int pointerIndex = e.ActionIndex; 
                int pointerId = e.GetPointerId(pointerIndex);
                TouchEvent te;

                switch (action)
                { 
                    case MotionEventActions.Down:
                    case MotionEventActions.PointerDown:
                        te = _touchEventPool.NewObject();
                        te.type = TouchEvent.TOUCH_DOWN;
                        te.pointer = pointerId;
                        te.x = _touchX[pointerId] = (int)(e.GetX(pointerIndex) * _scaleX);
                        te.y=_touchY[pointerId] = (int)(e.GetY(pointerIndex)*_scaleY);
                        _isTouched[pointerId] = true;
                        _touchEventsBuffer.Add(te);
                        break;
                    case MotionEventActions.Up:
                    case MotionEventActions.PointerUp:
                    case MotionEventActions.Cancel:
                        te = _touchEventPool.NewObject();
                        te.type = TouchEvent.TOUCH_UP;
                        te.pointer = pointerId;
                        te.x = _touchX[pointerId] = (int)(e.GetX(pointerIndex) * _scaleX);
                        te.y=_touchY[pointerId] = (int)(e.GetY(pointerIndex)*_scaleY);
                        _isTouched[pointerId] = false;
                        _touchEventsBuffer.Add(te);
                        break;
                    case MotionEventActions.Move:
                        int pointerCount = e.PointerCount;
                        for (int i = 0; i < pointerCount; i++)
                        {
                            pointerIndex = i;
                            pointerId = e.GetPointerId(pointerIndex);
                            te = _touchEventPool.NewObject();
                            te.type = TouchEvent.TOUCH_DRAGGED;
                            te.pointer = pointerId;
                            te.x=_touchX[pointerId]=(int)(e.GetX(pointerIndex)*_scaleX);
                            te.y=_touchY[pointerId]=(int)(e.GetY(pointerIndex)*_scaleY);
                            _touchEventsBuffer.Add(te);

                        }
                        break;
                }
            }
            return true;
        }

        public bool IsTouchDown(int pointerId)
        {
            lock (this)
            {
                if (pointerId < 0 || pointerId > _isTouched.Length)
                    return false;
                else
                    return _isTouched[pointerId];
            }
        
        }


        public int TouchX(int pointerId)
        {
            lock (this)
            {
                if (pointerId < 0 || pointerId >= _touchX.Length)
                    return 0;
                else
                    return _touchX[pointerId];
            }
        }
        
        public int TouchY(int pointerId)
        {
            lock (this)
            {
                if (pointerId < 0 || pointerId >= _touchY.Length)
                    return 0;
                else
                    return _touchY[pointerId];
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
                _touchEvents.AddRange(_touchEventsBuffer);
                _touchEventsBuffer.Clear();
                return _touchEvents;
            
            }
        
        }

    }
}