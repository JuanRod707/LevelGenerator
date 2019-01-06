using System.Linq;
using JuanRod.Common;
using UnityEngine;

namespace JuanRod.LevelGenerator.Code
{
    public class Section : MonoBehaviour
    {
        public string[] Tags;
        public string[] CreatesTags;
        public Exits Exits;
        public Bounds Bounds;
        public int DeadEndChance;
        
        private Level levelContainer;
        private int order;
        
        public void Initialize(Level level, int sourceOrder)
        {
            levelContainer = level;
            transform.SetParent(levelContainer.transform);
            levelContainer.RegisterNewSection(this);
            order = sourceOrder + 1;

            GenerateAnnexes();
        }

        void GenerateAnnexes()
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
        
        void GenerateSection(Transform exit)
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

        void PlaceDeadEnd(Transform exit)
        {
            Instantiate(levelContainer.DeadEnds.PickOne(), exit);
        }
    }
}