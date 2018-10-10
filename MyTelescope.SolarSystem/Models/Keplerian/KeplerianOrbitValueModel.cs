using System;
using System.Collections.Generic;
using System.Text;

namespace MyTelescope.SolarSystem.Models.Keplerian
{
    public class KeplerianOrbitValueModel
    {
        public KeplerianOrbitValueModel(double n, double a1MinusE2, double pi)
        {
            N = n;
            A1MinusE2 = a1MinusE2;
            Pi = pi;
        }

        /// <summary>
        /// n in degrees per day
        /// </summary>
        public double N
        {
            get;
        }

        /// <summary>
        /// a(1−e2) in astronomical units
        /// </summary>
        public double A1MinusE2
        {
            get;
        }

        /// <summary>
        /// Π in degrees
        /// </summary>
        public double Pi
        {
            get;
        }
    }
}
