using UnityEngine;
using System.Collections;

public class CellularAutomata : System.Object
{
	int TRUE=1;
	int FALSE=0;
	
	int width = 64;
	int height = 64;
	
	int[][] cellmap;
	
	float chanceToStartAlive = 0.35f; // sets how dense the initial grid is with
	// living cells.
	int starvationLimit = 3;// is the lower neighbour limit at which cells start
	// dying.
	int deathLimit = 3;// is the upper neighbour limit at which cells start
	// dying.
	int birthLimit = 4; // is the number of neighbours that cause a dead cell to
	// become alive.
	int numberOfSteps =12;// is the number of times we perform the simulation
	// step.
	
	public CellularAutomata():this(32,32)
	{

	}
	
	public CellularAutomata(int width, int height)
	{

		this.width = width;
		this.height = height;
	}
	
	public int[,] initialiseMap(int[,] map)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (Random.value < chanceToStartAlive)
				{
					map[x,y] = TRUE;
				}
				else
				{
					map[x,y] = FALSE;
				}
			}
		}
		return map;
	}
	
	
	
	// Returns the number of cells in a ring around (x,y) that are alive.
	public int countAliveNeighbours(int[,] map, int x, int y)
	{
		int count = 0;
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				int neighbour_x = x + i;
				int neighbour_y = y + j;
				// If we're looking at the middle point
				if (i == 0 && j == 0)
				{
					// Do nothing, we don't want to add ourselves in!
				}
				// In case the index we're looking at it off the edge of the map
				else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1))
				{
					count = count + 1;
				}
				// Otherwise, a normal check of the neighbour
				else if (map[neighbour_x,neighbour_y]==TRUE)
				{
					count = count + 1;
				}
			}
		}
		
		return count;
	}
	
	public int[,] doSimulationStep(int[,] oldMap)
	{
		int[,] newMap = new int[width,height];
		// Loop over each row and column of the map
		for (int x = 0; x < oldMap.GetLength(0); x++)
		{
			for (int y = 0; y < oldMap.GetLength(1); y++)
			{
				int nbs = countAliveNeighbours(oldMap, x, y);
				// The new value is based on our simulation rules
				// First, if a cell is alive but has too few neighbours, kill
				// it.
				if (oldMap[x,y]==TRUE)
				{
					if (nbs < deathLimit)
					{
						newMap[x,y] = FALSE;
					}
					else
					{
						newMap[x,y] = TRUE;
					}
				} // Otherwise, if the cell is dead now, check if it has the
				// right number of neighbours to be 'born'
				else
				{
					if (nbs > birthLimit)
					{
						newMap[x,y] = TRUE;
					}
					else
					{
						newMap[x,y] = FALSE;
					}
				}
			}
		}
		return newMap;
	}
	
	public int[,] generateMap()
	{
		// Create a new map
		int[,] cellmap = new int[width,height];
		// Set up the map with random values
		cellmap = initialiseMap(cellmap);
		// And now run the simulation for a set number of steps
		for (int i = 0; i < numberOfSteps; i++)
		{
			cellmap = doSimulationStep(cellmap);
		}
		
		return cellmap;
	}

	public void simplePlaceTreasure(int[,] map,int treasureLimit)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				int n=this.countAliveNeighbours(map,x,y);
				if (n<treasureLimit && n==1) map[x,y]=2;
			}
		}
	}

	public void simplePlaceObject(int[,] map,float propability)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				int n=this.countAliveNeighbours(map,x,y);
				if (n==0 && Random.value<propability) map[x,y]=3;
			}
		}
	}
}
