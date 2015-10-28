using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    internal sealed class DbAssistorFactoryDefault : DbAssistorFactory
    {
        private DbAssistorFactoryDefault() { }

        private static object syncLock = new object();
        private static DbAssistorFactoryDefault instance = null;
        public static DbAssistorFactoryDefault Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncLock)
                    {
                        if (instance == null)
                        {
                            instance = new DbAssistorFactoryDefault();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
