using UnityEngine;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapChunk : MonoBehaviour {

		[SerializeField] private MeshFilter _groundMeshFilter;
		[SerializeField] private MeshFilter _blockMeshFilter;

		private int[,] _uvIdxGroundChunk;
		private int[,] _uvIdxBlockChunk;
		private int _posX, _posY;
		private int _size; 

		public void GenerateChunk(int[,] graphicsGroundChunk, int[,] graphicsBlockChunk, int posX, int posY, int size){
			_uvIdxGroundChunk = graphicsGroundChunk;
			_uvIdxBlockChunk = graphicsBlockChunk;
			_posX = posX;
			_posY = posY;
			_size = size;
	
			UpdateMesh(_groundMeshFilter, _uvIdxGroundChunk);
			UpdateMesh(_blockMeshFilter, _uvIdxBlockChunk);

			transform.position = new Vector2(posX, posY);
		}

		public void ChangeBlockTile(int x, int y, GraphicsBlockType gfxType){
			_uvIdxBlockChunk[x,y] = (int) gfxType;
			UpdateMesh(_blockMeshFilter, _uvIdxBlockChunk);
		}

		void UpdateMesh(MeshFilter meshFilter, int[,] chunk){
			var mesh = new Mesh();

			var vertices = new Vector3[_size*_size*4];
			for (int i = 0; i < _size; i++){
				for (int j = 0; j < _size; j++){
					vertices[i*4+j*4*_size]   =new Vector3(1+i,j+1, 0);
					vertices[i*4+1+j*4*_size] =new Vector3(1+i,j,0);
					vertices[i*4+2+j*4*_size] =new Vector3(i,j+1, 0);
					vertices[i*4+3+j*4*_size] =new Vector3(i,j,0);
				}
			}

			var uv = new Vector2[_size*_size*4];
			for (int i = 0; i < _size; i++){
				for (int j = 0; j < _size; j++){

					int xInt = chunk[i,j]%32;
					int yInt = chunk[i,j]/32;

					float uvSize = 1f / 32f;
					//float startX = xInt / 32f;
					//float endX = (xInt + 1) / 32f;
					//float startY = yInt / 32f;
					//float endY = (yInt + 1) / 32f;

					float edge = 0.0001f;
					float startX = xInt * uvSize + edge;
					float endX = xInt * uvSize + uvSize - edge;
					float startY = yInt * uvSize + edge;
					float endY = yInt * uvSize + uvSize - edge;
					//uv[i * 4 + j * 4 * _size] = new Vector2(endX, endY);
					//uv[i * 4 + 1 + j * 4 * _size] = new Vector2(endX, startY);
					//uv[i * 4 + 2 + j * 4 * _size] = new Vector2(startX, endY);
					//uv[i * 4 + 3 + j * 4 * _size] = new Vector2(startX, startY);

					//uv[i * 4 + j * 4 * _size] = new Vector2(endX - 0.01f, endY - 0.01f);
					//uv[i * 4 + 1 + j * 4 * _size] = new Vector2(endX - 0.01f, startY);
					//uv[i * 4 + 2 + j * 4 * _size] = new Vector2(startX, endY - 0.01f);
					//uv[i * 4 + 3 + j * 4 * _size] = new Vector2(startX, startY);

					uv[i * 4 + j * 4 * _size] = new Vector2(endX, endY);
					uv[i * 4 + 1 + j * 4 * _size] = new Vector2(endX, startY);
					uv[i * 4 + 2 + j * 4 * _size] = new Vector2(startX, endY);
					uv[i * 4 + 3 + j * 4 * _size] = new Vector2(startX, startY);
				}
			}

			var triangles= new int[_size*_size*6];
			for (int i = 0; i < _size; i++){
				for (int j = 0; j < _size; j++){
					triangles[i*6+j*6*_size]      =0+i*4+j*4*_size;
					triangles[i*6+1+j*6*_size]    =1+i*4+j*4*_size;
					triangles[i*6+2+j*6*_size]    =2+i*4+j*4*_size;
					triangles[i*6+3+j*6*_size]    =2+i*4+j*4*_size;
					triangles[i*6+4+j*6*_size]    =1+i*4+j*4*_size;
					triangles[i*6+5+j*6*_size]    =3+i*4+j*4*_size;
				}
			}

			mesh.Clear();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

		
			meshFilter.mesh = mesh;
		}


		#region Helpers
		private int[,] GraphicsToIntArray(GraphicsGroundType[,] groundTypeArr)
		{
			int[,] arr = new int[groundTypeArr.GetLength(0), groundTypeArr.GetLength(1)];
			for (int x = 0; x < groundTypeArr.GetLength(0); x++)
			{
				for (int y = 0; y < groundTypeArr.GetLength(1); y++)
				{
					arr[x, y] = (int)groundTypeArr[x, y];
				}
			}
			return arr;
		}
		private int[,] GraphicsToIntArray(GraphicsBlockType[,] groundBlockArr)
		{
			int[,] arr = new int[groundBlockArr.GetLength(0), groundBlockArr.GetLength(1)];
			for (int x = 0; x < groundBlockArr.GetLength(0); x++)
			{
				for (int y = 0; y < groundBlockArr.GetLength(1); y++)
				{
					arr[x, y] = (int)groundBlockArr[x, y];
				}
			}
			return arr;
		}
		#endregion Helpers

	}
}