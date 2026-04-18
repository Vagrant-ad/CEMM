using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEMM.Common
{
    public class StringSimilarityChecker
    {
        //方法一：使用Levenshtein距离（编辑距离）
        //最适合一般的字符串相似度比较

        /// <summary>
        /// 使用Levenshtein距离计算字符串相似度,最适合一般的字符串相似度比较
        /// </summary>
        public static bool AreStringsSimilar(string str1, string str2, double threshold = 0.9)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return str1 == str2;

            double similarity = CalculateLevenshteinSimilarity(str1, str2);
            return similarity >= threshold;
        }

        /// <summary>
        /// 计算Levenshtein相似度（0-1之间）
        /// </summary>
        private static double CalculateLevenshteinSimilarity(string str1, string str2)
        {
            int maxLength = Math.Max(str1.Length, str2.Length);
            if (maxLength == 0) return 1.0;

            int distance = ComputeLevenshteinDistance(str1, str2);
            return 1.0 - (double)distance / maxLength;
        }

        /// <summary>
        /// 计算Levenshtein编辑距离
        /// </summary>
        private static int ComputeLevenshteinDistance(string str1, string str2)
        {
            int[,] distance = new int[str1.Length + 1, str2.Length + 1];

            // 初始化边界条件
            for (int i = 0; i <= str1.Length; i++)
                distance[i, 0] = i;

            for (int j = 0; j <= str2.Length; j++)
                distance[0, j] = j;

            // 动态规划计算编辑距离
            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(
                            distance[i - 1, j] + 1,      // 删除
                            distance[i, j - 1] + 1),     // 插入
                        distance[i - 1, j - 1] + cost);  // 替换
                }
            }

            return distance[str1.Length, str2.Length];
        }

        //方法二：使用Jaro-Winkler距离（更适合短字符串和姓名匹配）
        //适合短字符串，特别是姓名、地址等
        /// <summary>
        /// 使用Jaro-Winkler距离计算字符串相似度,适合短字符串，特别是姓名、地址等
        /// </summary>
        public static bool AreStringsSimilarJaroWinkler(string str1, string str2, double threshold = 0.9)
        {
            double similarity = CalculateJaroWinklerSimilarity(str1, str2);
            return similarity >= threshold;
        }

        /// <summary>
        /// 计算Jaro-Winkler相似度
        /// </summary>
        private static double CalculateJaroWinklerSimilarity(string str1, string str2)
        {
            // 计算Jaro距离
            double jaroDistance = CalculateJaroDistance(str1, str2);

            // 计算前缀长度（最多4个字符）
            int prefixLength = 0;
            int maxPrefixLength = Math.Min(Math.Min(str1.Length, str2.Length), 4);

            for (int i = 0; i < maxPrefixLength; i++)
            {
                if (str1[i] == str2[i])
                    prefixLength++;
                else
                    break;
            }

            // Jaro-Winkler公式
            double winklerSimilarity = jaroDistance + (prefixLength * 0.1 * (1 - jaroDistance));
            return Math.Min(winklerSimilarity, 1.0);
        }

        /// <summary>
        /// 计算Jaro距离
        /// </summary>
        private static double CalculateJaroDistance(string str1, string str2)
        {
            if (str1 == str2) return 1.0;

            int len1 = str1.Length;
            int len2 = str2.Length;

            if (len1 == 0 || len2 == 0) return 0.0;

            // 匹配窗口大小
            int matchDistance = Math.Max(len1, len2) / 2 - 1;

            // 匹配的字符
            bool[] str1Matches = new bool[len1];
            bool[] str2Matches = new bool[len2];

            int matches = 0;
            int transpositions = 0;

            // 查找匹配的字符
            for (int i = 0; i < len1; i++)
            {
                int start = Math.Max(0, i - matchDistance);
                int end = Math.Min(i + matchDistance + 1, len2);

                for (int j = start; j < end; j++)
                {
                    if (!str2Matches[j] && str1[i] == str2[j])
                    {
                        str1Matches[i] = true;
                        str2Matches[j] = true;
                        matches++;
                        break;
                    }
                }
            }

            if (matches == 0) return 0.0;

            // 计算换位
            int k = 0;
            for (int i = 0; i < len1; i++)
            {
                if (str1Matches[i])
                {
                    while (!str2Matches[k]) k++;
                    if (str1[i] != str2[k]) transpositions++;
                    k++;
                }
            }

            double jaro = ((double)matches / len1 +
                          (double)matches / len2 +
                          (double)(matches - transpositions / 2.0) / matches) / 3.0;

            return jaro;
        }

        //方法三：使用余弦相似度（基于词频），适合长文本、文档比较

        /// <summary>
        /// 检查字符串是否只包含空白字符（兼容.NET 3.5）
        /// </summary>
        private static bool IsWhiteSpace(string value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                    return false;
            }

            return true;
        }
        /// <summary>
        /// 使用余弦相似度计算字符串相似度，适合长文本、文档比较
        /// </summary>
        public static bool AreStringsSimilarCosine(string str1, string str2, double threshold = 0.9)
        {
            //// 使用字符串自带的IsNullOrEmpty
            //if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            //    return str1 == str2;
            //// 检查是否为空白字符串
            //if (IsWhiteSpace(str1) || IsWhiteSpace(str2))
            //    return str1 == str2;

            //上面两个直接用下面这个
            if (string.IsNullOrWhiteSpace(str1) || string.IsNullOrWhiteSpace(str2))
                return str1 == str2;

            double similarity = CalculateCosineSimilarity(str1, str2);
            return similarity >= threshold;
        }

        /// <summary>
        /// 计算余弦相似度
        /// </summary>
        private static double CalculateCosineSimilarity(string str1, string str2)
        {
            // 分词
            var tokens1 = Tokenize(str1);
            var tokens2 = Tokenize(str2);

            // 获取所有唯一的词
            var allTokens = tokens1.Keys.Union(tokens2.Keys).Distinct().ToList();

            // 创建词频向量
            double[] vector1 = new double[allTokens.Count];
            double[] vector2 = new double[allTokens.Count];

            for (int i = 0; i < allTokens.Count; i++)
            {
                string token = allTokens[i];
                int count1;  // 先声明变量
                int count2;  // 先声明变量

                tokens1.TryGetValue(token, out count1);  // VS2012兼容语法
                tokens2.TryGetValue(token, out count2);  // VS2012兼容语法

                vector1[i] = count1;
                vector2[i] = count2;
            }

            // 计算余弦相似度
            return ComputeCosineSimilarity(vector1, vector2);
        }

        /// <summary>
        /// 分词并统计词频
        /// </summary>
        private static Dictionary<string, int> Tokenize(string text)
        {
            // 使用旧的Linq语法（VS2012兼容）
            var tokens = text.ToLower()
                            .Split(new char[] { ' ', ',', '.', ';', ':', '!', '?', '\t', '\n', '\r' },
                                   StringSplitOptions.RemoveEmptyEntries);

            // 手动统计词频（不使用ToDictionary的复杂重载）
            var dict = new Dictionary<string, int>();

            foreach (string token in tokens)
            {
                if (dict.ContainsKey(token))
                {
                    dict[token] = dict[token] + 1;
                }
                else
                {
                    dict[token] = 1;
                }
            }

            return dict;
        }

        /// <summary>
        /// 计算两个向量的余弦相似度
        /// </summary>
        private static double ComputeCosineSimilarity(double[] vector1, double[] vector2)
        {
            double dotProduct = 0.0;
            double magnitude1 = 0.0;
            double magnitude2 = 0.0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                magnitude1 += Math.Pow(vector1[i], 2);
                magnitude2 += Math.Pow(vector2[i], 2);
            }

            if (magnitude1 == 0 || magnitude2 == 0)
                return 0.0;

            return dotProduct / (Math.Sqrt(magnitude1) * Math.Sqrt(magnitude2));
        }

    }
}

//性能优化版本（针对长字符串）：当需要处理大量数据或长字符串时使用
public static class FastStringSimilarity
{
    /// <summary>
    /// 性能优化版本，当需要处理大量数据或长字符串时使用
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool IsSimilar(string a, string b, double threshold = 0.9)
    {
        // 快速检查：完全相等
        if (a == b) return true;

        // 快速检查：长度差异过大
        int lenDiff = Math.Abs(a.Length - b.Length);
        int maxLen = Math.Max(a.Length, b.Length);
        if ((double)lenDiff / maxLen > 0.1) return false;

        // 使用优化的编辑距离算法
        return OptimizedLevenshteinSimilarity(a, b) >= threshold;
    }

    private static double OptimizedLevenshteinSimilarity(string a, string b)
    {
        if (a.Length > b.Length)
        {
            //(a, b) = (b, a); // 确保a是较短的字符串
            string temp = a;
            a = b;
            b = temp;
        }
        
        int[] current = new int[a.Length + 1];
        int[] previous = new int[a.Length + 1];
        
        // 初始化第一行
        for (int i = 0; i <= a.Length; i++)
        {
            previous[i] = i;
        }
        
        for (int j = 1; j <= b.Length; j++)
        {
            current[0] = j;
            
            for (int i = 1; i <= a.Length; i++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                current[i] = Math.Min(
                    Math.Min(current[i - 1] + 1, previous[i] + 1),
                    previous[i - 1] + cost);
            }
            
            // 交换数组
            //(previous, current) = (current, previous);
            int[] tempArray = previous;
            previous = current;
            current = tempArray;
        }
        
        int distance = previous[a.Length];
        return 1.0 - (double)distance / Math.Max(a.Length, b.Length);
    }
}
