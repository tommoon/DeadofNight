using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	//Node's position in the grid
	public int x;
	public int y;

	//Node's costs for pathfinding purposes
	public int hCost;
	public int gCost;
	public Node[] neighbours;
	public Node parent;

	public int fCost
	{
		get //the fCost is the gCost+hCost so we can get it directly this way
		{
			return gCost + hCost;
		}
	}

	public bool open;
	public Node (bool _open, int _gridX, int _gridY){
	
	}
}
