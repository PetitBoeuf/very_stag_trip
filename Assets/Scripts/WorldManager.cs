using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class UiMission
{
    public GameObject MissionParent { get; set; }
    public RawImage MissionRI { get; set; }
    public TextMeshProUGUI MissionPNJName { get; set; }
    public TextMeshProUGUI MissionPNJBiome { get; set; }
    public TextMeshProUGUI MissionPNJDesc{ get; set; }
    public string MissionTitle { get; set; }

    public UiMission(GameObject missionParent, RawImage missionRI, TextMeshProUGUI missionPNJName, TextMeshProUGUI missionPNJBiome, TextMeshProUGUI missionPNJDesc)
    {
        MissionParent = missionParent;
        MissionRI = missionRI;
        MissionPNJName = missionPNJName;
        MissionPNJBiome = missionPNJBiome;
        MissionPNJDesc = missionPNJDesc;
    }
}


public class WorldManager : MonoBehaviour
{
    public GameObject particlePrefab;

    public Transform plainTerrainTransf;
    public Terrain plainTerrain;
    public Transform windParent;
    public int numParticles; // Nombre de particules à générer

    public StagManager stagScript;
    public Transform stagTransform;
    public Transform stagOrientation;
    public TextMeshPro bouffableText;
    [SerializeField]
    private Transform alimentBouffable;

    [SerializeField]
    private InteractableAnimal interactableAnimal;
    public bool goingDialog;
    public List<string[]> currentDialogSentences;
    public DialogueManager dialogueManager;
    public bool canInteract;

    public int overlapSphereRadius;
    public LayerMask interactableLayer;
    public LayerMask bouffableLayer;
    public TextMeshProUGUI nearbyIAnimalText;
    //public Camera minimapCam;

    public Animator MiniMapAnimator;

    public RawImage iaRawImage;

    #region missionUI
    [Header("Stored Mission : 1")]
    [HideInInspector]
    public GameObject MissionParent1;
    [HideInInspector]
    public RawImage Mission1RI;
    [HideInInspector]
    public TextMeshProUGUI MissionPNJName;
    [HideInInspector]
    public TextMeshProUGUI MissionPNJBiome;
    [HideInInspector]
    public TextMeshProUGUI MissionPNJDesc;


    [Header("Stored Mission : 2")]
    [HideInInspector]
    public GameObject MissionParent2;
    [HideInInspector]
    public RawImage Mission2RI;
    [HideInInspector]
    public TextMeshProUGUI Mission2PNJName;
    [HideInInspector]
    public TextMeshProUGUI Mission2PNJBiome;
    [HideInInspector]
    public TextMeshProUGUI Mission2PNJDesc;

    [Header("Stored Mission : 3")]
    [HideInInspector]
    public GameObject MissionParent3;
    [HideInInspector]
    public RawImage Mission3RI;
    [HideInInspector]
    public TextMeshProUGUI Mission3PNJName;
    [HideInInspector]
    public TextMeshProUGUI Mission3PNJBiome;
    [HideInInspector]
    public TextMeshProUGUI Mission3PNJDesc;

    [Header("Stored Mission : 4")]
    [HideInInspector]
    public GameObject MissionParent4;
    [HideInInspector]
    public RawImage Mission4RI;
    [HideInInspector]
    public TextMeshProUGUI Mission4PNJName;
    [HideInInspector]
    public TextMeshProUGUI Mission4PNJBiome;
    [HideInInspector]
    public TextMeshProUGUI Mission4PNJDesc;
    #endregion

    public List<UiMission> uiMissions;
    private bool canCollect;
    //public int uiMissionIndex;


    // Start is called before the first frame update
    void Start()
    {
        //RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;

        canCollect = false;
        goingDialog = false;
        canInteract = false;
        nearbyIAnimalText.text = "";
        bouffableText.text = "--";

        //uiMissionIndex = 0;
        uiMissions = new List<UiMission>()
        {
            new UiMission(
                MissionParent1, Mission1RI, MissionPNJName, MissionPNJBiome, MissionPNJDesc
                ),
            new UiMission(
                MissionParent2, Mission2RI, Mission2PNJName, Mission2PNJBiome, Mission2PNJDesc
                ),
            new UiMission(
                MissionParent3, Mission3RI, Mission3PNJName, Mission3PNJBiome, Mission3PNJDesc
                ),
            new UiMission(
                MissionParent4, Mission4RI, Mission4PNJName, Mission4PNJBiome, Mission4PNJDesc
                ),
        };
        StartCoroutine(SpawnParticles());
    }

    private IEnumerator SpawnParticles()
        {
            for (int i = 0; i < numParticles; i++)
            {
                //random pos

                float MinX = plainTerrainTransf.transform.position.x;
                float MaxX = plainTerrainTransf.transform.position.x + plainTerrain.terrainData.size.x;
                float MinZ = plainTerrainTransf.transform.position.z;
                float MaxZ = plainTerrainTransf.transform.position.z + plainTerrain.terrainData.size.z;

                float randX = Mathf.FloorToInt(Random.Range(MinX, MaxX));
                float randY = Mathf.Floor(Random.Range(5, 10));
                float randZ = Mathf.FloorToInt(Random.Range(MinZ, MaxZ));


                Vector3 randomPosition = new Vector3(
                    randX,
                    randY,
                    randZ
                );

                GameObject particle = Instantiate(particlePrefab, randomPosition, Quaternion.identity);
                particle.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

                particle.transform.SetParent(windParent);

            }
            yield return null;
        }
    //void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    //{
    //    Debug.Log(camera);
    //    if(camera == minimapCam)
    //    {
    //        Debug.Log("coucou minimap");
    //        RenderSettings.fog = false;
    //    }
    //}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canCollect)
        {
            stagScript.AddToInventory(alimentBouffable.name.Split(" ")[0]);
            bouffableText.text = " ";
            Destroy(alimentBouffable.gameObject);
            canCollect = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return) && canInteract) //&& there is a near ennemy
        {
            if (!goingDialog)
            {
                currentDialogSentences = interactableAnimal.currentMission.HandleMission();

                if(interactableAnimal.currentMission.missionState == MissionState.Started) {
                    //add mission to ui

                    int foundMissionIndex = uiMissions.FindIndex(uim => uim.MissionTitle == null);

                    uiMissions[foundMissionIndex].MissionParent.SetActive(true);
                    uiMissions[foundMissionIndex].MissionRI.texture = interactableAnimal.textureRenderer;
                    uiMissions[foundMissionIndex].MissionPNJName.text = interactableAnimal.animalName;
                    uiMissions[foundMissionIndex].MissionPNJBiome.text = interactableAnimal.currentMission.biome;
                    uiMissions[foundMissionIndex].MissionPNJDesc.text = interactableAnimal.currentMission.description;
                    uiMissions[foundMissionIndex].MissionTitle = interactableAnimal.currentMission.title;
                }

                if(interactableAnimal.currentMission.missionState == MissionState.Success)
                {

                    interactableAnimal.DequeueMission();

                    int foundMissionIndex = uiMissions.FindIndex(uim => uim.MissionTitle == interactableAnimal.succeededMissions.Last<SOMission>().title);

                    uiMissions[foundMissionIndex].MissionParent.SetActive(false);
                    uiMissions[foundMissionIndex].MissionRI.texture = null;
                    uiMissions[foundMissionIndex].MissionTitle = null;
                    uiMissions[foundMissionIndex].MissionPNJName.text = null;
                    uiMissions[foundMissionIndex].MissionPNJBiome.text = null;
                    uiMissions[foundMissionIndex].MissionPNJDesc.text = null;

                }

                dialogueManager.ConfigureDialogue(currentDialogSentences);
                goingDialog = dialogueManager.DisplayNextSentence();
                interactableAnimal.MoveToStagForward();
                interactableAnimal.AbleDisableMovement(goingDialog);
                return;
            }

            goingDialog = dialogueManager.DisplayNextSentence();
            interactableAnimal.AbleDisableMovement(goingDialog);
            return;
        }


        if(goingDialog) return;


        if (Input.GetKeyDown(KeyCode.M) && !stagScript.openedMinimap) {

            stagScript.openedMinimap = true;
            MiniMapAnimator.SetBool("Open", true);
            return;
        }
        if((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M) && stagScript.openedMinimap))
        {
            stagScript.openedMinimap = false;
            MiniMapAnimator.SetBool("Open", false);
            return;
        }
        //Fonctionnement 1
        //Fonctionnement entre stagManager et worldManager
        //R�f�rence dans le stagManager du worldmanager
        //Trigger d'un spherecollider dans le stag
        //Quand trigger prcq le cerf a collisionn� un truc => ajouter, via le worldmanager du stagmanager, le collider dans une liste de colliders de wolrdManager
        // Puis, ici, continuellement faire une v�rification du collider le plus proche et si il y en a aucun mettre le bool � false etc
        

        //F2
        //Overlap sphere et v�rification continue des colliders les plus proches avec le layer interactable
        Collider[] nearInteractables = Physics.OverlapSphere(stagTransform.position, overlapSphereRadius, interactableLayer);

        if (nearInteractables.Length == 0)
        {
            canInteract = false;
            nearbyIAnimalText.text = "";
            interactableAnimal = null;
        }
        else
        {
            //if(uiMissionIndex == 4)
            //{
            //    Debug.Log("Trop de missions");
            //    return;
            //}
            canInteract = true;

            if (nearInteractables.Length == 1)
                interactableAnimal = nearInteractables[0].gameObject.GetComponent<InteractableAnimal>();
            else
                interactableAnimal = FindNearestCol(nearInteractables).GetComponent<InteractableAnimal>();

            //Debug.Log(nearInteractables);
            //Debug.Log(interactableAnimal);
            //Debug.Log(interactableAnimal.animalName);
            //Debug.Log(nearbyIAnimalText.text);
            nearbyIAnimalText.text = interactableAnimal.animalName;
            iaRawImage.texture = interactableAnimal.textureRenderer;

            return;
        }


        Collider[] nearBouffables = Physics.OverlapSphere(stagTransform.position, overlapSphereRadius, bouffableLayer);
        //Debug.Log(nearBouffables.Length);

        if (nearBouffables.Length == 0)
            bouffableText.text = "";
        else
        {
            if (nearBouffables.Length == 1) alimentBouffable = nearBouffables[0].gameObject.GetComponent<Transform>();
            if(nearBouffables.Length > 1)
            {
                alimentBouffable = FindNearestCol(nearBouffables).GetComponent<Transform>();
            }
            Vector3 alimentPos = alimentBouffable.GetComponent<Transform>().position;

            alimentPos.y += 1.5f;
            canCollect = true;

            bouffableText.GetComponent<RectTransform>().position = alimentPos;
            bouffableText.text = "<size=10>(E)</size> Ramasser : " + alimentBouffable.name.Split(" ")[0];
            bouffableText.GetComponent<RectTransform>().forward = stagOrientation.forward;
            return;
        }

    }

    GameObject FindNearestCol(Collider[] nearColliders)
    {
        Collider nearest = null;
        float nearestDistance = 9999999f;

        foreach(Collider nearCollider in nearColliders)
        {
            float colliderDistance = Vector3.Distance(stagTransform.position, nearCollider.transform.position);

            if (colliderDistance < nearestDistance){
                nearest = nearCollider;
                nearestDistance = colliderDistance;
            }
        }

        return nearest.gameObject;
    }


}

