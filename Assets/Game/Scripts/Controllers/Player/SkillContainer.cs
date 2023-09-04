using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNEagleEngine.Player;

namespace VNEagleEngine
{
    public class SkillContainer : MonoBehaviour
    {
        [SerializeField] List<SkillBase> _skillSlots;
        private PlayerController _playerController;

        public void Init(PlayerController player)
        {
            this._playerController = player;
        }

        public SkillBase GetSkillAtSlot(int slot)
        {
            if (slot >= _skillSlots.Count || slot < 0)
                return null;

            var skill = _skillSlots[slot];
            skill.Init(this._playerController);

            return skill;
        }

        public void SetSkillSlot(int slot, SkillBase skill)
        {
            if (slot >= _skillSlots.Count || slot < 0)
                return;
            
            _skillSlots[slot] = skill;
        }
    }
}
