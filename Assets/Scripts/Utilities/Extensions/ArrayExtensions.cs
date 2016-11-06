using UnityEngine;
using System;

namespace Bolo
{
	public static class ArrayExtensions
	{

		//public static T[,][,] SplitArray<T>(this T[,] array, int splitCount) 
		//{
		//	if (splitCount == 0 || splitCount > array.GetLength(0) || splitCount > array.GetLength(1))
		//	{
		//		Debug.LogError("Split count must be higher than 0, and less than map size! (was " + splitCount + ")");
		//		return null;
		//	}
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	T[,][,] blockArray = new T[splitCount, splitCount][,];

		//	for (int i = 0; i < splitCount; i++) {
		//		for (int j = 0; j < splitCount; j++) {

		//			T[,] newArray = array.GetNewArray(splitCount, i * splitWidth, j * splitHeight);
			
		//			blockArray[i,j] = newArray;
		//		}
		//	}

		//	return blockArray;
		//}


		//private static T[,] GetNewArray<T>(this T[,] array, int splitCount, int x, int y)
		//{
		//	int splitWidth = array.GetLength(0) / splitCount;
		//	int splitHeight = array.GetLength(1) / splitCount;
		//	T[,] newArray = new T[splitWidth, splitHeight];

		//	for (int i = x; i < x + splitWidth; i++) {
		//		for (int j = y; j < y + splitHeight; j++) {
		//			newArray[i-x, j-y] = array[i,j];
		//		}
		//	}

		//	return newArray;
		//}
	}
}
