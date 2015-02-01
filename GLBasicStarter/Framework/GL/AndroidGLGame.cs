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
using Javax.Microedition.Khronos.Opengles;
using Android.Opengl;
using SimpleAndroidGame.Framework;
using SimpleAndroidGame.Framework.Interfaces;
using SimpleAndroidGame;
namespace SimpleAndroidGame.GL
{
	[Activity]
	public abstract class AndroidGLGame : Activity, IGame,GLSurfaceView.IRenderer
	{
		enum GLGameState {
			Initialized,
			Running,
			Paused,
			Finished,
			Idle
		}

		GLSurfaceView _glView;
		AndroidGLGraphics _glGraphics;
		IInput _input;
		IFileIO _file;
		IAudio _audio;
        Screen _screen;
		GLGameState _state = GLGameState.Initialized;
		Object _stateChanged = new object();
		long _startTime =  Java.Lang.JavaSystem.NanoTime();
		Android.OS.PowerManager.WakeLock _wakeLock;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			RequestWindowFeature (WindowFeatures.NoTitle);
			Window.SetFlags (WindowManagerFlags.Fullscreen,WindowManagerFlags.Fullscreen);
			_glView = new GLSurfaceView (this);
		    //mod
			//_glView.SetEGLConfigChooser (false);
			//_glView.SetEGLConfigChooser(8, 8, 8, 8, 0,0); 

            _glView.SetRenderer(this);
            _input = new AndroidInput(this,_glView,1,1);
            _file = new AndroidFileIO(this.Assets);
			_audio = new AndroidAudio (this);
            _glGraphics = new AndroidGLGraphics(_glView);
			SetContentView(_glView);
            PowerManager pm = (PowerManager)GetSystemService(Service.PowerService);
            _wakeLock = pm.NewWakeLock(WakeLockFlags.Full, "GLGame");

		}

        protected override void OnResume()
        {
            base.OnResume();
            try{
			_glView.OnResume();
            _wakeLock.Acquire();
			}
			catch{
			}
        }


        public IInput Input
        {
            get { return _input; }
        }

        public IFileIO FileIO
        {
            get { return _file; }
        }

        public IGraphics Graphics
        {
            get { throw new InvalidOperationException("Using GLGraphics property"); }
        }

		public AndroidGLGraphics GLGraphics
		{
			get{ return _glGraphics;}
		}

        public IAudio Audio
        {
            get { return _audio; }
        }

        public void SetScreen(Screen screen)
        {
			if (screen == null)
				throw new ArgumentNullException ();
			_screen.Pause ();
			_screen.Dispose ();
			screen.Resume ();
			screen.Update (0);
			_screen = screen;
        }

        public Screen CurrentScreen
        {
            get { return _screen; }
        }

        public void OnDrawFrame(IGL10 gl)
		{
			GLGameState state;
			lock (_stateChanged) {
				state = this._state;
			}

			if (state == GLGameState.Running) {
				float deltaTime = (Java.Lang.JavaSystem.NanoTime () - _startTime) / 1000000f;
				_startTime = Java.Lang.JavaSystem.NanoTime ();
				_screen.Update (deltaTime);
				_screen.Present (deltaTime);
			}

			if (state == GLGameState.Paused) {
				_screen.Pause ();
				lock (_stateChanged) {
					_state = GLGameState.Idle;
					//модификация
				}
			}
		
			if (state == GLGameState.Finished)
			{
				_screen.Pause ();
				_screen.Dispose ();
				lock (_stateChanged)
				{
					_state = GLGameState.Idle;
				}
			}
        }

        public void OnSurfaceChanged(IGL10 gl, int width, int height)
        {
            
        }

        public void OnSurfaceCreated(IGL10 gl, Javax.Microedition.Khronos.Egl.EGLConfig config)
        {
            _glGraphics.GL10 = gl;
            lock (_stateChanged)
            {
                if (_state == GLGameState.Initialized)
                    _screen = GetStartScreen();
                _state = GLGameState.Running;
                _screen.Resume();
                _startTime = Java.Lang.JavaSystem.NanoTime();

            }

        }

		protected override void OnPause ()
		{
			base.OnPause ();
			lock(_stateChanged)
			{
				if (this.IsFinishing)
				{
					_state = GLGameState.Finished;
				}
				else
				{
					_state = GLGameState.Paused;
				}
				//модификация кода

			}
			_wakeLock.Release ();
			_glView.OnPause ();
			//модификация кода
		}

		public abstract Screen GetStartScreen();
    }


}

