﻿using System;
using Nsb.Messages;
using NServiceBus;

namespace Nsb.PriceCalc
{
    public class PriceCalcHandler : IHandleMessages<PriceRequest>
    {
        private readonly IBus _bus;

        public PriceCalcHandler(IBus bus)
        {
            this._bus = bus;
        }

        public void Handle(PriceRequest message)
        {
            var total = Service.PriceCalculator.GetPrice(message);

            Console.WriteLine();
            Console.WriteLine("Calculating total price: EUR {0}", total);

            // wo do not need to configure anything to get this work
            // because we are using Bus.Reply here, the NServiceBus 
            // knows how it has to handle the message forwarding back
            this._bus.Reply(
                new PriceResponse
                {
                    Total = total
                });
        }
    }
}
