using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
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

    public Transform plainTerrain;
    public int numParticles; // Nombre de particules à générer

    public StagManager stagScript;
    [SerializeField]
    private InteractableAnimal interactableAnimal;
    public bool goingDialog;
    public List<string[]> currentDialogSentences;
    public DialogueManager dialogueManager;
    public bool canInteract;

    public int overlapSphereRadius;
    public LayerMask interactableLayer;
    public TextMeshProUGUI nearbyIAnimalText;
    public Transform stagTransform;

    public Animator MiniMapAnimator;

    public RawImage iaRawImage;

    [Header("Stored Mission : 1")]
    public GameObject MissionParent1;
    public RawImage Mission1RI;
    public TextMeshProUGUI MissionPNJName;
    public TextMeshProUGUI MissionPNJBiome;
    public TextMeshProUGUI MissionPNJDesc;

    [Header("Stored Mission : 2")]
    public GameObject MissionParent2;
    public RawImage Mission2RI;
    public TextMeshProUGUI Mission2PNJName;
    public TextMeshProUGUI Mission2PNJBiome;
    public TextMeshProUGUI Mission2PNJDesc;

    [Header("Stored Mission : 3")]
    public GameObject MissionParent3;
    public RawImage Mission3RI;
    public TextMeshProUGUI Mission3PNJName;
    public TextMeshProUGUI Mission3PNJBiome;
    public TextMeshProUGUI Mission3PNJDesc;

    [Header("Stored Mission : 4")]
    public GameObject MissionParent4;
    public RawImage Mission4RI;
    public TextMeshProUGUI Mission4PNJName;
    public TextMeshProUGUI Mission4PNJBiome;
    public TextMeshProUGUI Mission4PNJDesc;

    public List<UiMission> uiMissions;
    public int uiMissionIndex;


    // Start is called before the first frame update
    void Start()
    {
        goingDialog = false;
        canInteract = false;
        nearbyIAnimalText.text = "";

        uiMissionIndex = 0;
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
                // Générer une position aléatoire sur le plan 3D
                Vector3 randomPosition = new Vector3(
                    Random.Range(plainTerrain.position.x - plainTerrain.localScale.x / 2f, plainTerrain.position.x + plainTerrain.localScale.x / 2f),
                    Random.Range(plainTerrain.position.y - plainTerrain.localScale.y / 2f, plainTerrain.position.y + plainTerrain.localScale.y / 2f),
                    Random.Range(plainTerrain.position.z - plainTerrain.localScale.z / 2f, plainTerrain.position.z + plainTerrain.localScale.z / 2f)
                );

                // Instancier la préfabriquée de système de particules à la position générée
                GameObject particle = Instantiate(particlePrefab, randomPosition, Quaternion.identity);

                // Facultatif : Assigner le plan 3D comme parent des particules pour les maintenir organisées dans la hiérarchie
                particle.transform.SetParent(plainTerrain);

                // Attendre un court laps de temps avant de générer la particule suivante
                yield return new WaitForSeconds(0.1f);
            }
        }

    // Update is called once per frame
    void Update()
    {
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
                interactableAnimal = FindNearestIAnimal(nearInteractables);

            //Debug.Log(nearInteractables);
            //Debug.Log(interactableAnimal);
            //Debug.Log(interactableAnimal.animalName);
            //Debug.Log(nearbyIAnimalText.text);
            nearbyIAnimalText.text = interactableAnimal.animalName;
            iaRawImage.texture = interactableAnimal.textureRenderer;
        }
    }

    InteractableAnimal FindNearestIAnimal(Collider[] nearInteractables)
    {
        Collider nearest = null;
        float nearestDistance = 9999999f;

        foreach(Collider nearCollider in nearInteractables)
        {
            float colliderDistance = Vector3.Distance(stagTransform.position, nearCollider.transform.position);

            if (colliderDistance < nearestDistance){
                nearest = nearCollider;
                nearestDistance = colliderDistance;
            }
        }

        return nearest.gameObject.GetComponent<InteractableAnimal>();
    }


}

