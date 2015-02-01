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
using SimpleAndroidGame.GL;
using Javax.Microedition.Khronos.Opengles;
namespace GLBasicStarter
{
	[Activity]
	class GLBlendingTest : AndroidGLGame
	{
		public override Screen GetStartScreen ()
		{
			return new BlendingScreen (this);
		} 
	}

	class BlendingScreen : Screen
	{
		AndroidGLGraphics _glGraphics;
		AndroidVertices _vertices;
		AndroidTexture _textureRgb;
		AndroidTexture _textureRgba;

		public BlendingScreen(IGame g) : base(g)
		{
			AndroidGLGame game = g as AndroidGLGame;

			_glGraphics = game.GLGraphics;
			_textureRgb = new AndroidTexture (game,"bobrgb888.png");
			_textureRgba = new AndroidTexture (game,"bobargb8888.png");
			_vertices = new AndroidVertices (_glGraphics,8,12,true,true);

			float[] rects = new float[] {
				100, 100, 1, 1, 1, 0.5f, 0, 1,
				228, 100, 1, 1, 1, 0.5f, 1, 1,
				228, 228, 1, 1, 1, 0.5f, 1, 0,
				100, 228, 1, 1, 1, 0.5f, 0, 0,

				100, 300, 1, 1, 1, 1f, 0, 1, 
				228, 300, 1, 1, 1, 1f, 1, 1, 
				228, 428,1, 1, 1, 1f, 1, 0, 
				100, 428, 1, 1, 1, 1f, 0, 0
			};

			_vertices.SetVertices (rects,0,rects.Length);
			_vertices.SetIndices (new short[]{0,1,2,2,3,0,4,5,6,6,7,4},0,12);
		
		}

		public override void Present (float deltaTime)
		{
			IGL10 gl = _glGraphics.GL10;
			//gl.GlViewport (0, 0, _glGraphics.Width, _glGraphics.Height);


			//gl.GlClearColor(0,0,1,1);
			gl.GlClear (GL10.GlColorBufferBit);
			//gl.GlMatrixMode (GL10.GlProjection);

			//gl.GlLoadIdentity ();
			//gl.GlOrthof (0,320,0,480,1,-1);

			//gl.GlEnable (GL10.GlTexture2d);
			//gl.GlEnable (GL10.GlBlend);
			gl.GlBlendFunc (GL10.GlSrcAlpha,GL10.GlOneMinusSrcAlpha);
			_vertices.Bind ();
			_textureRgba.BindTexture ();
			_vertices.Draw (GL10.GlTriangles,6,6);


			_textureRgb.BindTexture ();
			_vertices.Draw (GL10.GlTriangles,0,6);
			_vertices.Unbind ();
		}

		public override void Pause ()
		{

		}

		public override void Resume ()
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlViewport (0, 0, _glGraphics.Width, _glGraphics.Height);
			gl.GlClearColor (0, 0, 1, 1);


			gl.GlMatrixMode (GL10.GlProjection);
			gl.GlLoadIdentity ();
			gl.GlOrthof (0,320,0,480,1,-1);
			gl.GlEnable (GL10.GlBlend);
			gl.GlEnable (GL10.GlTexture2d);
			_textureRgb.Reload ();
			_textureRgba.Reload ();

		}

		public override void Update (float deltaTime)
		{

		}

		public override void Dispose ()
		{

		}
	}
}

