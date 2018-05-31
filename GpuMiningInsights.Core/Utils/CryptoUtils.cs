﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Core.Utils
{
    public static class CryptoUtils
    {
        public enum RevenueCalcMethod
        {
            Method1, Method2, Method3, Method4,Method5
        }

        public static double CalculateCoinRevenuePerDayUsd(double hashrateMega, double networkDifficultyMega, double blockReward, double singleCoinUsdPrice, RevenueCalcMethod revenueCalcMethod = RevenueCalcMethod.Method1, double blockTimeInSeconds = 0)
        {
            return CalculateCoinRevenuePerDay(hashrateMega, networkDifficultyMega, blockReward, revenueCalcMethod) * singleCoinUsdPrice;
        }
        public static double CalculateCoinRevenuePerDayBtcEchangeRate(double hashrateMega, double networkDifficultyMega, double blockReward, double singleCoinBtcPrice, RevenueCalcMethod revenueCalcMethod = RevenueCalcMethod.Method1, double blockTimeInSeconds = 0)
        {
            return CalculateCoinRevenuePerDay(hashrateMega, networkDifficultyMega, blockReward, revenueCalcMethod,blockTimeInSeconds) * singleCoinBtcPrice;
        }
        public static double CalculateCoinRevenuePerDay(double hashrateMega, double networkDifficultyMega, double blockReward, RevenueCalcMethod revenueCalcMethod = RevenueCalcMethod.Method1,double blockTimeInSeconds=0)
        {
            switch (revenueCalcMethod)
            {
                case RevenueCalcMethod.Method1:
                    return CalculateCoinRevenuePerDayMethod1(hashrateMega, networkDifficultyMega, blockReward);
                case RevenueCalcMethod.Method2:
                    return CalculateCoinRevenuePerDayMethod2(hashrateMega, networkDifficultyMega, blockReward);
                case RevenueCalcMethod.Method3:
                    return CalculateCoinRevenuePerDayMethod3(hashrateMega, networkDifficultyMega, blockReward, blockTimeInSeconds);
                case RevenueCalcMethod.Method4:
                    return CalculateCoinRevenuePerDayMethod4(hashrateMega, networkDifficultyMega, blockReward);
                case RevenueCalcMethod.Method5:
                    return CalculateCoinRevenuePerDayMethod5(hashrateMega, networkDifficultyMega, blockReward);

            }
            throw new Exception(" out of switch");
        }
        public static double CalculateCoinRevenuePerDayMethod1(double hashrateMega, double networkDifficultyMega, double blockReward)
        {
            double coinsPerSecond = (hashrateMega * blockReward) / networkDifficultyMega;
            double perDay = coinsPerSecond * 60 * 60 * 24;
            return perDay;
        }
        public static double CalculateCoinRevenuePerDayMethod2(double hashrateMega, double networkDifficultyMega, double blockReward)
        {
            double difficultyConstanct = 4295032833;
            //Rewards = (seconds[use 1 for one second] * Blockreward*Hashrate)/ (Difficulty* 4295032833)
            double seconds = 60 * 60 * 24;
            double rewards = (seconds * blockReward* hashrateMega) / (networkDifficultyMega * difficultyConstanct);
            return rewards;
        }

        public static double CalculateCoinRevenuePerDayMethod3(double hashrateMega, double networkDifficultyMega, double blockReward,double blocktimeInSeconds)
        {
            /*
             
Calculate your share of the network hash rate for that particular hash algorithm (your hash rate divided by the network hash rate), let that be HR.
Calculate the emission by unit of time (block reward times 86400 divided by block time in seconds), let that be E.
Calculate your expected average coins mined in a day (HR times E), let that be C. Note that variance will be high, unless you mine on a large pool, or have a large HR.
Calculate the price in the coin you want to convert to (C divided by exchange rate, or C times exchange rate, depending on whether it's expressed by altcoin per bitcoin, or bitcoin per altcoin, respectively).
If you want per second, or per MH, scale accordingly.
             */
            double seconds = 86400;
            double shareOfNetwork = (hashrateMega / networkDifficultyMega);
            double emission = blockReward * 86400 / blocktimeInSeconds;
            double coinsMinedInDay = shareOfNetwork * emission;
            return coinsMinedInDay;
        }
        public static double CalculateCoinRevenuePerDayMethod4(double hashrateMega, double networkDifficultyMega, double blockReward)
        {
            // (S/(D*4295))*86400*R [removed P which is the price of single coin in USD as it will be calculated outside]
            /*
                S is your hash rate (in MH/sec), 
                D is the difficulty rating, 
                R is the block reward
             */
            double difficultyConstanct = 4295032833;
            double seconds = 86400;
            double rewards = (hashrateMega / (networkDifficultyMega * 4295)) * 86400 * blockReward;
            return rewards;
        }

        public static double CalculateCoinRevenuePerDayMethod5(double hashrateMega, double networkDifficultyMega, double blockReward)
        {
            //source code here view-source:http://www.holynerdvana.com/2014/02/how-to-calculate-coins-per-day-for-any.html
            var seconds = 86400;

            var hashrate = hashrateMega ;
            var difficulty = networkDifficultyMega;
            var reward = blockReward;

            var coinsperday = seconds * reward * hashrate / (difficulty * (Math.Pow(2, 48) / 0x00000000ffff));
            return coinsperday;
        }



        /*
         NEW Method which seems good for all!

        
29.5 Mh/s =>  1.32 USD

1 Mh/s =>  0.04474576271186440677966101694915

a day has 86400 Seconds
BT = 124s
BR = 10.00

86400 / BT * BR is the total coins for generated by the network
which is:
6967.741935483870967741935483871

then get a percent of that given ur hashrate over the network's hash rate

mine 0.000700 GH/s
network 3.28 GH/s

result maybe is 2.1341463414634146341463414634146e-4

My share of Coins per day = 2.1341463414634146341463414634146e-4 * 6967.741935483870967741935483871 = 1.4870180959874114870180959874115


         
         */
        /*
         ways to calc Coins Revenue
         - coins per second = (Hashrate * Reward) / Difficulty
         -http://www.holynerdvana.com/2014/02/how-to-calculate-coins-per-day-for-any.html:
            Rewards = (seconds[use 1 for one second] * Blockreward*Hashrate)/ (Difficulty* 4295032833)

         - http://www.holynerdvana.com/2014/02/how-to-calculate-coins-per-day-for-any.html Comments:
            I hard-coded in 86400 seconds for a day (and 2592000 seconds for a month) for my calculator. If you're doing it in Excel, it would just be:
            [seconds][use 1 for one second] * [block reward] * [hash rate] / (difficulty * 4295032833) = Coins
            And then Coins * BTC rate = BTC per day


        -https://bitcoin.stackexchange.com/questions/45811/how-do-i-calculate-expected-earnings-when-mining-a-cryptocurrency
            (S/(D*4295))*86400*R*P
            How is the answer you provided different from 
            [(S/(D*4295))*86400*R*P], where 
            S is your hash rate (in MH/sec), 
            D is the difficulty rating, 
            R is the block reward
            P is the price in USD of a single coin
         */
    }
}
