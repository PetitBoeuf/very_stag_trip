using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

//[CreateAssetMenu(fileName = "__", menuName = "Missions/MissionAvocat")]
public class MissionAvocat : SOMission
{
    //public string title { get; set; }
    //public string description { get; set; }
    //public string biome { get; set; }
    //public InteractableAnimal interactableAnimal { get; set; }
    //public DialogueManager dialogueManager { get; set; }
    //public MissionState missionState { get; set; }
    //public StagManager stagManager { get; set; }
    //public List<string[]> startSentences { get; set; }
    //public List<string[]> failureSentences { get; set; }
    //public List<string[]> successSentences { get; set; }
    //public bool startedDialogue { get; set; }

    public MissionAvocat(
        InteractableAnimal interactableAnimal,
        DialogueManager dialogueManager,
        StagManager stagManager
        )
    {
        this.title = "Robb-it le voleur";
        this.description = "Robb-it a volé des carottes, il a besoin d'aide avant son procès.";
        this.biome = "Pôle de la Justice";

        this.interactableAnimal = interactableAnimal;
        this.dialogueManager = dialogueManager;
        this.stagManager = stagManager;

        this.missionState = MissionState.Sleep;

        startSentences = new List<string[]>()
        {
            new string[] { stagManager.stagName, "Salut toi !" },
            new string[] { interactableAnimal.animalName, "A L'AAAAAAAAIDE" },
            new string[] { stagManager.stagName, "Oula oula qu'est-ce qu'il t'arrive ?" },
            new string[] { interactableAnimal.animalName, "Il reste plus que deux jours pour mon procès je sais pas quoi faaaaaaaaaaire" },
            new string[] { stagManager.stagName, "Pour ton procès ? Comment ça ? Je peux t'aider peut-être ?" },
            new string[] { interactableAnimal.animalName, "C'est-à-dire que j'ai volé quelques pommes chez BigPom et... elle a porté plainte." },
            new string[] { interactableAnimal.animalName, "Du coup..ben... il me faudrait le plus gros avocat de ce biome pour pouvoir me défendre correctement mais je n'ai rien trouvé de satisfaisant.." },
            new string[] { stagManager.stagName, "D'accord d'accord ? Je vais faire un tour voir si je trouve quelque chose et je reviens te voir." },
            new string[] { interactableAnimal.animalName, "Merciiiiii" }
        };

        failureSentences = new List<string[]>()
        {
            new string[] { interactableAnimal.animalName, "Oh coucou toi!" },
            new string[] { stagManager.stagName, "Salut" },
            new string[] { interactableAnimal.animalName, "..." },
            new string[] { interactableAnimal.animalName, "Comment ça non??" },
            new string[] { interactableAnimal.animalName, "Et bien écoute pas grave t'auras la mort du grand Carottino dans la conscience.." },
            new string[] { stagManager.stagName, "Super..merci la culpabilité.. " },
            new string[] { interactableAnimal.animalName, "AAAAAAAAAAAAAAAAAAAAAA" }
        };

        successSentences = new List<string[]>()
        {
            new string[] { stagManager.stagName, "Tiens ! Je t'ai trouvé une Karot !" },
            new string[] { interactableAnimal.animalName, "Oh mon dieu c'est pas vrai.. mais où l'as tu trouvée ? J'en ai cherché partout !" },
            new string[] { stagManager.stagName, "Bah écoute, j'en ai trouvé quelques unes à quelques pattes d'ici, mais c'était pas si simple ! C'était en fait bien enfoui sous terre." },
            new string[] { interactableAnimal.animalName, "Bah oui ! Les Karot ne poussent qu'à des endroits bien précis ! Mais visiblement.. l'âge me ratrappe.. je ne me souviens jamais exactement de l'endroit... en tout cas, je te remercie beaucoup !" },
            new string[] { stagManager.stagName, "Mais je t'en prie ! Je suis content d'avoir pu t'aider !" },
            new string[] { interactableAnimal.animalName, "AAAAAAAAAAAAAAAA" },
            new string[] { stagManager.stagName, "Et c'est reparti...." },
        };

    }
    //public List<string[]> GetSentences()
    //{
    //    switch (missionState)
    //    {
    //        case MissionState.Sleep:
    //            return startSentences;
    //        case MissionState.Success:
    //            return successSentences;
    //    }
    //}
    //public override List<string[]> HandleMission()
    //{
    //    List<string[]> returnedSentences = new List<string[]>();

    //    switch (missionState)
    //    {
    //        case MissionState.Sleep:
    //            missionState = MissionState.Started;
    //            returnedSentences = startSentences;
    //            break;
    //        case MissionState.Started:
    //        case MissionState.Failed:

    //            //Default is set to failure sentences
    //            returnedSentences = failureSentences;

    //            if (MissionSolved())
    //            {
    //                missionState = MissionState.Success;
    //                returnedSentences = successSentences;
    //                interactableAnimal.DequeueMission();
    //            }

    //            break;
    //    }
    //    return returnedSentences;
    //}

    //public override bool MissionSolved()
    //{
    //    return true;
    //}
}
