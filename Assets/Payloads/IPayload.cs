using UnityEngine;

namespace Payloads
{
    public interface IPayload
    {
        int Value { get; }
        int SpawnChanceWeight { get; }
        Sprite Sprite { get; }
    }

    public class ScrollPayload : IPayload
    {
        public int Value { get { return 5; } }
        public int SpawnChanceWeight { get { return 5; } }
        public Sprite Sprite { get; set; }
    }

    public class FoodPayload : IPayload
    {
        public int Value { get { return 10; } }
        public int SpawnChanceWeight { get { return 3; } }
        public Sprite Sprite { get; set; }
    }

    public class WinePayload : IPayload
    {
        public int Value { get { return 15; } }
        public int SpawnChanceWeight { get { return 2; } }
        public Sprite Sprite { get; set; }
    }

    public class MoneyBagPayload : IPayload
    {
        public int Value { get { return 20; } }
        public int SpawnChanceWeight { get { return 1; } }
        public Sprite Sprite { get; set; }
    }
}