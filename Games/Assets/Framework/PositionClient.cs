/*
    Framework for ELRO Wants To Play
    Copyright (C) 2015 Jan Willem Hoekman
    Copyright (C) 2015 Simon Voordouw
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Framework
{
	public class PositionClient : MonoBehaviour, LocationProvider
	{
		public event LocationUpdateHandler OnLocationUpdate;

		private UdpClient client = new UdpClient ();
		private IPEndPoint localEp = new IPEndPoint (IPAddress.Any, 2000);
		Thread socketListener;
		List<LocationUpdateArgs> locqueue;

		/**
         * 
         * Initialize Listener
         * 
         */
		void Start ()
		{
			locqueue = new List<LocationUpdateArgs> ();
			client.ExclusiveAddressUse = false;
			
			client.Client.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			
			client.Client.Bind (localEp);
			
			IPAddress multicastaddress = IPAddress.Parse ("239.255.0.1");
			client.JoinMulticastGroup (multicastaddress);
			socketListener = new Thread (ReceiveLocation);
			socketListener.Start ();
		}

		/**
         * 
         * Quit Network Connection when object destroyed
         * 
         */
		void OnDestroy ()
		{
			socketListener.Abort ();
		}

		/**
         * 
         * On each monobehavier update check for new locations in the location que
         * 
         */
		void Update ()
		{
			lock (locqueue) {
				if (OnLocationUpdate != null) {
					foreach (LocationUpdateArgs location in locqueue) {
						OnLocationUpdate (this, location);
					}

				}
				locqueue.Clear ();
			}
		}
		
		/**
         * 
         * Endless loop listening on socket for new locations
         * 
         */
		void ReceiveLocation ()
		{
			while (true) {
				Byte[] data = client.Receive (ref localEp);

				Vector3 location = new Vector3 (320 - Mathf.Floor (BitConverter.ToSingle (data, 0)), 
				                                Mathf.Floor (BitConverter.ToSingle (data, 8)), 
				                                Mathf.Floor (BitConverter.ToSingle (data, 4)) - 240) / 200.0f;

				Color rgb = new Color (BitConverter.ToSingle (data, 20) / 255.0f, 
				                       BitConverter.ToSingle (data, 16) / 255.0f, 
				                       BitConverter.ToSingle (data, 12) / 255.0f,
				                       1.0f);
				float h, s, v;
				RGBToHSV (rgb, out h, out s, out v);
				PlayerColor objid = HueObject (h * 360.0f);

				var loc = new LocationUpdateArgs ((int)objid, location);

				lock (locqueue) {
					locqueue.Add (loc);
				}
			}
		}

		/**
         * 
         * \param hue
         * 
         */
		PlayerColor HueObject (float hue)
		{
			if (hue >= 330 || hue <= 60) {
				return PlayerColor.Red;
			} else if (hue >= 90 && hue < 180) {
				return PlayerColor.Green;
			} else if (hue > 180 && hue <= 270) {
				return PlayerColor.Blue;
			} else {
				return PlayerColor.Unknown;
			}
		}

		/**
         * 
         * \param rgbColor
         * \param H
         * \param S
         * \param V
         * 
         */
		public static void RGBToHSV (Color rgbColor, out float H, out float S, out float V)
		{
			if (rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r) {
				RGBToHSVHelper (4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			} else {
				if (rgbColor.g > rgbColor.r) {
					RGBToHSVHelper (2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
				} else {
					RGBToHSVHelper (0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
				}
			}
		}
		
		/**
         * 
         * \param offset
         * \param dominantcolor
         * \param colorone
         * \param colortwo
         * \param H
         * \param S
         * \param V
         * 
         */
		private static void RGBToHSVHelper (float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
		{
			V = dominantcolor;
			if (V != 0f) {
				float num = 0f;
				if (colorone > colortwo) {
					num = colortwo;
				} else {
					num = colorone;
				}
				float num2 = V - num;
				if (num2 != 0f) {
					S = num2 / V;
					H = offset + (colorone - colortwo) / num2;
				} else {
					S = 0f;
					H = offset + (colorone - colortwo);
				}
				H /= 6f;
				if (H < 0f) {
					H += 1f;
				}
			} else {
				S = 0f;
				H = 0f;
			}
		}
	}
}