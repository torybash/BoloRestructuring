using UnityEngine;
using System.Collections.Generic;
using Bolo.DataClasses;

namespace Bolo.Map
{
	public class MapChunk : MonoBehaviour {

		[SerializeField] private MeshFilter groundMeshFilter;
		[SerializeField] private MeshFilter blockMeshFilter;

		private int[,] _graphicsGroundChunk;
		private int[,] _graphicsBlockChunk;
		private int _posX, _posY;
		private int _size; 

		public void GenerateChunk(int[,] graphicsGroundChunk, int[,] graphicsBlockChunk, int posX, int posY, int size){
			_graphicsGroundChunk = graphicsGroundChunk;
			_graphicsBlockChunk = graphicsBlockChunk;
			_posX = posX;
			_posY = posY;
			_size = size;
	
			UpdateMesh(groundMeshFilter, graphicsGroundChunk);
			UpdateMesh(blockMeshFilter, graphicsBlockChunk);

			transform.position = new Vector2(posX, posY);
		}

		public void ChangeBlockTile(int x, int y, GraphicsBlockTiles gfxType){
			_graphicsBlockChunk[x,y] = (int)gfxType;
			UpdateMesh(blockMeshFilter, _graphicsBlockChunk);
		}

		void UpdateMesh(MeshFilter meshFilter, int[,] chunk){
			Mesh mesh = new Mesh();

			Vector3[] vertices = new Vector3[_size*_size*4];
			for (int i = 0; i < _size; i++){
				for (int j = 0; j < _size; j++){
					vertices[i*4+j*4*_size]   =new Vector3(1+i,j+1, 0);
					vertices[i*4+1+j*4*_size] =new Vector3(1+i,j,0);
					vertices[i*4+2+j*4*_size] =new Vector3(i,j+1, 0);
					vertices[i*4+3+j*4*_size] =new Vector3(i,j,0);
				}
			}

			Vector2[] uv = new Vector2[_size*_size*4];
			for (int i = 0; i < _size; i++){
				for (int j = 0; j < _size; j++){

					int xInt = chunk[i,j]%32;
					int yInt = chunk[i,j]/32;



					float uvSize = 1f/32f;
					float startX = xInt * uvSize;
					float endX = xInt * uvSize + uvSize;
					float startY = yInt * uvSize;
					float endY = yInt * uvSize + uvSize;
				
				
					uv[i*4+j*4*_size]     =new Vector2(endX-0.001f,endY-0.001f);
					uv[i*4+1+j*4*_size]   =new Vector2(endX-0.001f,startY);
					uv[i*4+2+j*4*_size]   =new Vector2(startX,endY-0.001f);
					uv[i*4+3+j*4*_size]   =new Vector2(startX,startY);
				}
			}

			int[] triangles= new int[_size*_size*6];
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
		
	}
}