using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
*File copied from the Unify Wiki.
*Original Creator Unknown.
*/

public interface IPathNode<T>
{
	List<T> Connections { get; }
	Vector2 Position { get; }
	bool Invalid {get;}
}