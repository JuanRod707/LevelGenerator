using System.Collections.Generic;
using System.Linq;
using JuanRod.Common;
using JuanRod.LevelGenerator.Code.Exceptions;
using JuanRod.LevelGenerator.Code.Structure;
using UnityEngine;

namespace JuanRod.LevelGenerator.Code
{
    public class Level : MonoBehaviour
    {
        public int MaxLevelSize;
        public Section[] Sections;
        public GameObject[] DeadEnds;
        public string[] InitialSectionTags;
        public int MaxAllowedOrder;

        public TagRule[] SpecialRules;

        List<Section> registeredSections = new List<Section>();
        
        public int LevelSize { get; private set; }

        IEnumerable<Collider> RegisteredColliders => registeredSections.SelectMany(s => s.Bounds.Colliders);
        private bool HalfLevelBuilt => registeredSections.Count > LevelSize;

        void Start()
        {
            CheckRuleIntegrity();
            LevelSize = MaxLevelSize;
            CreateInitialSection();
        }

        private void CheckRuleIntegrity()
        {
            foreach (var ruleTag in SpecialRules.Select(r => r.Tag))
            {
                if (SpecialRules.Count(r => r.Tag.Equals(ruleTag)) > 1)
                    throw new InvalidRuleDeclarationException();
            }
        }

        private void CreateInitialSection() => Instantiate(PickSectionWithTag(InitialSectionTags), transform).Initialize(this, 0);

        public bool IsSectionValid(Bounds newSection, Bounds sectionToIgnore) => 
            !RegisteredColliders.Except(sectionToIgnore.Colliders).Any(c => c.bounds.Intersects(newSection.Colliders.First().bounds));

        public void RegisterNewSection(Section newSection)
        {
            registeredSections.Add(newSection);

            if(SpecialRules.Any(r => newSection.Tags.Contains(r.Tag)))
                SpecialRules.First(r => newSection.Tags.Contains(r.Tag)).PlaceRuleSection();

            LevelSize--;
        }

        public Section PickSectionWithTag(string[] tags)
        {
            if (RulesContainTargetTags(tags) && HalfLevelBuilt)
            {
                foreach (var rule in SpecialRules.Where(r => !r.RuleSatisfied))
                {
                    if (tags.Contains(rule.Tag))
                    {
                        return Sections.Where(x => x.Tags.Contains(rule.Tag)).PickOne();
                    }
                }
            }

            var pickedTag = PickFromExcludedTags(tags);
            return Sections.Where(x => x.Tags.Contains(pickedTag)).PickOne();
        }

        private string PickFromExcludedTags(string[] tags)
        {
            var tagsToExclude = SpecialRules.Where(r => r.RuleSatisfied).Select(rs => rs.Tag);
            return tags.Except(tagsToExclude).PickOne();
        }

        private bool RulesContainTargetTags(string[] tags) => tags.Intersect(SpecialRules.Where(r => !r.RuleSatisfied).Select(r => r.Tag)).Any();

        public void AddSectionTemplate() => Instantiate(Resources.Load("SectionTemplate"), Vector3.zero, Quaternion.identity);
    }
}