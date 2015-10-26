using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    internal sealed class DbMappingCollectionDefault : DbMappingCollection
    {
        private DbMappingCollectionDefault() { }

        private static object syncLock = new object();
        private static DbMappingCollectionDefault instance = null;
        public static DbMappingCollectionDefault Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbMappingCollectionDefault();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
