using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace MailAFriend_Client
{
    class Client
    {
        // The port number for the remote device.
        public const int port = 3456;
        // Establish the remote endpoint for the socket.
        // The name of the 
        // remote device is "host.contoso.com".
        public static IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        public static IPAddress ipAddress = ipHostInfo.AddressList[0];
        public static IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);


        // Create a TCP/IP socket.
        public static Socket client = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;
        private static String display = String.Empty;

        public static void startClient()
        {
            TextBox formDisplay = Application.OpenForms["FormClient"].Controls["tbDisplay"] as TextBox;
            // Connect to a remote device.
            try
            {
                             
                // Connect to the remote endpoint.
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                // Send test data to the remote device.
                Send(client, "err|password<EOF>");
                sendDone.WaitOne();

                // Receive the response from the remote device.
                Receive(client);
                receiveDone.WaitOne();

                // Write the response to the console.
                display = "Response received : " + response;
                formDisplay.Text = display;

                // Release the socket.
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void stopClient()
        {
            try
            {
                // Release the socket.
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
                Send(client, "This is a test <ENDCONNECT>");
                sendDone.WaitOne();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString());
            }
        
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void Receive(Socket client)
        {

            try
            {
                // Create the state object.
                ClientSocket state = new ClientSocket();
                state.socket = client;

                display = "Socket connected to " + client.RemoteEndPoint.ToString();
                state.formDisplay.Text = display;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, ClientSocket.bufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {

            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                ClientSocket state = (ClientSocket)ar.AsyncState;
                Socket client = state.socket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, ClientSocket.bufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.
                    receiveDone.Set();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
    public class ClientSocket
    {
        // Client socket.
        public Socket socket = null;
        // Size of receive buffer.
        public const int bufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[bufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
        // Communicator with form display.
        public TextBox formDisplay = Application.OpenForms["FormClient"].Controls["tbDisplay"] as TextBox;
    }
}
