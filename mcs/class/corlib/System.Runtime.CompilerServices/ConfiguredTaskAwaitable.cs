//
// ConfiguredTaskAwaitable.cs
//
// Authors:
//	Marek Safar  <marek.safar@gmail.com>
//
// Copyright (C) 2011 Xamarin, Inc (http://www.xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if NET_4_5

using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	public struct ConfiguredTaskAwaitable
	{
		public struct ConfiguredTaskAwaiter
		{
			readonly Task task;
			readonly bool continueOnSourceContext;

			internal ConfiguredTaskAwaiter (Task task, bool continueOnSourceContext)
			{
				this.task = task;
				this.continueOnSourceContext = continueOnSourceContext;
			}

			public bool IsCompleted {
				get {
					return task.IsCompleted;
				}
			}

			public void GetResult ()
			{
				if (task.Status != TaskStatus.RanToCompletion)
					throw TaskAwaiter.HandleUnexpectedTaskResult (task);
			}

			public void OnCompleted (Action continuation)
			{
				if (continuation == null)
					throw new ArgumentNullException ("continuation");

				TaskAwaiter.HandleOnCompleted (task, continuation, continueOnSourceContext);
			}
		}
		
		readonly ConfiguredTaskAwaiter awaiter;
		
		internal ConfiguredTaskAwaitable (Task task, bool continueOnSourceContext)
		{
			awaiter = new ConfiguredTaskAwaiter (task, continueOnSourceContext);
		}
		
		public ConfiguredTaskAwaiter GetAwaiter()
		{
			return awaiter;
		}
	}
}

#endif