// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Grpc.Core;
using Helloworld;

namespace GreeterClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            var ip = "127.0.0.1";
            var porta = "50051";

            if (args.Length != 0)
            {
                ip = args[0];
                porta = args[1];
            }
            Channel channel = new Channel(ip + ":" + porta, ChannelCredentials.Insecure);
            
            var client = new Greeter.GreeterClient(channel);
            double[] value = { 5, 7, 10, 15, 100.15, 123, -120, 1000, -50000};

            Console.WriteLine("Acessando servidor " + ip +" na porta " + porta);
            var reply = client.SayHello(new HelloRequest { Number = { value } });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
