/*
 * Created by SharpDevelop.
 * User: KK
 * Date: 2017/10/21
 * Time: 22:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Egode.OrderProcessors
{
	/// <summary>
	/// Description of OrderProcessorBase.
	/// </summary>
	public abstract class OrderProcessorBase
	{
		private OrderLib.Order _order;
		
		internal OrderProcessorBase(OrderLib.Order o)
		{
			_order = o;
		}
		
		public abstract void Run();
	}
}