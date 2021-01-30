using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField]
    static GameObject[] rooms;

    public static int roomIndex = 0;

    [SerializeField]
    Image FadeOverlay;

    float fadeValue = 0;

    static bool fade = false;
    bool unFade = false;
    static int transferIndex = 0;
    static Transform newLocation;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    float timoTEE = 0;

    [SerializeField]
    float fadeSpeed = 1;

    static bool canTransfer = true;

    private void Update()
    {
        if(fade)
        {
            fadeValue = Mathf.Lerp(fadeValue, 255, timoTEE);
            timoTEE += Time.deltaTime*fadeSpeed;
            if(fadeValue>254)
            {
                Transition();
                fade = false;
                unFade = true;
                timoTEE = 0;
            }
        }
        if(unFade)
        {
            fadeValue = Mathf.Lerp(fadeValue, 0, timoTEE);
            timoTEE += Time.deltaTime * fadeSpeed;
            if (fadeValue < 1)
            {
                timoTEE = 0;
                unFade = false;
            }
        }
    }

    void Transition()
    {
        player.transform.position = newLocation.position;
        rooms[roomIndex].transform.Find("COVER_OBJECT").gameObject.SetActive(true);
        rooms[roomIndex].SetActive(false);
        rooms[transferIndex].transform.Find("COVER_OBJECT").gameObject.SetActive(false);
        rooms[transferIndex].SetActive(true);
        roomIndex = transferIndex;
    }

    public static void EnterRoom(int index, Transform location)
    {
        if(canTransfer)
        {
            fade = true;
            transferIndex = index;
            canTransfer = false;
        }
    }
}
