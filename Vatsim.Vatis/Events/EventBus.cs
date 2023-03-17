using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Vatsim.Vatis.Events;

public static class EventBus
{
    private static readonly List<object> mRegisteredSubscribers = new();
    private static readonly Dictionary<Type, List<Handler>> mHandlers = new();

    public static void Register(object subscriber)
    {
        if (mRegisteredSubscribers.Contains(subscriber))
        {
            throw new ApplicationException("Subscriber is already registered");
        }

        if (subscriber is Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(() =>
                {
                    Register(control);
                });
                return;
            }
        }

        mRegisteredSubscribers.Add(subscriber);

        foreach (var method in subscriber.GetType().GetMethods().Where(n => n.Name == "HandleEvent"))
        {
            var parameters = method.GetParameters();
            foreach(var param in parameters)
            {
                if(typeof(IEvent).IsAssignableFrom(param.ParameterType))
                {
                    AddHandler(param.ParameterType, subscriber, method);
                }
            }
        }
    }

    public static void Unregister(object subscriber)
    {
        if (!mRegisteredSubscribers.Contains(subscriber))
        {
            throw new ApplicationException("Subscriber is not registered");
        }

        if (subscriber is Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(() =>
                {
                    Unregister(control);
                });
                return;
            }
        }

        mRegisteredSubscribers.Remove(subscriber);

        foreach (var handler in mHandlers.Values)
        {
            handler.RemoveAll(n => n.Subscriber == subscriber);
        }
    }

    public static void Publish(object sender, IEvent evt)
    {
        PublishInternal(sender, evt);
    }

    private static void PublishInternal(object sender, IEvent evt)
    {
        if (sender is Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(() =>
                {
                    PublishInternal(sender, evt);
                });
                return;
            }
        }

        var eventType = evt.GetType();

        if (!mHandlers.ContainsKey(eventType))
        {
            return;
        }

        foreach (var handler in mHandlers[eventType].Where(n => n.Subscriber != sender))
        {
            handler.Handle(evt);
        }
    }

    private static void AddHandler(Type eventType, object subscriber, MethodInfo method)
    {
        if (!mHandlers.ContainsKey(eventType))
        {
            mHandlers[eventType] = new List<Handler>();
        }
        mHandlers[eventType].Add(new Handler(subscriber, method));
    }

    private class Handler
    {
        public object Subscriber { get; set; }
        public MethodInfo Method { get; set; }

        public Handler(object subscriber, MethodInfo method)
        {
            Subscriber = subscriber;
            Method = method;
        }

        public void Handle(IEvent evt)
        {
            Method.Invoke(Subscriber, new object[] { evt });
        }
    }
}
