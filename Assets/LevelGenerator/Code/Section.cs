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

        protected Level levelContainer;
        protected int order;
        
        public void Initialize(Level level, int sourceOrder)
        {
            levelContainer = level;
            transform.SetParent(levelContainer.transform);
            levelContainer.RegisterNewSection(this);
            order = sourceOrder + 1;

            GenerateAnnexes();
        }

        protected void GenerateAnnexes()
        {
            if (CreatesTags.Any())
            {
                foreach (var e in Exits.ExitSpots)
                {
                    if (levelContainer.LevelSize > 0 && order < levelContainer.MaxAllowedOrder)
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
            var candidate = Instantiate(levelContainer.PickSectionWithTag(CreatesTags), exit).GetComponent<Section>();
            if (levelContainer.IsSectionValid(candidate.Bounds, Bounds))
            {
                candidate.Initialize(levelContainer, order);
            }
            else
            {
                Destroy(candidate.gameObject);
                PlaceDeadEnd(exit);
            }
        }

        protected void PlaceDeadEnd(Transform exit) => Instantiate(levelContainer.DeadEnds.PickOne(), exit).Initialize(levelContainer);
    }
}