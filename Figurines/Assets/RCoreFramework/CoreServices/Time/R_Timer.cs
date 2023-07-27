using System;
using UnityEngine;

namespace r_core.time
{
    public class R_Timer
    {

        float waitingTime = 99999999f;// Tiempo que espera el timer, los segundos que va a esperar 
        double timer = 0.0f; //El contador del propio timer, es decir la variable que descuenta el tiempo.
        uint idTimer = 0; //Identificador del timer.
        public bool CannotBePaused { private set; get; }
        public bool isActive = false;
        public Action<R_Timer> cacheDelegateAction; //Esta es la funcion que queremos ejecutar cuanto el timer acabe
        public Action cacheAction;
        public object Context { get; private set; } //El contexto donde se encuentra la funcion cacheada.
                                                    //Para que el timer sepa que funcion debe ejecutar, se ha de almacenar su contexto también.
                                                    //Burdamente: la clase que contiene el metodo a ejecutar

        public void SetContext(object _context)
        {
            Context = _context;
        }

        public void SetTimerActionCompleteCallback(Action newcallback)
        {
            cacheAction = newcallback;
        }

        public void SetTimerActionCompletedWithSelfCallback(Action<R_Timer> newcallback)
        {
            cacheDelegateAction = newcallback;
        }

        public bool GetIsActive()
        {
            return isActive;
        }

        public double GetTimerValue()
        {
            return timer;
        }

        public uint GetTimerID()
        {
            return idTimer;
        }

        public float GetWaitingTimer()
        {
            return waitingTime;
        }

        public void Destroy()
        {
            SetContext(null);
            SetTimerActionCompleteCallback(null);
            SetTimerActionCompletedWithSelfCallback(null);
        }

        #region metodos con chicha del timer
        //Pausamos todos los timers activos o despausamos todos los timers inactivos
        public void PauseTimer(bool gameIsPaused)
        {
            if (!CannotBePaused)
            {
                if (isActive && gameIsPaused)
                {
                    isActive = false;
                }
                else if (!isActive && !gameIsPaused && waitingTime != 99999999f)
                {
                    isActive = true;
                }
            }
        }

        public void StartTimer(float _waitValue, bool _isActive, bool _cannotbePaused, uint _timerid)
        {
            waitingTime = _waitValue;
            timer = Time.time;
            CannotBePaused = _cannotbePaused;
            isActive = _isActive;
            idTimer = _timerid;
        }

        public void StartTimer(float _waitValue, bool _isActive, uint _timerid)
        {
            waitingTime = _waitValue;
            timer = Time.time;
            isActive = _isActive;
            idTimer = _timerid;
            CannotBePaused = false;
        }

        public void StartTimer(float _waitValue, bool _isActive)
        {
            waitingTime = _waitValue;
            timer = Time.time;
            isActive = _isActive;
            idTimer = 0;
            CannotBePaused = false;
        }

        public void StopTimer()
        {
            isActive = false;
            CannotBePaused = false;
            waitingTime = 99999999f;
            timer = Time.time;
            idTimer = 0;
            SetContext(null);
            SetTimerActionCompleteCallback(null);
            SetTimerActionCompletedWithSelfCallback(null);
        }

        public void UpdateTimer()
        {
            if (isActive)
            {
                //elapsed = tiempo transcurrido
                double elapsed = Time.time - timer;

                if (elapsed >= waitingTime)
                {
                    //Si el tiempo transcurrido es superior o igual al tiempo
                    //de espera ejecutamos el metodo que quiere el usuario
                    timer = Time.time;

                    if (cacheDelegateAction != null)
                    {
                        cacheDelegateAction(this);
                        StopTimer();
                        return;
                    }

                    if (cacheAction != null)
                    {
                        cacheAction();
                        StopTimer();
                        return;
                    }
                }
            }
        }

        #endregion
    }
}
