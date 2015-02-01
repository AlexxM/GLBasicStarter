using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.Content.Res;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimpleAndroidGame.Framework.Interfaces;
namespace SimpleAndroidGame.Framework
{
    [Activity]
    public abstract class AndroidGame : Activity,IGame
    {
        AndroidFastRenderView _renderView;
        IGraphics _graphics;
        IAudio _audio;
        IInput _input;
        IFileIO _fileIO;
        Screen _screen;
        PowerManager.WakeLock _wl;

        public Screen CurrentScreen
        {
            get { return _screen; }
        }

        public IInput Input
        {
            get { return _input; }
        }

        public IFileIO FileIO
        {
            get { return _fileIO; }
        }

        public IGraphics Graphics
        {
            get { return _graphics; }
        }

        public IAudio Audio
        {
            get { return _audio; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
            bool isLandscape = this.Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape;

            int frameBufferWidth = isLandscape ? 960 : 540;
            int frameBufferHeight = isLandscape ? 540 : 960;

            Bitmap frameBuffer = Bitmap.CreateBitmap(frameBufferWidth, frameBufferHeight, Bitmap.Config.Rgb565);

            float scaleX = (float)frameBufferWidth / this.WindowManager.DefaultDisplay.Width;
            float scaleY = (float)frameBufferHeight / this.WindowManager.DefaultDisplay.Height;

            _renderView = new AndroidFastRenderView(this,frameBuffer);
            _graphics = new AndroidGraphics(Assets,frameBuffer);
            _fileIO = new AndroidFileIO(Assets);
            _audio = new AndroidAudio(this);
            _input = new AndroidInput(this,_renderView,scaleX,scaleY);
            _screen = GetStartScreen();
            SetContentView(_renderView);

            PowerManager pw = (PowerManager)GetSystemService(Context.PowerService);
            _wl = pw.NewWakeLock(WakeLockFlags.Full,"GLGame");
            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            _wl.Acquire();
            _screen.Resume();
            _renderView.Resume();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _wl.Release();
            _renderView.Pause();
            _screen.Pause();
            if (IsFinishing)
                _screen.Dispose();
        }

        public void SetScreen(Screen screen)
        {
            if (screen == null)
                throw new ArgumentNullException();

            _screen.Pause();
            _screen.Dispose();
            screen.Resume();
            screen.Update(0);
            _screen = screen;
        }

        
		public abstract Screen GetStartScreen();
    }
}