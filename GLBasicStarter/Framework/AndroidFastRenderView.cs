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
using Java.Lang;
namespace SimpleAndroidGame.Framework
{

    class AndroidFastRenderView : SurfaceView,IRunnable
    {
        IGame _game;
        Bitmap _frameBuffer;
        Thread _renderThread = null;
        ISurfaceHolder _holder;
        volatile bool _runnig = false;

        public AndroidFastRenderView(AndroidGame game, Bitmap frameBuffer) : base(game)
        {
            this._game = game;
            this._frameBuffer=frameBuffer;
            _holder = this.Holder;

        }


        public void Resume()
        {
            _runnig = true;
            _renderThread = new Thread(this);
            _renderThread.Start();
        
        }

        public void Run()
        {
			Rect dstRect = new Rect();
            long startTime = Java.Lang.JavaSystem.NanoTime();
            while (_runnig)
            {
                if (!_holder.Surface.IsValid)
                    continue;

                float deltaTime = (Java.Lang.JavaSystem.NanoTime() - startTime) / 1000000000f;
                startTime = Java.Lang.JavaSystem.NanoTime();
                _game.CurrentScreen.Update(deltaTime);
                _game.CurrentScreen.Present(deltaTime);
                Canvas c = _holder.LockCanvas();
                c.GetClipBounds(dstRect);
                c.DrawBitmap(_frameBuffer, null, dstRect, null);
				_holder.UnlockCanvasAndPost(c);
            }
           
        }

        public void Pause()
        {
            _runnig = false;
            while (true)
            {
                try
                {
                    _renderThread.Join();
                    break;
                }
                catch
                {
                
                }
 
            }
        
        }
    }
}