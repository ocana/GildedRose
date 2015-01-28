using System.Collections.Generic;

namespace GildedRose.Console
{
    public class GildedRose
    {
        private const int MINIMUM_QUALITY = 0;
        private const int MAXIMUM_QUALITY = 50;
        private const int MINIMUM_SELL_IN = 0;
        private const int QUALITY_GRANULARITY = 1;
        private const int SELL_IN_GRANULARITY = 1;
        private const int FIRST_SELL_IN_THRESHOLD = 10;
        private const int SECOND_SELL_IN_THRESHOLD = 5;

        private static IList<Item> Items;

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            UpdateQuality(Items);

            System.Console.ReadKey();

        }

        public static void UpdateQuality(IList<Item> items)
        {
            for (var i = 0; i < items.Count; i++)
            {
                Item current = items[i];

                bool notAgedBrie = current.Name != "Aged Brie";
                bool isBackstage = current.Name == "Backstage passes to a TAFKAL80ETC concert";
                bool notBackstage = current.Name != "Backstage passes to a TAFKAL80ETC concert";
                bool notSulfuras = current.Name != "Sulfuras, Hand of Ragnaros";

                if (notAgedBrie && notBackstage)
                {
                    if (HasSomeQuality(current))
                    {
                        if (notSulfuras)
                        {
                            DecreaseQuality(current);
                        }
                    }
                }
                else
                {
                    IncreaseQuality(current);

                    if (isBackstage)
                    {
                        if (IsInDoubleIncrement(current))
                        {
                            IncreaseQuality(current);
                        }

                        if (IsInTripleIncrement(current))
                        {
                            IncreaseQuality(current);
                        }
                    }
                }

                if (notSulfuras)
                {
                    DecreaseSellIn(current);
                }

                bool sellDatePassed = current.SellIn < MINIMUM_SELL_IN;
                if (sellDatePassed)
                {
                    if (notAgedBrie)
                    {
                        if (notBackstage)
                        {
                            if (HasSomeQuality(current))
                            {
                                if (notSulfuras)
                                {
                                    DecreaseQuality(current);
                                }
                            }
                        }
                        else
                        {
                            ResetQuality(current);
                        }
                    }
                    else
                    {
                        IncreaseQuality(current);
                    }
                }
            }
        }

        private static bool IsInTripleIncrement(Item current)
        {
            bool isInTripleIncrement = current.SellIn <= SECOND_SELL_IN_THRESHOLD;
            return isInTripleIncrement;
        }

        private static bool IsInDoubleIncrement(Item current)
        {
            bool isInDoubleIncrement = current.SellIn <= FIRST_SELL_IN_THRESHOLD;
            return isInDoubleIncrement;
        }

        private static bool MaximumQualityReached(Item current)
        {
           return current.Quality >= MAXIMUM_QUALITY;
        }

        private static bool HasSomeQuality(Item current)
        {
            bool hasSomeQuality = current.Quality > MINIMUM_QUALITY;
            return hasSomeQuality;
        }

        private static void DecreaseSellIn(Item current)
        {
            current.SellIn = current.SellIn - SELL_IN_GRANULARITY;
        }

        private static void ResetQuality(Item current)
        {
            current.Quality = MINIMUM_QUALITY;
        }

        private static void IncreaseQuality(Item current)
        {
            if (MaximumQualityReached(current)) return;

            current.Quality = current.Quality + QUALITY_GRANULARITY;
        }

        private static void DecreaseQuality(Item current)
        {
            current.Quality = current.Quality - QUALITY_GRANULARITY;
        }
    }
}
