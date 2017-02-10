//======================================================================
//
//        Copyright (C)  1996-2012  lfz    
//        All rights reserved
//
//        Filename : BufferManager
//        DESCRIPTION : ����ع����û�SocketAsyncEventArgs
//
//        Created By �ַ��� at  2013-01-08 09:11:01
//        https://git.oschina.net/lfz
//
//======================================================================   

using System.Collections.Generic;
using System.Net.Sockets;

namespace Lfz.Network
{
    /// <summary>
    /// ����ع����û�SocketAsyncEventArgs
    /// </summary>
    internal class BufferManager
    {
        /// <summary>
        ///  the total number of bytes controlled by the buffer pool
        /// </summary>
        readonly int _mNumBytes;

        /// <summary>
        /// the underlying byte array maintained by the Buffer Manager
        /// </summary> 
        byte[] _mBuffer;

        readonly Stack<int> _mFreeIndexPool;
        int _mCurrentIndex;
        readonly int _mBufferSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalBytes">�������ֽ�</param>
        /// <param name="bufferSize">������������С</param>
        public BufferManager(int totalBytes, int bufferSize)
        {
            _mNumBytes = totalBytes;
            _mCurrentIndex = 0;
            _mBufferSize = bufferSize;
            _mFreeIndexPool = new Stack<int>();
        }

        /// <summary>
        /// ʹ��BufferManager���仺��ؼ�
        /// </summary>
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            _mBuffer = new byte[_mNumBytes];
        }

        /// <summary>
        /// �ӻ�����з��仺���SocketAsyncEventArgs����
        /// </summary>
        /// <param name="args"></param>
        /// <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (_mFreeIndexPool.Count > 0)
            {
                args.SetBuffer(_mBuffer, _mFreeIndexPool.Pop(), _mBufferSize);
            }
            else
            {
                if ((_mNumBytes - _mBufferSize) < _mCurrentIndex)
                {
                    return false;
                }
                args.SetBuffer(_mBuffer, _mCurrentIndex, _mBufferSize);
                _mCurrentIndex += _mBufferSize;
            }
            return true;
        }

        /// <summary>
        /// �Ƴ�SocketAsyncEventArg�����Ļ��棬������ѹ�뻺���
        /// </summary>
        /// <param name="args"></param>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            _mFreeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

    }
}