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

        [Header("Animation Names")] [SerializeField]
        private string _deal = "DemonDeal";

        [SerializeField] private string _idle = "DemonIdle";
        [SerializeField] private string _sad = "DemonSad";
        [SerializeField] private string _happy = "DemonHappy";
        [SerializeField] private string _tableFlip = "DemonTableflip";

        //for when a card is placed on the table
        private EventData _cardTableEventData;
        private bool _addCardToTable;

        private EventData _slapTableEventData;
        private bool _slappedTable;

        private EventData _changeFaceEventData;
        private bool _changeFace;

        private EventData _happyEventData;
        private bool _happyFace;

        private EventData _drawEventData;
        private bool _drawCard;
        
        private EventData _flipTableEventData;
        private bool _flipTable;

        private void OnValidate()
        {
            if (_skeletonGraphic == null)
                _skeletonGraphic = GetComponent<SkeletonGraphic>();
        }

        private void Start()
        {
            _cardTableEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.CardTable);
            _slapTableEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.Slap);
            _happyEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.Happy);
            _drawEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.Draw);
            _changeFaceEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.ChangeFace);
            _flipTableEventData = _skeletonGraphic.Skeleton.Data.FindEvent(DealerEventNames.Flip);
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
        
        public void PlayTableFlip()
        {
            _skeletonGraphic.AnimationState.SetAnimation(0, _tableFlip, false);
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Event e)
        {
            if (_cardTableEventData == e.Data)
                _addCardToTable = true;
            else if (_slapTableEventData == e.Data)
                _slappedTable = true;
            else if (_changeFaceEventData == e.Data)
                _changeFace = true;
            else if (_happyEventData == e.Data)
                _happyFace = true;
            else if (_drawEventData == e.Data)
                _drawCard = true;
        }

        public bool AddCardToTable() => _addCardToTable;
        public void ResetAddCardToTable() => _addCardToTable = false;

        public bool SlappedTable() => _slappedTable;
        public void ResetSlappedTable() => _slappedTable = false;
        
        public bool ChangeFace() => _changeFace;
        public void ResetChangeFace() => _changeFace = false;
        
        public bool HappyFace() => _happyFace;
        public void ResetHappyFace() => _happyFace = false;
        
        public bool DrawCard() => _drawCard;
        public void ResetDrawCard() => _drawCard = false;
        
        public bool FlipTable() => _flipTable;
        public void ResetFlipTable() => _flipTable = false;
    }
}
