using System.Collections.Generic;

namespace GildedRose.Console
{
    public class GildedRose
    {
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

                bool hasSomeQuality = items[i].Quality > 0;
                bool notMaximumQualityReached = items[i].Quality < 50;

                if (notAgedBrie && notBackstage)
                {
                    if (hasSomeQuality)
                    {
                        if (notSulfuras)
                        {
                            items[i].Quality = items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (notMaximumQualityReached)
                    {
                        items[i].Quality = items[i].Quality + 1;

                        if (isBackstage)
                        {
                            if (items[i].SellIn < 11)
                            {
                                if (notMaximumQualityReached)
                                {
                                    items[i].Quality = items[i].Quality + 1;
                                }
                            }

                            if (items[i].SellIn < 6)
                            {
                                if (notMaximumQualityReached)
                                {
                                    items[i].Quality = items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (notSulfuras)
                {
                    items[i].SellIn = items[i].SellIn - 1;
                }

                bool sellDatePassed = items[i].SellIn < 0;
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
                                    items[i].Quality = items[i].Quality - 1;
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
                            items[i].Quality = items[i].Quality + 1;
                        }
                    }
                }
            }
        }

    }
}
