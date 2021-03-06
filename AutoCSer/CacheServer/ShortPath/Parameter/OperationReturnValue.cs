﻿using System;
using System.Runtime.CompilerServices;

namespace AutoCSer.CacheServer.ShortPath.Parameter
{
    /// <summary>
    /// 操作参数节点
    /// </summary>
    /// <typeparam name="valueType"></typeparam>
    public sealed partial class OperationReturnValue<valueType> : Node
    {
        /// <summary>
        /// 操作参数节点
        /// </summary>
        /// <param name="node">短路径节点</param>
        /// <param name="operationType">操作类型</param>
        internal OperationReturnValue(ShortPath.Node node, OperationParameter.OperationType operationType) : base(node, operationType) { }
        /// <summary>
        /// 操作参数节点
        /// </summary>
        /// <param name="node">短路径节点</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="value">数据</param>
        internal OperationReturnValue(ShortPath.Node node, OperationParameter.OperationType operationType, bool value) : base(node, operationType)
        {
            Parameter.Set(value);
        }
        /// <summary>
        /// 操作数据
        /// </summary>
        /// <returns></returns>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        public ReturnValue<valueType> Operation()
        {
            return new ReturnValue<valueType>(ShortPath.Client.Operation(this));
        }
        /// <summary>
        /// 操作数据
        /// </summary>
        /// <param name="onGet"></param>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        public void Operation(Action<ReturnValue<valueType>> onGet)
        {
            if (onGet == null) throw new ArgumentNullException();
            ShortPath.Client.OperationNotNull(this, onGet);
        }
        /// <summary>
        /// 操作数据
        /// </summary>
        /// <param name="onGet">直接在 Socket 接收数据的 IO 线程中处理以避免线程调度，适应于快速结束的非阻塞函数；需要知道的是这种模式下如果产生阻塞会造成 Socket 停止接收数据甚至死锁</param>
        [MethodImpl(AutoCSer.MethodImpl.AggressiveInlining)]
        public void OperationStream(Action<ReturnValue<valueType>> onGet)
        {
            if (onGet == null) throw new ArgumentNullException();
            ShortPath.Client.OperationStream(this, onGet);
        }
    }
}
