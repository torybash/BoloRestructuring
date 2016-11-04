using UnityEngine;
using Bolo.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Bolo.Map
{
	public class FogOfWar : MonoBehaviour
	{
		[SerializeField]
		private MeshFilter _fogFilter;

		

		private Mesh _fogMesh;

		private int _fogSizeX = 32; 
		private int _fogSizeY = 32; 

		private byte[,] _fogMap;

		private Vector2 _fogDisplacement;
		private byte[,] tilesExplored;

		private List<Pos> lastVisibilityTiles = new List<Pos>();

		private MapInfo _mapInfo;

		void Awake()
		{
			_fogMesh = _fogFilter.mesh;
		}

		public void Setup(MapInfo mapInfo)
		{
			_mapInfo = mapInfo;
			_fogMap = new byte[_fogSizeX,_fogSizeY];
			_fogDisplacement = new Vector2(-_fogSizeY/2, -_fogSizeX/2);

			//Initialize explored tiles
			tilesExplored = new byte[_mapInfo.size, _mapInfo.size];
			for (int i = 0; i < _mapInfo.size; i++) {
				for (int j = 0; j < _mapInfo.size; j++) {
					tilesExplored[i,j] = 0;
				}
			}
		}


		public void UpdateFog(Pos pos)
		{
			var visibilityTiles = FindVisibleTiles(pos, 11);
			foreach (var tilePos in visibilityTiles) {
				tilesExplored[tilePos.x, tilePos.y] = 2;
			}

			var missingList = new List<Pos>(lastVisibilityTiles);
			foreach (var tilePos in visibilityTiles) {
				if (missingList.Contains(tilePos)){
					missingList.Remove(tilePos);
				}
			}

			StartCoroutine(_RemoveFromVisibility(missingList));
			UpdateFogMap(pos, tilesExplored);

			lastVisibilityTiles = visibilityTiles;
		}

		private void UpdateFogMap(Pos pos, byte[,] exploredMap){
			var startFogPos = new Pos(pos.x - _fogSizeX / 2, pos.y - _fogSizeY / 2);
			for (int x = 0; x < _fogSizeX; x++) {
				for (int y = 0; y < _fogSizeY; y++) {
					
					int realX = startFogPos.x + x;
					int realY = startFogPos.y + y;

					if (realX < 0 || realX >= _mapInfo.size ||
						realY < 0 || realY >= _mapInfo.size) continue;

					_fogMap[x,y] = exploredMap[realX, realY];
				}
			}

			UpdateMesh();

			transform.position = new Vector3(startFogPos.x + 0.5f, startFogPos.y + 0.5f, -4f); //TODO Convert tile pos, create constant
		}
	
		private IEnumerator _RemoveFromVisibility(List<Pos> missingList){
			yield return new WaitForSeconds(0.4f);

			foreach (var tilePos in missingList) {
				tilesExplored[tilePos.x, tilePos.y] = 1;
			}

			var playerPos = Game.player.vehicle.transform.position; //TODO make reference to player somewhere better?
			UpdateFogMap(new Pos((int)playerPos.x, (int)playerPos.y), tilesExplored);
		}

		private List<Pos> FindVisibleTiles(Pos pos, int range){
			HashSet<int> visibleTiles = new HashSet<int>();
			HashSet<int> clearTiles = new HashSet<int>();
			int x0 = pos.x; int y0 = pos.y;

			visibleTiles.Add(x0 + (y0*_mapInfo.size));
			clearTiles.Add(x0 + (y0*_mapInfo.size));
		
		
			int numberOfAngles = 96;
			float angleInterval = 360f/numberOfAngles;
		
			for (int i = 0; i < numberOfAngles; i++) {
				float angle = i * angleInterval * Mathf.PI / 180f;
				int goalX = (int) (x0 + 0.5f + (Mathf.Cos(angle)*range));
				int goalY = (int) (y0 + 0.5f + (Mathf.Sin(angle)*range));
			
				var tilesOnLine = GetTilesOnBresenhamLine(x0, y0, goalX, goalY);
			
				foreach (Pos tilePos in tilesOnLine) {
					visibleTiles.Add(tilePos.x + (tilePos.y*_mapInfo.size));
				}
			}

			var list = new List<Pos>();
			foreach (int tileID in visibleTiles) {
				list.Add(new Pos(tileID%_mapInfo.size, tileID/_mapInfo.size));
			}

			return list;
		
		}
	
		private List<Pos> GetTilesOnBresenhamLine(int xStart, int yStart, int xEnd, int yEnd){
			var tilesOnLine = new List<Pos>();
		
			int x, y, sx = 0, sy = 0;
		
			int dx = Mathf.Abs (xEnd - xStart);
			int dy = Mathf.Abs (yEnd - yStart);
		
			if (xStart < xEnd) sx = 1; 
			else if (xStart > xEnd) sx = -1;
			if (yStart < yEnd) sy = 1; 
			else if (yStart > yEnd) sy = -1;
			int err = dx - dy;
			int err2 = 0;
		
			x = xStart;
			y = yStart;
		
			while(true){
				tilesOnLine.Add(new Pos(x, y));

				//var blockType = _mapInfo.GetBlockAt(x, y);
				if (x == xEnd && y == yEnd || (int)_mapInfo.GetBlockAt(x, y) >= (int)BlockType.IMPASSABLE) break; //has reached goal?
			
				err2 = err * 2;
				if (err2 > -dy){
					err = err - dy;
					x = x + sx;				
				
					tilesOnLine.Add(new Pos(x, y + sy));
					if (_mapInfo.GetBlockAt(x, y + sy) >= BlockType.IMPASSABLE) break;
				}
				if (x == xEnd && y == yEnd || (int)_mapInfo.GetBlockAt(x, y) >= (int)BlockType.IMPASSABLE){ //has reached goal now???
					tilesOnLine.Add(new Pos(x, y));
					break;
				}
				if (err2 < dx){
					err = err + dx;
					y = y + sy;
								
					tilesOnLine.Add(new Pos(x + sx, y));
					if ((int)_mapInfo.GetBlockAt(x + sx, y) >= (int)BlockType.IMPASSABLE) break;
				}
			}
			return tilesOnLine;
		}





		private void UpdateMesh(){

			var mesh = _fogMesh;
		
			Vector3[] vertices = new Vector3[_fogSizeX*_fogSizeY*5];
			for (int i = 0; i < _fogSizeX; i++){
				for (int j = 0; j < _fogSizeY; j++){
					vertices[i*5+j*5*_fogSizeX]   =new Vector3(1+i,j+1, 0);
					vertices[i*5+1+j*5*_fogSizeX] =new Vector3(1+i,j,0);
					vertices[i*5+2+j*5*_fogSizeX] =new Vector3(i,j+1, 0);
					vertices[i*5+3+j*5*_fogSizeX] =new Vector3(i,j,0);
					vertices[i*5+4+j*5*_fogSizeX] =new Vector3(i+0.5f,j+0.5f,0);
				}
			}

			Vector2[] uv = new Vector2[_fogSizeX*_fogSizeY*5];
			for (int i = 0; i < _fogSizeX; i++){
				for (int j = 0; j < _fogSizeY; j++){
				
					int xInt = 0;
					int yInt = 0;

					float startX = xInt * 0.25f;
					float endX = xInt * 0.25f + 0.25f;
					float startY = yInt * 0.25f;
					float endY = yInt * 0.25f + 0.25f;

				
					uv[i*4+j*4*_fogSizeX]     =new Vector2(endX-0.01f,endY-0.01f);
					uv[i*4+1+j*4*_fogSizeX]   =new Vector2(endX-0.01f,startY);
					uv[i*4+2+j*4*_fogSizeX]   =new Vector2(startX,endY-0.01f);
					uv[i*4+3+j*4*_fogSizeX]   =new Vector2(startX,startY);
					uv[i*4+4+j*4*_fogSizeX]   =new Vector2(1,1);
				
				
					//				uv[i*4+j*4*size]     =new Vector2(0.249f,0.249f);
					//				uv[i*4+1+j*4*size]   =new Vector2(0.249f,0);
					//				uv[i*4+2+j*4*size]   =new Vector2(0,0.249f);
					//				uv[i*4+3+j*4*size]   =new Vector2(0,0);
				}
			}
		
			int[] triangles= new int[_fogSizeX*_fogSizeY*12];
			for (int i = 0; i < _fogSizeX; i++){
				for (int j = 0; j < _fogSizeY; j++){
					triangles[i*12+j*12*_fogSizeX]      =0+i*5+j*5*_fogSizeX;
					triangles[i*12+1+j*12*_fogSizeX]    =1+i*5+j*5*_fogSizeX;
					triangles[i*12+2+j*12*_fogSizeX]    =4+i*5+j*5*_fogSizeX;
					triangles[i*12+3+j*12*_fogSizeX]    =1+i*5+j*5*_fogSizeX;
					triangles[i*12+4+j*12*_fogSizeX]    =3+i*5+j*5*_fogSizeX;
					triangles[i*12+5+j*12*_fogSizeX]    =4+i*5+j*5*_fogSizeX;

					triangles[i*12+6+j*12*_fogSizeX]    =3+i*5+j*5*_fogSizeX;
					triangles[i*12+7+j*12*_fogSizeX]    =2+i*5+j*5*_fogSizeX;
					triangles[i*12+8+j*12*_fogSizeX]    =4+i*5+j*5*_fogSizeX;
					triangles[i*12+9+j*12*_fogSizeX]    =2+i*5+j*5*_fogSizeX;
					triangles[i*12+10+j*12*_fogSizeX]   =0+i*5+j*5*_fogSizeX;
					triangles[i*12+11+j*12*_fogSizeX]   =4+i*5+j*5*_fogSizeX;

				}
			}
		
		
			Color[] colors = new Color[_fogSizeX*_fogSizeY*5];
		
			for (int i = 0; i < _fogSizeX-1; i++){
				for (int j = 0; j < _fogSizeY-1; j++){
				
					float alpha1 = 1 - _fogMap[i+1,j+1]/2f;
					float alpha2 = 1 - _fogMap[i+1,j]/2f;
					float alpha3 = 1 - _fogMap[i,j+1]/2f;
					float alpha4 = 1 - _fogMap[i,j]/2f;
	//				float alpha1 = 0.5f;
	//				float alpha2 = 0.5f;
	//				float alpha3 = 0.5f;
	//				float alpha4 = 0.5f;
				
				
					colors[i*5+j*5*_fogSizeX]     =new Color(1, 1, 1, alpha1);
					colors[i*5+1+j*5*_fogSizeX]   =new Color(1, 1, 1, alpha2);
					colors[i*5+2+j*5*_fogSizeX]   =new Color(1, 1, 1, alpha3);
					colors[i*5+3+j*5*_fogSizeX]   =new Color(1, 1, 1, alpha4);
					colors[i*5+4+j*5*_fogSizeX]   =new Color(1, 1, 1, (alpha1+alpha2+alpha3+alpha4)/4f);
				
				
					//				uv[i*4+j*4*size]     =new Vector2(0.249f,0.249f);
					//				uv[i*4+1+j*4*size]   =new Vector2(0.249f,0);
					//				uv[i*4+2+j*4*size]   =new Vector2(0,0.249f);
					//				uv[i*4+3+j*4*size]   =new Vector2(0,0);
				}
			}
		
		
		
			mesh.Clear();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.colors = colors;
			mesh.RecalculateBounds();

			//_fogFilter.sharedMesh = mesh;
			_fogMesh = mesh;
		}

	}
}
