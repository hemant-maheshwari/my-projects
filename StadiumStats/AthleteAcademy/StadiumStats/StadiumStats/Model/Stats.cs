using System;
using System.Collections.Generic;
using System.Text;

namespace StadiumStats.Model
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


        public Stats() {
            kickPrecision = 0;
            kickEasiness = 0;
            headballPrecision = 0;
            headballCoordination = 0;
            throwInPrecision = 0;
            throwInCoordination = 0;
            trapCoordination = 0;
            trapEasiness = 0;
            tacklingPrecision = 0;
            tacklingRapidity = 0;
            playerDribblingCoordination = 0;
            playerDribblingEasiness = 0;
            footworkFluency = 0;
            footworkCoordination = 0;
            ballProtectionCoordination = 0;
            ballProtectionRapidity = 0;
            ballCatchingPrecision = 0;
            ballCatchingCoordination = 0;
            plungePrecision = 0;
            plungeRapidity = 0;
            boxingBallFluency = 0;
            boxingBallEase = 0;
            divertingBallCoordination = 0;
            divertingBallEase = 0;
            handThrowingPrecision = 0;
            handThrowingCoordination = 0;
            blockingPrecision = 0;
            blockingRapidity = 0;
            goalieDribblingFluency = 0;
            goalieDribblingEase = 0;
            ballKickPrecision = 0;
            ballKickFluency = 0;
        }

        public int getPlayerOffense()
        {
            return kickPrecision + kickEasiness + headballPrecision + headballCoordination + throwInPrecision + throwInCoordination;
        }
        public int getPlayerDefense()
        {
            return trapCoordination + trapEasiness + tacklingPrecision + tacklingRapidity;
        }
        public int getPlayerBallSKills()
        {
            return playerDribblingCoordination + playerDribblingEasiness + footworkFluency + footworkCoordination + ballProtectionCoordination + ballProtectionRapidity;
        }
        public int getGoalieDefense()
        {
            return ballCatchingPrecision + ballCatchingCoordination + plungePrecision + plungeRapidity + boxingBallFluency + boxingBallEase + divertingBallCoordination + divertingBallEase;
        }
        public int getGoalieBallSkills()
        {
            return goalieDribblingFluency + goalieDribblingEase + ballKickPrecision + ballKickFluency;
        }
        public int getGoalieHandSkills()
        {
            return handThrowingPrecision + handThrowingCoordination + blockingPrecision + blockingRapidity;
        }
    }
}
