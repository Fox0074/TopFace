﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestAwaiter : INotifyCompletion
{
	private UnityWebRequestAsyncOperation asyncOp;
	private Action continuation;

	public WebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
	{
		this.asyncOp = asyncOp;
		asyncOp.completed += OnRequestCompleted;
	}

	public bool IsCompleted { get { return asyncOp.isDone; } }

	public void GetResult() { }

	public void OnCompleted(Action continuation)
	{
		this.continuation = continuation;
	}

	private void OnRequestCompleted(AsyncOperation obj)
	{
		continuation();
	}
}

public static class ExtensionMethods
{
	public static WebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
	{
		return new WebRequestAwaiter(asyncOp);
	}
}
