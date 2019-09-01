using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorInput : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject[] batteries;

    [SerializeField] private Texture2D cursor;
    private Vector2 cursorHotspot;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
        cursorHotspot = new Vector2(cursor.width / 2f, cursor.height / 2f);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameController.missilesBattery1 > 0)
        {
            Instantiate(missile, batteries[0].transform.position, Quaternion.identity);
            Debug.Log("Launch Battery 1");
            gameController.missilesBattery1--;
        }
        if (Input.GetMouseButtonDown(2) && gameController.missilesBattery2 > 0)
        {
            Instantiate(missile, batteries[1].transform.position, Quaternion.identity);
            Debug.Log("Launch Battery 2");
            gameController.missilesBattery2--;
        }
        if (Input.GetMouseButtonDown(1) && gameController.missilesBattery3 > 0)
        {
            Instantiate(missile, batteries[2].transform.position, Quaternion.identity);
            Debug.Log("Launch Battery 3");
            gameController.missilesBattery3--;
        }

    }
}
