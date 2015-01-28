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

        private const string AGED_BRIE_NAME = "Aged Brie";
        private const string BACKSTAGE_NAME = "Backstage passes to a TAFKAL80ETC concert";
        private const string SULFURAS_NAME = "Sulfuras, Hand of Ragnaros";

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

                if (!EitherAgedBrieOrBackstage(current))
                {
                    if (HasSomeQuality(current))
                    {
                        if (!IsSulfuras(current))
                        {
                            DecreaseQuality(current);
                        }
                    }
                }
                else
                {
                    IncreaseQuality(current);

                    if (IsBackstage(current))
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

                if (HasPassedSellDate(current))
                {
                    if (!AgedBrie(current))
                    {
                        if (!IsBackstage(current))
                        {
                            if (HasSomeQuality(current))
                            {
                                if (!IsSulfuras(current))
                                {
                                    DecreaseQuality(current);
                                }
                            }
                        }
                    }
                    else
                    {
                        IncreaseQuality(current);
                    }

                    if (IsBackstage(current))
                    {
                        ResetQuality(current);
                    }
                }

                DecreaseSellIn(current);
            }
        }

        private static bool HasPassedSellDate(Item current)
        {
            return current.SellIn <= MINIMUM_SELL_IN;
        }

        private static bool EitherAgedBrieOrBackstage(Item current)
        {
            return AgedBrie(current) || IsBackstage(current);
        }

        private static bool IsSulfuras(Item current)
        {
            return SULFURAS_NAME.Equals(current.Name);
        }

        private static bool IsBackstage(Item current)
        {
            return BACKSTAGE_NAME.Equals(current.Name);
        }

        private static bool AgedBrie(Item current)
        {
            return AGED_BRIE_NAME.Equals(current.Name);
        }

        private static bool IsInTripleIncrement(Item current)
        {
            return current.SellIn <= SECOND_SELL_IN_THRESHOLD;
        }

        private static bool IsInDoubleIncrement(Item current)
        {
            return current.SellIn <= FIRST_SELL_IN_THRESHOLD;
        }

        private static bool MaximumQualityReached(Item current)
        {
           return current.Quality >= MAXIMUM_QUALITY;
        }

        private static bool HasSomeQuality(Item current)
        {
            return current.Quality > MINIMUM_QUALITY;
        }

        private static void DecreaseSellIn(Item current)
        {
            if (IsSulfuras(current)) return;
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
