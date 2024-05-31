public class SocketClientManager
    {
        public Socket _socket = null;
        public EndPoint endPoint = null;
        public bool _isConnected = false;
        public SocketClientManager(string ip, int port)
        {
            IPAddress _ip = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(_ip, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Start()
        {
            _socket.BeginConnect(endPoint, ConnectedCallback, _socket);
            _isConnected = true;
            Thread socketClient = new Thread(SocketClientReceive);
            socketClient.IsBackground = true;
            socketClient.Start();
        }
        public void SocketClientReceive()
        {
            while (_isConnected)
            {
                try {
                    _socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, buffer);
                }
                catch (SocketException ex)
                {
                    _isConnected = false;
                }
                
                Thread.Sleep(100);
            }
        }
        public void ReceiveCallback(IAsyncResult ar)
        {
        }
    }
