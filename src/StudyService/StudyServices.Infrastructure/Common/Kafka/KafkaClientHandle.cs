﻿using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using StudyServices.Infrastructure.Settings;

namespace StudyServices.Infrastructure.Common.Kafka;

internal sealed class KafkaClientHandle : IDisposable
{
    public KafkaClientHandle(StudyProducerSettings settings)
    {
        Console.Write(settings.Username + " " + settings.Password);
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = settings.Servers,
            SecurityProtocol = SecurityProtocol.SaslPlaintext,
            SaslMechanism = SaslMechanism.ScramSha512,
            SaslUsername = "gen_user",
            SaslPassword = "SnlXh2p6DfP9gs",
            AllowAutoCreateTopics = false,
            ClientId = "StudyServices",
            SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None
        };
            
        _kafkaProducer = new ProducerBuilder<byte[], byte[]>(producerConfig).Build();
    }
    
    public void Dispose()
    {
        _kafkaProducer.Flush();
        _kafkaProducer.Dispose();
    }
    
    public Handle Handle => _kafkaProducer.Handle;
    private readonly IProducer<byte[], byte[]> _kafkaProducer;
}