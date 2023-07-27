using System.Collections.Generic;
using System;
using UnityEngine;

namespace r_core.time
{
	public class R_TimerController
	{
		//Lista de timers disponibles
		List<R_Timer> timerList;

		//Contador para asignar id's a los timers
		uint timerID = 0;

		int currentActiveTimer = 0;

		//Cantidad total de timers
		int totalList = 0;

        #region Init and destroy timers
        public void InitTimerManager(int _amountOfTimers)
		{
			timerList = new List<R_Timer>();

			for(int cnt = 0; cnt < _amountOfTimers; cnt++)
			{
				R_Timer timer = new R_Timer
				{
					isActive = false
				};

				timerList.Add(timer);
			}

			totalList = timerList.Count;
		}

		public void DestroyAndCleanUp()
		{
			for (int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				timer.Destroy();
			}

			timerList = null;
		}
        #endregion

		void IncreaseTimerPosition()
		{
			//Es propable que se haya que tener algo mas en cuenta
			//Preguntarselo a Alexa cuando se pueda
			currentActiveTimer++;
		}

		public void PauseAllTimers(bool gameIsPaused)
		{
			for(int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				timer.PauseTimer(gameIsPaused);
			}
		}

		public void StopAllTimers() 
		{
			for(int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				timer.StopTimer();
			}
		}

        #region control para timers individuales

		public void StopTimerWithID(uint timerID) 
		{
			for(int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				if(timer.GetTimerID() == timerID)
				{
					timer.StopTimer();
				}
			}		
		}

		public R_Timer GetUnusedTimer()
		{
			for(int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				if (!timer.GetIsActive())
					return timer;
			}

			return null;
		}
        #endregion
		
        #region timers for lambdas
		public void StartTimer(float _timerValue, object _aContext, Action<R_Timer> _newcallback)
		{
			StartTimer(_timerValue, false, _aContext, _newcallback);
		}

		public void StartTimer(float _timerValue, bool _cannotBePause, object _aContext, Action<R_Timer> _newcallback)
		{
			timerID++;

			if(timerID >= uint.MaxValue)
			{
				timerID = 1;
			}

			for(int cnt = currentActiveTimer; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				if (!timer.GetIsActive())
				{
					//clear any previous callback set
					timer.StopTimer();

					//set the new callback and start the timer
					timer.SetContext(_aContext);
					timer.SetTimerActionCompletedWithSelfCallback(_newcallback);
					timer.StartTimer(_timerValue, true, _cannotBePause, timerID);
					IncreaseTimerPosition();

					break;
				
				}
			}
		}

		public uint StartTimerId(float _timerValue, object _aContext, Action<R_Timer> _newcallback)
		{
			return StartTimerId(_timerValue, false, _aContext, _newcallback);
		}

		public uint StartTimerId(float _timerValue, bool _cannotBePaused, object _aContext, Action<R_Timer> _newcallback)
		{
			timerID++;

			if(timerID >= uint.MaxValue)
			{
				timerID = 1;
			}

			for(int cnt = currentActiveTimer; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				if (!timer.GetIsActive())
				{
					//clear any previous callback set
					timer.StopTimer();

					//set the new callback and start the timer
					timer.SetContext(_aContext);
					timer.SetTimerActionCompletedWithSelfCallback(_newcallback);
					timer.StartTimer(_timerValue, true, _cannotBePaused, timerID);

					return timerID;
				}
			}

			return 0; //No se ha encontrado ningun timer, por lo que debemos incrementar nuestra pool de timers.
		}

        #endregion

        //Aqui es donde se hace la magia
        #region
		//Actualizamos solo los timers activos, actualizar basicamente es
		//incrementa el contador de tiempo de cada timer y comprueba si ese te ha llegado
		//al final
		public void UpdateTimers()
		{
			for(int cnt = 0; cnt < totalList; cnt++)
			{
				R_Timer timer = timerList[cnt];

				if (timer == null)
					continue;

				if (timer.CannotBePaused)
				{
					timer.UpdateTimer();
				}
				else
				{
					if (timer.isActive)
					{
						timer.UpdateTimer();
					}
				}
			}
		}
        #endregion


    }
}
