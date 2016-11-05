using UnityEngine;
using System;
using System.Collections;

namespace Bolo.DataClasses
{

	#region Map
	public enum GroundType
	{
		GRASS = 0,
		DIRT = 1,
		SAND = 2
	}

	public enum BlockType
	{
		EMPTY = 0,
		IMPASSABLE = 1,
		ROCK = 2,
		CRYSTAL = 3
	}

	public enum GraphicsGroundType
	{
		GRASS = 0,
		DIRT = 1,
		SAND = 2
	}

	public enum GraphicsBlockType
	{
		EMPTY = 0,
		MID_ROCK = 1,
		R_ROCK = 2,
		U_ROCK = 3,
		L_ROCK = 4,
		D_ROCK = 5,
		RL_ROCK = 6,
		UD_ROCK = 7,
		LD_ROCK = 8,
		DR_ROCK = 9,
		RU_ROCK = 10,
		UL_ROCK = 11,
		ULD_ROCK = 12,
		LDR_ROCK = 13,
		DRU_ROCK = 14,
		RUL_ROCK = 15,
		RULD_ROCK = 16,
		IMPASSABLE = 17,
		CRYSTAL = 18
	}
	#endregion Map

	[Serializable]
	public enum WeaponType
	{
		NONE,
		CANNON,
		LASER,
		PLASMA,
		ROCKET,
		MISSILES
	}

	[Serializable]
	public enum ResourceType
	{
		EMPTY,
		METAL,
		CRYSTALS,
		GOLD,
	}
}