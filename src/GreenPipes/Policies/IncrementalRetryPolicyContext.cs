// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace GreenPipes.Policies
{
    using System;
    using System.Threading.Tasks;
    using Util;


    public class IncrementalRetryPolicyContext<T> :
        RetryPolicyContext<T>
        where T : class
    {
        readonly T _context;
        readonly IncrementalRetryPolicy _policy;

        public IncrementalRetryPolicyContext(IncrementalRetryPolicy policy, T context)
        {
            _policy = policy;
            _context = context;
        }

        public T Context => _context;

        public bool CanRetry(Exception exception, out RetryContext<T> retryContext)
        {
            retryContext = new IncrementalRetryContext<T>(_policy, _context, exception, 1, _policy.InitialInterval, _policy.IntervalIncrement);

            return _policy.Matches(exception);
        }

        public Task RetryFaulted(Exception exception)
        {
            return TaskUtil.Completed;
        }
    }
}