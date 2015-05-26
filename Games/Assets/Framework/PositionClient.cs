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
		private UdpClient client = new UdpClient();
		private IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2000);
		Thread socketListener;

		List<LocationUpdateArgs> locqueue;

		void Start ()
		{
			locqueue = new List<LocationUpdateArgs> ();
			client.ExclusiveAddressUse = false;
			
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			
			client.Client.Bind(localEp);
			
			IPAddress multicastaddress = IPAddress.Parse("239.255.0.1");
			client.JoinMulticastGroup(multicastaddress);
			Debug.Log (client.GetType ().ToString ());
			socketListener = new Thread(SendLocation);
			socketListener.Start ();
		}

		void Update(){
			lock(locqueue){
				if (OnLocationUpdate != null) {
					foreach (LocationUpdateArgs location in locqueue) {
						OnLocationUpdate (this, location);
						Debug.Log(location);
					}
				}
				locqueue.Clear ();
			}
		}
		
		void SendLocation()
		{
			while (true)
			{
				Byte[] data = client.Receive(ref localEp);
				
				Vector3 location = new Vector3(BitConverter.ToSingle(data, 0),BitConverter.ToSingle(data, 4),BitConverter.ToSingle(data, 8));
				
				var loc = new LocationUpdateArgs (1, location, 1);

				lock(locqueue){
					locqueue.Add(loc);
				}
			}
		}
	}
}