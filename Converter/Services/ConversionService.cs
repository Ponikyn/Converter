using System;
using System.Collections.Generic;
using Converter.Models;

namespace Converter.Services
{
    public static class ConversionService
    {
        public static double Convert(UnitItem from, UnitItem to, double value)
        {
            if (from == null || to == null)
                throw new ArgumentNullException("Units must be provided");

            // convert to base then to target
            double baseValue = value * from.ToBase;
            double result = baseValue / to.ToBase;
            return result;
        }

        public static List<UnitCategory> GetSampleCategories()
        {
            return new List<UnitCategory>
            {
                new UnitCategory
                {
                    Id = "length",
                    Name = "Длина",
                    Units = new List<UnitItem>
                    {
                        new UnitItem { Id = "m", Name = "Метры", ToBase = 1 },
                        new UnitItem { Id = "cm", Name = "Сантиметры", ToBase = 0.01 },
                        new UnitItem { Id = "km", Name = "Километры", ToBase = 1000 }
                    }
                },
                new UnitCategory
                {
                    Id = "weight",
                    Name = "Вес",
                    Units = new List<UnitItem>
                    {
                        new UnitItem { Id = "kg", Name = "Килограммы", ToBase = 1 },
                        new UnitItem { Id = "g", Name = "Граммы", ToBase = 0.001 },
                        new UnitItem { Id = "t", Name = "Тонны", ToBase = 1000 }
                    }
                },
                new UnitCategory
                {
                    Id = "volume",
                    Name = "Объем",
                    Units = new List<UnitItem>
                    {
                        new UnitItem { Id = "l", Name = "Литры", ToBase = 1 },
                        new UnitItem { Id = "ml", Name = "Миллилитры", ToBase = 0.001 },
                        new UnitItem { Id = "m3", Name = "Кубические метры", ToBase = 1000 }
                    }
                },
                new UnitCategory
                {
                    Id = "temperature",
                    Name = "Температура",
                    Units = new List<UnitItem>
                    {
                        // For temperature we will handle specially since linear multiplier doesn't suffice
                        new UnitItem { Id = "c", Name = "Цельсий", ToBase = 1 },
                        new UnitItem { Id = "f", Name = "Фаренгейт", ToBase = 1 },
                        new UnitItem { Id = "k", Name = "Кельвин", ToBase = 1 }
                    }
                },
                new UnitCategory
                {
                    Id = "speed",
                    Name = "Скорость",
                    Units = new List<UnitItem>
                    {
                        new UnitItem { Id = "mps", Name = "м/с", ToBase = 1 },
                        new UnitItem { Id = "kph", Name = "км/ч", ToBase = 1000.0/3600.0 },
                        new UnitItem { Id = "mph", Name = "миль/ч", ToBase = 1609.344/3600.0 }
                    }
                }
            };
        }

        public static bool TryConvertTemperature(string fromId, string toId, double value, out double result)
        {
            result = double.NaN;
            if (fromId == toId)
            {
                result = value;
                return true;
            }

            try
            {
                double celsius;
                switch (fromId)
                {
                    case "c": celsius = value; break;
                    case "f": celsius = (value - 32) * 5.0/9.0; break;
                    case "k": celsius = value - 273.15; break;
                    default: return false;
                }

                switch (toId)
                {
                    case "c": result = celsius; break;
                    case "f": result = celsius * 9.0/5.0 + 32; break;
                    case "k": result = celsius + 273.15; break;
                    default: return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}