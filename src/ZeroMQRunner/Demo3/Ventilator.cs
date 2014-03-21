﻿using System;
using fszmq;

namespace ZeroMQRunner.Demo3
{
    public class Ventilator : Endpoint, IDemo3Endpoint
    {
        public void Execute(Demo3Input input)
        {
            ConsoleApp.MoveWindow(100, 100);

            using (var context = new Context())
            using (var sender = context.Push())
            {
                sender.Bind("tcp://*:5557");

                Console.WriteLine("Press enter when the workers are ready: ");
                Console.ReadKey();

                Console.WriteLine("Sending tasks to workers...");

                // Signal the start of the batch
                sender.Send(BitConverter.GetBytes(0));

                var random = new Random();
                for (int i = 0; i < 500; i++)
                {
                    int sleepTime = random.Next(1, 100);
                    var message = BitConverter.GetBytes(sleepTime);
                    sender.Send(message);
                }
            }
        }
    }
}