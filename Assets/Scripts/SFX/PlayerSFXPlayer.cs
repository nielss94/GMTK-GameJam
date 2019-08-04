using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXPlayer : SFXPlayer
{
    bool timeForOne = true;
    public void PlayWalk()
    {

        Play(audioClips[timeForOne ? 0 : 1]);
        timeForOne = timeForOne ? false : true;
    }
}
