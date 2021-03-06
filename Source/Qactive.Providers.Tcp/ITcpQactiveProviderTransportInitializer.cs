﻿using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;

namespace Qactive
{
  /// <remarks>
  /// A type is required to define the methods that this interface contains rather than simply having delegate parameters in the provider factory methods
  /// because the secure overloads require the functions to be called within the sandbox AppDomain, thus either remoting permission is required to serialize 
  /// the delegates across the AppDomain, which is undesireable, or a type must be instantiated from within the AppDomain itself. The latter choice is used.
  /// </remarks>
  [ContractClass(typeof(ITcpQactiveProviderTransportInitializerContract))]
  public interface ITcpQactiveProviderTransportInitializer
  {
    void StartedListener(int serverNumber, EndPoint endPoint);

    void StoppedListener(int serverNumber, EndPoint endPoint);

    /// <summary>
    /// Prepares the socket for transport; e.g., you could call <see cref="Socket.SetSocketOption(SocketOptionLevel, SocketOptionName, bool)"/>
    /// to disable the Nagel algorithm and to enable keep-alives.
    /// </summary>
    /// <remarks>
    /// This method is called once for the client socket when used for the client provider.
    /// This method is called once for each connected socket when used for the server provider.
    /// </remarks>
    void Prepare(Socket socket);

    /// <summary>
    /// Returns the transport formatter to be used for a socket, or returns null to use the default formatter.
    /// </summary>
    /// <remarks>
    /// This method is called once for the client socket when used for the client provider.
    /// This method is called once for each connected socket when used for the server provider.
    /// </remarks>
    IRemotingFormatter CreateFormatter();
  }

  [ContractClassFor(typeof(ITcpQactiveProviderTransportInitializer))]
  internal abstract class ITcpQactiveProviderTransportInitializerContract : ITcpQactiveProviderTransportInitializer
  {
    public void StartedListener(int serverNumber, EndPoint endPoint)
    {
      Contract.Requires(endPoint != null);
    }

    public void StoppedListener(int serverNumber, EndPoint endPoint)
    {
      Contract.Requires(endPoint != null);
    }

    public void Prepare(Socket socket)
    {
      Contract.Requires(socket != null);
    }

    public IRemotingFormatter CreateFormatter() => null;
  }
}
