using System;


[Serializable]
public struct Pos{
	public int x;
	public int y;


	public static Pos Zero{
		get{return new Pos(0,0);}
	}

	public Pos(Pos vec){
		this.x = vec.x;
		this.y = vec.y;
	}

	public Pos(int x, int y){
		this.x = x;
		this.y = y;
	}

	public override string ToString ()
	{
		return string.Format ("[Pos x: {0}, y: {1}]", x, y);
	}

	public override bool Equals(object obj)
	{
		return (obj is Pos) ? this == (Pos)obj : false;
	}

	public bool Equals(Pos other)
	{
		return this == other;
	}

	public override int GetHashCode()
	{
		return x ^ y;
	}

	#region Operators

	public static bool operator ==(Pos value1, Pos value2)
	{
		return value1.x == value2.x
			&& value1.y == value2.y;
	}

	public static bool operator !=(Pos value1, Pos value2)
	{
		return !(value1 == value2);
	}

	public static Pos operator +(Pos value1, Pos value2)
	{
		Pos result = new Pos();
		result.x = value1.x +value2.x;
		result.y = value1.y +value2.y;
		return result;
	}

	public static Pos operator -(Pos value)
	{
		Pos result = new Pos(-value.x, -value.y);
		return result;
	}

	public static Pos operator -(Pos value1, Pos value2)
	{
		Pos result = new Pos();
		result.x = value1.x - value2.x;
		result.y = value1.y - value2.y;
		return result;
	}

	public static Pos operator *(Pos value1, Pos value2)
	{
		Pos result = new Pos();
		result.x = value1.x * value2.x;
		result.y = value1.y * value2.y;
		return result;
	}

	public static Pos operator /(Pos value1, Pos value2)
	{
		Pos result = new Pos();
		result.x = value1.x / value2.x;
		result.y = value1.y / value2.y;
		return result;
	}



	#endregion
}