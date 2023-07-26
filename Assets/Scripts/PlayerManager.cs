using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject Painter;
    public GameObject PainterModel;
    public GameObject Platformer;
    public GameObject PlatformerModel;

    int ActivePlayer = 0; //0 Painter, 1 Platformer

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Painter.SetActive(true);
            Platformer.SetActive(false);
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(ActivePlayer == 0) {
                ActivePlayer = 1;
                Painter.SetActive(false);
                PainterModel.SetActive(true);

                Platformer.SetActive(true);
                PlatformerModel.SetActive(false);
            }
            else {
                ActivePlayer = 0;
                Platformer.SetActive(false);
                PlatformerModel.SetActive(true);

                Painter.SetActive(true);
                PainterModel.SetActive(false);

            }
        }
    }

    public bool PainterActive() {
        return ActivePlayer == 0;
    }

    public bool PlatformerActive() {
        return ActivePlayer == 1;
    }
}
