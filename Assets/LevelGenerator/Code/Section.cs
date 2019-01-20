using System.Linq;
using LevelGenerator.Helpers;
using UnityEngine;

namespace LevelGenerator.Code
{
    public class Section : MonoBehaviour
    {
        /// <summary>
        /// Section tags
        /// </summary>
        public string[] Tags;

        /// <summary>
        /// Tags that this section can annex
        /// </summary>
        public string[] CreatesTags;

        /// <summary>
        /// Exits node in hierarchy
        /// </summary>
        public Exits Exits;

        /// <summary>
        /// Bounds node in hierarchy
        /// </summary>
        public Bounds Bounds;

        /// <summary>
        /// Chances of the section spawning a dead end
        /// </summary>
        public int DeadEndChance;

        protected Generator GeneratorContainer;
        protected int order;
        
        public void Initialize(Generator generator, int sourceOrder)
        {
            GeneratorContainer = generator;
            transform.SetParent(GeneratorContainer.transform);
            GeneratorContainer.RegisterNewSection(this);
            order = sourceOrder + 1;

            GenerateAnnexes();
        }

        protected void GenerateAnnexes()
        {
            if (CreatesTags.Any())
            {
                foreach (var e in Exits.ExitSpots)
                {
                    if (GeneratorContainer.LevelSize > 0 && order < GeneratorContainer.MaxAllowedOrder)
                        if (RandomService.RollD100(DeadEndChance))
                            PlaceDeadEnd(e);
                        else
                            GenerateSection(e);
                    else
                        PlaceDeadEnd(e);
                }
            }
        }

        protected void GenerateSection(Transform exit)
        {
            var candidate = IsAdvancedExit(exit)
                ? BuildSectionFromExit(exit.GetComponent<AdvancedExit>())
                : BuildSectionFromExit(exit);
                
            if (GeneratorContainer.IsSectionValid(candidate.Bounds, Bounds))
            {
                candidate.Initialize(GeneratorContainer, order);
            }
            else
            {
                Destroy(candidate.gameObject);
                PlaceDeadEnd(exit);
            }
        }

        protected void PlaceDeadEnd(Transform exit) => Instantiate(GeneratorContainer.DeadEnds.PickOne(), exit).Initialize(GeneratorContainer);

        protected bool IsAdvancedExit(Transform exit) => exit.GetComponent<AdvancedExit>() != null;

        protected Section BuildSectionFromExit(Transform exit) => Instantiate(GeneratorContainer.PickSectionWithTag(CreatesTags), exit).GetComponent<Section>();

        protected Section BuildSectionFromExit(AdvancedExit exit) => Instantiate(GeneratorContainer.PickSectionWithTag(exit.CreatesTags), exit.transform).GetComponent<Section>();
    }
}