using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class MissionSoif : IMission
{
    public InteractableAnimal interactableAnimal { get; set; }
    public DialogueManager dialogueManager { get; set; }
    public MissionState missionState { get; set; }
    public StagManager stagManager { get; set; }
    public List<string[]> startSentences { get; set; }
    public List<string[]> failureSentences { get; set; }
    public List<string[]> successSentences { get; set; }
    public bool startedDialogue { get; set; }

    //Problème : comment accéder d'ici au dialoguemanager

    public MissionSoif(
        InteractableAnimal interactableAnimal,
        DialogueManager dialogueManager,
        StagManager stagManager
        )
    {
        this.interactableAnimal = interactableAnimal;
        this.dialogueManager = dialogueManager;
        this.stagManager = stagManager;

        this.missionState = MissionState.Sleep;

        startSentences = new List<string[]>()
        {
            new string[] { interactableAnimal.animalName, "J'ai soif... " },
            new string[] { stagManager.stagName, "Décidément.. " },
            new string[] { interactableAnimal.animalName, "Mais tu ne peux pas comprendre ! J'ai 67 ans moi mon petit cerf, j'ai passé la belle époque !" },
            new string[] { stagManager.stagName, "Oui oui, bon, qu'est-ce que je peux faire pour toi? " },
            new string[] { interactableAnimal.animalName, "DE L'EAUUUUUUUUU" }
        };

        failureSentences = new List<string[]>()
        {
            new string[] { interactableAnimal.animalName, "Alors ? Tu as trouvé mon petit plan d'eau ??" },
            new string[] { interactableAnimal.animalName, "..." },
            new string[] { interactableAnimal.animalName, "Comment ça non??" },
            new string[] { interactableAnimal.animalName, "Et bien écoute pas grave t'auras la mort du grand Carottino dans la conscience.." },
            new string[] { stagManager.stagName, "Mais t'as fini avec ça oui !Roh... " },
            new string[] { interactableAnimal.animalName, "DE L'EAUUUUUUUUUUU" }
        };

        successSentences = new List<string[]>()
        {
            new string[] { stagManager.stagName, "Bon... je pense avoir trouvé !" },
            new string[] { interactableAnimal.animalName, "Ah ? C'est vrai ??? C'est où c'est où ?? " },
            new string[] { stagManager.stagName, "En fait je rigole, je n'ai pas trouvé.." },
            new string[] { interactableAnimal.animalName, "C'est une blague ???" },
            new string[] { stagManager.stagName, "Oui." },
            new string[] { interactableAnimal.animalName, "Hein ? " },
            new string[] { stagManager.stagName, "C'était une blague. Le plan d'eau que tu cherches est situé en plein milieu de la prairie des carottes." },
            new string[] { interactableAnimal.animalName, "Mais ? Ce n'est pas drôle du tout comme blague ! J'ai encore cru que j'allais y passer moi !" },
            new string[] { interactableAnimal.animalName, "Bon..malgré tout, je te remercie grandement ! Je t'en dois une." },
            new string[] { stagManager.stagName, "Youhou..quel honneur... je passerai te faire un coucou d'ici peu voir si tu as (encore) besoin d'aide." },
            new string[] { interactableAnimal.animalName, "Avec plaisir ! " },
        };

    }

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
