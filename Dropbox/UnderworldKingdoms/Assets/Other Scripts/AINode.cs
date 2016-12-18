using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
*File originally created by: Austin Locke
*Creation Date: April 28, 2014 8:40 AM
*/

public class AINode : IPathNode<AINode>  {

	List<AINode> connections;
	
	private Vector2 position;
	private int m_label;

	public bool Invalid
	{
		get { return (this == null); }
	}
	
	public List<AINode> Connections
	{
		get { return connections; }
	}
	
	public Vector2 Position
	{
		get	{ return position; }
	}

	public int Label {
		get { return m_label; }
		set	{ m_label = value; }
	}

	void Start() {

	}

	public void Initiate(Vector2 pos, int label) {
		position = pos;
		m_label = label;
		connections = new List<AINode> ();
	}
	public void AddConnection(AINode connection) {
		connections.Add (connection);
	}
}
