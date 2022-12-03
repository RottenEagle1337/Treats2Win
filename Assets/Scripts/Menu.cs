using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open;
    public bool cursorVisibility = true;

    public void Open()
    {
        gameObject.SetActive(true);
        open = true;
        Cursor.visible = cursorVisibility;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        open = false;
    }
}
