using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigValueGetter
{
    public static class ContainerChecker
    {
        private const string ContainerFlag = "DOTNET_RUNNING_IN_CONTAINER";

        public static bool AmIinAContainer()
        {
            string dockerFlag = Environment.GetEnvironmentVariable(ContainerFlag);
            bool isInContainer = !string.IsNullOrEmpty(dockerFlag);
            return isInContainer;
        }
    }
}
