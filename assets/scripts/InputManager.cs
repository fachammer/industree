using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public Player[] players;

    public delegate void SelectHandler(Player player, float selectDirection);
    public delegate void CastHandler(Player player, float castDirection);

    public event SelectHandler PlayerSelect;
    public event CastHandler PlayerCast;

    private float[] previousPlayerSelectAxes;
    private float[] previousPlayerCastAxes;

    void Start()
    {
        previousPlayerSelectAxes = new float[players.Length];
        previousPlayerCastAxes = new float[players.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            float playerSelectAxis;
            float playerCastAxis;

            float playerSelectDirection = Utilities.GetAxisRawDown(players[i].selectInputName, previousPlayerSelectAxes[i], out playerSelectAxis);
            float playerCastDirection = Utilities.GetAxisRawDown(players[i].castInputName, previousPlayerCastAxes[i], out playerCastAxis);

            if (playerSelectDirection != 0)
            {
                PlayerSelect(players[i], playerSelectDirection);
            }

            if (playerCastDirection != 0)
            {
                PlayerCast(players[i], playerCastDirection);
            }

            previousPlayerSelectAxes[i] = playerSelectAxis;
            previousPlayerCastAxes[i] = playerCastAxis;
        }
    }
}
