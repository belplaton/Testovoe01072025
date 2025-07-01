using System;
using System.Collections.Generic;
using System.Linq;

namespace waterb.Networking
{
    public static class NetworkHandlerRegistry
    {
        public struct HandlerInfo
        {
            public Type HandlerType;
            public Type RequestType;
            public Type ResponseType;
        }

        private static readonly Dictionary<Type, HandlerInfo> _handlersInfoDict;
        private static readonly HandlerInfo[] _handlersInfoArray;

        public static IReadOnlyList<HandlerInfo> HandlersInfoArray => _handlersInfoArray;
        
        static NetworkHandlerRegistry()
        {
            var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INetworkRequestHandler<,>))
                    .Select(i => new { HandlerType = t, InterfaceType = i }))
                .ToList();

            _handlersInfoDict = new Dictionary<Type, HandlerInfo>();
            foreach (var entry in handlerTypes)
            {
                var tRequest = entry.InterfaceType.GetGenericArguments()[0];
                var tResponse = entry.InterfaceType.GetGenericArguments()[1];
                if (!_handlersInfoDict.ContainsKey(tRequest))
                {
                    _handlersInfoDict[tRequest] = new HandlerInfo
                    {
                        HandlerType = entry.HandlerType,
                        RequestType = tRequest,
                        ResponseType = tResponse
                    };
                }
            }

            _handlersInfoArray = _handlersInfoDict.Values.ToArray();
        }

        public static bool TryGetHandlerInfo<TRequest>(out HandlerInfo info) where TRequest : IRequest
        {
            return _handlersInfoDict.TryGetValue(typeof(TRequest), out info);
        }
    }
} 