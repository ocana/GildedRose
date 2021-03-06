﻿using System.Collections.Generic;

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
        private const string CONJURED_NAME = "Conjured Mana Cake";

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

                ModifyQuality(current);

                DecreaseSellIn(current);
            }
        }

        private static void ModifyQuality(Item current)
        {
            if (IsAgedBrie(current))
            {
                ModifyQualityForAgedBrie(current);
            }

            if (IsBackstage(current))
            {
                ModifyQualityForBackstage(current);
            }

            if (IsConjured(current))
            {
                ModifyQualityForConjuredItem(current);
            }

            if (IsRegular(current))
            {
                ModifyQualityForRegularItem(current);
            }
        }

        private static bool IsRegular(Item current)
        {
            return !IsAgedBrie(current) && !IsBackstage(current) && !IsConjured(current);
        }

        private static void ModifyQualityForConjuredItem(Item current)
        {
            DecreaseQuality(current);
            DecreaseQuality(current);
        }

        private static bool IsConjured(Item current)
        {
            return CONJURED_NAME.Equals(current.Name);
        }

        private static void ModifyQualityForRegularItem(Item current)
        {
            DecreaseQuality(current);

            if (HasPassedSellDate(current))
            {
                DecreaseQuality(current);
            }
        }

        private static void ModifyQualityForBackstage(Item current)
        {
            if (HasPassedSellDate(current))
            {
                ResetQuality(current);
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
        }

        private static void ModifyQualityForAgedBrie(Item current)
        {
            IncreaseQuality(current);

            if (HasPassedSellDate(current))
            {
                IncreaseQuality(current);
            }
        }

        private static bool HasPassedSellDate(Item current)
        {
            return current.SellIn <= MINIMUM_SELL_IN;
        }

        private static bool IsSulfuras(Item current)
        {
            return SULFURAS_NAME.Equals(current.Name);
        }

        private static bool IsBackstage(Item current)
        {
            return BACKSTAGE_NAME.Equals(current.Name);
        }

        private static bool IsAgedBrie(Item current)
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

        private static bool HasNotQuality(Item current)
        {
            return current.Quality <= MINIMUM_QUALITY;
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
            if (IsBackstage(current) || HasNotQuality(current) || IsSulfuras(current)) return;
            current.Quality = current.Quality - QUALITY_GRANULARITY;
        }
    }
}
