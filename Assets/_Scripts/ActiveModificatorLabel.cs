using TMPro;
using UnityEngine;

public class ActiveModificatorLabel : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    void OnEnable() => resetPosition();

    void resetPosition()
    {
        Vector3 pos = copyVector(transform.localPosition);
        pos.y = 0;
        transform.localPosition = pos;
    }
    Vector3 copyVector(Vector3 oVect) => new Vector3(oVect.x, oVect.y, oVect.z);

    internal void flyWithText(string value)
    {
        resetPosition();
        text.text = value;
        Vector3 targetPos = copyVector(transform.localPosition);
        targetPos.y += 30;
        iTween.MoveTo(gameObject, iTween.Hash(
                     "position", targetPos,
                     "islocal", true,
                     "time", 1f,
                     "oncomplete", "hideMe"
                     ));

    }

    void hideMe() => gameObject.SetActive(false);
}
