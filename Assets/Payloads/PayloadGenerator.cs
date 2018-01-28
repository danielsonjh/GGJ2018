using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Payloads
{
    public class PayloadGenerator : MonoBehaviour
    {
        public static PayloadGenerator Instance { get; private set; }

        [SerializeField] private Sprite _scrollSprite;
        [SerializeField] private Sprite _foodSprite;
        [SerializeField] private Sprite _wineSprite;
        [SerializeField] private Sprite _moneyBagSprite;

        private List<IPayload> _payloads;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _payloads = new List<IPayload>
            {
                new ScrollPayload { Sprite = _scrollSprite },
                new FoodPayload  { Sprite = _foodSprite },
                new WinePayload  { Sprite = _wineSprite },
                new MoneyBagPayload  { Sprite = _moneyBagSprite }
            };
        }

        public IPayload Generate()
        {
            var r = Random.Range(1, _payloads.Select(i => i.Value).Sum());
            foreach (var payload in _payloads)
            {
                r -= payload.SpawnChanceWeight;
                if (r <= 0)
                {
                    return payload;
                }
            }

            return _payloads.First();
        }
    }
}
