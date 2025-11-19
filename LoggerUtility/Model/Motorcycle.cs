using System;
using LoggerUtility.Utils;

namespace LoggerUtility.Model
{
    public class Motorcycle
    {
        public void drive()
        {
            LoggerUtils.err("Motorcycle can't drive due to no oil!");
            throw new Exception("Motorcycle has no oil!");
        }
    }
}