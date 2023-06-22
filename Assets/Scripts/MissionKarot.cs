using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MissionKarot : IMission
{
    public string title { get; set; }
    public string description { get; set; }
    public string biome { get; set; }
    public InteractableAnimal interactableAnimal { get; set; }
    public DialogueManager dialogueManager { get; set; }
    public MissionState missionState { get; set; }
    public StagManager stagManager { get; set; } 
    public List<string[]> startSentences { get; set; }
    public List<string[]> failureSentences { get; set; }
    public List<string[]> successSentences { get; set; }
    public bool startedDialogue { get; set; }

    public MissionKarot(
        InteractableAnimal interactableAnimal, 
        DialogueManager dialogueManager, 
        StagManager stagManager
        )
    {
        this.title = "A la recherche d'une Karot";
        this.title = "Carottino a besoin d'une Karot, trouve la si tu ne veux pas avoir sa mort dans te conscience.";
        this.biome = "Prairie des Carottes";

        this.interactableAnimal = interactableAnimal;
        this.dialogueManager = dialogueManager;
        this.stagManager = stagManager;

        this.missionState = MissionState.Sleep;

        startSentences = new List<string[]>()
        {
            new string[] { stagManager.stagName, "Salut petit lapin !" },
            new string[] { interactableAnimal.animalName, "J'ai faim.." },
            new string[] { stagManager.stagName, "Ah zut ! Je peux t'aider peut-être? " },
            new string[] { interactableAnimal.animalName, "J'ai soif... " },
            new string[] { stagManager.stagName, "Euh..oui j'ai cru comprendre que t'étais dans le besoin oui... " },
            new string[] { interactableAnimal.animalName, "QU'ON ME DONNE UNE KAROT AAAAA " },
            new string[] { stagManager.stagName, "Bon, je vais voir ce que je peux faire.." },
            new string[] { interactableAnimal.animalName, "AAAAAAAAAAAAAA" }
        };

        failureSentences = new List<string[]>()
        {
            new string[] { interactableAnimal.animalName, "Oh ! Tu es revenu avec une Karot ??" },
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
    public List<string[]> HandleMission()
    {
        List<string[]> returnedSentences = new List<string[]>();

        switch (missionState)
        {
            case MissionState.Sleep:
                missionState = MissionState.Started;
                returnedSentences = startSentences;
                break;
            case MissionState.Started:
            case MissionState.Failed:

                //Default is set to failure sentences
                returnedSentences = failureSentences;

                if (MissionSolved())
                {
                    missionState = MissionState.Success;
                    returnedSentences = successSentences;
                    interactableAnimal.DequeueMission();
                }

                break;
        }
        return returnedSentences;
    }

    public bool MissionSolved()
    {
        return true;
    }
}
