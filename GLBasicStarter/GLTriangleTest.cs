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
using SimpleAndroidGame.GL;
using SimpleAndroidGame.Framework;
using SimpleAndroidGame.Framework.Interfaces;
using Javax.Microedition.Khronos.Opengles;
using Java.Nio;

namespace GLBasicStarter
{
	[Activity]
	class GLTriangleTest : SimpleAndroidGame.GL.AndroidGLGame
	{
		public override Screen GetStartScreen()
		{
			return new FirstTriangleScreen (this);
		}


	}
	//можно реализовать через OpenTK
	class FirstTriangleScreen : Screen
	{
		AndroidGLGraphics _glGraphics;
		FloatBuffer _vertices;
		int _vertexSize=8;
		public FirstTriangleScreen(IGame g) : base(g)
		{
			_glGraphics = ((AndroidGLGame)g).GLGraphics;
			ByteBuffer bb = ByteBuffer.AllocateDirect (3*_vertexSize);
			bb.Order (ByteOrder.NativeOrder());
			_vertices = bb.AsFloatBuffer ();
			_vertices.Put (new float[]{0.0f,0.0f,539.0f,0.0f,270.0f,959.0f});
			_vertices.Flip ();


		}

		public override void Present(float delta)
		{

			IGL10 gl = _glGraphics.GL10;
			gl.GlViewport (0,0,_glGraphics.Width,_glGraphics.Height-200);
			gl.GlClear (GL10.GlColorBufferBit);
			gl.GlMatrixMode (GL10.GlProjection);
			gl.GlLoadIdentity ();
			gl.GlOrthof (0, 540, 0, 960, 1, -1);

			gl.GlColor4f (1,0,0,1);
			gl.GlEnableClientState (GL10.GlVertexArray);
			_vertices.Position (0);
			gl.GlVertexPointer (2,GL10.GlFloat,_vertexSize,_vertices);
			gl.GlDrawArrays(GL10.GlTriangles,0,3);


		}

		public override void Update (float deltaTime)
		{

		}

		public override void Pause ()
		{
	
		}

		public override void Resume ()
		{
		
		}

		public override void Dispose ()
		{
		
		}
	}
}

