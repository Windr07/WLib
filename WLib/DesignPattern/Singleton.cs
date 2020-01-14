/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.DesignPattern
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T">单例类</typeparam>
    public class Singleton<T> where T : class, new()
    {
        /// <summary>
        /// 获取类T的单例
        /// </summary>
        private static T _instance;
        /// <summary>
        /// 
        /// </summary>
        private static readonly object _sync = new object();


        /// <summary>
        /// 单例模式
        /// </summary>
        private Singleton() { }


        /// <summary>
        /// 获取类T的单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_sync)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 重置类
        /// </summary>
        public static void Reset()
        {
            _instance = null;
        }
    }
}
