namespace MyTelescope.SolarSystem.Models.Keplerian
{
    using Extensions;
    using Helpers;
    using System;

    public class KeplerianDateModel
    {
        public DateTimeOffset ReferenceDate { get; }

        public double DaysDifference { get; }

        public double J2000CenturyFactor { get; }

        public KeplerianValueModel Values { get; }

        public KeplerianDateModel(
            DateTimeOffset referenceDate,
            KeplerianModel keplerianModel)
        {
            ReferenceDate = referenceDate;

            DaysDifference = KeplerianHelper.GetDaysDifference(referenceDate);
            J2000CenturyFactor = KeplerianHelper.GetJ2000CenturyFactor(referenceDate);

            Values = keplerianModel.GetKeplerianValueModel(J2000CenturyFactor);
        }
    }
}