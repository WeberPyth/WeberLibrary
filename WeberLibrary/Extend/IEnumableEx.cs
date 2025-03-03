using System;
using System.Collections.Generic;

namespace WeberLibrary.Extend
{
    /// <summary>
    /// 枚举拓展
    /// </summary>
    public static class IEnumableEx
    {
        /// <summary>
        /// 按指定大小进行分块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">需要被切块的原对象</param>
        /// <param name="chunkSize">每块的大小</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (chunkSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(chunkSize));

            return source.ChunkBy(chunkSize, i => i);
        }
        /// <summary>
        /// 按指定大小进行分块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">需要被切块的原对象</param>
        /// <param name="chunkSize">每块的大小</param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        private static IEnumerable<IEnumerable<T>> ChunkBy<T, TKey>(this IEnumerable<T> source, int chunkSize, Func<T, TKey> keySelector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (chunkSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(chunkSize));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var chunks = new List<T>();
            foreach (var item in source)
            {
                if (chunks.Count == chunkSize)
                {
                    yield return chunks;
                    chunks = new List<T>();
                }

                chunks.Add(item);
            }

            if (chunks.Count > 0)
                yield return chunks;
        }
    }
}
