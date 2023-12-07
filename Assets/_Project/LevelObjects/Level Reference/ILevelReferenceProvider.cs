using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtoGame.LevelObjects {

    public interface ILevelReferenceProvider {

        public bool TryGetTitleLevel(out TitleLevelReference levelReference);

        public bool TryGetStageLevel(out StageLevelReference levelReference);
    }

}