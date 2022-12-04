namespace Burst_Balloons
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var nums = new int[] { 3, 1, 5, 8 };
      Solution s = new Solution();
      var answer = s.MaxCoins(nums);
      Console.WriteLine(answer);
    }
  }

  // Time O(N^3) -> O(N^2) is for 
  public class Solution
  {
    public int MaxCoins(int[] nums)
    {
      // dp formula
      // nums[L - 1] +  nums[i] + nums[R + 1] + dp[i + 1][R] + dp[L][i - 1]
      int length = nums.Length;
      var cache = new int[length + 1][];
      for (int i = 0; i < cache.Length; i++)
      {
        cache[i] = new int[length + 1];
        for (int j = 0; j < cache[i].Length; j++)
        {
          cache[i][j] = int.MinValue;
        }
      }
      var temp = new int[length + 2];
      // for out of boundary we can assume it has 1
      temp[0] = 1;
      temp[temp.Length - 1] = 1;
      for (int i = 1; i <= temp.Length - 2; i++)
        temp[i] = nums[i - 1];
      int Dfs(int l, int r)
      {
        // base case 
        if (l > r)
        {
          return 0;
        }
        // if found in cache return in O(1)
        if (cache[l][r] != int.MinValue) return cache[l][r];

        // if not found we have to do O(N) single pass for boundary l to r
        for (int i = l; i <= r; i++)
        {
          var coins = temp[l - 1] * temp[i] * temp[r + 1];
          coins += Dfs(l, i - 1) + Dfs(i + 1, r);
          cache[l][r] = Math.Max(cache[l][r], coins);
        }

        return cache[l][r];
      }

      return Dfs(1, temp.Length - 2);
    }
  }
}