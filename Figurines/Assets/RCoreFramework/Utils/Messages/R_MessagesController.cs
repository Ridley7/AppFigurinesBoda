using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace r_core.utils.messages
{
    public static class R_MessagesController<U>
    {
        //Diccionario donde se guardan todos los mensajes
        private static Dictionary<int, List<Action<U>>> messageTable = new Dictionary<int, List<Action<U>>>();

        /// <summary>
        /// Funcion para añadir una escucha a un tipo de evento indicado
        /// </summary>
        /// <param name="_messageType"></param>
        /// <param name="_handler"></param>
        public static void AddObserver(int _messageType, Action<U> _handler)
        {
            List<Action<U>> list = null;

            if(!messageTable.TryGetValue(_messageType, out list))
            {
                list = new List<Action<U>>();
                messageTable.Add(_messageType, list);
            }

            if (!list.Contains(_handler))
            {
                messageTable[_messageType].Add(_handler);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageType"></param>
        /// <param name="_handler"></param>
        public static void RemoveObserver(int _messageType, Action<U> _handler)
        {
            List<Action<U>> list = null;

            if(messageTable.TryGetValue(_messageType, out list))
            {
                if (list.Contains(_handler))
                {
                    list.Remove(_handler);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageType"></param>
        /// <param name="_param"></param>
        public static void Post(int _messageType, U _param)
        {
            List<Action<U>> list = null;

            if(messageTable.TryGetValue(_messageType, out list))
            {
                if (list.Count == 0) return;

                for(var i = list.Count - 1; i > -1; i--)
                {
                    list[i](_param);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_messageType"></param>
        public static void ClearMessageTable(int _messageType)
        {
            if (messageTable.ContainsKey(_messageType))
            {
                messageTable.Remove(_messageType);
            }
        }

    }

}