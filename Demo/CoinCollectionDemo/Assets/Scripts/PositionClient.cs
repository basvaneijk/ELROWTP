using UnityEngine;
using UnityEditor;
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

		void OnApplicationQuit ()
		{
			socketListener.Abort ();
		}

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
		
		void ReceiveLocation ()
		{
			while (true) {
				Byte[] data = client.Receive (ref localEp);
				
				Vector3 location = new Vector3 (Mathf.Floor (BitConverter.ToSingle (data, 0)), 
				                                Mathf.Floor (BitConverter.ToSingle (data, 8)), 
				                                Mathf.Floor (BitConverter.ToSingle (data, 4))) / 10.0f;

				Color rgb = new Color (BitConverter.ToSingle (data, 20) / 255.0f, 
				                       BitConverter.ToSingle (data, 16) / 255.0f, 
				                       BitConverter.ToSingle (data, 12) / 255.0f,
				                       1.0f);
				float h, s, v;
				EditorGUIUtility.RGBToHSV (rgb, out h, out s, out v);
				PlayerColor objid = HueObject (h * 360.0f);

				var loc = new LocationUpdateArgs ((int)objid, location, 1);

				//Debug.Log (loc + " h:" + (h * 360.0f));
				lock (locqueue) {
					locqueue.Add (loc);
				}
			}
		}

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
	}
}