namespace IntegrationTests.Core.Abstractions;

[CollectionDefinition("IntegrationTests")]
public class SharedTestCollection : ICollectionFixture<IntegrationTestWebAppFactory> { }
