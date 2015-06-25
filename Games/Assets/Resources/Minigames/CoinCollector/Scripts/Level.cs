/*
	CoinCollector game for ELRO Wants To Play
    Copyright (C) 2015 Wouter Janssen
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

	public int levelNumber;
	public GameObject collectable;
	public ArrayList coinPositions = new ArrayList ();

	/**
	*	Create level object
	*	\param int levelNum level number
	*	\param GameObject coin prefab coin
	*	\param ArrayList coinsPos arraylist with coin positions
	*/
	public Level (int levelNum, GameObject coin, ArrayList coinsPos)
	{
		levelNumber = levelNum;
		collectable = coin;
		coinPositions = coinsPos;
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
