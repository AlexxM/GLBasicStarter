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
using Android.Graphics;
using SimpleAndroidGame.Framework.Interfaces;
using Javax.Microedition.Khronos.Opengles;
using Java.Nio;
namespace GLBasicStarter
{	
	[Activity]
	class GLTexturedTriangleTest : AndroidGLGame
	{
		
		public override Screen GetStartScreen ()
		{
			return new TexturedTriangleScreen (this);
		}
	}

	class TexturedTriangleScreen : Screen
	{
		int _vertexSize=(2+2)*4;
		int _textureId;
		FloatBuffer _vertices;
		AndroidGLGraphics _glGraphics;

		public TexturedTriangleScreen(IGame g) : base(g)
		{
	
			_glGraphics = ((AndroidGLGame)g).GLGraphics;
			ByteBuffer bb = ByteBuffer.AllocateDirect (_vertexSize * 3);
			bb.Order (ByteOrder.NativeOrder());
			_vertices = bb.AsFloatBuffer ();
			_vertices.Put (new float[] { 0f,0f,0f,1f,540f,0f,1f,1f,270f,960f,0.5f,0f });
			_vertices.Flip ();
			_textureId = LoadTexture ("bobargb8888.png");

		}

		int LoadTexture(string assetName)
		{
			try
			{
				Bitmap b= BitmapFactory.DecodeStream(game.FileIO.ReadAsset(assetName));
				IGL10 gl = _glGraphics.GL10;
				int[] tId=new int[1];
				gl.GlGenTextures(1,tId,0);
				int textureId=tId[0];
				gl.GlBindTexture(GL10.GlTexture2d,textureId);

				gl.GlTexParameterf(GL10.GlTexture2d,GL10.GlTextureMinFilter,GL10.GlNearest);
				gl.GlTexParameterf(GL10.GlTexture2d,GL10.GlTextureMagFilter,GL10.GlNearest);

				Android.Opengl.GLUtils.TexImage2D(GL10.GlTexture2d,0,b,0);

				b.Recycle();
				return textureId;
			}
			catch
			{
				throw new Java.Lang.RuntimeException ("Couldn't load asset");
			}

		}

		public override void Present(float deltaTime)
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlViewport(0, 0, _glGraphics.Width, _glGraphics.Height);
			gl.GlClearColor (0, 0, 0, 1);
			gl.GlClear(GL10.GlColorBufferBit);
			gl.GlMatrixMode(GL10.GlProjection);
			gl.GlLoadIdentity();
			gl.GlOrthof(0, 540, 0, 960, 1, -1);
			gl.GlEnable(GL10.GlTexture2d);


			gl.GlBlendFunc(GL10.GlSrcAlpha, GL10.GlOneMinusSrcAlpha);
			gl.GlEnable(GL10.GlBlend);

			gl.GlBindTexture(GL10.GlTexture2d, _textureId);


		

			gl.GlEnableClientState (GL10.GlVertexArray);
			gl.GlEnableClientState (GL10.GlTextureCoordArray);

			_vertices.Position (0);
			gl.GlVertexPointer (2,GL10.GlFloat,_vertexSize,_vertices);
			_vertices.Position (2);
			gl.GlTexCoordPointer (2, GL10.GlFloat, _vertexSize, _vertices);
			gl.GlDrawArrays (GL10.GlTriangles,0,3);
			//gl.GlDisableClientState(GL10.GlVertexArray);
			//gl.GlDisableClientState(GL10.GlTextureCoordArray);

		}

		public override void Update(float deltaTime)
		{

		}

		public override void Dispose ()
		{

		}

		public override void Pause ()
		{

		}
		public override void Resume ()
		{

		}

	}
}

