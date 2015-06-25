/*
    AudioBehavior for MiniGame2 - ELRO Wants To Play
    Copyright (C) 2015 Jan-Willem Hoekman

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
using System.Collections.Generic;

public class audioBehavior : MonoBehaviour {


    private AudioSource audiosource;
    public List<AudioClip> soundFiles = new List<AudioClip>();

	// Use this for initialization
	void Start () {
        audiosource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /**
     * Play the sound out of the array depending on the given index
     * \param ind the index parameter
     */
    public void playsound(int ind)
    {
        if(ind < soundFiles.Count){
            audiosource.clip = soundFiles[ind];
            audiosource.Play();
        }
    }
}
