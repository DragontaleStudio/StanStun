using UnityEngine;
using System.Collections.Generic;

public class TheCube :System.Object
{
	public enum Direction{NORTH,SOUTH,EAST,WEST,ANY_FUCKIN_WHERE};

	Dictionary<string,int> neighbours=new Dictionary<string, int>();

	public int currentSide=1;
	int width=32;
	int height=32;

	public  TheCube()
	{
		neighbours.Add(1+Direction.NORTH.ToString(),2);
		neighbours.Add(1+Direction.SOUTH.ToString(),3);
		neighbours.Add(1+Direction.EAST.ToString(),4);
		neighbours.Add(1+Direction.WEST.ToString(),6);

		neighbours.Add(2+Direction.NORTH.ToString(),5);
		neighbours.Add(2+Direction.SOUTH.ToString(),1);
		neighbours.Add(2+Direction.EAST.ToString(),4);
		neighbours.Add(2+Direction.WEST.ToString(),6);

		neighbours.Add(3+Direction.NORTH.ToString(),1);
		neighbours.Add(3+Direction.SOUTH.ToString(),5);
		neighbours.Add(3+Direction.EAST.ToString(),4);
		neighbours.Add(3+Direction.WEST.ToString(),6);

		neighbours.Add(4+Direction.NORTH.ToString(),2);
		neighbours.Add(4+Direction.SOUTH.ToString(),3);
		neighbours.Add(4+Direction.EAST.ToString(),5);
		neighbours.Add(4+Direction.WEST.ToString(),1);

		neighbours.Add(5+Direction.NORTH.ToString(),3);
		neighbours.Add(5+Direction.SOUTH.ToString(),2);
		neighbours.Add(5+Direction.EAST.ToString(),4);
		neighbours.Add(5+Direction.WEST.ToString(),6);

		neighbours.Add(6+Direction.NORTH.ToString(),2);
		neighbours.Add(6+Direction.SOUTH.ToString(),3);
		neighbours.Add(6+Direction.EAST.ToString(),1);
		neighbours.Add(6+Direction.WEST.ToString(),5);

	}

	public int getNeighbour(Direction d)
	{
		return neighbours[currentSide+d.ToString()];
	}

	public TheCube.Direction getOverflowDirection(int[] pos)
	{
		int x=pos[0];
		int y=pos[1];
		TheCube.Direction dir=TheCube.Direction.ANY_FUCKIN_WHERE;
		if (x<0) 
		{
			dir=TheCube.Direction.EAST;
		}
		else if (x>=width) 
		{
			dir=TheCube.Direction.WEST;
		}
		else if (y<0)
		{
			dir=TheCube.Direction.SOUTH;
		}
		else if (y>=height)
		{
			dir=TheCube.Direction.NORTH;
		}
		return dir;
	}
	
	public int[] getOverFlowClampedPos(int[] pos)
	{
		int x=pos[0];
		int y=pos[1];
		if (x<0) 
		{
			x=width-1;
		}
		else if (x>=width) 
		{
			x=0;
		}
		else if (y<0)
		{
			y=height-1;
		}
		else if (y>=height)
		{
			y=0;
		}
		return new int[]{x,y};
	}
}
