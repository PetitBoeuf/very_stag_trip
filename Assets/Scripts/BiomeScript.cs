using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BiomeScript : MonoBehaviour
{

    //public WorldManager worldManager;
    public TextMeshProUGUI biomeText;
    public TextMeshProUGUI biomeLabel;
    public string biomeName;
    public TextMeshProUGUI biomeMinimapName;
    public Animator biomeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        biomeText.text = "";
        biomeLabel.text = "";
        //biomeAnimator = GetComponent<Animator>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter(Collider other)
    {
        biomeLabel.text = "Biome";
        biomeText.text = biomeName;
        biomeMinimapName.text = biomeName;
        biomeAnimator.SetBool("InBiome", true);
    }
    private void OnTriggerExit(Collider other)
    {
        biomeAnimator.SetBool("InBiome", false);
        //biomeText.text = "";
        //biomeLabel.text = "";
    }
}
