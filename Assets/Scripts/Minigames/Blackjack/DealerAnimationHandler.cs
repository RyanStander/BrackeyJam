using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

namespace Minigames.Blackjack
{
    public class DealerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private SkeletonGraphic _skeletonGraphic;
        
        [Header("Animation Names")]
        [SerializeField] private string _deal = "DemonDeal";
        [SerializeField] private string _idle = "DemonIdle";
        [SerializeField] private string _sad = "DemonSad";
        [SerializeField] private string _happy = "DemonHappy";

        //for when a card is placed on the table
        private EventData _cardTableEventData;
        private bool _addCardToTable;
        
        private EventData _slapTableEventData;
        private bool _slappedTable;
        
        private void OnValidate()
        {
            if(_skeletonGraphic == null)
                _skeletonGraphic = GetComponent<SkeletonGraphic>();
        }

        private void Start()
        {
            _cardTableEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.CardTable);
            _slapTableEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.Slap);
            _skeletonGraphic.AnimationState.Event += HandleAnimationStateEvent;
        }

        public void PlayIdle()
        {
            _skeletonGraphic.AnimationState.SetAnimation(0, _idle, true);
        }
        
        public void PlayDeal()
        {
            _skeletonGraphic.AnimationState.SetAnimation(0, _deal, false);
            _skeletonGraphic.AnimationState.AddAnimation(0, _idle, true, 0f);
        }
        
        public void PlaySad()
        {
            _skeletonGraphic.AnimationState.SetAnimation(0, _sad, false);
            _skeletonGraphic.AnimationState.AddAnimation(0, _idle, true, 0f);
        }
        
        public void PlayHappy()
        {
            _skeletonGraphic.AnimationState.SetAnimation(0, _happy, false);
            _skeletonGraphic.AnimationState.AddAnimation(0, _idle, true, 0f);
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
            if (_cardTableEventData == e.Data)
                _addCardToTable = true;
            
            if (_slapTableEventData == e.Data)
                _slappedTable = true;
        }
        
        public bool AddCardToTable() => _addCardToTable;
        public void ResetAddCardToTable() => _addCardToTable = false;
        
        public bool SlappedTable() => _slappedTable;
        public void ResetSlappedTable() => _slappedTable = false;
    }
}
