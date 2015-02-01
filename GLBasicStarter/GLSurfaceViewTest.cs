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
using Android.Opengl;
namespace GLBasicStarter
{
    [Activity]
    class GLSurfaceViewTest : Activity
	{

		GLSurfaceView glView;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			this.RequestWindowFeature (WindowFeatures.NoTitle);
			this.Window.SetFlags (WindowManagerFlags.Fullscreen,WindowManagerFlags.Fullscreen);
			glView = new GLSurfaceView (this);
			glView.SetRenderer (new SimpleRender());
            SetContentView(glView);

		}

		protected override void OnResume ()
		{
			base.OnResume ();
			glView.OnResume ();
		}
		protected override void OnPause()
		{
			base.OnPause ();
			glView.OnPause ();
		}
	
	}

	class SimpleRender : Java.Lang.Object,Android.Opengl.GLSurfaceView.IRenderer
	{
		public void OnDrawFrame (Javax.Microedition.Khronos.Opengles.IGL10  gl)
		{
			Java.Util.Random r = new Java.Util.Random ();

			gl.GlClearColor (r.NextFloat(),r.NextFloat(),r.NextFloat(),1);
			gl.GlClear (Javax.Microedition.Khronos.Opengles.GL10.GlColorBufferBit);
		}
		public void OnSurfaceChanged (Javax.Microedition.Khronos.Opengles.IGL10 gl, int width, int height)
		{

		}
		public void OnSurfaceCreated (Javax.Microedition.Khronos.Opengles.IGL10 gl, Javax.Microedition.Khronos.Egl.EGLConfig  config)
		{

		}
	} 
}

