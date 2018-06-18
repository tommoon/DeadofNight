using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinder : MonoBehaviour {

	NodeFinder nodefind;

	public Transform player;

	// Use this for initialization
	void Awake () {
		nodefind = GetComponent<NodeFinder> ();
	}
	
	public List <Node> findPath (Transform zombie){

		Transform starter = nodefind.NearestPlayerNode (zombie);
		Transform ender = nodefind.NearestPlayerNode (player);

		Node startNode = starter.gameObject.GetComponent<Node> ();
		Node targetNode = ender.gameObject.GetComponent<Node> ();


		List <Node> openSet = new List<Node> ();
		HashSet<Node> closedSet = new HashSet<Node> ();
		List <Node> route = new List<Node> ();

		openSet.Add (startNode);


		while (openSet.Count > 0) {
			Node checker = openSet [0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < checker.fCost || openSet[i].fCost == checker.fCost) {
					if (openSet[i].hCost < checker.hCost)
						checker = openSet[i];
				}
			}

			openSet.Remove(checker);
			closedSet.Add(checker);

			if (checker == targetNode) {
				
				route = RetracePath(startNode,targetNode);
				break;
			}

			foreach (Node neighbour in checker.neighbours) {
				if (closedSet.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = checker.gCost + GetDistance(checker, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = checker;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
		route.Add (targetNode);


		return route;
	}

	List <Node> RetracePath(Node start, Node finish){
		List<Node> path = new List<Node>();
		Node currentNode = finish;

		while (currentNode != start) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Add (start);
		path.Reverse ();
		return path;


	}

	int GetDistance(Node first, Node second) {
		int result = Mathf.RoundToInt (Vector3.Distance (first.transform.position, second.transform.position));
		return result;
	}
}
