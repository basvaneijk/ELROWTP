using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Framework
{
	
	public class PositionClient : MonoBehaviour, LocationProvider
	{
		public event LocationUpdateHandler OnLocationUpdate;
		private UdpClient client = new UdpClient();
		IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2000);
		
		void Start ()
		{
			StartCoroutine (SendLocation ());
			
			client.ExclusiveAddressUse = false;
			
			client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			client.ExclusiveAddressUse = false;
			
			client.Client.Bind(localEp);
			
			IPAddress multicastaddress = IPAddress.Parse("239.255.0.1");
			client.JoinMulticastGroup(multicastaddress);
		}
		
		IEnumerator SendLocation()
		{
			while (true)
			{
				Byte[] data = client.Receive(ref localEp);
				
				Vector3 location = new Vector3(BitConverter.ToSingle(data, 0),BitConverter.ToSingle(data, 8),BitConverter.ToSingle(data, 8));
				
				var loc = new LocationUpdateArgs (1, location, 1);
				
				if (OnLocationUpdate != null) {
					OnLocationUpdate (this, loc);
				}
			}
		}
	}
}