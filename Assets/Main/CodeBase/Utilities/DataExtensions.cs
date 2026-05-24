using System;
using System.Collections.Generic;
using System.Linq;
using Main.CodeBase.Infrastructure.Services.LocalizationService;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Main.CodeBase.Utilities
{
    public static class DataExtensions
    {
        private static readonly string[] _numberNames = { "", "K", "M", "B", "T" };

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T FromJson<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static long ToUnixSeconds(this DateTime dateTime) =>
            (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;

        public static string ToTimeString(this int seconds, Localization localization)
        {
            if (seconds / 60 >= 60)
                return
                    $"{seconds / 60 / 60}{localization.GetHoursShortWord()} " +
                    $"{seconds / 60 % 60}{localization.GetMinutesShortWord()}";

            return $"{seconds / 60:00}:{seconds % 60:00}";
        }

        public static string ToFormatNumberString(this ulong number)
        {
            if (number < 1000)
                return number.ToString();

            int n = 0;
            decimal result = number;

            while (n + 1 < _numberNames.Length && result >= 1000m)
            {
                result /= 1000m;
                n++;
            }
            
            decimal rounded = Math.Round(result, 2);

            return rounded % 1 == 0 ? $"{(int)rounded}{_numberNames[n]}" : $"{rounded:0.00}.{_numberNames[n]}";
        }

        public static Color ToColor(this string targetColor)
        {
            var result = ColorUtility.TryParseHtmlString(targetColor, out Color color)
                ? color
                : Color.white;

            return result;
        }

        public static string ToHexString(this Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGB(color);
        }
        
        public static Vector3 CenterColliderPosition(this GameObject gameObject)
        {
            return new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + gameObject.GetComponent<CapsuleCollider2D>().size.y / 2 *
                                                gameObject.transform.localScale.y
                                                + gameObject.GetComponent<CapsuleCollider2D>().offset.y *
                                                gameObject.transform.localScale.y,
                gameObject.transform.position.z);
        }

        public static T[] Blend<T>(this T[] source)
        {
            List<T> targetCollection = source.ToList();
            List<T> result = new List<T>();
            int length = targetCollection.Count;

            for (int i = 0; i < length; i++)
            {
                int randomIndex = Random.Range(0, targetCollection.Count);
                result.Add(targetCollection[randomIndex]);
                targetCollection.RemoveAt(randomIndex);
            }

            return result.ToArray();
        }
    }
}