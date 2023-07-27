//Core reusable para cualquier proyecto hecho con Unity
//Estado del proyecto:
/*
 * - Timers controllers : hecho
 * - Messages controllers : hecho
 * - Language controller: hecho
 * - Audio controller: play sounds, play music : en pruebas
 */

//Cosas que faltan, y sobretodo entenderlas:
/*
 - Actions controller: Load audio, load translations, Load level. -> I
 - Scene controller: Load unity scene
 - Screen controller: UGUI y NGUI -> I
 - Gamepad Controller: PS4, PC, Xbox One, Swithc, WiiU
 - Save/Load:
 */

using UnityEngine;
using r_core.util;
using r_core.time;
using System;
using r_core.language;
using r_core.save_load;

namespace r_core.core
{
    public class R_Core : R_Singleton<R_Core>
    {
        //Servicios del core
        //---------------------------------------------------------
        //Servicio de timers.
        private R_TimerController timer = null;

        //Servicio de traduccion
        private LanguageController language;

        //Servicio de audio
        //serialized fields are initiated by unity (called new instance)
        [SerializeField] private R_SoundsGamePool soundsGamePool = null;

        //Servicio de guardar y cargar datos
        private SaveLoadController saveLoad;

        protected override void Initialize(bool dontdestroy = false)
        {
            //Indicamos que no se destruya el core
            base.Initialize(true);

            //Indicamos el frame rate al que se va a ejecutar nuestro juego
            Application.targetFrameRate = R_CoreConstants.TARGET_FRAME_RATE;

            //Cargamos los demos elementos del core
            LoadCoreSystem();

            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            //Limpiamos la memoria de timers
            timer.DestroyAndCleanUp();
            timer = null;

            //Limpiamos la memoria de todas las referencias a traducciones
            language.ClearAllStrings();
            language = null;

            //Limpiamos el servicio de sonido
            soundsGamePool = null;

            //Limpiamos el servicio de guardado y carga de datos
            saveLoad = null;
        }

        private void Update()
        {
            //Actualizamos los timers
            timer.UpdateTimers();
        }

        private void LoadCoreSystem()
        {
            //Creamos el controlador de timers
            timer = new R_TimerController();
            timer.InitTimerManager(R_CoreConstants.TIMERS); //Hasta 3000 timers usa Alex
            //Evitamos que el GC se lleve nuestro timer por algun tipo de inactividad
            System.GC.KeepAlive(timer);

            //Creamos el sistema de idiomas
            language = new LanguageController();

            //Cargamos los ficheros de idiomas de la carpeta resources
            //ATENCION POR QUE ESTO LO TIENE QUE HACER OTRO ELEMENO QUIZAS EL SISTEMA ACCIONES
            //ESTA BASURILLA TIENE QUE ESTAR EN OTRO LADO, QUE HA DE SER OTRA PARTE DEL CORE
            for(int i = 0; i < Resources.LoadAll<TextAsset>("Translations/TranslationFilesJson/").Length; i++)
            {

                TextAsset translationIndexFile = Resources.LoadAll<TextAsset>("Translations/TranslationFilesJson/")[i];

                language.LoadLanguageFileJson(translationIndexFile, R_CoreConstants.GLOBAL_LANGUAGE_SELECTED, (result) =>
                {
                    if (!result)
                    {
                        Debug.LogError("error loading translations file");
                    }
                });
            }

            //Cargamos el audio controller
            soundsGamePool = FindObjectOfType<R_SoundsGamePool>();
            soundsGamePool.InitLoadAllSounds();

            //Cargamos el servicio de guardado y carga de datos
            saveLoad = new SaveLoadController();
        }

        #region API Timers
        //Solo declaro lo que he gastado alguna vez en mi vida
        /// <summary>
        /// Función para pone en marcha un timer
        /// </summary>
        /// <param name="_timerValue">Duración del timer</param>
        /// <param name="_context">Clase que tiene el metodo que se va a ejecutar cuando acabe el timer</param>
        /// <param name="_callback">Metodo que se va a ejecutar cuando acabe el timer</param>
        public void StartTimer(float _timerValue, object _context, Action<R_Timer> _callback)
        {
            timer.StartTimer(_timerValue, _context, _callback);
        }

        /// <summary>
        /// Función que pone en marcha un timer y se obtiene un identificador para el rastreo
        /// </summary>
        /// <param name="_timerValue">Duración del timer</param>
        /// <param name="_context">Clase que tiene el método que se va a ejecutar cuando acabe el timer. Suele ser this</param>
        /// <param name="_callback">Método que se va a ejecutar cuando acabe el timer</param>
        /// <returns>Identificador del timer</returns>
        public uint StartTimerWithId(float _timerValue, object _context, Action<R_Timer> _callback)
        {
            return timer.StartTimerId(_timerValue, _context, _callback);
        }

        /// <summary>
        /// Función que detiene un timer.
        /// </summary>
        /// <param name="_idTimer">Identificador del timer que se va a parar</param>
        public void StopTimerWithId(uint _idTimer)
        {
            timer.StopTimerWithID(_idTimer);
        }
        #endregion

        #region API Languages
        public string GetString(String key_json)
        {
            return language.GetString(key_json);
        }

        public void SetNewLanguage(LanguageType languageType)
        {
            language.SetNewLanguage(languageType);
        }

        public LanguageType GetLanguage()
        {
            return language.GetLanguage();
        }
        #endregion

        #region API audio
        public void PlaySound(string name, float volume)
        {
            soundsGamePool.PlaySound(name, volume);
        }

        public void EnableSounds()
        {
            soundsGamePool.EnableSounds();
        }

        public void DisableSounds()
        {
            soundsGamePool.DisableSounds();
        }

        #endregion

        #region API SaveLoad

        public void Save(string information, string namefile)
        {
            saveLoad.Save(information, namefile);
        }

        public R_DummyData LoadDummyData(string filename)
        {
            return saveLoad.Load<R_DummyData>(filename);
        }

        public R_DummyInfo LoadDummyInfo(string filename)
        {
            return saveLoad.Load<R_DummyInfo>(filename);
        }

        #endregion
    }

}
