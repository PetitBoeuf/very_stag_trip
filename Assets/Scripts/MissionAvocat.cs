using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionAvocat", menuName = "Missions/MissionAvocat")]
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

    public override void InitializeMission(
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
            new string[] { interactableAnimal.animalName, "Oh tu es reviendu !" },
            new string[] { stagManager.stagName, "Ouais..mais j'ai pas trouvé grand chose." },
            new string[] { interactableAnimal.animalName, "Ah bon ??? Oh..bah c'est gentil d'avoir essayé en tout cas. Je ne peux m'en vouloir qu'à moi-même de toutes manières.." },
            new string[] { stagManager.stagName, "Ne dis pas ça ! Allez, je vais continuer de chercher, on va le trouver cet avocat ne t'en fais pas !" },
            new string[] { interactableAnimal.animalName, "Wow.. Tu es d'une bonté.." },
            new string[] { stagManager.stagName, "Mais non ! Toi aussi tu aurais fait ça pour moi, non ? " },
            new string[] { interactableAnimal.animalName, "Ben non.." },
            new string[] { stagManager.stagName, "Ah." },
        };

        successSentences = new List<string[]>()
        {
            new string[] { stagManager.stagName, "Devine qui a trouvé le plus gros Avocat du Pôle de la Justice ? " },
            new string[] { interactableAnimal.animalName, "Ben je sais pas du tout moi.." },
            new string[] { stagManager.stagName, "..." },
            new string[] { stagManager.stagName, "C'est moi !" },
            new string[] { interactableAnimal.animalName, "Ah bon ?? Je suis trop conteeeeeeeeeeeeeeeent !" },
            new string[] { stagManager.stagName, "Ah c'est vrai ? " },
            new string[] { interactableAnimal.animalName, "Bien évidemment j'ai au moins deux fois plus de chances d'éviter de devenir le prochain repas de Big Pom c'est juste merveilleux !" },
            new string[] { interactableAnimal.animalName, "Merci merci merci merci de m'avoir aidé ! Je te revaudrai ça !" },
            new string[] { stagManager.stagName, "Ah, euh, ben écoute, je suis ravi d'avoir pu t'aider. A bientôt ! " },
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
