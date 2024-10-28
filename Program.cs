// See https://aka.ms/new-console-template for more information

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Images;
using DotNet.Testcontainers.Networks;

Console.WriteLine("Hello, World!");

var network = new NetworkBuilder()
    .WithName("repro-network")
    .Build();

var firebaseImage = new ImageFromDockerfileBuilder()
    .WithDockerfileDirectory(CommonDirectoryPath.GetGitDirectory(),
        "firebase")
    .WithDockerfile("Dockerfile")
    .Build();
    
var firebaseBuilder = new ContainerBuilder()
    .WithNetwork(network)
    .WithPortBinding(4000, 4000)
    .WithPortBinding(9199, 9199)
    .WithWaitStrategy(Wait.ForUnixContainer()
        .UntilMessageIsLogged("Emulator Hub running.*\n"));

var firebaseContainer = firebaseBuilder
    .WithImage(firebaseImage)
    .Build();
        
await firebaseImage.CreateAsync();
await firebaseContainer.StartAsync();