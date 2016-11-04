using Bolo.DataClasses;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bolo
{
	[Serializable]
	public class WeaponData
	{
		public class WeaponUpgradeData
		{
			public PriceData price;

			public float cooldownDuration;
			public float accuracy;
			public float ammoUse;
			public float recoil;

			public float speed;
			public float damage;
			public float range;
			public float knockBack;
		}

		public WeaponType type; //For gfx + maybe scripts/behaviours?
		public string title;

		public PriceData price;

		//Weapon stats
		public float cooldownDuration;
		public float accuracy;
		public float ammoUse;
		public float recoil;

		//Upgrades
		public WeaponUpgradeData[] upgrades;
		
		//Projectile stats
		public float speed;
		public float damage;
		public float range;
		public float knockBack;
		


		public static WeaponData DBGWeapon
		{
			get
			{
				var weaponData = new WeaponData();
				weaponData.accuracy = 0;
				weaponData.cooldownDuration = 0.2f;
				weaponData.damage = 5f;
				weaponData.range = 8f;
				weaponData.title = "Cannon";
				weaponData.speed = 4f;
				weaponData.type = WeaponType.CANNON;
				weaponData.knockBack = 10f;
				return weaponData;
			}
		}
	}
}

//DATA:
//Title, cooldownDuration, accuracy
//Projectile type, speed, damage, range
//Ammo use, self knockback, other knockback
//Price- metal, gold, crystals
//Upgrades[]: price, new cooldownDuration, new accuracy, .., new other knockback 

//WEAPON INSTANCE:
//cooldown/next-time allowed, upgrade level