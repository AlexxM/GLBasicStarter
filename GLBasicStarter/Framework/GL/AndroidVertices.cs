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
using Java.Nio;
using Javax.Microedition.Khronos.Opengles;
namespace GLBasicStarter
{
	class AndroidVertices
	{
		AndroidGLGraphics _glGraphics;
		bool _hasColor;
		bool _hasTexCoords;
		int _vertexSize;
		FloatBuffer _vertices;
		ShortBuffer _indices;

		public AndroidVertices(AndroidGLGraphics glGraphics,int maxVertices,int maxIndices,bool color, bool tecCoords)
		{
			_glGraphics = glGraphics;
	
			_hasColor = color;
			_hasTexCoords = tecCoords;
			_vertexSize = (2 + (_hasColor ? 4 : 0) + (_hasTexCoords ? 2 : 0)) * 4;

			ByteBuffer buffer = ByteBuffer.AllocateDirect (maxVertices*_vertexSize);
			buffer.Order (ByteOrder.NativeOrder());
			_vertices = buffer.AsFloatBuffer ();
			if (maxVertices > 0)
			{
				buffer = ByteBuffer.AllocateDirect (maxIndices*2);
				buffer.Order (ByteOrder.NativeOrder());
				_indices = buffer.AsShortBuffer ();
			}

		}

		public void SetVertices(float[] vertices,int offset,int length)
		{
			_vertices.Clear ();
			_vertices.Put (vertices,offset,length);
			_vertices.Flip ();

		}

		public void SetIndices(short[] indices,int offset,int length)
		{
			_indices.Clear ();
			_indices.Put (indices,offset,length);
			_indices.Flip ();

		}


		public void Bind()
		{
			IGL10  gl = _glGraphics.GL10;
			gl.GlEnableClientState (GL10.GlVertexArray);
			_vertices.Position (0);
			gl.GlVertexPointer (2,GL10.GlFloat,_vertexSize,_vertices);

			if (_hasColor)
			{
				gl.GlEnableClientState (GL10.GlColorArray);
				_vertices.Position (2);
				gl.GlColorPointer (4,GL10.GlFloat,_vertexSize,_vertices);
			}

			if (_hasTexCoords)
			{
				gl.GlEnableClientState (GL10.GlTextureCoordArray);
				_vertices.Position (_hasColor?6:2);
				gl.GlTexCoordPointer (2,GL10.GlFloat,_vertexSize,_vertices);
			}
		}

		public void Draw(int primitiveType,int offset, int numVertices)
		{
			IGL10  gl = _glGraphics.GL10;


			if (_indices != null)
			{
				_indices.Position (offset);
				gl.GlDrawElements (primitiveType, numVertices, GL10.GlUnsignedShort, _indices);
			}
			else
			{
				gl.GlDrawArrays (primitiveType,offset,numVertices);
			}


		}


		public void Unbind()
		{
			IGL10  gl = _glGraphics.GL10;
			if (_hasTexCoords)
				gl.GlDisableClientState (GL10.GlTextureCoordArray);

			if (_hasColor)
				gl.GlDisableClientState (GL10.GlColorArray);
		}
	}
}

