using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    internal sealed class DbEntityFactoryDefault : DbContextFactory
    {
        private DbEntityFactoryDefault() { }

        private static object syncLock = new object();
        private static DbEntityFactoryDefault instance = null;
        public static DbEntityFactoryDefault Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbEntityFactoryDefault();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
