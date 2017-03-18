/// <summary>
   /// Redis鎿嶄綔绫?
   /// </summary>
   public sealed class RedisHelper
   {
       public static PooledRedisClientManager poolUser;
       public static PooledRedisClientManager poolUser_Info;
       public static PooledRedisClientManager poolUser_Friend;
       public static PooledRedisClientManager poolUser_Point;
       public static PooledRedisClientManager poolShow;
       public static PooledRedisClientManager poolProduct_Discount;
       public static PooledRedisClientManager poolProduct_List;
       public static PooledRedisClientManager poolProduct_Detail;
       public static PooledRedisClientManager poolProduct_Show;

       static RedisHelper()
       {
           InitPools();
       }

       private static void InitPools()
       {
           //璇婚厤缃枃浠?
           XmlDocument doc = new XmlDocument();
           if (File.Exists(@"Data\Redis.config"))
               doc.Load(@"Data\Redis.config");
           else
               doc.Load(HttpContext.Current.Server.MapPath("/Redis.config"));

           //鏍硅妭鐐?
           XmlElement root = doc.DocumentElement;

           //閬嶅巻鑺傜偣
           XmlNodeList nodes = root.SelectNodes("redispool");
           if (nodes != null)
           {
               foreach (XmlElement node in nodes)
               {
                   string poolName = node.GetAttribute("id");
                   if (!String.IsNullOrWhiteSpace(poolName))
                   {
                       PooledRedisClientManager pool = null;
                       string equalPoolName = node.GetAttribute("equal");
                       if (!String.IsNullOrWhiteSpace(equalPoolName))
                       {
                           //閾炬帴宸叉湁pool
                           RedisPoolType poolType = (RedisPoolType)Enum.Parse(typeof(RedisPoolType), equalPoolName);
                           pool = GetRedisPool(poolType);
                       }
                       else
                       {
                           //鏂皃ool
                           String host = node.GetAttribute("host");
                           if (!String.IsNullOrWhiteSpace(host))
                           {
                               String port = node.GetAttribute("port");
                               String pw = node.GetAttribute("pw");
                               if (!String.IsNullOrWhiteSpace(port))
                                   host = host + ":" + port;
                               if (!String.IsNullOrWhiteSpace(pw))
                                   host = pw + "@" + host;
                               pool = new PooledRedisClientManager(host);

                           }
                       }
                       switch (poolName)
                       {
                           case "USER":
                               poolUser = pool;
                               break;
                           case "USER_INFO":
                               poolUser_Info = pool;
                               break;
                           case "USER_FIREND":
                               poolUser_Friend = pool;
                               break;
                           case "USER_POINT":
                               poolUser_Point = pool;
                               break;
                           case "SHOW":
                               poolShow = pool;
                               break;
                           case "PRODUCT_DISCOUNT":
                               poolProduct_Discount = pool;
                               break;
                           case "PRODUCT_LIST":
                               poolProduct_List = pool;
                               break;
                           case "PRODUCT_DETAIL":
                               poolProduct_Detail = pool;
                               break;
                           case "PRODUCT_SHOW":
                               poolProduct_Show = pool;
                               break;
                           default:
                               break;
                       }

                   }
               }
           }
       }

       private static PooledRedisClientManager GetRedisPool(RedisPoolType poolType)
       {
           PooledRedisClientManager pool = null;
           switch (poolType)
           {
               case RedisPoolType.USER:
                   pool = poolUser;
                   break;
               case RedisPoolType.USER_INFO:
                   pool = poolUser_Info;
                   break;
               case RedisPoolType.USER_FRIEND:
                   pool = poolUser_Friend;
                   break;
               case RedisPoolType.USER_POINT:
                   pool = poolUser_Point;
                   break;
               case RedisPoolType.SHOW:
                   pool = poolShow;
                   break;
               case RedisPoolType.PRODUCT_DISCOUNT:
                   pool = poolProduct_Discount;
                   break;
               case RedisPoolType.PRODUCT_LIST:
                   pool = poolProduct_List;
                   break;
               case RedisPoolType.PRODUCT_DETAIL:
                   pool = poolProduct_Detail;
                   break;
               case RedisPoolType.PRODUCT_SHOW:
                   pool = poolProduct_Show;
                   break;
               default:
                   pool = poolUser;
                   break;
           }
           return pool;
       }

       #region pipe 绠￠亾

       /// <summary>
       /// 閫氳繃PipeLine鎵归噺鑾峰彇鏁版嵁,Hash,Set,ZSet鏆備笉鍙敤锛屽緟瑙ｅ喅
       /// </summary>
       /// <param name="keys"></param>
       /// <param name="dataType"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static Dictionary<string, object> GetWithPipeLine(List<string> keys, RedisDataType dataType, RedisPoolType poolType)
       {
           Dictionary<string, object> result = new Dictionary<string, object>();
           Dictionary<string, Dictionary<string, string>> r1 = new Dictionary<string, Dictionary<string, string>>();

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           var pipe = redis.CreatePipeline();
           try
           {
               for (int i = 0; i < keys.Count; i++)
               {
                   string key = keys[i];
                   switch (dataType)
                   {
                       case RedisDataType.Simple:
                           pipe.QueueCommand(r => r.GetValue(key), b => result.Add(key, b));
                           break;
                       case RedisDataType.List:
                           pipe.QueueCommand(r => r.GetAllItemsFromList(key), b => result.Add(key, b));
                           break;
                       case RedisDataType.Hash:
                           pipe.QueueCommand(r => { r.GetAllEntriesFromHash(key); return new byte[] { }; }, b => result.Add(key, (Object)b));
                           break;
                       case RedisDataType.Set:
                           //pipe.QueueCommand(r => r.GetAllItemsFromSet(key), b => result.Add(key, b));
                           break;
                       case RedisDataType.ZSet:
                           //pipe.QueueCommand(r => r.GetAllWithScoresFromSortedSet(key), b => result.Add(key, b));
                           break;
                       case RedisDataType.Exists:
                           pipe.QueueCommand(r => r.ContainsKey(key), b => result.Add(key, b));
                           break;
                       default:
                           break;
                   }
               }
               pipe.Flush();

           }
           catch { throw; }
           finally
           {
               if (pipe != null)
                   pipe.Dispose();
               if (redis != null)
                   redis.Dispose();
           }

           return result;
       }

       #endregion

       #region 绠€鍗曢敭鍊煎

       /// <summary>
       /// 鏌ヨ涓€涓敭鏄惁瀛樺湪
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool Exists(string key, RedisPoolType poolType)
       {
           bool exists = false;

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               exists = redis.ContainsKey(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return exists;
       }

       /// <summary>
       /// 鍒犻櫎涓€涓敭
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool Delete(string key, RedisPoolType poolType)
       {
           bool success = false;

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               success = redis.Remove(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return success;
       }

       /// <summary>
       /// 璁剧疆杩囨湡鏃堕棿
       /// </summary>
       /// <param name="key"></param>
       /// <param name="seconds"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool Expire(string key, int seconds, RedisPoolType poolType)
       {
           bool success = false;

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               success = redis.ExpireEntryAt(key, DateTime.Now.AddSeconds(seconds));
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return success;
       }

       /// <summary>
       /// 鑾峰彇涓€涓敭鐨勫墿浣欒繃鏈熸椂闂?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static TimeSpan Ttl(string key, RedisPoolType poolType)
       {
           TimeSpan ts = new TimeSpan();

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               ts = redis.GetTimeToLive(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return ts;
       }


       /// <summary>
       /// 鍙栦竴涓畝鍗曞€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static string Get(string key, RedisPoolType poolType)
       {
           String value = null;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               value = redis.GetValue(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return value;
       }

       /// <summary>
       /// 璁句竴涓畝鍗曞€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool Set<T>(string key, T value, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               success = redis.Set(key, value);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return success;
       }

       #endregion

       #region hash 鍝堝笇

       /// <summary>
       /// 鍙栦竴涓狧ash鐨勬墍鏈夊€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static Dictionary<string, string> HGetAll(string key, RedisPoolType poolType)
       {
           Dictionary<string, string> values = new Dictionary<string, string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               values = redis.GetAllEntriesFromHash(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓狧ash鐨勯儴鍒嗗€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="subKeys"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> HGetBatch(string key, string[] subKeys, RedisPoolType poolType)
       {
           List<string> values = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               values = redis.GetValuesFromHash(key, subKeys);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 璁剧疆涓€涓狧ash鍊?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="subKey"></param>
       /// <param name="value"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool HSet(string key, string subKey, string value, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               redis.SetEntryInHash(key, subKey, value);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return true;
       }

       /// <summary>
       /// 鎵归噺璁剧疆/澧炲姞涓€涓狧ash鐨勯敭鍊煎
       /// </summary>
       /// <param name="key"></param>
       /// <param name="values"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool HSetAll(string key, Dictionary<string, string> values, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               redis.SetRangeInHash(key, values);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return true;
       }

       /// <summary>
       /// 澧為噺涓€涓狧ash鐨勫€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="subKey"></param>
       /// <param name="incrementBy"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool HIncrease(string key, string subKey, int incrementBy, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               redis.IncrementValueInHash(key, subKey, incrementBy);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return true;
       }

       #endregion

       #region zset 鏈夊簭闆嗗悎

       /// <summary>
       /// 鑾峰彇鏈夊簭闆嗗悎涓竴涓垚鍛樼殑鍒嗗€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="member"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static double? ZGet(string key, string member, RedisPoolType poolType)
       {
           double? value = null;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               double valueTemp = redis.GetItemScoreInSortedSet(key, member);
               if (!double.IsNaN(valueTemp))
                   value = valueTemp;
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return value;
       }

       /// <summary>
       /// 鍦ㄦ湁搴忛泦鍚堜腑璁剧疆/鍔犲叆涓€涓垚鍛?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="member"></param>
       /// <param name="score"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool ZSet(string key, string member, double score, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               success = redis.AddItemToSortedSet(key, member, score);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return success;
       }

       /// <summary>
       /// 瀵规湁搴忛泦鍚堢殑涓€涓垚鍛樼殑鍊艰繘琛屽閲?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="member"></param>
       /// <param name="incrementBy"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool ZIncrece(string key, string member, double incrementBy, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               redis.IncrementItemInSortedSet(key, member, incrementBy);
               success = true;
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return success;
       }

       /// <summary>
       /// 鏈夊簭闆嗗悎涓竴涓垚鍛?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="member"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool ZRem(string key, string member, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               success = redis.RemoveItemFromSortedSet(key, member);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return success;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堟墍鏈夋垚鍛?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> ZGetAll(string key, RedisPoolType poolType)
       {
           List<string> values = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               values = redis.GetAllItemsFromSortedSet(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堟墍鏈夋垚鍛樺強鍒嗘暟
       /// </summary>
       /// <param name="key"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static IDictionary<string, double> ZGetAllWithScores(string key, RedisPoolType poolType)
       {
           IDictionary<string, double> values = new Dictionary<string, double>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               values = redis.GetAllWithScoresFromSortedSet(key);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堥儴鍒嗘垚鍛?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="fromScore"></param>
       /// <param name="toScore"></param>
       /// <param name="skip"></param>
       /// <param name="take"></param>
       /// <param name="isAsc">鏄惁鍗囧簭</param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> ZGetRangeByScore(string key, long fromScore, long toScore, int? skip, int? take, bool isAsc, RedisPoolType poolType)
       {
           List<string> values = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               if (isAsc)
                   values = redis.GetRangeFromSortedSetByLowestScore(key, fromScore, toScore, skip, take);
               else
                   values = redis.GetRangeFromSortedSetByHighestScore(key, fromScore, toScore, skip, take);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堥儴鍒嗘垚鍛樺強鍒嗘暟
       /// </summary>
       /// <param name="key"></param>
       /// <param name="fromScore"></param>
       /// <param name="toScore"></param>
       /// <param name="skip"></param>
       /// <param name="take"></param>
       /// <param name="isAsc">鏄惁鍗囧簭</param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static IDictionary<string, double> ZGetRangeByScoreWithScores(string key, long fromScore, long toScore, int? skip, int? take, bool isAsc, RedisPoolType poolType)
       {
           IDictionary<string, double> values = new Dictionary<string, double>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               if (isAsc)
                   values = redis.GetRangeWithScoresFromSortedSetByLowestScore(key, fromScore, toScore, skip, take);
               else
                   values = redis.GetRangeWithScoresFromSortedSetByHighestScore(key, fromScore, toScore, skip, take);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堥儴鍒嗘垚鍛?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="fromRank"></param>
       /// <param name="toRank"></param>
       /// <param name="isAsc"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> ZGetRange(string key, int fromRank, int toRank, bool isAsc, RedisPoolType poolType)
       {
           List<string> values = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               if (isAsc)
                   values = redis.GetRangeFromSortedSet(key, fromRank, toRank);
               else
                   values = redis.GetRangeFromSortedSetDesc(key, fromRank, toRank);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鍙栦竴涓湁搴忛泦鍚堥儴鍒嗘垚鍛樺強鍒嗘暟
       /// </summary>
       /// <param name="key"></param>
       /// <param name="fromRank"></param>
       /// <param name="toRank"></param>
       /// <param name="isAsc"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static IDictionary<string, double> ZGetRangeWithScores(string key, int fromRank, int toRank, bool isAsc, RedisPoolType poolType)
       {
           IDictionary<string, double> values = new Dictionary<string, double>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               if (isAsc)
                   values = redis.GetRangeWithScoresFromSortedSet(key, fromRank, toRank);
               else
                   values = redis.GetRangeWithScoresFromSortedSetDesc(key, fromRank, toRank);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return values;
       }

       /// <summary>
       /// 鎵归噺璁剧疆/澧炲姞涓€涓湁搴忛泦鍚堢殑鎴愬憳鍙婂垎鏁?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="members"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool ZSetAll(string key, Dictionary<string, double> members, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           var tran = redis.CreateTransaction();

           try
           {
               foreach (string memberkey in members.Keys)
               {
                   tran.QueueCommand(r => r.AddItemToSortedSet(key, memberkey, members[memberkey]));
               }
               tran.Commit();
               success = true;
           }
           catch
           {
               if (tran != null)
               {
                   try
                   {
                       success = false;
                       tran.Rollback();
                   }
                   catch { }
               }
               throw;
           }
           finally
           {
               if (tran != null)
                   tran.Dispose();
               if (redis != null)
                   redis.Dispose();
           }

           return success;
       }

       /// <summary>
       /// 浠庝竴绯诲垪鏈夊簭闆嗗悎涓紝鍙栦竴涓垚鍛樺悇鑷殑鍒嗗€?
       /// </summary>
       /// <param name="zSetKeys"></param>
       /// <param name="member"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static Dictionary<string, double> ZGetMemberSocresFromZSets(List<string> zSetKeys, String member, RedisPoolType poolType)
       {
           Dictionary<string, double> scores = new Dictionary<string, double>();

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           var pipe = redis.CreatePipeline();

           try
           {
               for (int i = 0; i < zSetKeys.Count; i++)
               {
                   string key = zSetKeys[i];
                   pipe.QueueCommand(r => r.GetItemScoreInSortedSet(key, member)
                                          , b =>
                                          {
                                              if (!double.IsNaN(b))
                                                  scores.Add(key, b);
                                          });
               }
               pipe.Flush();

           }
           catch { throw; }
           finally
           {
               if (pipe != null)
                   pipe.Dispose();
               if (redis != null)
                   redis.Dispose();
           }

           return scores;
       }

       /// <summary>
       /// 鏌ヨ涓€绯诲垪鐨勬湁搴忛泦鍚堟槸鍚﹀瓨鍦ㄤ竴瀹氳寖鍥寸殑鍊?
       /// </summary>
       /// <param name="zSetKeys"></param>
       /// <param name="keyPre"></param>
       /// <param name="keyExt"></param>
       /// <param name="fromScore"></param>
       /// <param name="toScore"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> ZExists(List<string> zSetKeys, string keyPre, string keyExt, long fromScore, long toScore, RedisPoolType poolType)
       {
           List<string> existsKeys = new List<string>();
           if (keyPre == null)
               keyPre = "";
           if (keyExt == null)
               keyExt = "";

           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           var pipe = redis.CreatePipeline();

           try
           {
               for (int i = 0; i < zSetKeys.Count; i++)
               {
                   string key = zSetKeys[i];
                   pipe.QueueCommand(r => r.GetSortedSetCount(keyPre + key + keyExt, fromScore, toScore)
                                   , b =>
                                   {
                                       if (b > 0)
                                           existsKeys.Add(key);
                                   });
               }
               pipe.Flush();

           }
           catch { throw; }
           finally
           {
               if (pipe != null)
                   pipe.Dispose();
               if (redis != null)
                   redis.Dispose();
           }

           return existsKeys;
       }

       /// <summary>
       /// 鍙栨湁搴忛泦鍚堢殑骞堕泦
       /// </summary>
       /// <param name="desKey"></param>
       /// <param name="zSetKeys"></param>
       /// <param name="aggregate"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static long ZUnionStore(string desKey, string[] zSetKeys, RedisAggregate aggregate, RedisPoolType poolType)
       {
           long rtn = 0;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               if (zSetKeys == null || zSetKeys.Length == 0)
               {
                   redis.Remove(desKey);
                   redis.AddItemToSortedSet(desKey, "-1", -1);
                   rtn = 0;
               }
               else
               {
                   string[] args = new string[] { "AGGREGATE", aggregate.ToString() };
                   rtn = redis.StoreUnionFromSortedSets(desKey, zSetKeys, args);
               }
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return rtn;
       }

       #endregion

       #region List 鍒楄〃

       /// <summary>
       /// 璁剧疆闃熷垪List鐨勫€?
       /// </summary>
       /// <param name="key"></param>
       /// <param name="valuelist"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool LSetAll(string listid, List<string> valuelist, RedisPoolType poolType)
       {
           bool success = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           var tran = redis.CreateTransaction();
           try
           {
               foreach (string value in valuelist)
               {
                   tran.QueueCommand(r => r.AddItemToList(listid, value));
               }
               tran.Commit();
               success = true;
           }
           catch
           {
               if (tran != null)
               {
                   try
                   {
                       success = false;
                       tran.Rollback();
                   }
                   catch { }
               }
               throw;
           }
           finally
           {
               if (tran != null)
                   tran.Dispose();
               if (redis != null)
                   redis.Dispose();
           }

           return success;
       }

       /// <summary>
       /// 鍙朙ist鐨勬墍鏈夊€?
       /// </summary>
       /// <param name="listid"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> LGetAll(string listid, RedisPoolType poolType)
       {
           List<string> list = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               list = redis.GetAllItemsFromList(listid);
               if (redis != null)
                   redis.Dispose();
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return list;
       }

       /// <summary>
       /// 浠巐ist鍙栭儴鍒嗗€?
       /// </summary>
       /// <param name="listid"></param>
       /// <param name="startIndex"></param>
       /// <param name="endIndex"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static List<string> LRange(string listid, int startIndex, int endIndex, RedisPoolType poolType)
       {
           List<string> list = new List<string>();
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetReadOnlyClient();
           try
           {
               list = redis.GetRangeFromList(listid, startIndex, endIndex);
           }
           catch { throw; }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }

           return list;
       }

       /// <summary>
       /// 闃熷垪涓彇鍑哄€?
       /// </summary>
       /// <param name="listid"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static string LPop(string listid, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           string rs = null;
           try
           {
               rs = redis.PopItemFromList(listid);
           }
           catch { }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return rs;
       }

       /// <summary>
       /// 闃熷垪涓姞鍏ュ€?
       /// </summary>
       /// <param name="listid"></param>
       /// <param name="value"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool LPush(string listid, string value, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           bool issuccess = false;
           try
           {

               redis.PushItemToList(listid, value);
               issuccess = true;
           }
           catch (Exception ex)
           {
               issuccess = false;
           }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return issuccess;
       }

       /// <summary>
       /// 浠庨槦鍒椾腑绉婚櫎鍖归厤椤?
       /// </summary>
       /// <param name="listId"></param>
       /// <param name="value"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static long RemoveItemFromList(string listId, string value, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           long rs = 0;
           try
           {

               rs = redis.RemoveItemFromList(listId, value);
           }
           catch (Exception ex)
           {
               rs = 0;
           }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return rs;
       }

       /// <summary>
       /// 绉婚櫎list鍐呴」鐩?
       /// </summary>
       /// <param name="listid"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool RemoveList(string listid, RedisPoolType poolType)
       {
           bool issuccess = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           try
           {

               redis.RemoveAllFromList(listid);
               issuccess = true;
           }
           catch (Exception ex)
           {
               issuccess = false;
           }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return issuccess;
       }
       #endregion

       #region Set 闆嗗悎

       /// <summary>
       /// set 鍐呮坊鍔犳垚鍛?
       /// </summary>
       /// <param name="setId"></param>
       /// <param name="item"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool AddSetItem(string setId, string item, RedisPoolType poolType)
       {
           bool issuccess = false;
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           try
           {

               redis.AddItemToSet(setId, item);
               issuccess = true;
           }
           catch (Exception ex)
           {
               issuccess = false;
           }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return issuccess;
       }
       /// <summary>
       /// 鍒ゆ柇set 鏄惁瀛樺湪鏌愭垚鍛?
       /// </summary>
       /// <param name="setId"></param>
       /// <param name="item"></param>
       /// <param name="poolType"></param>
       /// <returns></returns>
       public static bool SetContainsItem(string setId, string item, RedisPoolType poolType)
       {
           PooledRedisClientManager pool = GetRedisPool(poolType);
           IRedisClient redis = pool.GetClient();
           bool rs = false;
           try
           {
               rs = redis.SetContainsItem(setId, item);
           }
           catch
           {

           }
           finally
           {
               if (redis != null)
                   redis.Dispose();
           }
           return rs;
       }

       #endregion

   }

   /// <summary>
   /// Redis杩炴帴绫诲瀷
   /// </summary>
   public enum RedisPoolType
   {
       /// <summary>
       /// 鐢ㄦ埛鐨則oken銆佹敞鍐屻€侀摼鎺ヤ俊鎭?
       /// </summary>
       USER,

       /// <summary>
       /// 鐢ㄦ埛鐨勬樀绉般€佹墜鏈恒€佸簵閾轰俊鎭?
       /// </summary>
       USER_INFO,

       /// <summary>
       /// 鐢ㄦ埛鐨勫叧娉ㄣ€佺矇涓濅俊鎭?
       /// </summary>
       USER_FRIEND,

       /// <summary>
       /// 鐢ㄦ埛鐨勭Н鍒嗕俊鎭?
       /// </summary>
       USER_POINT,

       /// <summary>
       /// 绉€璇︽儏
       /// </summary>
       SHOW,

       /// <summary>
       /// 鍟嗗搧鎶樻墸淇℃伅
       /// </summary>
       PRODUCT_DISCOUNT,

       /// <summary>
       /// 鍟嗗搧鍒楄〃
       /// </summary>
       PRODUCT_LIST,

       /// <summary>
       /// 鍟嗗搧璇︽儏
       /// </summary>
       PRODUCT_DETAIL,

       /// <summary>
       /// 鍟嗗搧鏅掑崟鐩稿叧淇℃伅
       /// </summary>
       PRODUCT_SHOW
   }

   /// <summary>
   /// Redis鏁版嵁绫诲瀷
   /// </summary>
   public enum RedisDataType
   {
       Simple,
       List,
       Hash,
       Set,
       ZSet,
       /// <summary>
       /// 鐗规畩绫诲瀷锛屽彇涓€涓敭瀛樺湪涓庡惁
       /// </summary>
       Exists
   }

   public enum RedisAggregate
   {
       MIN,
       MAX,
       SUM
   }
