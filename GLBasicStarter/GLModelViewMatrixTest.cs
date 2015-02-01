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
using Javax.Microedition.Khronos.Opengles;
namespace GLBasicStarter
{
	[Activity]
	class GLModelViewMatrixTest : AndroidGLGame
	{
		public override Screen GetStartScreen ()
		{
			return new ModelViewMatrixScreen (this);
		}
	}

	class ModelViewMatrixScreen : Screen
	{
		const int NUM_BOBS = 100;
		AndroidTexture _texture;
		AndroidVertices _vertices;
		BobModel[] _bobs;
		AndroidGLGraphics _glGraphics;


		public ModelViewMatrixScreen(IGame g) : base(g)
		{
			AndroidGLGame game = g as AndroidGLGame;
			_glGraphics = game.GLGraphics;
			_texture = new AndroidTexture (game,"bobrgb888.png");
			_vertices = new AndroidVertices (_glGraphics,4,6,false,true);
			_vertices.SetVertices (new float[]{	-32,-32,0,1,
											  	32,-32,1,1,
											  	32,32,1,0,
												-32,32,0,0},0,16);
			_vertices.SetIndices (new short[]{0,1,2,2,3,0},0,6);

			_bobs = new BobModel[NUM_BOBS];

			for (int i=0; i<NUM_BOBS; i++)
			{
				_bobs [i] = new BobModel ();
			}

		}

		public override void Update (float deltaTime)
		{
			for (int i=0; i<NUM_BOBS; i++)
			{
				_bobs [i].Update (deltaTime);
			}
		}

		public override void Present (float deltaTime)
		{

			IGL10 gl = _glGraphics.GL10;
			gl.GlClear (GL10.GlColorBufferBit);
			gl.GlMatrixMode (GL10.GlModelview);
			_vertices.Bind ();
			for (int i=0; i<NUM_BOBS; i++) 
			{
				gl.GlLoadIdentity ();


				gl.GlTranslatef (_bobs[i].dx, _bobs [i].dy, 0);
				gl.GlRotatef (_bobs[i].grad, 0, 0, -1f);

				_vertices.Draw (GL10.GlTriangles, 0, 6);
			}
			_vertices.Unbind ();

		}

		public override void Pause ()
		{
		
		}

		public override void Resume ()
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlViewport (0, 0, _glGraphics.Width, _glGraphics.Height);
			gl.GlClearColor (0, 1, 0, 1);


			gl.GlMatrixMode (GL10.GlProjection);
			gl.GlLoadIdentity ();
			gl.GlOrthof (0,540,0,960,1,-1);

			gl.GlEnable (GL10.GlTexture2d);
			_texture.Reload ();
			//_texture.BindTexture ();
		}

		public override void Dispose ()
		{

		}
	


	}
}

