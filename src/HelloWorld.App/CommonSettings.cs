using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld.App
{
    public static class CommonSettings
    {
        //ClusterId is the name for the Orleans cluster must be the same for silo and client so they can talk to each other
        public static string ClusterId => "dev";

        //ServiceId is the ID used for the application and it must not change across deployments
        public static string ServiceId => "HelloWorldApp";
    }
}
