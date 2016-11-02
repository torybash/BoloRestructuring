using UnityEngine;
using System.Collections;
using Bolo.DataClasses;
using System;

namespace Bolo.Net
{
	public struct ChangeBlockCommand
	{
		public Pos pos;
		public BlockType block;
	}
}
