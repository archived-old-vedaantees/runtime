using System.Collections.Generic;
using Vedaantees.Framework.Utilities;
using Type = System.Type;

namespace Vedaantees.Shells.Windows.Messaging
{
    public static class MessagingEngine
    {
        private static Dictionary<Type, List<ISubscriber>> _registry;
        
        public static void Init()
        {
            _registry = new Dictionary<Type, List<ISubscriber>>();
        }

        public static void Subscribe(ISubscriber instance)
        {
            var type = instance.GetType();

            type.GetInterfaces().ForEach(interfaceDefinition =>
            {
                if (interfaceDefinition.IsGenericType && interfaceDefinition.GetGenericTypeDefinition() == typeof(ISubscriber<>))
                {
                    var t = interfaceDefinition.GetGenericArguments()[0];

                    if (_registry.ContainsKey(t))
                        _registry[t].Add(instance);
                    else
                        _registry.Add(t, new List<ISubscriber> { instance });
                }
            });
        }

        public static void SendMessage<TMessage>(TMessage message)
        {
            if (_registry.ContainsKey(typeof(TMessage)))
                _registry[typeof(TMessage)].ForEach(instance =>
                                                   {
                                                        if(instance is ISubscriber<TMessage> handler)
                                                            handler.Execute(message);
                                                   });
        }
    }
}
