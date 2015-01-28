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
        private const int FIRST_SELL_IN_THRESHOLD = 11;
        private const int SECOND_SELL_IN_THRESHOLD = 6;

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
                bool notAgedBrie = items[i].Name != "Aged Brie";
                bool isBackstage = items[i].Name == "Backstage passes to a TAFKAL80ETC concert";
                bool notBackstage = items[i].Name != "Backstage passes to a TAFKAL80ETC concert";
                bool notSulfuras = items[i].Name != "Sulfuras, Hand of Ragnaros";

                bool hasSomeQuality = items[i].Quality > MINIMUM_QUALITY;
                bool notMaximumQualityReached = items[i].Quality < MAXIMUM_QUALITY;

                if (notAgedBrie && notBackstage)
                {
                    if (hasSomeQuality)
                    {
                        if (notSulfuras)
                        {
                            items[i].Quality = items[i].Quality - QUALITY_GRANULARITY;
                        }
                    }
                }
                else
                {
                    if (notMaximumQualityReached)
                    {
                        items[i].Quality = items[i].Quality + QUALITY_GRANULARITY;

                        if (isBackstage)
                        {
                            if (items[i].SellIn < FIRST_SELL_IN_THRESHOLD)
                            {
                                if (notMaximumQualityReached)
                                {
                                    items[i].Quality = items[i].Quality + QUALITY_GRANULARITY;
                                }
                            }

                            if (items[i].SellIn < SECOND_SELL_IN_THRESHOLD)
                            {
                                if (notMaximumQualityReached)
                                {
                                    items[i].Quality = items[i].Quality + QUALITY_GRANULARITY;
                                }
                            }
                        }
                    }
                }

                if (notSulfuras)
                {
                    items[i].SellIn = items[i].SellIn - SELL_IN_GRANULARITY;
                }

                bool sellDatePassed = items[i].SellIn < MINIMUM_SELL_IN;
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
                                    items[i].Quality = items[i].Quality - QUALITY_GRANULARITY;
                                }
                            }
                        }
                        else
                        {
                            items[i].Quality = items[i].Quality - items[i].Quality;
                        }
                    }
                    else
                    {
                        if (notMaximumQualityReached)
                        {
                            items[i].Quality = items[i].Quality + QUALITY_GRANULARITY;
                        }
                    }
                }
            }
        }

    }
}
