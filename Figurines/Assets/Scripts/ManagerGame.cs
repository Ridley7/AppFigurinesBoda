using DG.Tweening;
using r_core.core;
using r_core.utils.messages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [Header("Cabezas")]
    public Transform containerCabezaButton;
    public GameObject PFB_button_cabeza;
    private UIGrid grid_cabezas;
    public GameObject panelCabezas;
    public Transform transformCabeza;
    private UISprite cabezaFigurin;

    [Header("Figurines")]
    public Transform containerFigurinButton;
    public GameObject PFB_button_figurin;
    private UIGrid grid_figurines;
    public GameObject panelVestidos;
    public Transform transformFigurin;
    private UISprite cuerpoFigurin;
    [SerializeField] private UIScrollView scrollViewFigurines;

    [Header("Poses")]
    public Transform containerPoseButton;
    public GameObject PFB_button_pose;    
    private UIGrid grid_poses;
    public GameObject panelFigurines;
    
    public GameObject sideRightMenu;

    #region SCREENS
    public GameObject introScreen;
    public GameObject mainScreen;
    public GameObject panelConfig;
    #endregion

    #region ANIMATIONS
    private bool togglePanelCabezas = false;
    private bool togglePanelVestidos = false;
    private bool togglePanelPoses = false;
    #endregion

    //Lista de figurines
    FigurinButton[] botones_figurin;
    bool hay_defecto = true;
    int index = 0;

    private void Awake()
    {
        grid_cabezas = containerCabezaButton.GetComponent<UIGrid>();
        cabezaFigurin = transformCabeza.GetComponent<UISprite>();

        grid_figurines = containerFigurinButton.GetComponent<UIGrid>();
        cuerpoFigurin = transformFigurin.GetComponent<UISprite>();

        grid_poses = containerPoseButton.GetComponent<UIGrid>();
    }

    void Start()
    {
        Init();
    }

    public void ShowPanelCabezas()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        //Animamos el panel de las cabezas para que salga
        AnimatePanelCabezas();
        sideRightMenu.SetActive(!sideRightMenu.activeInHierarchy);
        
        //Ocultamos los otros paneles en caso de que estuviesen abiertos
        HidePanelFigurines();
        HidePanelPoses();
    }

    public void ShowPanelRopa()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        //Animamos el panel de la ropa para que salga
        AnimatePanelRopa();

        //Ocultamos los otros paneles en caso de que estuviesen abiertos
        HidePanelCabezas();
        HidePanelPoses();
    }

    public void ShowPanelFigurines()
    {
        //Reproducimos sonido
        R_Core.GetInstance().PlaySound("tap_button", 1.0f);

        //Animamos el panel de la ropa para que salga
        AnimatePanelPoses();

        //Ocultamos los otros paneles en caso de que estuviesen abiertos
        HidePanelCabezas();
        HidePanelFigurines();
    }

    private void AnimatePanelCabezas()
    {
        togglePanelCabezas = !togglePanelCabezas;

        if (togglePanelCabezas)
        {
            //DesplazarPanelCabeza(GameEnums.DireccionAnimacion.UP);
            DesplazarPanel(GameEnums.DireccionAnimacion.UP, grid_cabezas);
        }
        else
        {
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_cabezas);
        }
    }

    private void AnimatePanelRopa()
    {
        togglePanelVestidos = !togglePanelVestidos;

        if (togglePanelVestidos)
        {
            DesplazarPanel(GameEnums.DireccionAnimacion.UP, grid_figurines);
        }
        else
        {
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_figurines);
        }
    }

    private void AnimatePanelPoses()
    {
        togglePanelPoses = !togglePanelPoses;

        if (togglePanelPoses)
        {
            DesplazarPanel(GameEnums.DireccionAnimacion.UP, grid_poses);
        }
        else
        {
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_poses);
        }
    }

    private void HidePanelPoses()
    {
        //Ocultamos panel poses
        if (grid_poses.transform.localPosition.y == 0)
        {
            togglePanelPoses = false;
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_poses);
        }
    }

    private void HidePanelFigurines()
    {
        //Ocultamos panel figurines
        if (grid_figurines.transform.localPosition.y == 0)
        {
            togglePanelVestidos = false;
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_figurines);
        }
    }

    private void HidePanelCabezas()
    {
        //Ocultamos panel cabeza
        if (grid_cabezas.transform.localPosition.y == 0)
        {
            togglePanelCabezas = false;
            DesplazarPanel(GameEnums.DireccionAnimacion.DOWN, grid_cabezas);
        }

        if (sideRightMenu.activeInHierarchy) sideRightMenu.SetActive(false);
    }

    private void HideAllPanels()
    {
        HidePanelCabezas();
        HidePanelFigurines();
        HidePanelPoses();
    }

    private void DesplazarPanel(GameEnums.DireccionAnimacion direccion, UIGrid grid)
    {
        if(direccion == GameEnums.DireccionAnimacion.DOWN)
        {
            grid.transform.DOLocalMove(new Vector3(grid.transform.localPosition.x, -grid.cellHeight, 0), 0.5f);

            for (int i = 0; i < grid.transform.childCount; i++)
            {
                TweenAlpha.Begin(grid.transform.GetChild(i).transform.gameObject, 0.5f, 0.0f);
            }
        }
        else
        {
            grid.transform.DOLocalMove(new Vector3(grid.transform.localPosition.x, 0, 0), 0.5f);
            
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                TweenAlpha.Begin(grid.transform.GetChild(i).transform.gameObject, 0.5f, 1.0f);
            }
        }
    }

    public void ZoomInHead()
    {
        transformCabeza.localScale = new Vector3(transformCabeza.localScale.x + 0.1f, transformCabeza.localScale.y + 0.10f, transformCabeza.localScale.z);
    }

    public void ZoomOutHead()
    {
        transformCabeza.localScale = new Vector3(transformCabeza.localScale.x - 0.1f, transformCabeza.localScale.y - 0.10f, transformCabeza.localScale.z);

    }
    private void Init()
    {
        //Cargamos la informacion de los botones
        TextAsset data_app = Resources.Load<TextAsset>("data");

        //Decodificamos la informacion del JSON de datos
        Hashtable decodedData = (Hashtable)MiniJSON.jsonDecode(data_app.text);

        //Obtenemos la lista de amigos
        ArrayList data_amigos = (ArrayList)decodedData["lista_amigos"];

        //Obtenemos los hijos del containerCabezaButton
        CabezaButton[] botones_cabeza = containerCabezaButton.GetComponentsInChildren<CabezaButton>();

        for (int i = 0; i < data_amigos.Count; i++)
        {
            Hashtable decodeTable = (Hashtable)data_amigos[i];

            //Si añadimos un personaje más a los datos creamos un prefab
            if (i >= botones_cabeza.Length)
            {
                //Tenemos que instanciar un prefab de boton
                GameObject pfb_button_cabeza = Instantiate(PFB_button_cabeza, containerCabezaButton);
                pfb_button_cabeza.transform.localScale = Vector3.one;

                CabezaButton cabezaButton = pfb_button_cabeza.GetComponent<CabezaButton>();
                cabezaButton.SetValues(
                    decodeTable["sprite_cabeza"].ToString(),
                    decodeTable["nombre_cabeza"].ToString(),
                    (GameEnums.TipoAmigo)System.Enum.Parse(typeof(GameEnums.TipoAmigo), decodeTable["tipo_amigo"].ToString()));

                
            }
            else
            {
                botones_cabeza[i].SetValues(
                    decodeTable["sprite_cabeza"].ToString(),
                    decodeTable["nombre_cabeza"].ToString(),
                    (GameEnums.TipoAmigo)System.Enum.Parse(typeof(GameEnums.TipoAmigo), decodeTable["tipo_amigo"].ToString()));
            }
        }

        grid_cabezas.Reposition();
        
        //Obtenemos la lista de figurines de los datos
        ArrayList data_figurines = (ArrayList)decodedData["lista_figurines"];

        //Obtenemos los hijos del containerFigurinButton
        //NOTA: Si estan desactivados los prefab estos no se recogen.
        botones_figurin = containerFigurinButton.GetComponentsInChildren<FigurinButton>();
        
        for (int i = 0; i < data_figurines.Count; i++)
        {
            Hashtable decode_figurin = (Hashtable)data_figurines[i];

            //Si añadimos un figurin más a los datos creamos un prefab
            if (i >= botones_figurin.Length)
            {
                //Tenemos que instancia un prefab de boton figurin
                GameObject pfb_button_figurin = Instantiate(PFB_button_figurin, containerFigurinButton);
                pfb_button_figurin.transform.localScale = Vector3.one;

                FigurinButton figurinButton = pfb_button_figurin.GetComponent<FigurinButton>();
                figurinButton.SetValues(
                    decode_figurin["sprite_figurin"].ToString(),
                    decode_figurin["label_figurin"].ToString(),
                    (GameEnums.TipoFigurin)System.Enum.Parse(typeof(GameEnums.TipoFigurin), decode_figurin["tipo_figurin"].ToString()),
                    (GameEnums.TipoPose)System.Enum.Parse(typeof(GameEnums.TipoPose), decode_figurin["tipo_pose"].ToString()));

                hay_defecto = false;                
            }
            else
            {                
                botones_figurin[i].SetValues(
                    decode_figurin["sprite_figurin"].ToString(),
                    decode_figurin["label_figurin"].ToString(),
                    (GameEnums.TipoFigurin)System.Enum.Parse(typeof(GameEnums.TipoFigurin), decode_figurin["tipo_figurin"].ToString()),
                    (GameEnums.TipoPose)System.Enum.Parse(typeof(GameEnums.TipoPose), decode_figurin["tipo_pose"].ToString()));
                
            }            
        }

        //Desactivamos el defecto
        if (hay_defecto)
        {
            int numero_datos = data_figurines.Count;
            int numero_prefab = botones_figurin.Length;

            //Calculamos el numero de prefabs que tenemos que desactiva
            index = numero_prefab - numero_datos;

            //Calculamos el indice de donde tenemos que empezar el recorrido
            index = numero_prefab - index;

            for (int i = index; i < botones_figurin.Length; i++)
            {
                botones_figurin[i].gameObject.SetActive(false);
            }

        }

        grid_figurines.Reposition();

        //Obtenemos la lista de poses
        ArrayList data_poses = (ArrayList)decodedData["lista_poses"];

        //Obtebenos los hijos del containerPosesButton
        PoseButton[] botones_pose = containerPoseButton.GetComponentsInChildren<PoseButton>();

        for (int i = 0; i < data_poses.Count; i++)
        {
            Hashtable decode_pose = (Hashtable)data_poses[i];

            //Si añadimos una pose mas a los datos creamos un prefab
            if (i >= botones_pose.Length)
            {
                //Tenemos que instanciar un prefab de boton pose
                GameObject pfb_button_pose = Instantiate(PFB_button_pose, containerPoseButton);
                pfb_button_pose.transform.localScale = Vector3.one;

                PoseButton poseButton = pfb_button_pose.GetComponent<PoseButton>();
                poseButton.SetValues(
                    decode_pose["sprite_pose"].ToString(),
                    decode_pose["label_pose"].ToString(),
                    (GameEnums.TipoPose)System.Enum.Parse(typeof(GameEnums.TipoPose), decode_pose["tipo_pose"].ToString()));

                
            }
            else
            {
                botones_pose[i].SetValues(
                    decode_pose["sprite_pose"].ToString(),
                    decode_pose["label_pose"].ToString(),
                    (GameEnums.TipoPose)System.Enum.Parse(typeof(GameEnums.TipoPose), decode_pose["tipo_pose"].ToString()));

            }

            grid_poses.Reposition();
        }
    }

    public void OpenPanelConfig()
    {
        //Ocultamos cualquier panel que haya activo
        HideAllPanels();

        //Activamos el panel de configuracion
        panelConfig.SetActive(true);
    }

    private void OnEnable()
    {
        R_MessagesController<R_MessageCabezaButton>.AddObserver((int)GameEnums.MessagesTypes.PressHeadButton, HandleClickHeadButton);
        R_MessagesController<R_MessageFigurinButton>.AddObserver((int)GameEnums.MessagesTypes.PressFigurinButton, HandleClickFigurinButton);
        R_MessagesController<R_MessagePoseButton>.AddObserver((int)GameEnums.MessagesTypes.PressPoseButton, HandleClickPoseButton);
        R_MessagesController<R_MessageUI>.AddObserver((int)GameEnums.MessagesTypes.EndProcessLoading, HandleEndLoading);
    }

    private void OnDisable()
    {
        R_MessagesController<R_MessageCabezaButton>.RemoveObserver((int)GameEnums.MessagesTypes.PressHeadButton, HandleClickHeadButton);
        R_MessagesController<R_MessageFigurinButton>.RemoveObserver((int)GameEnums.MessagesTypes.PressFigurinButton, HandleClickFigurinButton);
        R_MessagesController<R_MessagePoseButton>.RemoveObserver((int)GameEnums.MessagesTypes.PressPoseButton, HandleClickPoseButton);
        R_MessagesController<R_MessageUI>.RemoveObserver((int)GameEnums.MessagesTypes.EndProcessLoading, HandleEndLoading);
    }

    private void HandleEndLoading(R_MessageUI obj)
    {
        if (obj.SenderId != (uint)GameEnums.Senders.AnimationLoading) return;

        introScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    private void HandleClickPoseButton(R_MessagePoseButton obj)
    {
        if (obj.SenderId != (uint)GameEnums.Senders.PoseButton) return;
        
        RestoreFigurinesList();

        if (obj.tipo_pose == GameEnums.TipoPose.TODAS) return;

        FilterFigurinByPose(obj.tipo_pose);
    }

    //Metodo para restaurar la lista de figurines
    private void RestoreFigurinesList()
    {
        FigurinButton[] figurines = containerFigurinButton.GetComponentsInChildren<FigurinButton>(true);

        if (hay_defecto)
        {
            for(int i = 0; i < index; i++)
            {
                figurines[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < figurines.Length; i++)
            {
                figurines[i].gameObject.SetActive(true);
            }
        }

        grid_figurines.Reposition();
    }

    //Metodo que filtra los figurines segun el tipo de pose
    private void FilterFigurinByPose(GameEnums.TipoPose pose)
    {
        //Recorremos la lista de figurines
        for(int i = 0; i < botones_figurin.Length; i++)
        {
            if (botones_figurin[i].GetTipoPose() != pose)
            {
                botones_figurin[i].gameObject.SetActive(false);
            }
        }

        grid_figurines.Reposition();

        //Reiniciamos el scroll view, por si algun vestido se queda colgado
        //despues de la filtracion
        scrollViewFigurines.ResetPosition();
    }

    private void HandleClickFigurinButton(R_MessageFigurinButton obj)
    {
        if (obj.SenderId != (uint)GameEnums.Senders.FigurinButton) return;

        cuerpoFigurin.spriteName = obj.name_sprite_figurin;
    }

    private void HandleClickHeadButton(R_MessageCabezaButton obj)
    {
        if (obj.SenderId != (uint)GameEnums.Senders.CabezaButton) return;

        //Necesito el nombre del sprite
        cabezaFigurin.spriteName = obj.name_sprite_head;
    }
}
