using UnityEngine;
using System.Collections.Generic;

public class TheCube :System.Object
{
	public enum Direction{NORTH,SOUTH,EAST,WEST,ANY_FUCKIN_WHERE};

	Dictionary<string,int> neighbours=new Dictionary<string, int>();

	public int currentSide=1;

	public  TheCube()
	{
		neighbours.Add(1+Direction.NORTH.ToString(),6);
		neighbours.Add(1+Direction.SOUTH.ToString(),4);
		neighbours.Add(1+Direction.EAST.ToString(),3);
		neighbours.Add(1+Direction.WEST.ToString(),2);

		neighbours.Add(2+Direction.NORTH.ToString(),6);
		neighbours.Add(2+Direction.SOUTH.ToString(),4);
		neighbours.Add(2+Direction.EAST.ToString(),1);
		neighbours.Add(2+Direction.WEST.ToString(),5);

		neighbours.Add(3+Direction.NORTH.ToString(),6);
		neighbours.Add(3+Direction.SOUTH.ToString(),4);
		neighbours.Add(3+Direction.EAST.ToString(),2);
		neighbours.Add(3+Direction.WEST.ToString(),1);

		neighbours.Add(4+Direction.NORTH.ToString(),1);
		neighbours.Add(4+Direction.SOUTH.ToString(),5);
		neighbours.Add(4+Direction.EAST.ToString(),3);
		neighbours.Add(4+Direction.WEST.ToString(),2);

		neighbours.Add(5+Direction.NORTH.ToString(),4);
		neighbours.Add(5+Direction.SOUTH.ToString(),6);
		neighbours.Add(5+Direction.EAST.ToString(),3);
		neighbours.Add(5+Direction.WEST.ToString(),2);

		neighbours.Add(6+Direction.NORTH.ToString(),5);
		neighbours.Add(6+Direction.SOUTH.ToString(),1);
		neighbours.Add(6+Direction.EAST.ToString(),3);
		neighbours.Add(6+Direction.WEST.ToString(),2);

	}

	public int getNeighbour(Direction d)
	{
		return neighbours[currentSide+d.ToString()];
	}
}
