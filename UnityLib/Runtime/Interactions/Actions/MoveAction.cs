#region Usings

using System.Collections;
using UnityEngine;

#endregion

public enum MoveType
{
    Add,
    By,
    To,
    From,

}

public class MoveAction : Action
{

    public MoveType MoveType = MoveType.To;

    public Vector3 Amount = Vector3.zero;

    public iTween.EaseType EaseType = iTween.EaseType.easeInOutQuint;

    public iTween.LoopType LoopType = iTween.LoopType.none;

    public Space SpaceToMoveIn = Space.World;

    public float Time,
                 Delay;

    public bool OrientToPath,
                IgnoreTimeScale;

    public override void OnTriggerActivated(object sender, System.EventArgs e)
    {
        Hashtable args = iTween.Hash("amount",   Amount,        "orienttopath", OrientToPath,
                                     "space",    SpaceToMoveIn, "time",         Time, 
                                     "easetype", EaseType,      "looptype",     LoopType,
                                     "delay",    Delay);
        switch (MoveType)
        {
                case MoveType.Add:
                iTween.MoveAdd(gameObject, args);
                break;
                case MoveType.By:
                iTween.MoveBy(gameObject, args);
                break;
                case MoveType.To:
                iTween.MoveTo(gameObject, args);
                break;
                case MoveType.From:
                iTween.MoveFrom(gameObject, args);
                break;

        }
        iTween.MoveAdd(gameObject, args);

        base.OnTriggerActivated(sender, e);
    }

#if UNITY_EDITOR || RELEASE || DEBUG
    public override void OnEditorGUI()
    {
        GUILayout.Label("Action Type: " + GetType(), "IN TitleText");
        base.OnEditorGUI();
    }
#endif
}
