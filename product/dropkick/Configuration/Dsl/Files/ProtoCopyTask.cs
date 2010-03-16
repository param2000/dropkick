// Copyright 2007-2008 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using DeploymentModel;
    using Tasks;
    using Tasks.Files;

    public class ProtoCopyTask :
        BaseTask,
        CopyOptions
    {
        readonly Server _server;
        readonly string _to;
        Action<FileActions> _followOn;
        string _from;

        public ProtoCopyTask(Server server, string to)
        {
            _to = to;
            _server = server;
        }

        #region CopyOptions Members

        public CopyOptions From(string sourcePath)
        {
            _from = sourcePath;
            return this;
        }

        public void With(Action<FileActions> copyAction)
        {
            _followOn = copyAction;
            copyAction(new SomeFileActions(_server));
        }

        #endregion

        public override Task ConstructTasksForServer(DeploymentServer server)
        {
            return new CopyTask(_from, _to);
        }
    }
}