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
				RGBToHSV (rgb, out h, out s, out v);
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