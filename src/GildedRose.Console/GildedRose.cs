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

                bool hasSomeQuality = current.Quality > MINIMUM_QUALITY;
                bool notMaximumQualityReached = current.Quality < MAXIMUM_QUALITY;

                if (notAgedBrie && notBackstage)
                {
                    if (hasSomeQuality)
                    {
                        if (notSulfuras)
                        {
                            DecreaseQuality(current);
                        }
                    }
                }
                else
                {
                    if (notMaximumQualityReached)
                    {
                        IncreaseQuality(current);

                        if (isBackstage)
                        {
                            bool isInDoubleIncrement = current.SellIn <= FIRST_SELL_IN_THRESHOLD;
                            bool isInTripleIncrement = current.SellIn <= SECOND_SELL_IN_THRESHOLD;

                            if (isInDoubleIncrement)
                            {
                                IncreaseQuality(current);
                            }

                            if (isInTripleIncrement)
                            {
                                IncreaseQuality(current);
                            }
                        }
                    }
                }

                if (notSulfuras)
                {
                    current.SellIn = current.SellIn - SELL_IN_GRANULARITY;
                }

                bool sellDatePassed = current.SellIn < MINIMUM_SELL_IN;
                if (sellDatePassed)
                {
                    if (notAgedBrie)
                    {
                        if (notBackstage)
                        {
                            if (hasSomeQuality)
                            {
                                if (notSulfuras)
                                {
                                    DecreaseQuality(current);
                                }
                            }
                        }
                        else
                        {
                            current.Quality = current.Quality - current.Quality;
                        }
                    }
                    else
                    {
                        if (notMaximumQualityReached)
                        {
                            IncreaseQuality(current);
                        }
                    }
                }
            }
        }

        private static void IncreaseQuality(Item current)
        {
            current.Quality = current.Quality + QUALITY_GRANULARITY;
        }

        private static void DecreaseQuality(Item current)
        {
            current.Quality = current.Quality - QUALITY_GRANULARITY;
        }
    }
}
