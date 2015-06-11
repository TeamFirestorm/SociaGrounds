using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Lidgren.Network
{
    /// <summary>
    /// Platform Specific implementation of the Socket Class    
    /// </summary>
    public class PlatformSocket
    {
        private Socket socket;

        /// <summary>
        /// 
        /// </summary>
        public PlatformSocket()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Broadcast
        {
            set
            {
                //this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, value);
            }
        }
        static ManualResetEvent _clientDone = new ManualResetEvent(false);


        int TIMEOUT = 1000;


        /// <summary>
        /// 
        /// </summary>
        public int Available
        {
            get
            {
                return BufferQueue.Count;
                //return this.socket.Available;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ReceiveBufferSize
        {
            get { return this.socket.ReceiveBufferSize; }
            set { this.socket.ReceiveBufferSize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SendBufferSize
        {
            get { return this.socket.SendBufferSize; }
            set { this.socket.SendBufferSize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Blocking
        {
            get
            {
                return false;//;this.socket.Blocking; 
            }
            set
            { //this.socket.Blocking = value; 
            }
        }

        internal void Bind(System.Net.EndPoint ep)
        {
            //this.socket.Bind(ep);
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Net.EndPoint LocalEndPoint
        {
            get{

#if WINDOWS_PHONE
                 return new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
#else
                if (this.socket.LocalEndPoint == null)
                {
                    return new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
                }
                else
                {
                    return this.socket.LocalEndPoint;
                }
#endif
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsBound
        {
            get
            {
                return false;// this.socket.IsBound;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public void Close(int timeout)
        {
            this.socket.Close(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool DontFragment
        {
            get
            {
                return true;// this.socket.DontFragment; 
            }
            set
            {// this.socket.DontFragment = value; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="microseconds"></param>
        /// <returns></returns>
        public bool Poll(int microseconds)
        {
            Thread.Sleep(microseconds);
            return BufferQueue.Count > 0;
            //return this.socket.Poll(microseconds, SelectMode.SelectRead);
        }


        //private bool m_pollRec = true;
        //private bool m_pollSend = true;
        private SocketAsyncEventArgs lastRec;
        private SocketAsyncEventArgs lastSend;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveBuffer"></param>
        /// <param name="offset"></param>
        /// <param name="numBytes"></param>
        /// <param name="senderRemote"></param>
        /// <returns></returns>
        private int ReceiveFrom(byte[] receiveBuffer, int offset, int numBytes, ref System.Net.EndPoint senderRemote)
        {
            //if (m_pollRec == false)
            //    return 0;
            int bytesReceived = 0;

            if (socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = senderRemote;
                // Setup the buffer to receive the data

                socketEventArg.SetBuffer(receiveBuffer, offset, numBytes);

                // Inline event handler for the Completed event.
                // Note: This even handler was implemented inline in order to make this method self-contained.
                // Stopwatch sw = new Stopwatch();
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(socketEventArg_Completed);

                bool isAsync = true;
                try
                {
                    isAsync = socket.ReceiveFromAsync(socketEventArg);
                    if (!isAsync)
                    {
                        socketEventArg_Completed(this, socketEventArg);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            return bytesReceived;//;this.socket.ReceiveFrom(receiveBuffer, offset, numBytes, SocketFlags.None, ref senderRemote);
        }
        object queueLock = new object();
        public Queue<byte[]> BufferQueue
        {
            get {
                lock (queueLock)
                {
                    return m_bufferQueue;
                }
            }
        }
        public Queue<EndPoint> BufferSender
        {
            get
            {
                lock (queueLock)
                {
                    return m_bufferSender;
                }
            }
        }
        public Queue<int> BufferTransferred
        {
            get
            {
                lock (queueLock)
                {
                    return m_bufferTransferred;
                }
            }
        }
        private Queue<byte[]> m_bufferQueue = new Queue<byte[]>();
        private Queue<EndPoint> m_bufferSender = new Queue<EndPoint>();
        private Queue<int> m_bufferTransferred = new Queue<int>();
        public object recLock = new object();
        void socketEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            lock (recLock)
            {
                if (e.SocketError != SocketError.Success)
                {

                    //
                    // we got error on the socket or connection was closed
                    //
                    // Close();
                    receiving = false;
                    return;
                }

                try
                {
                    // try to process a new video frame if enough data was read
                    // base.ProcessPacket(m_Buffer, e.Offset, e.BytesTransferred);
                    if (e.Buffer != null)
                    {
                        byte[] receiveBuffer = new byte[e.Buffer.Length];
                        Buffer.BlockCopy(e.Buffer, e.Offset, receiveBuffer, e.Offset, e.BytesTransferred);

                        BufferQueue.Enqueue(receiveBuffer);
                        BufferTransferred.Enqueue(e.BytesTransferred);
                        BufferSender.Enqueue(e.RemoteEndPoint);
                        PlatformSocket.BuffersReceived += 1;
                    }
                }
                catch (Exception ex)
                {
                    // log and error
                }
                try
                {
                    bool willRaiseEvent = socket.ReceiveFromAsync(e);
                    if (!willRaiseEvent)
                    {
                        socketEventArg_Completed(this, e);
                    }
                }
                catch (Exception ex)
                {
                    
                    
                }
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="numBytes"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int SendTo(byte[] data, int offset, int numBytes, System.Net.EndPoint target)
        {
            BuffersSend += 1;
            int bytesTransferred = 0;
            if (socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                // Set properties on context object
                socketEventArg.RemoteEndPoint = target;
                // Inline event handler for the Completed event.
                // Note: This event handler was implemented inline in order to make this method self-contained.
                // System.Diagnostics.Stopwatch sw = new Stopwatch();
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                  
                    bytesTransferred = e.BytesTransferred;
                    // Unblock the UI thread
                    lastSend = e;
                    _clientDone.Set();
                    //sw.Stop();
                });
                // Add the data to be sent into the buffer
                socketEventArg.SetBuffer(data, offset, numBytes);
                // Sets the state of the event to nonsignaled, causing threads to block
                _clientDone.Reset();
                //  m_pollSend = false;

                //  sw.Start();
                bool isAsync = true;
                try
                {
                    isAsync = socket.SendToAsync(socketEventArg);
                    if (isAsync)
                    {
                        _clientDone.WaitOne(TIMEOUT);
                    }
                    else
                    {
                        bytesTransferred = socketEventArg.BytesTransferred;
                        // Unblock the UI thread
                        lastSend = socketEventArg;
                        BuffersSend += 1;
                    }
                }
                catch (Exception e)
                {
                    //m_pollSend = true;
                    return 0;
                }


            }

            SendFirst = true;
            return bytesTransferred;// this.socket.SendTo(data, offset, numBytes, SocketFlags.None, target);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="socketShutdown"></param>
        public void Shutdown(SocketShutdown socketShutdown)
        {
            this.socket.Shutdown(socketShutdown);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Setup()
        {
        }




        bool receiving =false;
        public bool SendFirst = false;
        internal void StartReceive(byte[] m_receiveBuffer, int p, int p_2, ref EndPoint m_senderRemote)
        {
            if (receiving == false)
            {
                receiving = true;
                
                ReceiveFrom(m_receiveBuffer, p, p_2, ref m_senderRemote);
                
            }
        }

        public static int BuffersReceived { get; set; }

        public static int BuffersSend { get; set; }
    }
}