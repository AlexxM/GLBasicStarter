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
using SimpleAndroidGame.Framework.Interfaces;
using Java.Nio;
using Javax.Microedition.Khronos.Opengles;
using Android.Graphics;
namespace GLBasicStarter
{
	[Activity]
    class GLIndexedVerticesTest : AndroidGLGame
	{
		public override Screen GetStartScreen ()
		{
			return new IndexedVerticesScreen (this);
		}
	}

	class IndexedVerticesScreen : Screen
	{
		int _vertexSize=(2+2)*4;
		AndroidGLGraphics _glGraphics;
		FloatBuffer _vertices;
		ShortBuffer _indices;
		AndroidTexture _texture;

		public IndexedVerticesScreen(IGame g) : base(g)
		{
			_glGraphics=((AndroidGLGame)g).GLGraphics;
			ByteBuffer bb = ByteBuffer.AllocateDirect (_vertexSize*4);
			bb.Order (ByteOrder.NativeOrder());
			_vertices = bb.AsFloatBuffer ();
			_vertices.Put (new float[] {100f,100f,0f,1f,428f,100f,1f,1f,428f,428f,1f,0f,100f,428f,0f,0f });
			_vertices.Flip ();

			bb = ByteBuffer.AllocateDirect (6*2);
			bb.Order (ByteOrder.NativeOrder());
			_indices = bb.AsShortBuffer ();
			_indices.Put (new short[]{0,1,2,2,3,0});
			_indices.Flip ();

			_texture= new AndroidTexture((AndroidGLGame)g,"bobrgb888.png");
		}

		public override void Present (float deltaTime)
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlViewport (0,0,_glGraphics.Width,_glGraphics.Height);
			gl.GlClear (GL10.GlColorBufferBit);
			gl.GlMatrixMode (GL10.GlProjection);
			gl.GlLoadIdentity ();
			gl.GlOrthof (0, 540, 0, 960, 1, -1);
			gl.GlEnable (GL10.GlTexture2d);

			_texture.BindTexture ();

			gl.GlEnableClientState (GL10.GlVertexArray);
			gl.GlEnableClientState (GL10.GlTextureCoordArray);
			_vertices.Position (0);
			gl.GlVertexPointer (2,GL10.GlFloat,_vertexSize,_vertices);
			_vertices.Position (2);
			gl.GlTexCoordPointer (2,GL10.GlFloat,_vertexSize,_vertices);
			gl.GlDrawElements (GL10.GlTriangles, 6, GL10.GlUnsignedShort, _indices);

		}

		public override void Pause ()
		{

		}
		public override void Dispose ()
		{

		}

		public override void Resume ()
		{

		}

		public override void Update (float deltaTime)
		{

		}
	}
}

