namespace IntegrationTests.Core;

[CollectionDefinition("IntegrationTests")]
public class SharedTestCollection : ICollectionFixture<IntegrationTestWebAppFactory> { }
