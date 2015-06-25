/*
    Framework for ELRO Wants To Play
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

public class gameMenu : MonoBehaviour
{

	void Start ()
	{
	
	}
	
	void Update ()
	{
	
	}
	
	/**
	*	Determines to which menu it should return to. Check the "PlayerPrefs" for the corresponding menu. 
	*/
	public void returnToMenu ()
	{
		Application.LoadLevel (PlayerPrefs.GetString ("menu"));
	}

	public void MenuView ()
	{
		Application.LoadLevel (1);
	}

}