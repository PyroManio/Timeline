using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalReferences
{
    private static TimelineManagerNew timelineManager;
    private static UICaller uicaller;
    private static DialogueUI dialogueui;
    private static Fader fader;
    private static CameraShake cameraShake;
    private static DespairGameObject despair;
    private static AmyGameObject amy;
    private static PlayerMovement player;

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

    public static Fader Fader
    {
        get
        {
            if (fader)
                return fader;

            return null;
        }

        set
        {
            fader = value;
        }
    }

    public static CameraShake CameraShake
    {
        get
        {
            if (cameraShake)
                return cameraShake;

            return null;
        }

        set
        {
            cameraShake = value;
        }
    }

    public static DespairGameObject Despair
    {
        get
        {
            if (despair)
                return despair;

            return null;
        }

        set
        {
            despair = value;
        }
    }

    public static AmyGameObject Amy
    {
        get
        {
            if (amy)
                return amy;

            return null;
        }

        set
        {
            amy = value;
        }
    }

    public static PlayerMovement Player
    {
        get
        {
            if (player)
                return player;

            return null;
        }

        set
        {
            player = value;
        }
    }
}
