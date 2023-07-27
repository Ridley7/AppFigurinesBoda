using UnityEngine;

namespace r_core.util
{
	public class R_Singleton<T> : MonoBehaviour where T : R_Singleton<T>
	{
		//Instancia de nuestro singleton
		private static T _instance;

		//Boleano para saber si esta inicializado
		protected bool initialized { get; set; }

		//Booleano que nos indica si la aplicacion se esta destruyendo
		//y no produzca un warning o error no controlado
		private static bool applicationIsQuitting = false;

		//Objeto para bloquear el hilo de ejecución en el caso de que Unity
		//fuera multihilo en el futuro
		private static object syncRoot = new Object();

		public static T GetInstance()
		{
			if (applicationIsQuitting)
			{
				return null;
			}

			if(_instance == null)
			{
				lock (syncRoot)
				{
					if(_instance == null)
					{
						_instance = GameObject.FindObjectOfType<T>();
					}

					//Si nuestro objeto todavia esta a null
					if(_instance == null)
					{
						GameObject go = new GameObject(typeof(T).Name);
						_instance = go.AddComponent<T>();
					}

					if (!_instance.initialized)
					{
						_instance.Initialize();
						_instance.initialized = true;
					}
				}
			}

			return _instance;

		}


		private void Awake()
		{
			if(_instance == null)
			{
				GetInstance();
			}
		}

		public virtual void OnDestroy()
		{
			//Esto es un parche para evitar el destroy aleatorio de scripts
			applicationIsQuitting = true;
		}

		protected virtual void Initialize(bool dontdestroy = false)
		{
			if (dontdestroy)
			{
				DontDestroyOnLoad(_instance.gameObject);
			}
		}

	}

}