﻿using EFGetStarted.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TP01SWII06
{
    class Program
    {
        static void Main()
        {
            
            /*teste a1 = new teste();
            a1.testexecutar();*/
            

            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run();

        }
    }
}
