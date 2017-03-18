/// <summary>
	/// 缂撳瓨鐩稿叧鐨勬搷浣滅被
    /// Copyright (C) 鍔ㄨ蒋鍗撹秺
	/// </summary>
	public class DataCache
	{
		/// <summary>
		/// 鑾峰彇褰撳墠搴旂敤绋嬪簭鎸囧畾CacheKey鐨凜ache鍊?
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string CacheKey)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			return objCache[CacheKey];
		}

		/// <summary>
		/// 璁剧疆褰撳墠搴旂敤绋嬪簭鎸囧畾CacheKey鐨凜ache鍊?
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject);
		}

		/// <summary>
		/// 璁剧疆褰撳墠搴旂敤绋嬪簭鎸囧畾CacheKey鐨凜ache鍊?
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration,TimeSpan slidingExpiration )
		{
			System.Web.Caching.Cache objCache = HttpRuntime.Cache;
			objCache.Insert(CacheKey, objObject,null,absoluteExpiration,slidingExpiration);
		}

        /// <summary>
        /// 鍒犻櫎褰撳墠搴旂敤绋嬪簭鎸囧畾CacheKey鐨凜ache鍊?
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static void DeleteCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Remove(CacheKey);
        }
	}
