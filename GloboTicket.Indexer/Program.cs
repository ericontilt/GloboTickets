﻿using GloboTicket.Indexer.Elasticsearch;
using GloboTicket.Promotion.Messages.Shows;
using GreenPipes;
using MassTransit;
using Nest;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("shows");
            var elasticClient = new ElasticClient(settings);
            var elasticsearchRepository = new ElasticsearchRepository(elasticClient);
            var showAddedHandler = new ShowAddedHandler(elasticsearchRepository);

            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
                busConfig.ReceiveEndpoint("GloboTicket.Indexer", endpointConfig =>
                {
                    endpointConfig.UseMessageRetry(r => r.Exponential(
                        retryLimit: 5,
                        minInterval: TimeSpan.FromSeconds(5),
                        maxInterval: TimeSpan.FromSeconds(30),
                        intervalDelta: TimeSpan.FromSeconds(5)));

                    endpointConfig.Handler<ShowAdded>(async context =>
                        await showAddedHandler.Handle(context.Message));
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
