using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumStatsWebAPI.Model
{
    public class Stats
    {
        public int id { get; set; }
        public int athleteId { get; set; }
        public string gameDate { get; set; }
        public int kickPrecision { get; set; }
        public int kickEasiness { get; set; }
        public int headballPrecision { get; set; }
        public int headballCoordination { get; set; }
        public int throwInPrecision { get; set; }
        public int throwInCoordination { get; set; }
        public int trapCoordination { get; set; }
        public int trapEasiness { get; set; }
        public int tacklingPrecision { get; set; }
        public int tacklingRapidity { get; set; }
        public int playerDribblingCoordination { get; set; }
        public int playerDribblingEasiness { get; set; }
        public int footworkFluency { get; set; }
        public int footworkCoordination { get; set; }
        public int ballProtectionCoordination { get; set; }
        public int ballProtectionRapidity { get; set; }
        public int ballCatchingPrecision { get; set; }
        public int ballCatchingCoordination { get; set; }
        public int plungePrecision { get; set; }
        public int plungeRapidity { get; set; }
        public int boxingBallFluency { get; set; }
        public int boxingBallEase { get; set; }
        public int divertingBallCoordination { get; set; }
        public int divertingBallEase { get; set; }
        public int handThrowingPrecision { get; set; }
        public int handThrowingCoordination { get; set; }
        public int blockingPrecision { get; set; }
        public int blockingRapidity { get; set; }
        public int goalieDribblingFluency { get; set; }
        public int goalieDribblingEase { get; set; }
        public int ballKickPrecision { get; set; }
        public int ballKickFluency { get; set; }
    }
}
