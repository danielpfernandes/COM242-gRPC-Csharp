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
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace GreeterServer
{
    class GreeterImpl : Greeter.GreeterBase
    {
        // Server side handler do RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //Seta a variável que receberá a soma dos valores contidos no vetor que será
            //enviado pelo cliente
            double sum = 0;
            foreach (double i in request.Number)
            {   
                    //soma o valor daquela posição do vetor recursivamente
                    sum = sum + i;
            }            
            //retorna a soma dos valores no vetor enviado
            return Task.FromResult(new HelloReply { Message = sum });
        }

        
    }

    class Program
    {
        //atribui a porta padrão 11234 caso não seja passado argumento
        const int Port = 11234;

        public static void Main(string[] args)
        {
            //se o usuário passar argumentos na linha de comando
            if (args.Length != 0)
            {
                Server server = new Server
                {
                    Services = { Greeter.BindService(new GreeterImpl()) },
                    //Ouve requisições na porta especificada no arg[0]
                    Ports = { new ServerPort("0.0.0.0", int.Parse(args[0]), ServerCredentials.Insecure) }
                };
                server.Start();

                Console.WriteLine("Greeter server listening on port " + args[0]);
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
            //se não receber argumentos, inicializa na porta padrão
            else
            {
                Server server = new Server
                {
                    Services = { Greeter.BindService(new GreeterImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();

                Console.WriteLine("Greeter server listening on port " + Port);
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();

                server.ShutdownAsync().Wait();
            }
        }
    }
}
