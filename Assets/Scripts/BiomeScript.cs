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

    // Start is called before the first frame update
    void Start()
    {
        biomeText.text = "";
        biomeLabel.text = "";
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
    }
    private void OnTriggerExit(Collider other)
    {
        biomeText.text = "";
        biomeLabel.text = "";
    }
}
