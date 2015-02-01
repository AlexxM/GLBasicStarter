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
using Javax.Microedition.Khronos.Opengles;
namespace SimpleAndroidGame.GL
{

	public class AndroidGLGraphics
	{
		GLSurfaceView gl;
		IGL10 gl10;

		public IGL10 GL10
		{ 
			get
			{
				return gl10;
			} 
			set
			{
				gl10 = value;
			}
		}

		public int Width
		{
			get{return gl.Width;}

		}

		public int Height
		{
			get{return gl.Height;}

		}

		public AndroidGLGraphics(GLSurfaceView gl)
		{
			this.gl = gl;

		}

		 
	
	}
}

