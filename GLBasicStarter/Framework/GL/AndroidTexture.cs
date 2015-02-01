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
using SimpleAndroidGame.Framework;
using Javax.Microedition.Khronos.Opengles;
using Android.Graphics;
namespace GLBasicStarter
{
	class AndroidTexture
	{
		// используется только IGL из объекта
		AndroidGLGraphics _glGraphics;
		IFileIO _fileIO;
		string _fileName;
		int _textureId;
		//int _minFilter;
		//int _magFilter;

		public AndroidTexture(AndroidGLGame glGame,string fileName)
		{
			this._glGraphics = glGame.GLGraphics;
			this._fileIO = glGame.FileIO;
			this._fileName = fileName;
			Load ();

		
		}

	    void SetFilters (int minF,int magF)
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlTexParameterf (GL10.GlTexture2d, GL10.GlTextureMinFilter, minF);
			gl.GlTexParameterf (GL10.GlTexture2d, GL10.GlTextureMagFilter, magF);
		}

		private void Load()
		{
			System.IO.Stream fs = null;
			try
			{
				IGL10 gl = _glGraphics.GL10;
				int[] textureId = new int[1];
				gl.GlGenTextures (1,textureId,0);
				_textureId = textureId [0];
				fs=_fileIO.ReadAsset(_fileName);
				Bitmap b= BitmapFactory.DecodeStream(fs);
			
				gl.GlBindTexture(GL10.GlTexture2d,_textureId);

				SetFilters (GL10.GlNearest,GL10.GlNearest);

				Android.Opengl.GLUtils.TexImage2D(GL10.GlTexture2d,0,b,0);

				b.Recycle();
			}
			catch
			{
				throw new Java.Lang.RuntimeException ("Couldn't load texture from asset");
			}
			finally
			{
				if (fs != null)
					fs.Close ();
			}

		}

		public void  Reload()
		{
			Load();
			BindTexture();
			SetFilters (GL10.GlNearest,GL10.GlNearest);

		}

		public void BindTexture()
		{
			IGL10 gl = _glGraphics.GL10;
			gl.GlBindTexture (GL10.GlTexture2d, _textureId);

		}

		public void Dispose()
		{

			IGL10 gl = _glGraphics.GL10;
			// возможно метод не нужен
			gl.GlBindTexture(GL10.GlTexture2d,_textureId);
			gl.GlDeleteTextures(1,new int[]{_textureId},0);


		}
	
	}
}

