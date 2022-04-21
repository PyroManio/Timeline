using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalReferences
{
    private static TimelineManagerNew timelineManager;
    private static UICaller uicaller;
    private static DialogueUI dialogueui;

    public static TimelineManagerNew TimelineManager
    {
        get
        {
            if (timelineManager)
                return timelineManager;

            return null;
        }

        set
        {
            timelineManager = value;
        }
    }

    public static UICaller UIcaller
    {
        get
        {
            if (uicaller)
                return uicaller;

            return null;
        }

        set
        {
            uicaller = value;
        }
    }

    public static DialogueUI Dialogueui
    {
        get
        {
            if (dialogueui)
                return dialogueui;

            return null;
        }

        set
        {
            dialogueui = value;
        }
    }
}
