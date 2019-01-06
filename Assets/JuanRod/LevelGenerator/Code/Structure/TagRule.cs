using System;

namespace JuanRod.LevelGenerator.Code.Structure
{
    [Serializable]
    public class TagRule
    {
        public string Tag;
        public int MinAmount;
        public int MaxAmount;

        public bool RuleSatisfied => sectionsPlaced >= MaxAmount;

        int sectionsPlaced;

        public void PlaceRuleSection() => sectionsPlaced++;
    }
}
