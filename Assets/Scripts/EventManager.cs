using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GPE338Final.Units;

namespace GPE338Final
{
    namespace CustomEvents
    {
        [System.Serializable]
        public struct MyEvent
        {
            public string eventID;
            public GameObject eventTrigger;

            public MyEvent(string inID, GameObject inObj)
            {
                eventID = inID;
                eventTrigger = inObj;
            }
        }

        public class EventManager
        {

            public delegate void CallBackMethod(string inEvent);

            private static Dictionary<string, CallBackMethod> listeners = new Dictionary<string, CallBackMethod>();

            public static void SubscribeListeners(string inEvent, CallBackMethod listener)
            {
                if (!listeners.ContainsKey(inEvent))
                {
                    listeners.Add(inEvent, listener);
                }
                else
                {
                    listeners[inEvent] += listener;
                }
            }

            public static void UnSubscribeListeners(string inEvent, CallBackMethod listener)
            {
                if (listeners.ContainsKey(inEvent))
                {
                    listeners[inEvent] -= listener;

                    if (listeners[inEvent] == null)
                    {
                        listeners.Remove(inEvent);
                    }
                }
            }

            public static void TriggerEvent(string inEvent)
            {
                if (listeners.ContainsKey(inEvent))
                {
                    listeners[inEvent](inEvent);
                }
            }

        }
    }
}
