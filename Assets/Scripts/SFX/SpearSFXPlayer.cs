using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSFXPlayer : SFXPlayer
{
  public void PlayImpact()
  {
    Play(audioClips[0]);
  }
}
