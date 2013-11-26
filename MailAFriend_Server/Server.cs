using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Collections;


namespace MailAFriend_Server
{
    class Server
    {
        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static string display { get; set; }
        public static HashMapLinkedList emailDatabaseServer = new HashMapLinkedList();
        private volatile bool stopServer = false;

        public Thread serverThread;


        public delegate void serverThreadHandler();


        public event serverThreadHandler serverThreadComplete;


        public void startTheServer()
        {
            serverThread = new Thread(new ThreadStart(this.startServer));
            serverThread.Start();
        }
        public void stopTheServer()
        {
            stopServer = true;
            allDone.Set();
        }
        public void startServer()
        {
            display = String.Empty;
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];
            
            // Establish the local endpoint for the socket.
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 3456);

            // Create a TCP/IP socket.
            Socket serverSocket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {

                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);

                while (!stopServer)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();
                    // Start an asynchronous socket to listen for connections.
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

                //serverSocket.Close();
                serverThreadComplete();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {

            display = String.Empty;
            // Signal the main thread to continue.
            allDone.Set();
            
            // Get the socket that handles the client request.
            Socket severSocket = (Socket)ar.AsyncState;
            Socket handler = severSocket.EndAccept(ar);
   
            // Create the state object.
            ClientSocket client = new ClientSocket();
            client.socket = handler;
            handler.BeginReceive(client.buffer, 0, ClientSocket.bufferSizeConst, 0, new AsyncCallback(ReadCallback), client);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String data = String.Empty;
            display = String.Empty;
            Hashtable retrieveEmails = new Hashtable();

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            ClientSocket client = (ClientSocket)ar.AsyncState;
            Socket handler = client.socket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                client.sb.Append(Encoding.ASCII.GetString(
                    client.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                data = client.sb.ToString();

                if (data.IndexOf("<EOF>") > -1)
                {
                    LoginCheck loginCheck = new LoginCheck(data);
                    if (loginCheck.checkID())
                    {
                        Send(handler, "validUser<EOF>");
                    }
                    else
                    {
                        Send(handler, "Invalid user "+ data);
                    }
                }
                else if (data.IndexOf("<ENDCONNECT>") > -1)
                {
                    Send(handler, data);
                    handler.Shutdown(SocketShutdown.Both);
                }
                else if (data.IndexOf("<MAILSEND>") > -1)
                {
                    emailDatabaseServer.newEmail(data);
                }
                else if (data.IndexOf("<MAILRECEIVE>") > -1)
                {
                    retrieveEmails = emailDatabaseServer.retrieveEmail(data);
                    foreach (string value in retrieveEmails.Values)
                    {
                        //convert value into ascci data
                        Send(handler, data);
                    }
                }

                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(client.buffer, 0, ClientSocket.bufferSizeConst, 0,
                    new AsyncCallback(ReadCallback), client);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
 
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                display += "Sent " + bytesSent + " bytes to client.";

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    // State object for reading client data asynchronously
    public class ClientSocket
    {
        // Client  socket.
        public Socket socket = null;
        // Size of receive buffer.
        public const int bufferSizeConst = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[bufferSizeConst];
        // Received data string.
        public StringBuilder sb = new StringBuilder();

    }
}
