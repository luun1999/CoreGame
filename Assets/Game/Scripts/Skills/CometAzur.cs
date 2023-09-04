using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNEagleEngine
{
    public class CometAzur : SkillBase
    {
        [SerializeField] CometAzurActiveRange cometRange;

        public override void Cast()
        {
            if (!CanCastSkill)
                return;
            base.Cast();
            SetCoolDown();

            StartCoroutine(ShowRange());
        }

        public IEnumerator ShowRange()
        {
            var comet = Instantiate(cometRange, _player.SkillCastingContainer.transform);
            comet.transform.position = new Vector3(_player.transform.position.x + 5f, _player.transform.position.y, _player.transform.position.z);
            comet.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            comet.gameObject.SetActive(false);
            Destroy(comet.gameObject);

        }
    }
}
