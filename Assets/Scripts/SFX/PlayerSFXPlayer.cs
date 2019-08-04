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

  public void PlayJump()
  {
    Play(audioClips[2]);
  }

  public void PlayDash()
  {
    Play(audioClips[3]);
  }

  public void PlayDeath()
  {
    Play(audioClips[4]);
  }
}
