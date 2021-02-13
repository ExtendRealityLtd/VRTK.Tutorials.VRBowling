using UnityEngine;
using Zinnia.Extension;

public class Pin : MonoBehaviour
{
    public float pinFallThreshold = 0.02f;
    public float toppleLife = 3f;
    public int tries = 10;

    private Quaternion defaultRotation;
    private int currentTries;

    protected void Awake()
    {
        defaultRotation = transform.localRotation;
    }

    public void CheckTopple()
    {
        CancelToppleCheck();
        CheckRotation();
        InvokeRepeating("CheckRotation", 0f, 1f);
    }

    public void CancelToppleCheck()
    {
        currentTries = 0;
        CancelInvoke("CheckRotation");
        CancelInvoke("HidePin");
    }

    protected void CheckRotation()
    {
        currentTries++;
        if (!Mathf.Abs(Quaternion.Dot(defaultRotation, transform.localRotation)).ApproxEquals(1f, pinFallThreshold))
        {
            Invoke("HidePin", toppleLife);
        }
        else
        {
            if (currentTries > tries)
            {
                CancelToppleCheck();
            }
        }
    }

    protected void HidePin()
    {
        gameObject.SetActive(false);
    }
}