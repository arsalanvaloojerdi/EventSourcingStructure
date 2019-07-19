using Castle.MicroKernel.Lifestyle;
using Framework.QueryModel.Synchronizer.Subscriber;
using System;
using EventSource.Structure.Config.QueryModel.Syncrhonizer;

namespace EventSource.Structure.QueryModels.Synchronizer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Bootstrapper.WireUp();

			using (Bootstrapper.Container.BeginScope())
			{
				var subscribers = Bootstrapper.Container.ResolveAll<ISubscriber>();

				foreach (var subscriber in subscribers)
				{
					subscriber.SubscribeAsync().Wait();
				}
			}

			Console.WriteLine("Projections Are Running ... !");

			Console.ReadKey();
		}
	}
}